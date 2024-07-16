using System.ComponentModel;

namespace DigitalPrint.Core.Domain.Products.Enums;

public enum ProductStatus
{
    [Description("غیرفعال")]
    Inactive = 1,
    [Description("در انتظار انتشار")]
    PublishPending = 2,
    [Description("فعال")]
    Active = 3,
}