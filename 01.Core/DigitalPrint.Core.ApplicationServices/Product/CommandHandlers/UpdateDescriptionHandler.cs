using DigitalPrint.Core.Domain.Products.Commands;
using DigitalPrint.Core.Domain.Products.Data;
using DigitalPrint.Core.Domain.Products.ValueObjects;
using Framework.Domain.ApplicationServices;
using Framework.Domain.Data;

namespace DigitalPrint.Core.ApplicationServices.Product.CommandHandlers;

public class UpdateDescriptionHandler : ICommandHandler<UpdateDescription>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductsRepository _productsRepository;


    public UpdateDescriptionHandler(IUnitOfWork unitOfWork, IProductsRepository productsRepository)
    {
        this._productsRepository = productsRepository;
        this._unitOfWork = unitOfWork;
    }
    public void Handle(UpdateDescription command)
    {
        var product = _productsRepository.Load(command.Id);
        if (product == null)
            throw new InvalidOperationException($"آگهی با شناسه {command.Id} یافت نشد.");
        product.UpdateDescription(ProductDescription.FromString(command.Description));
        _unitOfWork.Commit();

    }
}