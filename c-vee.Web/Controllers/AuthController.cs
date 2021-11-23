using c_vee.Web.Common;
using c_vee.Web.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace c_vee.Web.Controllers
{
    public class AuthController : Controller
    {
       
       
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index", "Home", new { confirmation = "failed" });
            }
            var baseUrl = UrlHelper.BaseAddress();
            var model = new ConfirmEmail
            {
                Email = email,
                Token = token
            };
            var (body, header) = await HttpHelper.PostContentAsync<ConfirmEmail>(baseUrl, model, "");
            bool status = false;
            if (header.IsSuccessStatusCode)
            {
                status = true;
                return RedirectToAction("EmailConfimation", status);
            }
            else
            {
                var err = JsonConvert.DeserializeObject<GlobalResponse<string>>(body);
                foreach(var error in err.Errs)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
                return RedirectToAction("EmailConfirmation", status);
            }
        }

       



    }

}
