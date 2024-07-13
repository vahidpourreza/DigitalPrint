using Framework.Domain.ValueObjects;

namespace DigitalPrint.Core.Domain.Products.ValueObjects;

public class ProductPlacard : BaseValueObject<ProductPlacard>
{
    public string Value { get; private set; }
    public static ProductPlacard FromString(string value) => new ProductPlacard(value);

    private ProductPlacard()
    {

    }
    public ProductPlacard(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("برای شعار محصول مقدار لازم است", nameof(value));
        }
        if (value.Length > 100)
        {
            throw new ArgumentOutOfRangeException("شعار محصول نباید بیش از 100 کاراکتر باشد", nameof(value));
        }
        Value = value;
    }
    public override int ObjectGetHashCode() => Value.GetHashCode();
    public override bool ObjectIsEqual(ProductPlacard otherObject) => Value == otherObject.Value;

    public static implicit operator string(ProductPlacard ProductPlacard) => ProductPlacard.Value;
}
