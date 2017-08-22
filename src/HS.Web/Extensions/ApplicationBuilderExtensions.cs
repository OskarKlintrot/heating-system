using HS.Web.Middlewares;
using HS.Web.Models.Options;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HS.Web.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Will add <see cref="ProtectFolderMiddleware"/> to the pipeline. Must be used before .UseStaticFiles()!
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseProtectFolder(this IApplicationBuilder builder, ProtectFolderOptions options)
        {
            return builder.UseMiddleware<ProtectFolderMiddleware>(options);
        }
    }
}
