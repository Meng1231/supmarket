using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Supermarket.EneityFramework;
using Supermarket.Code;

namespace Supermarket.Web.Controllers
{
    public class UserController : Controller
    {
        SupermarketDB db = new SupermarketDB();
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取需要的会员信息
        /// </summary>
        /// <param name="limit">页面大小</param>
        /// <param name="offset">页码</param>
        /// <returns></returns>
        public ActionResult GetInfo(int limit, int offset)
        {
            try
            {
                string UserName = Request["UserName"];
                int UserCard = 0;
                if (!string.IsNullOrEmpty(Request["UserCard"]))
                {
                    UserCard = Convert.ToInt32(Request["UserCard"]);
                }
                //延迟查询
                var result = from cus in db.User
                             join card in db.Card on cus.CardID equals card.CardID
                             where ((UserName.Length == 0) || (cus.UserName.Contains(UserName)))
                             where ((UserCard == 0) || (cus.CardID == UserCard))
                             select new
                             {
                                 cus.UserID,
                                 cus.UserName,
                                 cus.Sex,
                                 cus.Age,
                                 cus.UserPassWord,
                                 cus.AlterInTime,
                                 cus.IDNumber,
                                 cus.IsDelete,
                                 card.CardID,
                                 card.TotalCost,
                                 card.Score
                             };
                //得到结果总数
                var total = result.Count();
                //即时查询，当前页数据
                var rows = result.OrderBy(m => m.UserID).Skip((offset-1)*limit).Take(limit).ToList();
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
        /// 删除会员
        /// </summary>
        /// <param name="UserIDs">会员ID</param>
        /// <returns></returns>
        public ActionResult Delete(string[] UserIDs)
        {
            SysLog log = new SysLog { Behavior = (Session["Admin"] as AdminInfo).UserName + "删除了" + UserIDs.Length + "个会员", FK_TypeID = 7, FK_AdminID = (Session["Admin"] as AdminInfo).AdminID, Parameters = "UserID", IP = Request.UserHostAddress, CheckInTime = DateTime.Now };
            try
            {
                for (int i = 0; i < UserIDs.Length; i++)
                {
                    //找到要删除的对象 
                    User info = db.User.Find(Convert.ToInt32(UserIDs[i]));
                    info.IsDelete = 1;
                }
                //更新到数据库
                db.SaveChanges();

                log.IsException = 0; log.Exception = string.Empty;
                db.SysLog.Add(log); db.SaveChanges();

                return Json(new AjaxResult { state = ResultType.success.ToString(), message = "执行删除操作成功！" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.IsException = 1; log.Exception = ex.Message;
                db.SysLog.Add(log); db.SaveChanges();

                return Json(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 激活会员
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Activate()
        {
            SysLog log = null;

            try
            {
                User info = db.User.Find(Convert.ToInt32(Request["UserID"]));

                log = new SysLog { Behavior = (Session["Admin"] as AdminInfo).UserName + "激活了会员" + info.UserName, FK_TypeID = 8, FK_AdminID = (Session["Admin"] as AdminInfo).AdminID, Parameters = "UserID", IP = Request.UserHostAddress, CheckInTime = DateTime.Now };

                info.IsDelete = 0;
                db.SaveChanges();

                log.IsException = 0; log.Exception = string.Empty;
                db.SysLog.Add(log); db.SaveChanges();

                return Json(new AjaxResult { state = ResultType.success.ToString(), message = "会员已成功激活！" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                log.IsException = 1; log.Exception = ex.Message;
                db.SysLog.Add(log); db.SaveChanges();

                return Json(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 添加会员信息
        /// </summary>
        /// <param name="info">需要添加的新会员信息</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddUser(User info)
        {
            SysLog log = new SysLog { Behavior = (Session["Admin"] as AdminInfo).UserName + "添加了新的会员" + info.UserName, FK_TypeID = 6, FK_AdminID = (Session["Admin"] as AdminInfo).AdminID, Parameters = "EF.User", IP = Request.UserHostAddress, CheckInTime = DateTime.Now };

            try
            {
                db.Card.Add(new Card { TotalCost = 0, Score = 0 });
                info.CardID = db.Card.Max(c => c.CardID) + 1;
                info.AlterInTime = DateTime.Now;
                db.User.Add(info);
                db.SaveChanges();

                log.IsException = 0; log.Exception = string.Empty;
                db.SysLog.Add(log); db.SaveChanges();

                return Json(new AjaxResult { state = ResultType.success.ToString(), message = "添加会员操作成功！" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.IsException = 1; log.Exception = ex.Message;
                db.SysLog.Add(log); db.SaveChanges();

                return Json(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 编辑会员信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditUser(User info)
        {
            SysLog log = new SysLog { Behavior = (Session["Admin"] as AdminInfo).UserName + "修改了会员" + info.UserName, FK_TypeID = 8, FK_AdminID = (Session["Admin"] as AdminInfo).AdminID, Parameters = "EF.User", IP = Request.UserHostAddress, CheckInTime = DateTime.Now };

            try
            {
                info.AlterInTime = DateTime.Now;

                db.Entry(info).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                log.IsException = 0; log.Exception = string.Empty;
                db.SysLog.Add(log); db.SaveChanges();

                return Json(new AjaxResult { state = ResultType.success.ToString(), message = "编辑会员信息成功！" }, JsonRequestBehavior.AllowGet);
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