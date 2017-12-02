using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Supermarket.Code;
using Supermarket.EneityFramework;
using System.Web.Security;

namespace Supermarket.Web.Controllers
{
    public class LoginController : Controller
    {
        SupermarketDB db = new SupermarketDB();

        /// <summary>
        /// 显示登录视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult Index()
        {
            var test = string.Format("{0:E2}", 1);
            return View();
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAuthCode()
        {
            return File(new VerifyCode().GetVerifyCode(), @"image/Gif");
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="username">登录账号</param>
        /// <param name="password">密码</param>
        /// <param name="code">验证码</param>
        /// <returns>返回json</returns>
        [HttpPost]
        public ActionResult CheckLogin(string username, string password, string code)
        {
            var Admin = db.AdminInfo.FirstOrDefault(p => p.Account == username);
            SysLog log = null;
            if (Admin == null)
            {
                log = new SysLog { Behavior = username + "登录系统", FK_TypeID = 1, FK_AdminID = null, Parameters = "UserName,Password", IP = Request.UserHostAddress, CheckInTime = DateTime.Now };
            }
            else
            {
                log = new SysLog { Behavior = username + "登录系统", FK_TypeID = 1, FK_AdminID = Admin.AdminID, Parameters = "UserName,Password", IP = Request.UserHostAddress, CheckInTime = DateTime.Now };
            }

            try
            {
                if (string.IsNullOrWhiteSpace(Session["nfine_session_verifycode"].ToString()) || code.ToLower() != Session["nfine_session_verifycode"].ToString())
                {
                    throw new Exception("验证码错误");
                }
                var data = db.AdminInfo.FirstOrDefault(model => model.Account == username && model.PassWord == password);
                if (data == null)
                {
                    throw new Exception("账号或密码错误");
                }
                Session["Admin"] = data;
                FormsAuthentication.SetAuthCookie("Admin", false);
                log.IsException = 0;
                log.Exception = string.Empty;
                db.SysLog.Add(log);
                db.SaveChanges();

                return Json(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功。" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.IsException = 1;
                log.Exception = ex.Message;
                db.SysLog.Add(log);
                db.SaveChanges();

                return Json(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Exit()
        {
            try
            {
                SysLog log = new SysLog { Behavior = (Session["Admin"] as AdminInfo).UserName + "退出系统", FK_TypeID = 2, FK_AdminID = (Session["Admin"] as AdminInfo).AdminID, Parameters = "UserName,Password", IP = Request.UserHostAddress, CheckInTime = DateTime.Now,IsException=0,Exception=string.Empty };
                Session.Clear();
                db.SysLog.Add(log);
                db.SaveChanges();

                return Json(new AjaxResult { state = ResultType.success.ToString(), message = "成功退出系统！" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                SysLog log = new SysLog { Behavior = (Session["Admin"] as AdminInfo).UserName + "退出系统", FK_TypeID = 2, FK_AdminID = (Session["Admin"] as AdminInfo).AdminID, Parameters = "UserName,Password", IP = Request.UserHostAddress, CheckInTime = DateTime.Now, IsException = 1, Exception = ex.Message};
                db.SysLog.Add(log);
                db.SaveChanges();

                return Json(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}