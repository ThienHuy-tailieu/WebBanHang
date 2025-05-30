﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var listCategory = _db.Categories.ToList();
            return View(listCategory);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Category category)
        {
            if (ModelState.IsValid) //kiem tra hop le
            {
                //thêm category vào table Categories
                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["success"] = "Category được thêm thành công";
                return RedirectToAction("Index");
            }
            return View();
        }
        
        public IActionResult Update(int id)
        {
            var category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        // Xử lý cập nhật chủng loại
        [HttpPost]
        public IActionResult Update(Category category)
        {
            if (ModelState.IsValid) //kiem tra hop le
            {
                //cập nhật category vào table Categories
                _db.Categories.Update(category);
                _db.SaveChanges();
                TempData["success"] = "Category cập nhật thành công";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            var category = _db.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        
        public IActionResult DeleteConfirmed(int id)
        {
            var category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            if (_db.Products.Where(x => x.CategoryId == category.Id).ToList().Count > 0)
            {
                TempData["error"] = "Đã có sản phẩm theo thể loại này. Không thể xoá";
                return RedirectToAction("Index");
            }
            _db.Categories.Remove(category);
            _db.SaveChanges();
            TempData["success"] = "Category xóa thành công";
            return RedirectToAction("Index");
        }
    }
}
