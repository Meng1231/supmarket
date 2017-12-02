using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Supermarket.Code;
using Supermarket.EneityFramework;

namespace Supermarket.Web.Controllers
{
    public class SalesController : Controller
    {
        SupermarketDB db = new SupermarketDB();
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取需要的销售信息
        /// </summary>
        /// <param name="limit">页面大小</param>
        /// <param name="offset">页码</param>
        /// <returns></returns>
        public ActionResult GetInfo(int limit, int offset)
        {
            try
            {
                int SalesID = Convert.ToInt32(Request["SalesID"]);
                var result = from sales in db.Orders
                             where ((SalesID == 0) || (sales.AdminID == SalesID))
                             select new
                             {
                                 sales.OrderFormID,
                                 sales.User.UserName,
                                 sales.SunPrice,
                                 sales.Way,
                                 sales.CheckInTime,
                                 AdminName = sales.AdminInfo.UserName
                             };

                var rows = result.OrderBy(p => p.OrderFormID).Skip(offset).Take(limit).ToList();

                return Json(new { total = result.Count(), rows = rows }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 销售详情
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDetail(int limit, int offset)
        {
            try
            {
                int DetailtID = Convert.ToInt32(Request["DetailID"]);
                var result = from detail in db.DetailOrder
                             where ((DetailtID == 0) || (detail.OrderFormID == DetailtID))
                             select new
                             {
                                 detail.DetailOrderID,
                                 detail.Product.ProductName,
                                 detail.Amount,
                                 detail.subtotal
                             };
                var rows = result.OrderBy(p => p.DetailOrderID).Skip(offset).Take(limit).ToList();

                return Json(new { total = result.Count(), rows = rows }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 获取销售员
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetSalesName()
        {
            try
            {
                var result = from admin in db.AdminInfo
                             where admin.RoleID == 2
                             select new
                             {
                                 admin.AdminID,
                                 admin.UserName
                             };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new AjaxResult { state = ResultType.error, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}