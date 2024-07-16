using Framework.Domain.ValueObjects;

namespace DigitalPrint.Core.Domain.Products.ValueObjects;

public class ProductCategory : BaseValueObject<ProductCategory>
{
    public string Value { get; private set; }
    public static ProductCategory FromString(string value) => new ProductCategory(value);

    private ProductCategory()
    {

    }
    public ProductCategory(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("برای دسته محصول مقدار لازم است", nameof(value));
        }
        if (value.Length > 30)
        {
            throw new ArgumentOutOfRangeException("دسته محصول نباید بیش از 30 کاراکتر باشد", nameof(value));
        }
        Value = value;
    }
    public override int ObjectGetHashCode() => Value.GetHashCode();
    public override bool ObjectIsEqual(ProductCategory otherObject) => Value == otherObject.Value;

    public static implicit operator string(ProductCategory productCategory) => productCategory.Value;
}