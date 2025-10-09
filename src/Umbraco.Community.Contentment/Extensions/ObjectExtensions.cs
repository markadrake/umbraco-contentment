using System.ComponentModel;
using System.Linq.Expressions;
using Umbraco.Cms.Core;

namespace Umbraco.Community.Contentment.Extensions;

public static class ObjectExtensions
{
    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IEnumerable<T> AsEnumerableOfOne<T>(this T input) => Enumerable.Repeat(input, 1);

    /// <summary>
    ///     Turns object into dictionary
    /// </summary>
    /// <param name="o"></param>
    /// <param name="ignoreProperties">Properties to ignore</param>
    /// <returns></returns>
    [Obsolete("Use of this can be replaced with RouteValueDictionary or HtmlHelper.AnonymousObjectToHtmlAttributes(). The method will be removed in Umbraco 17.")]
    public static IDictionary<string, TVal> ToDictionary<TVal>(this object o, params string[] ignoreProperties)
    {
        if (o != null)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(o);
            var d = new Dictionary<string, TVal>();
            foreach (PropertyDescriptor prop in props.Cast<PropertyDescriptor>()
                         .Where(x => ignoreProperties.Contains(x.Name) == false))
            {
                var val = prop.GetValue(o);
                if (val != null)
                {
                    d.Add(prop.Name, (TVal)val);
                }
            }

            return d;
        }

        return new Dictionary<string, TVal>();
    }

    /// <summary>
    ///     Converts an object into a dictionary
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <typeparam name="TVal"> </typeparam>
    /// <param name="o"></param>
    /// <param name="ignoreProperties"></param>
    /// <returns></returns>
    [Obsolete("This method is no longer used in Umbraco. The method will be removed in Umbraco 17.")]
    public static IDictionary<string, TVal>? ToDictionary<T, TProperty, TVal>(
        this T o,
        params Expression<Func<T, TProperty>>[] ignoreProperties) => o?.ToDictionary<TVal>(ignoreProperties
        .Select(e => o.GetPropertyInfo(e)).Select(propInfo => propInfo.Name).ToArray());

}
