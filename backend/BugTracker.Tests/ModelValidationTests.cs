
using Xunit;
using BugTracker.Model;
using BugTracker.DTO;
using FluentAssertions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Tests
{
    public class ModelValidationTests
    {
        [Fact]
        public void Bug_WithValidData_ShouldBeValid()
        {
            var bug = new Bug
            {
                Title = "Login button unresponsive",
                Description = "Clicking login does nothing",
                ReporterId = 1,
                Reporter = new User { Id = 1, Username = "tester" },
                Status = BugStatus.Open,
                Comments = new List<Comment>()
            };

            var context = new ValidationContext(bug);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(bug, context, results, true);

            isValid.Should().BeTrue();
        }


        [Fact]
        public void User_WithValidData_ShouldBeValid()
        {
            var user = new User
            {
                Username = "qa_user",
                BugsReported = new List<Bug>(),
                Comments = new List<Comment>()
            };

            var context = new ValidationContext(user);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(user, context, results, true);

            isValid.Should().BeTrue();
        }


        [Fact]
        public void Bug_DefaultStatus_ShouldBeOpen()
        {
            var bug = new Bug
            {
                Title = "Something broke",
                ReporterId = 2,
                Reporter = new User { Id = 2, Username = "newuser" },
                Comments = new List<Comment>()
            };

            bug.Status.Should().Be(BugStatus.Open);
        }

        [Fact]
        public void Comment_WithValidData_ShouldBeValid()
        {
            var comment = new Comment
            {
                Content = "Repro steps: click login, observe nothing.",
                BugId = 1,
                Bug = new Bug { Id = 1, Title = "Login issue", ReporterId = 1, Reporter = new User { Id = 1, Username = "qa" } },
                UserId = 2,
                User = new User { Id = 2, Username = "dev" }
            };

            var context = new ValidationContext(comment);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(comment, context, results, true);

            isValid.Should().BeTrue();
        }


        [Fact]
        public void BugDTO_MapsCorrectly_FromBug()
        {
            var bug = new Bug
            {
                Id = 10,
                Title = "Dropdown not closing",
                Description = "Occurs on mobile",
                Status = BugStatus.Resolved,
                Reporter = new User { Username = "alex" }
            };

            var dto = new BugDTO(bug);

            dto.Id.Should().Be(10);
            dto.Title.Should().Be("Dropdown not closing");
            dto.Description.Should().Be("Occurs on mobile");
            dto.Status.Should().Be("Resolved");
            dto.ReporterUsername.Should().Be("alex");
        }




    } // End
}
