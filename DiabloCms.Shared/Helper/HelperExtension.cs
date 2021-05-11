using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace DiabloCms.Shared.Helper
{
    public static class HelperExtension
    {
        public static TSource FirstOrDefaultValue<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] TSource defaultValue)
        {
            var value = source.FirstOrDefault();
            return value ?? defaultValue;
        }
    }
}