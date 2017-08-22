using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HS.Web.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNull<T>(this T obj)
            where T : class
        {
            return (obj == null);
        }
    }
}
