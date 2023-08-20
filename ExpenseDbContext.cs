using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using HomeBookkeeping.Models;

namespace HomeBookkeeping
{
    public class ExpenseDbContext : DbContext
    {
        public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options)
            : base(options)
        {
        }

        public DbSet<ExpenseModel> Expenses { get; set; }
        public DbSet<ExpenseCategoryModel> ExpenseCategories { get; set; }
    }
}
