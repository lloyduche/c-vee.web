using c_vee.Web.Common;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace c_vee.Web.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }


        public IActionResult PageNotFoundHandler( int statusCode)
        {

            switch (statusCode)
            {
                case 404:
                    var statusDetails = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
                    var path = statusDetails.OriginalPath;
                    var qString = statusDetails.OriginalQueryString;

                    _logger.LogError(path, qString);
                    break;

                case 401:
                    var statusDetail = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
                    var paths = statusDetail.OriginalPath;
                    var currentPath = UrlHelper.CreateUrl("Account/Login", HttpContext);
                    var returnUrl = $"{currentPath}?returnUrl={paths}";
                    return Redirect(returnUrl);
                    
            }
            return View("NotFound");
        }

        public IActionResult ExceptionHandler(int statusCode)
        {
            var errordetail = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var path = errordetail.Path;
            var err = errordetail.Error;

            _logger.LogError(err, path);
            return View("Error");
        }


        public IActionResult AccessDeniedHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    var statusDetails = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
                    var path = statusDetails.OriginalPath;
                    var qString = statusDetails.OriginalQueryString;

                    _logger.LogError(path, qString);
                    break;
            }
            return View("AccessDenied");
        }
    }
}
