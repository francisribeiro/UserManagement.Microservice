using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using NSubstitute;
using NUnit.Framework;
using UserManagement.Application.Contracts;
using UserManagement.Application.DTOs;
using UserManagement.Application.Exceptions;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Services;
using UserManagement.Domain.Entities;
using UserManagement.Domain.ValueObjects;

namespace UserManagement.Application.Tests.Services
{
    public class CreateUserAsyncTests
    {
        private IFixture _fixture;
        private IMapper _mapper;
        private IUserRepository _userRepository;
        private IValidationService _validationService;
        private IUserManagementService _userManagementService;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();

            _mapper = Substitute.For<IMapper>();
            _userRepository = Substitute.For<IUserRepository>();
            _validationService = Substitute.For<IValidationService>();
            _userManagementService = new UserManagementService(_userRepository, _mapper, _validationService);
        }

        [Test]
        public async Task CreateUserAsync_ValidUserCreateDto_ReturnsUserDto()
        {
            // Arrange
            var userCreateDto = _fixture.Build<UserCreateDto>().Create();
            var user = _fixture.Build<User>().With(u => u.Email, new EmailAddress(userCreateDto.Email)).Create();
            var userDto = _fixture.Build<UserDto>().With(u => u.Email, userCreateDto.Email).Create();

            _userRepository.FindByEmailAsync(userCreateDto.Email).Returns((User)null);
            _mapper.Map<UserDto>(user).Returns(userDto);

            // Assert properties inside the lambda expression
            _userRepository.CreateAsync(Arg.Is<User>(u =>
                userCreateDto.FirstName == u.FirstName &&
                userCreateDto.LastName == u.LastName &&
                new EmailAddress(userCreateDto.Email) == u.Email &&
                u.Password.Equals(userCreateDto.Password))).Returns(Task.FromResult(user));

            // Act
            var result = await _userManagementService.CreateUserAsync(userCreateDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userCreateDto.Email, result.Email);
            _validationService.Received(1).Validate(userCreateDto);
        }

        [Test]
        public void CreateUserAsync_InvalidUserCreateDto_ThrowsValidationException()
        {
            // Arrange
            var userCreateDto = new UserCreateDto
            {
                FirstName = "",
                LastName = "",
                Email = "invalid",
                Password = "123"
            };

            _validationService
                .When(x => x.Validate(userCreateDto))
                .Do(x => throw new ValidationException());

            // Act
            async Task Act() => await _userManagementService.CreateUserAsync(userCreateDto);

            // Assert
            Assert.ThrowsAsync<ValidationException>(Act);
            _validationService.Received(1).Validate(userCreateDto);
        }

        [Test]
        public void CreateUserAsync_EmailAlreadyExists_ThrowsEmailAlreadyExistsException()
        {
            // Arrange
            var userCreateDto = _fixture.Build<UserCreateDto>().Create();
            var existingUser = _fixture.Build<User>().With(u => u.Email, new EmailAddress(userCreateDto.Email)).Create();

            _userRepository.FindByEmailAsync(userCreateDto.Email).Returns(existingUser);

            // Act
            async Task Act() => await _userManagementService.CreateUserAsync(userCreateDto);

            // Assert
            var exception = Assert.ThrowsAsync<EmailAlreadyExistsException>(Act);
            Assert.AreEqual(userCreateDto.Email, exception.Message);
            _validationService.Received(1).Validate(userCreateDto);
        }

        [Test]
        public void CreateUserAsync_InvalidFirstName_ThrowsValidationException()
        {
            // Arrange
            var userCreateDto = _fixture.Build<UserCreateDto>()
                .With(u => u.FirstName, string.Empty)
                .Create();

            // Act & Assert
            Assert.Throws<ValidationException>(() => _userManagementService.CreateUserAsync(userCreateDto));
        }

        [Test]
        public void CreateUserAsync_InvalidLastName_ThrowsValidationException()
        {
            // Arrange
            var userCreateDto = _fixture.Build<UserCreateDto>()
                .With(u => u.LastName, "   ")
                .Create();

            // Act & Assert
            Assert.Throws<ValidationException>(() => _userManagementService.CreateUserAsync(userCreateDto));
        }

        [Test]
        public void CreateUserAsync_InvalidEmail_ThrowsValidationException()
        {
            // Arrange
            var userCreateDto = _fixture.Build<UserCreateDto>()
                .With(u => u.Email, string.Empty)
                .Create();

            // Act & Assert
            Assert.Throws<ValidationException>(() => _userManagementService.CreateUserAsync(userCreateDto));
        }

        [Test]
        public void CreateUserAsync_InvalidPassword_ThrowsValidationException()
        {
            // Arrange
            var userCreateDto = _fixture.Build<UserCreateDto>()
                .With(u => u.Password, "   ")
                .Create();

            // Act & Assert
            Assert.Throws<ValidationException>(() => _userManagementService.CreateUserAsync(userCreateDto));
        }

        [Test]
        public void CreateUserAsync_OverlongFirstName_ThrowsValidationException()
        {
            // Arrange
            var userCreateDto = _fixture.Build<UserCreateDto>()
                .With(u => u.FirstName, new string('A', 101)) // Adjust the limit according to your application constraints
                .Create();

            // Act & Assert
            Assert.Throws<ValidationException>(() => _userManagementService.CreateUserAsync(userCreateDto));
        }

