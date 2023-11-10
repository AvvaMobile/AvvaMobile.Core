namespace AvvaMobile.Core.ExcelExport;
[AttributeUsage(AttributeTargets.Property)]
public class ExcelColumnNameAttribute : Attribute
{
    public string Name { get; set; }

    public ExcelColumnNameAttribute(string name)
    {
        Name = name;
    }
}
