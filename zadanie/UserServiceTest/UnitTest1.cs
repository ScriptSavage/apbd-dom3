using LegacyApp;

namespace UserServiceTest;

public class Tests 
{

    [Fact]
    public void AddUser_Should_Return_False_When_FirstName_Is_Empty()
    {
        // Arrange
        var service = new UserService();
        string firstName = "";
        string lastName = "Doe";
        DateTime birthDate = new DateTime(1980, 1, 1);
        int clientId = 1;
        string email = "john.doe@example.com";

        // Act
        bool result = service.AddUser(firstName, lastName, email, birthDate, clientId);

        // Assert
        Assert.False(result);
    }


}
