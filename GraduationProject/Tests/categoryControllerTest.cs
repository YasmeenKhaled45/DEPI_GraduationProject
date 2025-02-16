//using GraduationProject.Controllers;
//using GraduationProject.Models;
//using GraduationProject.Data;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Xunit;

//namespace DemoForMvc.Tests
//{
//    public class CategoriesControllerTests
//    {
//        private readonly CategoryController _controller;
//        private readonly ApplicationDbContext _context;

//        public CategoriesControllerTests()
//        {
//            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
//                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
//                .Options;

//            _context = new ApplicationDbContext(options);
//            SeedDatabase();

//            _controller = new CategoryController(_context);
//        }

//        private void SeedDatabase()
//        {
//            _context.Categories.Add(new Category { Name = "Category1" });
//            _context.Categories.Add(new Category { Name = "Category2" });
//            _context.SaveChanges();
//        }

//        [Fact]
//        public void Index_ReturnsViewWithCategories()
//        {
//            // Act
//            var result = _controller.Index() as ViewResult;

//            // Assert
//            Assert.NotNull(result);
//            var model = Assert.IsAssignableFrom<IEnumerable<Category>>(result.Model);
//            Assert.Equal(2, model.Count());
//        }

//        [Fact]
//        public void Details_CategoryExists_ReturnsView()
//        {
//            // Act
//            var result = _controller.Details(1) as ViewResult;

//            // Assert
//            Assert.NotNull(result);
//            var model = Assert.IsType<Category>(result.Model);
//            Assert.Equal("Category1", model.Name);
//        }

//        [Fact]
//        public void Details_CategoryDoesNotExist_ReturnsNotFound()
//        {
//            // Act
//            var result = _controller.Details(99);

//            // Assert
//            Assert.IsType<NotFoundResult>(result);
//        }

//        [Fact]
//        public void Create_Get_ReturnsView()
//        {
//            // Act
//            var result = _controller.Create() as ViewResult;

//            // Assert
//            Assert.NotNull(result);
//        }

//        [Fact]
//        public void Create_Post_ValidCategory_RedirectsToIndex()
//        {
//            // Arrange
//            var newCategory = new Category { Name = "Category3" };

//            // Act
//            var result = _controller.Create(newCategory) as RedirectToActionResult;

//            // Assert
//            Assert.NotNull(result);
//            Assert.Equal("Index", result.ActionName);
//            Assert.Equal(3, _context.Categories.Count());
//        }

//        [Fact]
//        public void Create_Post_InvalidModel_ReturnsView()
//        {
//            // Arrange
//            _controller.ModelState.AddModelError("Name", "Required");

//            // Act
//            var result = _controller.Create(new Category()) as ViewResult;

//            // Assert
//            Assert.NotNull(result);
//            Assert.IsType<Category>(result.Model);
//        }

//        [Fact]
//        public void Edit_Get_CategoryExists_ReturnsView()
//        {
//            // Act
//            var result = _controller.Edit(1) as ViewResult;

//            // Assert
//            Assert.NotNull(result);
//            var model = Assert.IsType<Category>(result.Model);
//            Assert.Equal("Category1", model.Name);
//        }

//        [Fact]
//        public void Edit_Get_CategoryDoesNotExist_ReturnsNotFound()
//        {
//            // Act
//            var result = _controller.Edit(88);

//            // Assert
//            Assert.IsType<NotFoundResult>(result);
//        }

//        [Fact]
//        public void Edit_Post_ValidCategory_RedirectsToIndex()
//        {
//            // Arrange
//            var originalCategory = _context.Categories.FirstOrDefault(c => c.Id == 1);
//            Assert.NotNull(originalCategory);

//            originalCategory.Name = "UpdatedCategory1";

//            // Act
//            var result = _controller.Edit(originalCategory.Id, originalCategory) as RedirectToActionResult;

//            // Assert
//            Assert.NotNull(result);
//            Assert.Equal("Index", result.ActionName);

//            var category = _context.Categories.Find(originalCategory.Id);
//            Assert.NotNull(category);
//            Assert.Equal("UpdatedCategory1", category.Name);
//        }

//        [Fact]
//        public void Delete_CategoryExists_ReturnsView()
//        {
//            // Act
//            var result = _controller.Delete(1) as ViewResult;

//            // Assert
//            Assert.NotNull(result);
//            var model = Assert.IsType<Category>(result.Model);
//            Assert.Equal("Category1", model.Name);
//        }

//        [Fact]
//        public void Delete_CategoryDoesNotExist_ReturnsNotFound()
//        {
//            // Act
//            var result = _controller.Delete(94);

//            // Assert
//            Assert.IsType<NotFoundResult>(result);
//        }

//        [Fact]
//        public void DeleteConfirmed_CategoryExists_RedirectsToIndex()
//        {
//            // Act
//            var result = _controller.DeleteConfirmed(1) as RedirectToActionResult;

//            // Assert
//            Assert.NotNull(result);
//            Assert.Equal("Index", result.ActionName);
//            Assert.Equal(1, _context.Categories.Count());
//        }

//        [Fact]
//        public void DeleteConfirmed_CategoryDoesNotExist_ReturnsNotFound()
//        {
//            // Act
//            var result = _controller.DeleteConfirmed(99);

//            // Assert
//            Assert.IsType<NotFoundResult>(result);
//        }
//    }
//}
