using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using HomeBookkeeping.Models;
using HomeBookkeeping;

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
            return View(categories);
        }
    }
}
