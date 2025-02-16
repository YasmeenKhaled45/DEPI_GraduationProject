using GraduationProject.Models;
using GraduationProject.Data;
using GraduationProject.Helper;
using GraduationProject.Interface;
using GraduationProject.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Services
{
    public class ServiceService : IServiceService
    {
        private readonly ApplicationDbContext _context;

        public ServiceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedResult<ServiceViewModel>> GetAllAsync(string? search, string? sortBy, int page = 1, int pageSize = 10)
        {
            var query = _context.Services.Include(s => s.Category).AsQueryable();

            // Search
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.Description.Contains(search));
            }

            // Sorting
            query = sortBy?.ToLower() switch
            {
                "price" => query.OrderBy(s => s.Price),
                "description" => query.OrderBy(s => s.Description),
                _ => query.OrderBy(s => s.Id),
            };

            // Total Count for Pagination
            int totalCount = await query.CountAsync();

            // Paging
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var services = await query.ToListAsync();

            var serviceViewModels = services.Select(s => new ServiceViewModel
            {
                Id = s.Id,
                Description = s.Description,
                Image = s.Image,
                //ImageFile = s.ImageFile,
                Price = s.Price,
                CategoryId = s.CategoryId,
                CategoryName = s.Category.Name
            }).ToList();

            return new PaginatedResult<ServiceViewModel>
            {
                Items = serviceViewModels,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }

    
        public async Task<ServiceViewModel> GetByIdAsync(int id)
        {
            var service = await _context.Services.Include(s => s.Category).FirstOrDefaultAsync(s => s.Id == id);
            if (service == null) throw new Exception("Service not found");

            return new ServiceViewModel
            {
                Id = service.Id,
                Description = service.Description,
                Image = service.Image,
                //ImageFile = service.ImageFile,
                Price = service.Price,
                CategoryId = service.CategoryId,
                CategoryName = service.Category.Name
            };
        }

        public async Task CreateAsync(ServiceViewModel model)
        {
            var service = new Service
            {
                Description = model.Description,
                Price = model.Price,
                CategoryId = model.CategoryId
            };

            // Handle image upload
            if (model.ImageFile != null)
            {
                var filePath = Path.Combine("wwwroot/images", model.ImageFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }
                service.Image = $"/images/{model.ImageFile.FileName}";
            }

            _context.Services.Add(service);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ServiceViewModel model)
        {
            var service = await _context.Services.FindAsync(model.Id);
            if (service == null) throw new Exception("Service not found");

            service.Description = model.Description;
            service.Price = model.Price;
            service.CategoryId = model.CategoryId;

            // Handle image upload
            if (model.ImageFile != null)
            {
                var filePath = Path.Combine("wwwroot/images", model.ImageFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }
                service.Image = $"/images/{model.ImageFile.FileName}";
            }

            _context.Services.Update(service);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null) throw new Exception("Service not found");

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
        }
    }
}
