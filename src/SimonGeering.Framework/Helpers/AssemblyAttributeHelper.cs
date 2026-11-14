using System.Reflection;

namespace SimonGeering.Framework.Helpers;

public interface IAssemblyAttributeHelper
{
    TAttribute GetCustomAssemblyAttribute<TAttribute>(Assembly assembly)
        where TAttribute : Attribute;

    TProperty GetCustomAssemblyAttributeProperty<TProperty, TAttribute>(Func<TAttribute, TProperty> propertyHelper, Assembly assembly)
        where TAttribute : Attribute;

    string GetCulture(Assembly assembly);
    string GetFullName(Assembly assembly);
    string GetName(Assembly assembly);
    string GetTitle(Assembly assembly);
    string GetCopyright(Assembly assembly);
    string GetConfiguration(Assembly assembly);
    string GetTrademark(Assembly assembly);
    string GetFileVersion(Assembly assembly);
    string GetVersion(Assembly assembly);
    string GetProduct(Assembly assembly);
    string GetCompany(Assembly assembly);
    string GetDescription(Assembly assembly);
}
public class AssemblyAttributeHelper : IAssemblyAttributeHelper
{
    public TAttribute GetCustomAssemblyAttribute<TAttribute>(Assembly assembly)
        where TAttribute : Attribute
        => GetAttribute<TAttribute>(assembly)
        ?? throw new InvalidOperationException($"Assembly does not contain attribute {typeof(TAttribute).Name}");

    public TProperty GetCustomAssemblyAttributeProperty<TProperty, TAttribute>(Func<TAttribute, TProperty> propertyHelper, Assembly assembly)
        where TAttribute : Attribute
        => GetAttributeProperty(assembly, propertyHelper)
        ?? throw new InvalidOperationException($"Assembly does not contain attribute {typeof(TAttribute).Name}");

    public string GetCompany(Assembly assembly)
        => GetAttributeProperty(assembly, (AssemblyCompanyAttribute a) => a.Company) ?? string.Empty;

    public string GetCopyright(Assembly assembly)
        => GetAttributeProperty(assembly, (AssemblyCopyrightAttribute a) => a.Copyright) ?? string.Empty;

    public string GetConfiguration(Assembly assembly)
        => GetAttributeProperty(assembly, (AssemblyConfigurationAttribute a) => a.Configuration) ?? string.Empty;

    public string GetCulture(Assembly assembly)
        => GetAttributeProperty(assembly, (AssemblyCultureAttribute a) => a.Culture) ?? string.Empty;

    public string GetDescription(Assembly assembly)
        => GetAttributeProperty(assembly, (AssemblyDescriptionAttribute a) => a.Description) ?? string.Empty;

    public string GetFileVersion(Assembly assembly)
        => GetAttributeProperty(assembly, (AssemblyFileVersionAttribute a) => a.Version) ?? string.Empty;

    public string GetFullName(Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);
        return assembly.GetName().FullName;
    }

    public string GetName(Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);
        return assembly.GetName().Name ?? "Unknown Assembly Name";
    }

    public string GetProduct(Assembly assembly)
        => GetAttributeProperty(assembly, (AssemblyProductAttribute a) => a.Product) ?? string.Empty;

    public string GetTitle(Assembly assembly)
        => GetAttributeProperty(assembly, (AssemblyTitleAttribute a) => a.Title) ?? string.Empty;

    public string GetTrademark(Assembly assembly)
        => GetAttributeProperty(assembly, (AssemblyTrademarkAttribute a) => a.Trademark) ?? string.Empty;

    public string GetVersion(Assembly assembly)
        => GetAttributeProperty<string, AssemblyVersionAttribute>(assembly, a => a.Version) ?? string.Empty;

    private static TAttribute? GetAttribute<TAttribute>(Assembly assembly)
        where TAttribute : Attribute
    {
        ArgumentNullException.ThrowIfNull(assembly);
        return assembly.GetCustomAttributes(typeof(TAttribute), true)
                   .Cast<TAttribute>()
                   .FirstOrDefault();
    }

    private static TProperty? GetAttributeProperty<TProperty, TAttribute>(Assembly assembly, Func<TAttribute, TProperty> propertyHelper)
        where TAttribute : Attribute
    {
        var attribute = GetAttribute<TAttribute>(assembly);
        return attribute is null ? default : propertyHelper(attribute);
    }
}
