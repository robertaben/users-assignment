using BlazorUserApp.Server.Data;
using BlazorUserApp.Server.DTOs;
using BlazorUserApp.Server.Repositories;
using BlazorUserApp.Tests.Helpers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;

namespace BlazorUserApp.Tests.Repositories
{
    [TestClass]
    public class UserRepositoryTests
    {
        private UsersDbContext _context;
        private UserRepository _userRepository;

        [TestInitialize]
        public void Setup()
        {
            _context = DbContextFactory.CreateDbContext(seedData: true);
            _userRepository = new UserRepository(_context);
        }

        [TestMethod]
        public async Task GetUsersAsync_ReturnsUsers()
        {
            var expectedUsers = new List<UserReadModel>
            {
                new UserReadModel { Id = 1, FirstName = "Alice", LastName = "Smith", PhoneNumber = "+37069999999", Email = "alice@example.com" },
                new UserReadModel { Id = 2, FirstName = "Bob", LastName = "Johnson", PhoneNumber = "0037061111111", Email = "bob@example.com" },
                new UserReadModel { Id = 3, FirstName = "Charlie", LastName = "Brown", PhoneNumber = "861234567", Email = "charlie@example.com" }
            };

            var users = await _userRepository.GetUsersAsync();

            users.Should().BeEquivalentTo(expectedUsers);
        }

        [TestMethod]
        public async Task GetGetUserByIdAsync_ReturnsUser()
        {
            var expectedUser = new UserReadModel
            {
                Id = 1,
                FirstName = "Alice",
                LastName = "Smith",
                PhoneNumber = "+37069999999",
                Email = "alice@example.com"
            };

            var user = await _userRepository.GetUserByIdAsync(1);

            user.Should().BeEquivalentTo(expectedUser);
        }

        [TestMethod]
        public async Task GetUserByIdAsync_ReturnsNull()
        {
            var user = await _userRepository.GetUserByIdAsync(99);
            Assert.IsNull(user);
        }

        [TestMethod]
        public async Task AddUserAsync_AddsUser()
        {
            var newUser = new UserWriteModel
            {
                FirstName = "David",
                LastName = "Lee",
                PhoneNumber = "+37069888888",
                Email = "david@example.com"
            };

            await _userRepository.AddUserAsync(newUser);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == 4);
            user?.FirstName.Should().Be("David");
            user?.LastName.Should().Be("Lee");
            user?.PhoneNumber.Should().Be("+37069888888");
            user?.Email.Should().Be("david@example.com");
        }

        [TestMethod]
        public async Task UpdateUserAsync_UserNotExists()
        {
            var updatedUser = new UserWriteModel
            {
                FirstName = "Nonexistent",
                LastName = "User",
                PhoneNumber = "+37063333333",
                Email = "nonexistent@example.com"
            };

            await _userRepository.UpdateUserAsync(99, updatedUser);
            Assert.AreEqual(3, _context.Users.Count());
        }

        [TestMethod]
        public async Task UpdateUserAsync_UpdatesUser()
        {
            var updatedUser = new UserWriteModel
            {
                FirstName = "UpdatedName",
                LastName = "UpdatedLast",
                PhoneNumber = "+37069999900",
                Email = "updated@example.com"
            };

            await _userRepository.UpdateUserAsync(1, updatedUser);

            var user =await _context.Users.FirstOrDefaultAsync(u => u.Id == 1);
            user?.FirstName.Should().Be("UpdatedName");
            user?.LastName.Should().Be("UpdatedLast");
            user?.PhoneNumber.Should().Be("+37069999900");
            user?.Email.Should().Be("updated@example.com");
        }

        [TestMethod]
        public async Task DeleteUserAsync_RemovesUser()
        {
            await _userRepository.DeleteUserAsync(1);
            Assert.AreEqual(2, _context.Users.Count());
            Assert.IsFalse(_context.Users.Any(u => u.Id == 1));
        }

        [TestMethod]
        public async Task DeleteUserAsync_UserNotExists_Nothing()
        {
            await _userRepository.DeleteUserAsync(99);
            Assert.AreEqual(3, _context.Users.Count());
        }

        //Testing UserWriteModel validations

        [TestMethod]
        public void ValidateUserWriteModel_InvalidFirstName_TooShort()
        {
            var model = new UserWriteModel { FirstName = "A", LastName = "Smith", PhoneNumber = "+37069999999", Email = "alice@example.com" };
            var validationResults = ValidateModel(model);

            validationResults.Should().Contain(r => r.ErrorMessage == "First name must be at least 2 characters.");
        }

        [TestMethod]
        public void ValidateUserWriteModel_InvalidFirstName_TooLong()
        {
            var model = new UserWriteModel { FirstName = new string('A', 51), LastName = "Smith", PhoneNumber = "+37069999999", Email = "alice@example.com" };
            var validationResults = ValidateModel(model);

            validationResults.Should().Contain(r => r.ErrorMessage == "First name cannot exceed 50 characters.");
        }

        [TestMethod]
        public void ValidateUserWriteModel_InvalidFirstName_Regex()
        {
            var model = new UserWriteModel { FirstName = "Alice123", LastName = "Smith", PhoneNumber = "+37069999999", Email = "alice@example.com" };
            var validationResults = ValidateModel(model);

            validationResults.Should().Contain(r => r.ErrorMessage == "First name can only contain letters.");
        }

        [TestMethod]
        public void ValidateUserWriteModel_InvalidPhoneNumber()
        {
            var model = new UserWriteModel { FirstName = "Alice", LastName = "Smith", PhoneNumber = "12345", Email = "alice@example.com" };
            var validationResults = ValidateModel(model);

            validationResults.Should().Contain(r => r.ErrorMessage == "Phone number must start with '+', '00', or contain only digits with a length of 7 to 15 characters.");
        }

        [TestMethod]
        public void ValidateUserWriteModel_InvalidEmail()
        {
            var model = new UserWriteModel { FirstName = "Alice", LastName = "Smith", PhoneNumber = "+37069999999", Email = "invalidemail" };
            var validationResults = ValidateModel(model);

            validationResults.Should().Contain(r => r.ErrorMessage == "Invalid email address format.");
        }

        [TestMethod]
        public void ValidateUserWriteModel_ValidModel()
        {
            var model = new UserWriteModel { FirstName = "Alice", LastName = "Smith", PhoneNumber = "+37069999999", Email = "alice@example.com" };
            var validationResults = ValidateModel(model);

            validationResults.Should().BeEmpty();
        }

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }
    }
}
