using UserDomain.Extensions;

namespace UserServiceTest;

public class UserRegisterTest
{
    [Theory]
    [InlineData("Th1sIsapassword!", true)]
    [InlineData("thisIsapassword123!", true)]
    [InlineData("Abc$123456", true)]
    [InlineData("Th1s!", false)]
    [InlineData("thisIsAPassword", false)]
    [InlineData("thisisapassword#", false)]
    [InlineData("THISISAPASSWORD123!", false)]
    [InlineData("", false)]
    public void ValidatePassword(string password, bool expectedResult)
    {
        //Arrange
        

        //Act
        bool isValid = password.IsValid();
  
        //Assert
        Assert.Equal(expectedResult, isValid);
    }
}