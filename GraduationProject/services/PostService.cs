using GraduationProject.Models;
using GraduationProject.Data;
using GraduationProject.Helper;
using GraduationProject.Interface;
using GraduationProject.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _context;

        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedResult<PostViewModel>> GetAllAsync(string? search, string? sortBy, int page = 1, int pageSize = 10)
        {
            var query = _context.Posts.AsQueryable();

            // Search
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Content.Contains(search));
            }

            // Sorting
            query = sortBy?.ToLower() switch
            {
                "content" => query.OrderBy(p => p.Content),
                "createdat" => query.OrderBy(p => p.CreatedAt),
                _ => query.OrderBy(p => p.Id),
            };

            // Total Count for Pagination
            int totalCount = await query.CountAsync();

            // Paging
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var posts = await query.ToListAsync();

            var postViewModels = posts.Select(p => new PostViewModel
            {
                Id = p.Id,
                Content = p.Content,
                Image = p.Image,
                //ImageFile = p.ImageFile,
                CreatedAt = p.CreatedAt,
                UserId = p.UserId
            }).ToList();

            return new PaginatedResult<PostViewModel>
            {
                Items = postViewModels,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }

        public async Task<PostViewModel> GetByIdAsync(int id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (post == null) throw new Exception("Post not found");

            return new PostViewModel
            {
                Id = post.Id,
                Content = post.Content,
                Image = post.Image,
                //ImageFile = post.ImageFile,
                CreatedAt = post.CreatedAt,
                UserId = post.UserId
            };
        }

        public async Task CreateAsync(PostViewModel model)
        {
            var post = new Post
            {
                Content = model.Content,
                CreatedAt = model.CreatedAt,
                UserId = model.UserId
            };

            // Handle image upload
            if (model.ImageFile != null)
            {
                var filePath = Path.Combine("wwwroot/images", model.ImageFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }
                post.Image = $"/images/{model.ImageFile.FileName}";
            }

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PostViewModel model)
        {
            var post = await _context.Posts.FindAsync(model.Id);
            if (post == null) throw new Exception("Post not found");

            post.Content = model.Content;
            post.CreatedAt = model.CreatedAt;
            post.UserId = model.UserId;

            // Handle image upload
            if (model.ImageFile != null)
            {
                var filePath = Path.Combine("wwwroot/images", model.ImageFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }
                post.Image = $"/images/{model.ImageFile.FileName}";
            }

            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null) throw new Exception("Post not found");

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }
    }
    }
