// using NUnit.Framework;
// using UserManagement.Domain.Entities;
// using UserManagement.Domain.Enums;
//
// namespace UserManagement.Domain.Tests.Entities;
//
// public class UserRoleTests
// {
//     private static User CreateUser()
//     {
//         // Create a User instance with sample data
//         return new User("John", "Doe", "john.doe@example.com", "Password123!");
//     }
//
//     private static Role CreateRole()
//     {
//         // Create a Role instance with sample data
//         return new Role(UserRoleType.Administrator);
//     }
//
//     [Test]
//     public void Constructor_WithValidParameters_CreatesUserRole()
//     {
//         // Arrange
//         var user = CreateUser();
//         var role = CreateRole();
//
//         // Act
//         var userRole = new UserRole(user, role);
//         
//         // Assert
//         Assert.Multiple(() =>
//         {
//             Assert.That(userRole.UserId, Is.EqualTo(user.Id));
//             Assert.That(userRole.RoleId, Is.EqualTo(role.Id));
//         });
//     }
// }