using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using HomeBookkeeping.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeBookkeeping.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ExpenseDbContext _dbContext;

        public ExpenseController(ExpenseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(DateTime? startDate, DateTime? endDate, string selectedCategories)
        {
            IQueryable<ExpenseModel> expenses = _dbContext.Expenses;

            if (!string.IsNullOrEmpty(selectedCategories))
            {
                List<int> selectedCategoryIDs = selectedCategories.Split(',').Select(cat => int.Parse(cat)).ToList();
                expenses = expenses.Where(expense => selectedCategoryIDs.Contains(expense.CategoryID));
            }

            if (startDate != null && endDate != null)
            {
                expenses = expenses.Where(expense => expense.Date >= startDate && expense.Date <= endDate);
            }



            ViewBag.CategoryNames = GetCategoryNames();
            List<ExpenseCategoryModel> categories = _dbContext.ExpenseCategories.ToList();
            ViewBag.Categories = categories;
            return View(expenses.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<ExpenseCategoryModel> categories = _dbContext.ExpenseCategories.ToList();
            ViewBag.Categories = new SelectList(categories, "ID", "CategoryName"); 
            return View();
        }

        [HttpPost]
        public IActionResult Create(ExpenseModel expense)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Expenses.Add(expense);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            List<ExpenseCategoryModel> categories = _dbContext.ExpenseCategories.ToList();
            ViewBag.Categories = new SelectList(categories, "ID", "CategoryName"); 
            return View(expense);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ExpenseModel expense = _dbContext.Expenses.Find(id);
            if (expense == null)
            {
                return NotFound();
            }

            List<ExpenseCategoryModel> categories = _dbContext.ExpenseCategories.ToList();
            ViewBag.Categories = new SelectList(categories, "ID", "CategoryName", expense.CategoryID); 

            // Debug output
            Console.WriteLine($"Expense ID: {expense.ID}, Category ID: {expense.CategoryID}");

            return View(expense);
        }


        [HttpPost]
        public IActionResult Edit(ExpenseModel expense)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(expense).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            List<ExpenseCategoryModel> categories = _dbContext.ExpenseCategories.ToList();
            ViewBag.Categories = new SelectList(categories, "ID", "CategoryName", expense.CategoryID);

            return View(expense);
        }

        public IActionResult Delete(int id)
        {
            ExpenseModel expense = _dbContext.Expenses.Find(id);
            if (expense != null)
            {
                _dbContext.Expenses.Remove(expense);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        private string GetCategoryNameById(int categoryId)
        {
            ExpenseCategoryModel category = _dbContext.ExpenseCategories.Find(categoryId);
            return category?.CategoryName ?? "Unknown";
        }
        private Dictionary<int, string> GetCategoryNames()
        {

            return _dbContext.ExpenseCategories.ToDictionary(category => category.ID, category => category.CategoryName);
        }
       
    }

}
