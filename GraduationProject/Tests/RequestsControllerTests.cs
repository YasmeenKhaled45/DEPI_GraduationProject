//using Xunit;
//using Moq;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using GraduationProject.Controllers;
//using GraduationProject.Data;
//using GraduationProject.Models;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace GraduationProject.Tests
//{
//    public class RequestsControllerTests
//    {
//        private readonly RequestsController _controller;
//        private readonly ApplicationDbContext _context;

//        public RequestsControllerTests()
//        {
//            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
//                .UseInMemoryDatabase(databaseName: "TestDb")
//                .Options;
//            _context = new ApplicationDbContext(options);
//            SeedDatabase();
//            _controller = new RequestsController(_context);
//        }

//        private void SeedDatabase()
//        {
//            _context.Requests.Add(new Request { Id = 1, Description = "Request1" });
//            _context.Requests.Add(new Request { Id = 2, Description = "Request2" });
//            _context.SaveChanges();
//        }

//        [Fact]
//        public async Task Index_ReturnsViewWithRequests()
//        {
//            var result = await _controller.Index() as ViewResult;
//            Assert.NotNull(result);
//            var model = Assert.IsAssignableFrom<IEnumerable<Request>>(result.Model);
//            Assert.Equal(2, model.Count());
//        }

//        [Fact]
//        public async Task Details_RequestExists_ReturnsView()
//        {
//            var result = await _controller.Details(1) as ViewResult;
//            Assert.NotNull(result);
//            var model = Assert.IsType<Request>(result.Model);
//            Assert.Equal("Request1", model.Description);
//        }

//        [Fact]
//        public async Task Details_RequestDoesNotExist_ReturnsNotFound()
//        {
//            var result = await _controller.Details(99);
//            Assert.IsType<NotFoundResult>(result);
//        }

//        [Fact]
//        public void Create_Get_ReturnsView()
//        {
//            var result = _controller.Create() as ViewResult;
//            Assert.NotNull(result);
//        }

//        [Fact]
//        public async Task Create_Post_ValidRequest_RedirectsToIndex()
//        {
//            var newRequest = new Request { Description = "Request3" };
//            var result = await _controller.Create(newRequest) as RedirectToActionResult;
//            Assert.NotNull(result);
//            Assert.Equal("Index", result.ActionName);
//            Assert.Equal(3, _context.Requests.Count());
//        }

//        [Fact]
//        public async Task Edit_Get_RequestExists_ReturnsView()
//        {
//            var result = await _controller.Edit(1) as ViewResult;
//            Assert.NotNull(result);
//            var model = Assert.IsType<Request>(result.Model);
//            Assert.Equal("Request1", model.Description);
//        }

//        [Fact]
//        public async Task Edit_Get_RequestDoesNotExist_ReturnsNotFound()
//        {
//            var result = await _controller.Edit(88);
//            Assert.IsType<NotFoundResult>(result);
//        }

//        [Fact]
//        public async Task Edit_Post_ValidRequest_RedirectsToIndex()
//        {
//            var originalRequest = _context.Requests.FirstOrDefault(r => r.Id == 1);
//            Assert.NotNull(originalRequest);
//            originalRequest.Description = "UpdatedRequest1";
//            var result = await _controller.Edit(originalRequest.Id, originalRequest) as RedirectToActionResult;
//            Assert.NotNull(result);
//            Assert.Equal("Index", result.ActionName);
//            var request = _context.Requests.Find(originalRequest.Id);
//            Assert.NotNull(request);
//            Assert.Equal("UpdatedRequest1", request.Description);
//        }

//        [Fact]
//        public async Task Delete_RequestExists_ReturnsView()
//        {
//            var result = await _controller.Delete(1) as ViewResult;
//            Assert.NotNull(result);
//            var model = Assert.IsType<Request>(result.Model);
//            Assert.Equal("Request1", model.Description);
//        }

//        [Fact]
//        public async Task DeleteConfirmed_RequestExists_RedirectsToIndex()
//        {
//            var result = await _controller.DeleteConfirmed(1) as RedirectToActionResult;
//            Assert.NotNull(result);
//            Assert.Equal("Index", result.ActionName);
//            Assert.Equal(1, _context.Requests.Count());
//        }

//        [Fact]
//        public async Task DeleteConfirmed_RequestDoesNotExist_ReturnsNotFound()
//        {
//            var result = await _controller.DeleteConfirmed(99);
//            Assert.IsType<NotFoundResult>(result);
//        }
//    }
//}