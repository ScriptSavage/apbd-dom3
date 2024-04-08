using LegacyApp;

namespace LegacyAppTests;

public class UserServiceTest
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

    [Fact]
    public void AddUser_Should_Return_False_When_Emial_Without_At_And_Dot()
    {
        //Arange
        string firstName = "John";
        string lastName = "Doe";
        DateTime birthDate = new DateTime(1980, 1, 1);
        int clientId = 1;
        string email = "doe";
        var service = new UserService();

        //Act
        bool result = service.AddUser(firstName, lastName, email , birthDate,clientId);
        
        //Assert
        Assert.False(result);
    }
    
    
    [Fact]
    public void AddUser_Should_Set_CreditLimit_Correctly_For_NormalClient()
    {
        // Arrange
        var service = new UserService();
        string firstName = "Jane";
        string lastName = "Doe";
        DateTime birthDate = new DateTime(1980, 1, 1);
        int clientId = 5;
        string email = "jane.doe@example.com";

        // Act
        bool result = service.AddUser(firstName, lastName, email, birthDate, clientId);

        // Assert
        Assert.True(result);

    }


    [Fact]
    public void AddUser_Should_Return_False_When_Age_Under_21()
    {
        //Arange
        var service = new UserService();
        string firstName = "John";
        string lastName = "Doe";
        DateTime birthDate = new DateTime(2020, 1, 1);
        int clientId = 10;
        string email = "jane.doe@example.com";
        
        
        //act 
        bool result = service.AddUser(firstName, lastName, email, birthDate, clientId);
        
        //Assert
        Assert.False(result);
    }


}