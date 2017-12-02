using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Supermarket.EneityFramework;
using Supermarket.Code;

namespace Supermarket.Web.Controllers
{
    public class StockController : Controller
    {
        SupermarketDB db = new SupermarketDB();
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取需要的库存信息
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public ActionResult GetInfo(int limit, int offset)
        {
            try
            {
                var ProductName = Request["ProductName"];
                var TypeID = Convert.ToInt32(Request["TypeID"]);
                var StorageStarTime = Request["StorageStarTime"];
                var StorageEndTime = Request["StorageEndTime"];
                DateTime start=DateTime.Now, end=DateTime.Now;
                if (StorageStarTime != "")
                {
                    start= Convert.ToDateTime(StorageStarTime);
                }
                if (StorageEndTime != "")
                {
                    end= Convert.ToDateTime(StorageEndTime);
                }
                var result = from stock in db.Stock
                             where ((ProductName.Length == 0) || (stock.Product.ProductName.Contains(ProductName)))
                             where ((TypeID == 0) || (stock.Product.TypeID == TypeID))
                             where ((StorageStarTime.Length == 0)
                             || (stock.StorageTime >= start))
                             where ((StorageEndTime.Length == 0)
                             || (stock.StorageTime <= end))
                             select new
                             {
                                 stock.StockID,
                                 stock.StorageTime,
                                 stock.CommodityStockAmount,
                                 stock.StockUp,
                                 stock.StockDown,
                                 stock.AlterTime,
                                 stock.Product.ProductName,
                                 stock.Product.Type.TypeName,
                                 stock.Product.Unit.UnitName
                             };
                var rows = result.OrderBy(p => p.StockID).Skip(offset).Take(limit).ToList();
                return Json(new { total = result.Count(), rows = rows }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 修改库存量
        /// </summary>
        /// <param name="stock"></param>
        /// <returns></returns>
        public ActionResult Edit(EneityFramework.Stock stock)
        {
            SysLog log = new SysLog { Behavior = (Session["Admin"] as AdminInfo).UserName + "修改了编号为" + stock.StockID + "的库存", FK_TypeID = 23, FK_AdminID = (Session["Admin"] as AdminInfo).AdminID, Parameters = "StockID，CommodityStockAmount", IP = Request.UserHostAddress, CheckInTime = DateTime.Now };

            try
            {
                EneityFramework.Stock newStock = db.Stock.Find(stock.StockID);
                newStock.CommodityStockAmount = stock.CommodityStockAmount;
                db.SaveChanges();

                log.IsException = 0; log.Exception = string.Empty;
                db.SysLog.Add(log); db.SaveChanges();

                return Json(new AjaxResult { state = ResultType.success.ToString(), message = "成功修改库存" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                log.IsException = 1; log.Exception = ex.Message;
                db.SysLog.Add(log); db.SaveChanges();

                return Json(new AjaxResult {state=ResultType.error.ToString(),message=ex.Message },JsonRequestBehavior.AllowGet);
            }
        }
    }
}