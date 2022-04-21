using BanHangDienTu_ASP.Common;
using ModelDb.DAO;
using ModelDb.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHangDienTu_ASP.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ProductDAO();
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
        public ActionResult Create(Product model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.CreatedBy = session.UserName;
                new ProductDAO().Create(model);
                return RedirectToAction("Index");
            }
            SetViewBag();
            return View(model.CategoryID);
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dao = new ProductDAO();
            var Product = dao.GetByID(id);
            SetViewBag(Product.CategoryID);
            return View(Product);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Product model)
        {
           
      
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.ModifiedBy = session.UserName;
                new ProductDAO().Edit(model);                             
                return RedirectToAction("Index");
            }
            SetViewBag(model.CategoryID);
            return View();
        }
        public void SetViewBag(long? selectedId = null)
        {
            var dao = new ProductCategoryDAO();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            SetAlert("Xóa sản phẩm thành công", "success");
            new ProductDAO().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new ProductDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}