using DigitalPrint.Core.Domain.Products.Commands;
using DigitalPrint.Core.Domain.Products.Data;
using DigitalPrint.Core.Domain.Products.ValueObjects;
using DigitalPrint.Core.Domain.Shared.ValueObjects;
using Framework.Domain.ApplicationServices;
using Framework.Domain.Data;

namespace DigitalPrint.Core.ApplicationServices.Product.CommandHandlers;

public class CreateHandler : ICommandHandler<Create>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductsRepository _productsRepository;


    public CreateHandler(IUnitOfWork unitOfWork, IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
        _unitOfWork = unitOfWork;
    }
    public void Handle(Create command)
    {
        if (_productsRepository.Exists(command.Id))
            throw new InvalidOperationException($"قبلا محصول با شناسه {command.Id} ثبت شده است.");

        var product = new Domain.Products.Entities.Product(command.Id, new UserId(command.CreatorId),
                                                          ProductTitle.FromString(command.Title), ProductDescription.FromString(command.Description),
                                                          ProductCategory.FromString(command.Category), ProductPrice.FromLong(command.Price));
        _productsRepository.Add(product);
        _unitOfWork.Commit();
    }

}