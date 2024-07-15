using DigitalPrint.Core.Domain.Products.Commands;
using DigitalPrint.Core.Domain.Products.Data;
using DigitalPrint.Core.Domain.Products.ValueObjects;
using Framework.Domain.ApplicationServices;
using Framework.Domain.Data;

namespace DigitalPrint.Core.ApplicationServices.Product.CommandHandlers;

public class CreateHandler : ICommandHandler<Create>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductsRepository _productsRepository;


    public CreateHandler(IUnitOfWork unitOfWork, IProductsRepository productsRepository)
    {
        this._productsRepository = productsRepository;
        this._unitOfWork = unitOfWork;
    }
    public void Handle(Create command)
    {
        if (_productsRepository.Exists(command.Id))
            throw new InvalidOperationException($"قبلا محصول با شناسه {command.Id} ثبت شده است.");

        var product = new Domain.Products.Entities.Product(command.Id,
            new UserId(command.CreatorId)
        );
        _productsRepository.Add(product);
        _unitOfWork.Commit();
    }

}