using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanHangOnline.Models.Data;
using BanHangOnline.Models;
using BanHangOnline.Common;
using BanHangOnline.Areas.Admin.Common;
using BanHangOnline.Areas.Admin.Models;

namespace BanHangOnline.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private static int tryLogin;

        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoginSystem(LoginModel model)
        {
            var check = new CheckLogin();

            if (ModelState.IsValid)
            {
                var result = check.Login(model.UserName, model.PassWord);

                if (result == 1)
                {
                    var user = check.GetById(model.UserName);
                    var userSession = new UserLogin();
                    userSession.userName = user.USERNAMES;
                    userSession.passWord = user.PASSWORDS;

                    Session.Add(CommonLogin.USER_SESSION, userSession);

                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError(string.Empty, "Tài khoản không tồn tại.");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError(string.Empty, "Mật khẩu không đúng");
                    tryLogin++;
                    if (tryLogin == 4)
                    {
                        ModelState.AddModelError(string.Empty, "Bạn đã đăng nhập sai quá số lần cho phép (3 lần). Xin hãy đăng nhập lại sau 30s");
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
    }
}