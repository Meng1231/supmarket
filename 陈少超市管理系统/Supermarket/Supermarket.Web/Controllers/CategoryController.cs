using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Supermarket.EneityFramework;
using Supermarket.Code;

namespace Supermarket.Web.Controllers
{
    public class CategoryController : Controller
    {
        SupermarketDB db = new SupermarketDB();
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取商品类别信息
        /// </summary>
        /// <param name="limit">页面大小</param>
        /// <param name="offset">页码</param>
        /// <returns></returns>
        public ActionResult GetInfo(int limit, int offset)
        {
            try
            {
                var result = from type in db.Type select new { type.TypeID, type.TypeName };
                //得到结果总数
                var total = result.Count();
                //即时查询，当前页数据
                var rows = result.OrderBy(m => m.TypeID).Skip(offset).Take(limit).ToList();
                //返回数据
                return Json(new { total = total, rows = rows }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 添加新的类别信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult AddType(Supermarket.EneityFramework.Type type)
        {
            SysLog log = new SysLog { Behavior = (Session["Admin"] as AdminInfo).UserName + "添加了新的商品类别" + type.TypeName, FK_TypeID = 9, FK_AdminID = (Session["Admin"] as AdminInfo).AdminID, Parameters = "TypeName", IP = Request.UserHostAddress, CheckInTime = DateTime.Now };
            try
            {
                db.Type.Add(type);
                db.SaveChanges();

                log.IsException = 0; log.Exception = string.Empty;
                db.SysLog.Add(log); db.SaveChanges();

                return Json(new AjaxResult { state = ResultType.success.ToString(), message = "添加成功" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.IsException = 1; log.Exception = ex.Message;
                db.SysLog.Add(log); db.SaveChanges();

                return Json(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 商品类别的删除
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(string[] TypeIDs)
        {
            SysLog log = new SysLog { Behavior = (Session["Admin"] as AdminInfo).UserName + "删除了"+TypeIDs.Length+"个商品类别", FK_TypeID = 11, FK_AdminID = (Session["Admin"] as AdminInfo).AdminID, Parameters = "TypeID", IP = Request.UserHostAddress, CheckInTime = DateTime.Now };

            try
            {
                for (var i = 0; i < TypeIDs.Length; i++)
                {
                    EneityFramework.Type type = new EneityFramework.Type { TypeID = Convert.ToInt32(TypeIDs[i]) };
                    db.Type.Attach(type);
                    db.Type.Remove(type);
                }
                db.SaveChanges();

                log.IsException = 0; log.Exception = string.Empty;
                db.SysLog.Add(log); db.SaveChanges();

                return Json(new AjaxResult { state = ResultType.success.ToString(), message = "删除成功" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.IsException = 1; log.Exception = ex.Message;
                db.SysLog.Add(log); db.SaveChanges();

                return Json(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 编辑商品类别
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Supermarket.EneityFramework.Type type)
        {
            SysLog log = new SysLog { Behavior = (Session["Admin"] as AdminInfo).UserName + "修改了商品类别" + type.TypeName, FK_TypeID = 10, FK_AdminID = (Session["Admin"] as AdminInfo).AdminID, Parameters = "TypeID,TypeName", IP = Request.UserHostAddress, CheckInTime = DateTime.Now };

            try
            {
                db.Entry(type).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                log.IsException = 0; log.Exception = string.Empty;
                db.SysLog.Add(log); db.SaveChanges();

                return Json(new AjaxResult { state = ResultType.success.ToString(), message = "编辑商品成功" }, JsonRequestBehavior.AllowGet);
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