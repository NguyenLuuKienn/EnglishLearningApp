namespace EnglishLearning.Domain.Constants;

public static class AuthErrorMessages
{
    public const string UserNotFound = "User not found";
    public const string UsernameExists = "Username already exists";
    public const string EmailExists = "Email already exists";
    public const string InvalidCredentials = "Invalid username or password";
    public const string AccountDeactivated = "Your account is deactivated. Please contact support.";
    public const string InvalidAccessToken = "Invalid access token";
    public const string InvalidRefreshToken = "Invalid or expired refresh token";
}
