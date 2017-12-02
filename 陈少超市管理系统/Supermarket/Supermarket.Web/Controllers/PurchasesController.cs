using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Supermarket.EneityFramework;
using Supermarket.Code;

namespace Supermarket.Web.Controllers
{
    public class PurchasesController : Controller
    {
        SupermarketDB db = new SupermarketDB();
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取需要的进货信息
        /// </summary>
        /// <param name="limit">页面大小</param>
        /// <param name="offest">页码</param>
        /// <returns></returns>
        public ActionResult GetInfo(int limit, int offset)
        {
            try
            {
                var ID = Convert.ToInt32(Request["AdminID"]);
                var StarTime = Request["StarTime"];
                var EndTime = Request["EndTime"];
                DateTime start = DateTime.Now, end = DateTime.Now;
                if (StarTime != "")
                {
                    start = Convert.ToDateTime(StarTime);
                }
                if (EndTime != "")
                {
                    end = Convert.ToDateTime(EndTime);
                }
                var result = from pur in db.Purchases
                             where ((ID == 0) || (pur.AdminID == ID))
                             where ((StarTime.Length == 0) || (pur .PurchasesTime >= start))
                             where ((EndTime.Length == 0) || (pur.PurchasesTime <= end))
                             select new
                             {
                                 pur.PurchasesID,
                                 pur.PurchasesTime,
                                 pur.Reamrk,
                                 pur.AdminInfo.UserName,
                                 pur.PurchasesTotal

                             };
                var rows = result.OrderBy(p => p.PurchasesID).Skip(offset).Take(limit).ToList();

                return Json(new { total = result.Count(), rows = rows }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 进货详情
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public ActionResult GetDetail(int limit, int offset)
        {
            try
            {
                var PurID = Convert.ToInt32(Request["PurID"]);

                var retsult = from detail in db.DetailePurchases
                              where ((PurID == 0) || (detail.PurchasesID == PurID))
                              select new
                              {
                                  detail.DetailePurchasesID,
                                  detail.Product.ProductName,
                                  detail.Product.Unit.UnitName,
                                  detail.Product.Type.TypeName,
                                  detail.ProductAmount,
                                  detail.Reamrk
                              };
                var rows = retsult.OrderBy(p => p.DetailePurchasesID).Skip(offset).Take(limit).ToList();

                return Json(new { total = retsult.Count(), rows = rows }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 编辑入库信息
        /// </summary>
        /// <param name="pur"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditPurchases(EneityFramework.Purchases pur)
        {
            SysLog log = new SysLog { Behavior = (Session["Admin"] as AdminInfo).UserName + "修改了编号为" + pur.PurchasesID + "的入库信息", FK_TypeID = 17, FK_AdminID = (Session["Admin"] as AdminInfo).AdminID, Parameters = "PurchasesID，PurchasesTotal，Reamrk", IP = Request.UserHostAddress, CheckInTime = DateTime.Now };

            try
            {
                EneityFramework.Purchases newPur = db.Purchases.Find(pur.PurchasesID);
                newPur.PurchasesTotal = pur.PurchasesTotal;
                newPur.Reamrk = pur.Reamrk;
                db.SaveChanges();

                log.IsException = 0; log.Exception = string.Empty;
                db.SysLog.Add(log); db.SaveChanges();

                return Json(new AjaxResult { state = ResultType.success.ToString(), message = "编辑成功" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.IsException = 1; log.Exception = ex.Message;
                db.SysLog.Add(log); db.SaveChanges();

                return Json(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 删除入库信息
        /// </summary>
        /// <param name="PurIDs"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(string[] PurIDs)
        {
            SysLog log = new SysLog { Behavior = (Session["Admin"] as AdminInfo).UserName + "删除了" + PurIDs.Length + "个入库信息", FK_TypeID = 18, FK_AdminID = (Session["Admin"] as AdminInfo).AdminID, Parameters = "PurchasesID", IP = Request.UserHostAddress, CheckInTime = DateTime.Now };

            try
            {
                for(int i = 0; i < PurIDs.Length; i++)
                {
                    EneityFramework.Purchases newPur = db.Purchases.Find(Convert.ToInt32(PurIDs[i]));
                    db.Database.ExecuteSqlCommand("delete DetailePurchases where PurchasesID=" + Convert.ToInt32(PurIDs[i]));
                    db.SaveChanges();

                    log.IsException = 0; log.Exception = string.Empty;
                    db.SysLog.Add(log); db.SaveChanges();

                    db.Purchases.Remove(newPur);
                }
                db.SaveChanges();
                return Json(new AjaxResult { state = ResultType.success.ToString(), message = "成功删除入库信息" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                log.IsException = 1; log.Exception = ex.Message;
                db.SysLog.Add(log); db.SaveChanges();

                return Json(new AjaxResult { state=ResultType.error.ToString(),message=ex.Message},JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 获取仓库管理员
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetPurchasesName()
        {
            try
            {
                var result = from admin in db.AdminInfo
                             where admin.RoleID == 4
                             select new
                             {
                                 admin.AdminID,
                                 admin.UserName
                             };
                return Json(result,JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new AjaxResult {state=ResultType.error,message=ex.Message },JsonRequestBehavior.AllowGet);
            }
        }
    }
}