using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerApp.Helpers
{
    public static class EnumExtensions
    {
        public static string GetEnumName<T>(this T t) where T : struct, IConvertible
        {
            return Enum.GetName(typeof(T), t);
        }
    }
}
