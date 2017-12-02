using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Supermarket.Code;
using Supermarket.EneityFramework;

namespace Supermarket.Web.Controllers
{
    public class SysLogController : Controller
    {
        SupermarketDB db = new SupermarketDB();
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取需要的日志信息
        /// </summary>
        /// <param name="limit">页面大小</param>
        /// <param name="offset">页码</param>
        /// <returns></returns>
        public ActionResult GetInfo(int limit, int offset)
        {
            try
            {
                int AdminID = Convert.ToInt32(Request["AdminID"]);
                var result = from log in db.SysLog
                             where ((AdminID == 0) || (log.FK_AdminID == AdminID))
                             select new
                             {
                                 log.LogID,
                                 log.AdminInfo.UserName,
                                 log.LogDic.TypeName,
                                 log.IP,
                                 log.Parameters,
                                 log.CheckInTime,
                                 log.IsException,
                                 log.Exception,
                                 log.Behavior
                             };
                var rows = result.OrderBy(p => p.LogID).Skip(offset).Take(limit).ToList();

                return Json(new { total = result.Count(), rows = rows }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 获取管理员信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllAdmin()
        {
            try
            {
                var result = from admin in db.AdminInfo
                             select new
                             {
                                 admin.AdminID,
                                 admin.UserName
                             };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}