using BanHangDienTu_ASP.Common;
using ModelDb.DAO;
using ModelDb.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Content = ModelDb.EntityFramework.Content;

namespace BanHangDienTu_ASP.Areas.Admin.Controllers
{
    public class ContentController : BaseController
    {
        // GET: Admin/Content
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ContentDAO();
            var model = dao.ListAllPaging(searchString, page, pageSize);

            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Content model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.CreatedBy = session.UserName;
                var culture = Session[CommonConstants.CurrentCulture];
                model.Language = culture.ToString();
                new ContentDAO().Create(model);
                return RedirectToAction("Index");
            }
            SetViewBag();
            return View(model.CategoryID);
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dao = new ContentDAO();
            var content = dao.GetByID(id);
            SetViewBag(content.CategoryID);
            return View(content);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Content model)
        {
            if (ModelState.IsValid)
            {
                new ContentDAO().Edit(model);
                return RedirectToAction("Index");
            }
            SetViewBag(model.CategoryID);
            return View();
        }
        public void SetViewBag(long? selectedId = null)
        {
            var dao = new CategoryDAO();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            SetAlert("Xóa tin tức thành công", "success");
            new ContentDAO().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new ContentDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}