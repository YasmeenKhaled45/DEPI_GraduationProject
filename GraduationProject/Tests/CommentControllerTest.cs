using GraduationProject.Models;
using System;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace GraduationProject.Tests
{
    public class CommentTests
    {
        [Fact]
        public void Comment_ValidModel_ShouldNotHaveValidationErrors()
        {
            // Arrange
            var comment = new Comment
            {
                Content = "This is a comment.",
                CreatedAt = DateTime.UtcNow,
                PostId = 1,
                UserId = "User1"
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(comment, null, null);
            bool isValid = Validator.TryValidateObject(comment, validationContext, validationResults, true);

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void Comment_MissingContent_ShouldHaveValidationError()
        {
            // Arrange
            var comment = new Comment
            {
                Content = string.Empty, // Missing content
                CreatedAt = DateTime.UtcNow,
                PostId = 1,
                UserId = "User1"
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(comment, null, null);
            bool isValid = Validator.TryValidateObject(comment, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.MemberNames.Contains("Content"));
        }

        [Fact]
        public void Comment_MissingPostId_ShouldHaveValidationError()
        {
            // Arrange
            var comment = new Comment
            {
                Content = "This is a comment.",
                CreatedAt = DateTime.UtcNow,
                PostId = 0, // Invalid PostId (should be > 0)
                UserId = "User1"
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(comment, null, null);
            bool isValid = Validator.TryValidateObject(comment, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.MemberNames.Contains("PostId"));
        }
    }
}
