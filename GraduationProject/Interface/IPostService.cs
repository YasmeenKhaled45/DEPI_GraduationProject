using GraduationProject.Models;
using GraduationProject.Helper;
using GraduationProject.ViewModels;

namespace GraduationProject.Interface
{
    public interface IPostService
    {
        Task<PaginatedResult<PostViewModel>> GetAllAsync(string? search, string? sortBy, int page = 1, int pageSize = 10);
        Task<PostViewModel> GetByIdAsync(int id);
        Task CreateAsync(PostViewModel model);
        Task UpdateAsync(PostViewModel model);
        Task DeleteAsync(int id);
    }
}
