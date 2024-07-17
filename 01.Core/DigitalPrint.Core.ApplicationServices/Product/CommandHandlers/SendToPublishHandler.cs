using DigitalPrint.Core.Domain.Products.Commands;
using DigitalPrint.Core.Domain.Products.Data;
using Framework.Domain.ApplicationServices;
using Framework.Domain.Data;

namespace DigitalPrint.Core.ApplicationServices.Product.CommandHandlers;

public class SendToPublishHandler : ICommandHandler<SentForPublish>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductsRepository _productsRepository;


    public SendToPublishHandler(IUnitOfWork unitOfWork, IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
        _unitOfWork = unitOfWork;
    }
    public void Handle(SentForPublish command)
    {
        var product = _productsRepository.Load(command.Id);
        if (product == null)
            throw new InvalidOperationException($"محصول با شناسه {command.Id} یافت نشد.");
        product.RequestForPublish();
        _unitOfWork.Commit();

    }
}