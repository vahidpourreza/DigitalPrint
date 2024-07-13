using Framework.Domain.ValueObjects;

namespace DigitalPrint.Core.Domain.Products.ValueObjects;

public class ProductPrice : BaseValueObject<ProductPrice>
{
    public Rial Value { get; private set; }
    public static ProductPrice FromString(string value) => new ProductPrice(Rial.FromString(value));
    public static ProductPrice FromLong(long value) => new ProductPrice(Rial.FromLong(value));
    private ProductPrice()
    {

    }
    public ProductPrice(Rial rial)
    {
        if (rial < 1)
        {
            throw new ArgumentOutOfRangeException("مقدار قیمت کمتر از 1 ریال نمی‌تواند باشد", nameof(ProductPrice));
        }
        Value = rial;
    }
    public override int ObjectGetHashCode()
    {
        return Value.GetHashCode();
    }

    public override bool ObjectIsEqual(ProductPrice otherObject)
    {
        return Value == otherObject.Value;
    }
    public static implicit operator long(ProductPrice productPrice) => productPrice.Value;
}