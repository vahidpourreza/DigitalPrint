using Framework.Domain.ValueObjects;

namespace DigitalPrint.Core.Domain.Products.ValueObjects;

public class ProductDescription : BaseValueObject<ProductDescription>
{

    public string Value { get; private set; }
    public static ProductDescription FromString(string value) => new ProductDescription(value);
    private ProductDescription()
    {

    }
    public ProductDescription(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("برای توضیحات محصول مقدار لازم است", nameof(value));
        }
        Value = value;
    }
    public override int ObjectGetHashCode() => Value.GetHashCode();
    public override bool ObjectIsEqual(ProductDescription otherObject) => Value == otherObject.Value;

    public static implicit operator string(ProductDescription productDescription) => productDescription.Value;
}
