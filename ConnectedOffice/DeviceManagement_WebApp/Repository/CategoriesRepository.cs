using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace DeviceManagement_WebApp.Repository
{
    public class CategoriesRepository
    {
        private readonly ConnectedOfficeContext _Context = new ConnectedOfficeContext();
        public List<Category> Getall()
        {
            return _Context.Category.ToList();

            // TO DO: Add ‘Get By Id’
            // TO DO: Add ‘Create’
            // TO DO: Add ‘Edit’
            // TO DO: Add ‘Delete’
            // TO DO: Add ‘Exists’
        }

        public async Task <Category> GetById(Guid? id)
        {
            var category = await _context.Category.FirstOrDefaultAsync(m => m.CategoryId == id);
           
            return View(category);
        }

        public async Task<Category> GetById(Guid? id)
        {
            var category = await _context.Category.FirstOrDefaultAsync(m => m.CategoryId == id);

            return View(category);
        }
    }
}
