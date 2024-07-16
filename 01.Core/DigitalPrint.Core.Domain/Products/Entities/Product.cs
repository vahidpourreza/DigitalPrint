using DigitalPrint.Core.Domain.Products.ValueObjects;
using Framework.Domain.Entities;
using Framework.Domain.Exceptions;
using DigitalPrint.Core.Domain.Products.Events;
using Framework.Domain.Events;
using Framework.Tools.Enums;
using DigitalPrint.Core.Domain.Products.Enums;

namespace DigitalPrint.Core.Domain.Products.Entities;

public class Product : BaseAggregateRoot<Guid>
{

    #region Fields
    public UserId CreatorId { get; private set; }
    public ProductTitle Title { get; private set; }
    public ProductDescription Description { get; private set; }
    public ProductPrice Price { get; private set; }
    public ProductCategory Category { get; private set; }
    public DateTime CreationDate { get; private set; }
    public bool IsDeleted { get; private set; }
    public ProductStatus Status { get; private set; }
    public List<Picture> Pictures { get; private set; }
    #endregion

    #region Constructors
    public Product(Guid id, UserId creatorId, ProductTitle title, ProductDescription description, ProductCategory category, ProductPrice price)
    {
        Pictures = new List<Picture>();
        HandleEvent(new ProductCreated
        {
            Id = id,
            CreatorId = creatorId.Value,
            Price = price.Value.Value,
            Category = category.Value,
            Title = title.Value,
            Description = description.Value,
        });
    }

    private Product()
    {

    }
    #endregion


    public void RequestForPublish()
    {
        HandleEvent(new ProductSentForPublish()
        {
            Id = Id,
        });
    }
    public void AddPicture(Uri pictureUri, PictureSize size)
    {
        var newPic = new Picture(HandleEvent);
        newPic.HandleEvent(new PictureAddedToProduct()
        {
            PictureId = new Guid(),
            ClassifiedAdId = Id,
            Url = pictureUri.ToString(),
            Height = size.Height,
            Width = size.Width
        });
        Pictures.Add(newPic);
    }
    public void ResizePicture(Guid pictureId, PictureSize newSize)
    {
        var picture = FindPicture(pictureId);
        if (picture == null)
            throw new InvalidOperationException(
                "تصویری با شناسه وارد شده وجود برای تغییر سایز وجود ندارد");
        picture.Resize(newSize);
    }
    private Picture FindPicture(Guid id) => Pictures.FirstOrDefault(x => x.Id == id);

    protected override void SetStateByEvent(IEvent @event)
    {

        switch (@event)
        {
            case ProductCreated e:
                Id = e.Id;
                CreatorId = new UserId(e.CreatorId);
                Title = new ProductTitle(e.Title);
                Description = new ProductDescription(e.Description);
                Category = new ProductCategory(e.Category);
                Price = new ProductPrice(Rial.FromLong(e.Price));
                Status = ProductStatus.Inactive;
                break;
            case ProductSentForPublish e:
                Status = ProductStatus.PublishPending;
                break;


            default:
                throw new InvalidOperationException("امکان اجرای عملیات درخواستی وجود ندارد");
        }
    }
    protected override void ValidateInvariants()
    {
        var isValid =
            Id != null &&
            CreatorId != null &&
            (Status switch
            {
                ProductStatus.PublishPending =>
                    Title != null
                    && Description != null
                    && Price != null,
                ProductStatus.Active =>
                    Title != null
                    && Description != null
                    && Price != null
                    && CreatorId != null,
                _ => true
            });
        if (!isValid)
        {
            throw new InvalidEntityStateException(this, $"مقدار تنظیم شده برای آگهی در وضیعت {Status.GetDescription()} غیر قابل قبول است");
        }
    }
}