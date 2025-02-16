using GraduationProject.Data;
using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GraduationProject.Repositories
{
    public class CategoryRepository : Repository<Category>
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // دالة لجلب الفئات مع الخدمات المرتبطة بها
        public IEnumerable<Category> GetCategoriesWithServices()
        {
            return _context.Categories.Include(c => c.Services).ToList();
        }

        // دالة لإيجاد فئة حسب الاسم
        public Category GetCategoryByName(string name)
        {
            return _context.Categories.FirstOrDefault(c => c.Name == name);
        }
    }
}