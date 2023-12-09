using System.Reflection;

namespace SimonGeering.Framework.Helpers;

public interface IAssemblyAttributeHelper
{
    TAttribute GetCustomAssemblyAttribute<TAttribute>(Assembly assembly);
    TProperty GetCustomAssemblyAttributeProperty<TProperty, A>(Func<A, TProperty> propertyHelper, Assembly assembly);

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
        => GetAttribute<TAttribute>(assembly);

    public TProperty GetCustomAssemblyAttributeProperty<TProperty, TAttribute>(Func<TAttribute, TProperty> propertyHelper, Assembly assembly)
        => GetAttributeProperty(assembly, propertyHelper);

    public string GetCompany(Assembly assembly)
        => GetAttributeProperty(assembly, (AssemblyCompanyAttribute a) => a.Company);

    public string GetCopyright(Assembly assembly)
        => GetAttributeProperty(assembly, (AssemblyCopyrightAttribute a) => a.Copyright);

    public string GetConfiguration(Assembly assembly)
        => GetAttributeProperty(assembly, (AssemblyConfigurationAttribute a) => a.Configuration);

    public string GetCulture(Assembly assembly)
        => GetAttributeProperty(assembly, (AssemblyCultureAttribute a) => a.Culture);

    public string GetDescription(Assembly assembly)
        => GetAttributeProperty(assembly, (AssemblyDescriptionAttribute a) => a.Description);

    public string GetFileVersion(Assembly assembly)
        => GetAttributeProperty(assembly, (AssemblyFileVersionAttribute a) => a.Version);

    public string GetFullName(Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);
        return assembly.GetName().FullName;
    }

    public string GetName(Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);
        return assembly.GetName().Name;
    }

    public string GetProduct(Assembly assembly)
        => GetAttributeProperty(assembly, (AssemblyProductAttribute a) => a.Product);

    public string GetTitle(Assembly assembly)
        => GetAttributeProperty(assembly, (AssemblyTitleAttribute a) => a.Title);

    public string GetTrademark(Assembly assembly)
        => GetAttributeProperty(assembly, (AssemblyTrademarkAttribute a) => a.Trademark);

    public string GetVersion(Assembly assembly)
        => GetAttributeProperty(assembly, (AssemblyVersionAttribute a) => a.Version);

    private TAttribute GetAttribute<TAttribute>(Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);
        return (TAttribute)assembly.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault();
    }

    private TProperty GetAttributeProperty<TProperty, TAttribute>(Assembly assembly, Func<TAttribute, TProperty> propertyHelper)
    {
        var attribute = GetAttribute<TAttribute>(assembly);
        return attribute == null ? default(TProperty) : propertyHelper.Invoke(attribute);
    }
}
