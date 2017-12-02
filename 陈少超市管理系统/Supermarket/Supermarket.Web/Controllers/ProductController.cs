using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Supermarket.EneityFramework;
using Supermarket.Code;

namespace Supermarket.Web.Controllers
{
    public class ProductController : Controller
    {
        SupermarketDB db = new SupermarketDB();

        /// <summary>
        /// 显示视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取想要的商品信息
        /// </summary>
        /// <param name="limit">页面大小</param>
        /// <param name="offset">页码</param>
        /// <returns></returns>
        public ActionResult GetInfo(int limit, int offset)
        {
            try
            {
                var ProName = Request["ProductName"];
                var ptype = Convert.ToInt32(Request["type"]);
                var IsDel = Convert.ToInt32(Request["IsDel"]);

                //延迟查询
                var result = from pro in db.Product
                             join type in db.Type on pro.TypeID equals type.TypeID
                             join unit in db.Unit on pro.UnitID equals unit.UnitID
                             where ((ProName.Length == 0) || (pro.ProductName.Contains(ProName)))
                             where ((ptype == 0) || (type.TypeID == ptype))
                             where ((IsDel == 2) || (pro.IsDelete == IsDel))
                             select new
                             {
                                 pro.ProductID,
                                 pro.ProductName,
                                 pro.Manufacturer,
                                 pro.CommodityPriceOut,
                                 pro.CommodityPriceIn,
                                 pro.CommodityDepict,
                                 pro.IsDelete,
                                 pro.BarCode,
                                 type.TypeName,
                                 unit.UnitName,
                             };
                //得到结果总数
                var total = result.Count();
                //即时查询，当前页数据
                var rows = result.OrderBy(m => m.ProductID).Skip(offset).Take(limit).ToList();
                //返回数据
                return Json(new { total = total, rows = rows }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //出现异常，前台可接收该字符串进行提示
                return Content(ex.Message);
            }
        }


        /// <summary>
        /// 获取商品类别
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetProductType()
        {
            try
            {
                var type = from t in db.Type select new { t.TypeID, t.TypeName };
                return Json(type, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 商品的上下架
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult IsActivate()
        {
            SysLog log = null;
            try
            {
                var state = Convert.ToInt32(Request["state"]);
                int TypeID = state == 0 ? 14 : 15;
                log = new SysLog { Behavior = (Session["Admin"] as AdminInfo).UserName + (state == 0 ? "上架" : "下架") + "了商品", FK_TypeID = TypeID, FK_AdminID = (Session["Admin"] as AdminInfo).AdminID, Parameters = "ProductID，IsDelete", IP = Request.UserHostAddress, CheckInTime = DateTime.Now };

                Product pro = db.Product.Find(Convert.ToInt32(Request["ProductID"]));
                pro.IsDelete = state;
                db.Entry(pro).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                log.IsException = 0; log.Exception = string.Empty;
                db.SysLog.Add(log); db.SaveChanges();

                return Json(new AjaxResult { state = ResultType.success.ToString(), message = "操作成功！" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.IsException = 1; log.Exception = ex.Message;
                db.SysLog.Add(log); db.SaveChanges();

                return Json(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 批量对商品进行操作
        /// </summary>
        /// <param name="ProductIDs">需要操作的商品ID的数组</param>
        /// <returns></returns>
        public ActionResult InActivate(string[] ProductIDs, int state)
        {
            int TypeID = state == 0 ? 14 : 15;
            SysLog log = new SysLog { Behavior = (Session["Admin"] as AdminInfo).UserName + (state == 0 ? "上架" : "下架")+"了"+ProductIDs.Length+"个商品", FK_TypeID = TypeID, FK_AdminID = (Session["Admin"] as AdminInfo).AdminID, Parameters = "ProductID，IsDelete", IP = Request.UserHostAddress, CheckInTime = DateTime.Now };

            try
            {
                for (var i = 0; i < ProductIDs.Length; i++)
                {
                    Product info = db.Product.Find(Convert.ToInt32(ProductIDs[i]));
                    info.IsDelete = state;
                    db.Entry(info).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();

                log.IsException = 0; log.Exception = string.Empty;
                db.SysLog.Add(log); db.SaveChanges();

                return Json(new AjaxResult { state = ResultType.success.ToString(), message = (state == 0 ? "上架" : "下架") + "商品操作成功！" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                log.IsException = 1; log.Exception = ex.Message;
                db.SysLog.Add(log); db.SaveChanges();

                return Json(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 编辑商品信息
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public ActionResult Edit(Product pro)
        {
            SysLog log = new SysLog { Behavior = (Session["Admin"] as AdminInfo).UserName + "修改了编号为"+pro.ProductID+"的商品信息", FK_TypeID = 13, FK_AdminID = (Session["Admin"] as AdminInfo).AdminID, Parameters = "ProductID,ProductName,CommodityPriceOut,CommodityDepict，Isdelete", IP = Request.UserHostAddress, CheckInTime = DateTime.Now };
            try
            {
                Product product = db.Product.Find(pro.ProductID);
                product.ProductName = pro.ProductName;
                product.CommodityPriceOut = pro.CommodityPriceOut;
                product.CommodityDepict = pro.CommodityDepict;
                product.IsDelete = pro.IsDelete;

                db.SaveChanges();

                log.IsException = 0; log.Exception = string.Empty;
                db.SysLog.Add(log); db.SaveChanges();

                return Json(new AjaxResult { state = ResultType.success.ToString(), message = "商品编辑成功！" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.IsException = 1; log.Exception = ex.Message;
                db.SysLog.Add(log); db.SaveChanges();

                return Json(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}