        [Test]
        public void CreateUserAsync_OverlongLastName_ThrowsValidationException()
        {
            // Arrange
            var userCreateDto = _fixture.Build<UserCreateDto>()
                .With(u => u.LastName, new string('A', 101)) // Adjust the limit according to your application constraints
                .Create();

            // Act & Assert
            Assert.Throws<ValidationException>(() => _userManagementService.CreateUserAsync(userCreateDto));
        }

        [Test]
        public void CreateUserAsync_OverlongEmail_ThrowsValidationException()
        {
            // Arrange
            var userCreateDto = _fixture.Build<UserCreateDto>()
                .With(u => u.Email, $"{new string('A', 245)}@example.com") // Adjust the limit according to your application constraints
                .Create();

            // Act & Assert
            Assert.Throws<ValidationException>(() => _userManagementService.CreateUserAsync(userCreateDto));
        }

        [Test]
        public void CreateUserAsync_OverlongPassword_ThrowsValidationException()
        {
            // Arrange
            var userCreateDto = _fixture.Build<UserCreateDto>()
                .With(u => u.Password, new string('A', 101)) // Adjust the limit according to your application constraints
                .Create();

            // Act & Assert
            Assert.Throws<ValidationException>(() => _userManagementService.CreateUserAsync(userCreateDto));
        }

        [Test]
        public void CreateUserAsync_InvalidEmailFormat_ThrowsValidationException()
        {
            // Arrange
            var userCreateDto = _fixture.Build<UserCreateDto>()
                .With(u => u.Email, "invalid_email_format")
                .Create();

            // Act & Assert
            Assert.Throws<ValidationException>(() => _userManagementService.CreateUserAsync(userCreateDto));
        }

        [Test]
        public async Task CreateUserAsync_CreateAsyncThrowsException_PropagatesException()
        {
            // Arrange
            var userCreateDto = _fixture.Build<UserCreateDto>().Create();
            var user = _fixture.Build<User>().With(u => u.Email, userCreateDto.Email).Create();

            _userRepository.FindByEmailAsync(userCreateDto.Email).Returns((User)null);
            _userRepository.When(u => u.CreateAsync(user)).Do(x => { throw new InvalidOperationException("Database error"); });

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _userManagementService.CreateUserAsync(userCreateDto));
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("\t")]
        public void CreateUserAsync_EmptyOrWhitespaceFirstName_ThrowsValidationException(string firstName)
        {
            // Arrange
            var userCreateDto = _fixture.Build<UserCreateDto>()
                .With(u => u.FirstName, firstName)
                .Create();

            // Act & Assert
            Assert.Throws<ValidationException>(() => _userManagementService.CreateUserAsync(userCreateDto));
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("\t")]
        public void CreateUserAsync_EmptyOrWhitespaceLastName_ThrowsValidationException(string lastName)
        {
            // Arrange
            var userCreateDto = _fixture.Build<UserCreateDto>()
                .With(u => u.LastName, lastName)
                .Create();

            // Act & Assert
            Assert.Throws<ValidationException>(() => _userManagementService.CreateUserAsync(userCreateDto));
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("\t")]
        public void CreateUserAsync_EmptyOrWhitespaceEmail_ThrowsValidationException(string email)
        {
            // Arrange
            var userCreateDto = _fixture.Build<UserCreateDto>()
                .With(u => u.Email, email)
                .Create();

            // Act & Assert
            Assert.Throws<ValidationException>(() => _userManagementService.CreateUserAsync(userCreateDto));
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("\t")]
        public void CreateUserAsync_EmptyOrWhitespacePassword_ThrowsValidationException(string password)
        {
            // Arrange
            var userCreateDto = _fixture.Build<UserCreateDto>()
                .With(u => u.Password, password)
                .Create();

            // Act & Assert
            Assert.Throws<ValidationException>(() => _userManagementService.CreateUserAsync(userCreateDto));
        }


        [Test]
        public void CreateUserAsync_UserRepositoryThrowsException_PropagatesException()
        {
            // Arrange
            var userCreateDto = _fixture.Create<UserCreateDto>();
            _userRepository.When(x => x.FindByEmailAsync(userCreateDto.Email)).Do(x => throw new InvalidOperationException());

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _userManagementService.CreateUserAsync(userCreateDto));
        }

        [Test]
        public void CreateUserAsync_MapperThrowsException_PropagatesException()
        {
            // Arrange
            var userCreateDto = _fixture.Create<UserCreateDto>();
            var user = _fixture.Build<User>().With(u => u.Email, userCreateDto.Email).Create();
            _userRepository.FindByEmailAsync(userCreateDto.Email).Returns((User)null);
            _userRepository.CreateAsync(user).Returns(user);
            _mapper.When(x => x.Map<UserDto>(user)).Do(x => throw new InvalidOperationException());

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _userManagementService.CreateUserAsync(userCreateDto));
        }

        [Test]
        public void CreateUserAsync_ValidationServiceThrowsCustomException_PropagatesException()
        {
            // Arrange
            var userCreateDto = _fixture.Create<UserCreateDto>();
            _validationService.When(x => x.Validate(userCreateDto)).Do(x => throw new CustomValidationException("Custom validation error"));

            // Act & Assert
            Assert.Throws<CustomValidationException>(() => _userManagementService.CreateUserAsync(userCreateDto));
        }

    }
}
