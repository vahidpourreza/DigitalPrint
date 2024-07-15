using DigitalPrint.Core.Domain.Products.ValueObjects;
using Framework.Domain.Entities;
using Framework.Domain.Exceptions;
using DigitalPrint.Core.Domain.Products.Events;
using Framework.Domain.Events;
using Framework.Tools.Enums;

namespace DigitalPrint.Core.Domain.Products.Entities;

public class Product : BaseEntity<Guid>
{
    public UserId CreatorId { get; private set; }
    public ProductTitle Title { get; private set; }
   // public ProductPlacard Placard { get; private set; }
    public ProductDescription Description { get; private set; }
    public int CreateDurationTime { get; private set; }
    public ProductPrice Price { get; private set; }
    public DateTime CreationDate { get; private set; }
    public bool IsDeleted { get; private set; }
    public ProductStatus Status { get; private set; }

    public Product(Guid id, UserId creatorId)
    {
        HandleEvent(new ProductCreated()
        {
            Id = id,
            CreatorId = creatorId.Value
        });
    }


    public void SetTitle(ProductTitle title)
    {
        HandleEvent(new ProductTitleUpdated()
        {
            Id = Id,
            Title = title.Value

        });
    }
    public void UpdateDescription(ProductDescription description)
    {
        HandleEvent(new ProductDescriptionUpdated()
        {
            Id = Id,
            Description = description.Value
        });
    }
    public void UpdatePrice(ProductPrice price)
    {
        HandleEvent(new ProductPriceUpdated()
        {
            Id = Id,
            Price = price.Value.Value
        });

    }
    public void RequestForPublish()
    {
        HandleEvent(new ProductSentForPublish()
        {
            Id = Id,
        });
    }
    protected override void SetStateByEvent(IEvent @event)
    {

        switch (@event)
        {
            case ProductCreated e:
                Id = e.Id;
                CreatorId = new UserId(e.CreatorId);
                Status = ProductStatus.Inactive;
                break;
            case ProductTitleUpdated e:
                Title = new ProductTitle(e.Title);
                break;
            case ProductDescriptionUpdated e:
                Description = new ProductDescription(e.Description);
                break;
            case ProductPriceUpdated e:
                Price = new ProductPrice(Rial.FromLong(e.Price));
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