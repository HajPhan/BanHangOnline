using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebDemo.Areas.Admin.Common;
using WebDemo.Areas.Admin.Models;
using WebDemo.Common;

namespace WebDemo.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        private static int tryLogin = 0;

        public ActionResult LoginSystem(LoginModel model)
        {
            var check = new CheckLogin();

            if (ModelState.IsValid)
            {
                var result = check.Login(model.Username, model.Password);

                if (result == 1)
                {
                    var user = check.GetById(model.Username);
                    var userSession = new UserLogin();

                    userSession.userName = user.Username;
                    userSession.passWord = user.Password;

                    Session.Add(CommonLogin.USER_SESSION, userSession);
                    //Session["USER_SESSION"] = userSession.userName.ToString();

                    return RedirectToAction("Index", "Homes");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError(string.Empty, "Tài khoản không tồn tại");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError(string.Empty, "Mật khẩu không đúng");

                    tryLogin++;
                    if (tryLogin == 4)
                    {
                        ModelState.AddModelError(string.Empty, "Bạ đã đăng nhập quá số lần cho phép (3 lần), Xin đăng nhập lại sau!");
                        tryLogin = 0;
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Đăng nhập thất bại");
                }
            }

            return View("Index");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}