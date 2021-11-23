using c_vee.Web.Common;
using c_vee.Web.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace c_vee.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
       

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }



        #region
        /// <summary>
        /// Get request
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
       
        [HttpGet]
        public IActionResult Register()
        {
            return View();
            
        }

        [HttpGet]
        public IActionResult RegistrationConfirmed()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EmailConfirmed(bool status)
        {
            ViewBag.ConfirmedStatus = status;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Profile(string UserId)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                return RedirectToAction("Guest");
            }
            var baseUrl = UrlHelper.BaseAddress();
            var (body, header) = await HttpHelper.GetContentWithoutTokenAsync(baseUrl,"", "api/v1/User/get-user-details/?id=" + UserId);
            if (!header.IsSuccessStatusCode)
            {
                var errData = JsonConvert.DeserializeObject<GlobalResponse<PaginatedResponse<string>>>(body);
            }
            var userDetails = JsonConvert.DeserializeObject<GlobalResponse<UserDetails>>(body);
            if (!header.IsSuccessStatusCode)
            {
                return RedirectToAction("PageNotFoundHandler", "Error", new { StatusCode = header.StatusCode });
            }

            return View(userDetails);
            
        }

        [HttpGet]
        public async Task<IActionResult> Guest(int page)
        {
            var ListOfUserToReturn = new PagedListOfUser();
            var baseUrl = UrlHelper.BaseAddress();
            var (body, header) = await HttpHelper.GetContentWithoutTokenAsync(baseUrl, "", "api/v1/User/get-all-users/?page=" + page);
            if (!header.IsSuccessStatusCode)
            {
                var errData = JsonConvert.DeserializeObject<GlobalResponse<PaginatedResponse<string>>>(body);
                return RedirectToAction("PageNotFoundHandler", "Error", new { StatusCode = header.StatusCode });
            }

            var usersData = JsonConvert.DeserializeObject<GlobalResponse<PaginatedResponse<ListOfUsers>>>(body);
            if (usersData.Data != null)
            {
                ListOfUserToReturn = new PagedListOfUser
                {
                    PageMetaData = usersData.Data.PageMetaData,
                    ResponseData = usersData.Data.ResponseData
                };
            }
            var res = await SideMenu(baseUrl, ListOfUserToReturn);
            return View("Guest", res);
        }

        [HttpGet]
        public async Task<IActionResult> QuerySearch(string query, int page)
        {
            var ListOfUserToReturn = new PagedListOfUser();
            if (string.IsNullOrEmpty(query))
            {
                return RedirectToAction("Guest");
            }
            var baseUrl = UrlHelper.BaseAddress();
            var (body, header) = await HttpHelper.GetContentWithoutTokenAsync(baseUrl, "", "api/v1/User/Search-by-Parameter?query=" + query + "&page=" + page);
            if (!header.IsSuccessStatusCode)
            {
                var errData = JsonConvert.DeserializeObject<GlobalResponse<PaginatedResponse<string>>>(body);
                ViewBag.ErrSearch = errData.Message.ToString();
            }

            var usersData = JsonConvert.DeserializeObject<GlobalResponse<PaginatedResponse<ListOfUsers>>>(body);
            if (usersData.Data != null)
            {
                ListOfUserToReturn = new PagedListOfUser
                {
                    PageMetaData = usersData.Data.PageMetaData,
                    ResponseData = usersData.Data.ResponseData
                };
            }
            var res = await SideMenu(baseUrl, ListOfUserToReturn);

            return View("Guest", res);
        }

        [HttpGet]
        public async Task<IActionResult> JobTitleSearch(string jobTitle, int page)
        {

            var ListOfUserToReturn = new PagedListOfUser();
            if (string.IsNullOrEmpty(jobTitle))
            {
                return RedirectToAction("Guest");
            }
            var baseUrl = UrlHelper.BaseAddress();
            var (body, header) = await HttpHelper.GetContentWithoutTokenAsync(baseUrl, "", "api/v1/User/get-user-jobTitle?jobTitle=" + jobTitle + "&page=" + page);
            if (!header.IsSuccessStatusCode)
            {
                var errData = JsonConvert.DeserializeObject<GlobalResponse<PaginatedResponse<string>>>(body);
                ViewBag.ErrSearch = errData.Message.ToString();
            }

            var usersData = JsonConvert.DeserializeObject<GlobalResponse<PaginatedResponse<ListOfUsers>>>(body);
            if (usersData.Data != null)
            {
                ListOfUserToReturn = new PagedListOfUser
                {
                    PageMetaData = usersData.Data.PageMetaData,
                    ResponseData = usersData.Data.ResponseData
                };
            }
            var res = await SideMenu(baseUrl, ListOfUserToReturn);

            return View("Guest", res);
        }
        private async Task<ListToReturn> SideMenu(string baseUrl, PagedListOfUser model)
        {
            var (body, header) = await HttpHelper.GetContentWithoutTokenAsync(baseUrl, "", "api/v1/User/get-all-jobTitle");
            if (!header.IsSuccessStatusCode)
            {
                var errData = JsonConvert.DeserializeObject<GlobalResponse<IEnumerable<string>>>(body);
            }

            var JobList = JsonConvert.DeserializeObject<GlobalResponse<List<string>>>(body);

            var list = new ListToReturn
            {
                Data = (model is null) ? null : model,
                Info = (JobList.Data is null) ? null : JobList.Data
            };
            return list;

        }

        #endregion





        #region
        /// <summary>
        /// Post request
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            ViewBag.LoginErrMsg = false;
            if (!ModelState.IsValid)
            {
                return View( model);
            }

            var baseUrl = UrlHelper.BaseAddress();
            var (body, header) = await HttpHelper.PostContentAsync(baseUrl, model, "/api/v1/Auth/Login");
            if (!header.IsSuccessStatusCode)
            {
                ViewBag.LoginErrMsg = true;
                var errorResponse = JsonConvert.DeserializeObject<GlobalResponse<string>>(body);
                foreach (var error in errorResponse.Errs)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
                return View(model);
            }
            var res = JsonConvert.DeserializeObject<GlobalResponse<LoginResponse>>(body);
            var userId = res.Data.UserId;
            var Token = res.Data.Token;
            var role = res.Data.Role;
            var firstname = res.Data.FirstName;
            var lastName = res.Data.LastName;
            var isActive = res.Data.isActive;

            HttpContext.Session.SetString("activeStatus", isActive.ToString());
            HttpContext.Session.SetString("UserId", userId);
            HttpContext.Session.SetString("Token", Token);
            HttpContext.Session.SetString("Role", role);
            HttpContext.Session.SetString("UserName", $"{firstname} {lastName}");

            return RedirectToAction("Index", "Dashboard");
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var baseUrl = UrlHelper.BaseAddress();
            var (body, header) = await HttpHelper.PostContentAsync<RegisterViewModel>(baseUrl, model, "/api/v1/Auth/Register");
            if (!header.IsSuccessStatusCode)
            {
                var errResponse = JsonConvert.DeserializeObject<GlobalResponse<string>>(body);
                foreach (var err in errResponse.Errs)
                {
                    ModelState.AddModelError(err.Key, err.Value);
                }
                return View(model);
            }

            return RedirectToAction("RegistrationConfirmed");
        }

        #endregion
       
    }

}
