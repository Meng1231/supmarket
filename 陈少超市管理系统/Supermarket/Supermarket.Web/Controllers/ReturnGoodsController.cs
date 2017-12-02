using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Supermarket.EneityFramework;
using Supermarket.Code;

namespace Supermarket.Web.Controllers
{
    public class ReturnGoodsController : Controller
    {
        SupermarketDB db = new SupermarketDB();
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 获取需要的出库信息
        /// </summary>
        /// <param name="limit">页面大小</param>
        /// <param name="offset">页码</param>
        /// <returns></returns>
        public ActionResult GetInfo(int limit, int offset)
        {
            try
            {
                var ProductName = Request["ProductName"];
                var TypeID = Convert.ToInt32(Request["TypeID"]);
                var StorageStarTime = Request["StarTime"];
                var StorageEndTime = Request["EndTime"];
                DateTime start = DateTime.Now, end = DateTime.Now;
                if (StorageStarTime != "")
                {
                    start = Convert.ToDateTime(StorageStarTime);
                }
                if (StorageEndTime != "")
                {
                    end = Convert.ToDateTime(StorageEndTime);
                }

                var result = from good in db.ReturnGoods
                             where ((ProductName.Length == 0) || (good.Product.ProductName.Contains(ProductName)))
                             where ((TypeID == 0) || (good.Product.TypeID == TypeID))
                             where ((StorageStarTime.Length == 0)
                             || (good.CheckInTime >= start))
                             where ((StorageEndTime.Length == 0)
                             || (good.CheckInTime <= end))
                             select new
                             {
                                 good.ReturnGoodsID,
                                 good.ReturnAmount,
                                 good.Remark,
                                 good.CheckInTime,
                                 good.Product.ProductName,
                                 good.Product.Type.TypeName,
                                 good.Product.Unit.UnitName
                             };
                //即时查询，当前页数据
                var rows = result.OrderBy(m => m.ReturnGoodsID).Skip(offset).Take(limit).ToList();
                //返回数据
                return Json(new { total = result.Count(), rows = rows }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //出现异常，前台可接收该字符串进行提示
                return Content(ex.Message);
            }
        }

        
        /// <summary>
        /// 编辑出库信息
        /// </summary>
        /// <param name="good"></param>
        /// <returns></returns>
        public ActionResult Edit(EneityFramework.ReturnGoods good)
        {
            SysLog log = new SysLog { Behavior = (Session["Admin"] as AdminInfo).UserName + "修改了编号为" + good.ReturnGoodsID + "的出库信息", FK_TypeID = 20, FK_AdminID = (Session["Admin"] as AdminInfo).AdminID, Parameters = "ReturnGoodsID，Remark，ReturnAmount", IP = Request.UserHostAddress, CheckInTime = DateTime.Now };

            try
            {
                EneityFramework.ReturnGoods newGood = db.ReturnGoods.Find(good.ReturnGoodsID);
                newGood.Remark = good.Remark;
                newGood.ReturnAmount = good.ReturnAmount;
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
        /// 删除出库信息
        /// </summary>
        /// <param name="Goods"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(string[] GoodIDs)
        {
            SysLog log = new SysLog { Behavior = (Session["Admin"] as AdminInfo).UserName + "删除了" + GoodIDs.Length + "个出库信息", FK_TypeID = 20, FK_AdminID = (Session["Admin"] as AdminInfo).AdminID, Parameters = "GoodIDs", IP = Request.UserHostAddress, CheckInTime = DateTime.Now };

            try
            {
                for (int i = 0; i < GoodIDs.Length; i++)
                {
                    Supermarket.EneityFramework.ReturnGoods newGood = db.ReturnGoods.Find(Convert.ToInt32(GoodIDs[i]));
                    db.ReturnGoods.Remove(newGood);
                }
                db.SaveChanges();

                log.IsException = 0; log.Exception = string.Empty;
                db.SysLog.Add(log); db.SaveChanges();

                return Json(new AjaxResult { state = ResultType.success.ToString(), message = "成功删除" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                log.IsException = 1; log.Exception = ex.Message;
                db.SysLog.Add(log); db.SaveChanges();

                return Json(new AjaxResult { state= ResultType.error.ToString() ,message=ex.Message},JsonRequestBehavior.AllowGet);
            }
        }

    }
}