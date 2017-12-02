using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Supermarket.EneityFramework;
using Supermarket.Code;

namespace Supermarket.Web.Controllers
{
    public class AdminInfoController : Controller
    {
        //实例化数据模型
        SupermarketDB db = new SupermarketDB();

        /// <summary>
        /// AdminInfo 
        /// </summary>
        /// <returns>Index视图</returns>
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <returns>返回json</returns>
        public ActionResult GetRoles()
        {
            var data = from role in db.Role select new {role.RoleID,role.RoleName};

            return Json(data,JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 获取AdminInfo表中数据方法，填充表格
        /// </summary>
        /// <param name="limit">页面大小</param>
        /// <param name="offset">页码</param>
        /// <param name="rolename">角色名称</param>
        /// <param name="username">用户名</param>
        /// <returns>table当前页面数据</returns>
        public ActionResult GetInfo(int limit, int offset, string rolename, string username)
        {
            try
            {
                int RoleID = Convert.ToInt32(rolename);
                //延迟查询
                var result = from a in db.AdminInfo
                                 //判断传入查询条件是否为空，true？skip：go
                             where ((RoleID == 0) || (a.Role.RoleID == RoleID))
                             where ((username.Length == 0) || (a.UserName.Contains(username)))
                             select new
                             {
                                 a.AdminID,
                                 a.UserName,
                                 a.Account,
                                 a.RoleID,
                                 a.Role.RoleName,
                                 a.PassWord
                             };
                //得到结果总数
                var total = result.Count();
                //即时查询，当前页数据
                var rows = result.OrderBy(m => m.AdminID).Skip(offset).Take(limit).ToList();
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
        /// 修改Action方法
        /// </summary>
        /// <param name="ai">存储当前要修改的数据</param>
        /// <returns>返回字符串用来判断执行结果</returns>
        public ActionResult Edit(AdminInfo admin)
        {
            SysLog log = new SysLog { Behavior = (Session["Admin"] as AdminInfo).UserName + "修改了"+admin.UserName+"的信息", FK_TypeID = 5, FK_AdminID = (Session["Admin"] as AdminInfo).AdminID, Parameters = "UserName", IP = Request.UserHostAddress, CheckInTime = DateTime.Now};
            try
            {
                //更新实体
                var data = db.AdminInfo.Find(admin.AdminID);
                data.UserName = admin.UserName;
                db.SaveChanges();

                log.IsException = 0; log.Exception = string.Empty;
                db.SysLog.Add(log);db.SaveChanges();

                return Json(new AjaxResult {state=ResultType.success.ToString(),message="修改成功" },JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.IsException = 1; log.Exception = ex.Message;
                db.SysLog.Add(log); db.SaveChanges();

                return Json(new AjaxResult { state = ResultType.error.ToString(), message =ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 添加Action方法
        /// </summary>
        /// <param name="ai">存储要添加的数据</param>
        /// <returns>返回字符串用来判断执行结果</returns>
        public ActionResult Add(AdminInfo admin)
        {
            SysLog log = new SysLog { Behavior = (Session["Admin"] as AdminInfo).UserName + "添加了新的管理员" + admin.UserName, FK_TypeID = 3, FK_AdminID = (Session["Admin"] as AdminInfo).AdminID, Parameters = "UserName", IP = Request.UserHostAddress, CheckInTime = DateTime.Now };
            try
            {
                db.AdminInfo.Add(admin);
                db.SaveChanges();

                log.IsException = 0; log.Exception = string.Empty;
                db.SysLog.Add(log); db.SaveChanges();

                return Json(new AjaxResult { state = ResultType.success.ToString(), message = "修改成功" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.IsException = 1; log.Exception = ex.Message;
                db.SysLog.Add(log); db.SaveChanges();

                return Json(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 删除Action方法
        /// </summary>
        /// <param name="AdminIDs">当前要删除的对象（主键）集合</param>
        /// <returns>返回字符串用来判断执行结果</returns>
        public ActionResult Delete(string[] AdminIDs)
        {
            SysLog log = new SysLog { Behavior = (Session["Admin"] as AdminInfo).UserName + "删除了"+AdminIDs.Length+"个管理员" , FK_TypeID = 4, FK_AdminID = (Session["Admin"] as AdminInfo).AdminID, Parameters = "UserName", IP = Request.UserHostAddress, CheckInTime = DateTime.Now };

            try
            {
                for (int i = 0; i < AdminIDs.Length; i++)
                {
                    //1、创建要删除的对象 
                    AdminInfo admin = new AdminInfo() { AdminID = Convert.ToInt32(AdminIDs[i]) };
                    //2、将对象添加到EF管理容器中
                    db.AdminInfo.Attach(admin);
                    //3、将对象包装类的状态标识为删除状态
                    db.AdminInfo.Remove(admin);
                }
                //4、更新到数据库
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
    }
}