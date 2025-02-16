using GraduationProject.Helper;
using GraduationProject.ViewModels;

namespace GraduationProject.Interface
{
    public interface IServiceService
    {
        Task<PaginatedResult<ServiceViewModel>> GetAllAsync(string? search, string? sortBy, int page = 1, int pageSize = 10);
        Task<ServiceViewModel> GetByIdAsync(int id);
        Task CreateAsync(ServiceViewModel model);
        Task UpdateAsync(ServiceViewModel model);
        Task DeleteAsync(int id);
    }
}
