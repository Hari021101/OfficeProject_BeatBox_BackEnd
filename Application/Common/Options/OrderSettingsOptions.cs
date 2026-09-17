namespace Application.Common.Options;

public class OrderSettingsOptions
{
    public const string SectionName = "OrderSettings";

    public int MaxNormalOrderQuantity { get; set; } = 15;
}
