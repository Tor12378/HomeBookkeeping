using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using HomeBookkeeping.Models;

namespace HomeBookkeeping.Controllers
{
    public class ExpenseCategoryController : Controller
    {
        private readonly ExpenseDbContext _dbContext;

        public ExpenseCategoryController(ExpenseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            List<ExpenseCategoryModel> categories = _dbContext.ExpenseCategories.ToList();
            ViewBag.Categories = categories;
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ExpenseCategoryModel category)
        {
            if (ModelState.IsValid)
            {
                _dbContext.ExpenseCategories.Add(category);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Delete(int id)
        {
            var category = _dbContext.ExpenseCategories.Find(id);
            if (category != null)
            {
                
                bool hasAssociatedExpenses = _dbContext.Expenses.Any(expense => expense.CategoryID == id);

                if (!hasAssociatedExpenses)
                {
                    _dbContext.ExpenseCategories.Remove(category);
                    _dbContext.SaveChanges();
                }
                
                return RedirectToAction("Index");
            }
            return NotFound(); 
        }
    }
}
