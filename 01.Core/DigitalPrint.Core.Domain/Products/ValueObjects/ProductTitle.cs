using Framework.Domain.ValueObjects;

namespace DigitalPrint.Core.Domain.Products.ValueObjects;

public class ProductTitle : BaseValueObject<ProductTitle>
{
    public string Value { get; private set; }
    public static ProductTitle FromString(string value) => new ProductTitle(value);

    private ProductTitle()
    {

    }
    public ProductTitle(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("برای عنوان محصول مقدار لازم است", nameof(value));
        }
        if (value.Length > 100)
        {
            throw new ArgumentOutOfRangeException("عنوان محصول نباید بیش از 100 کاراکتر باشد", nameof(value));
        }
        Value = value;
    }
    public override int ObjectGetHashCode() => Value.GetHashCode();
    public override bool ObjectIsEqual(ProductTitle otherObject) => Value == otherObject.Value;

    public static implicit operator string(ProductTitle productTitle) => productTitle.Value;
}