using Hangfire.Dashboard;

namespace EnglishLearning.WebAPI.Middlewares;

public class HangfireBasicAuthAuthorizationFilter : IDashboardAuthorizationFilter
{
    private readonly string _login;
    private readonly string _password;

    public HangfireBasicAuthAuthorizationFilter(string login, string password)
    {
        _login = login;
        _password = password;
    }

    public bool Authorize(DashboardContext context)
    {
        var request = context.GetHttpContext().Request;
        var header = request.Headers["Authorization"];

        if (!string.IsNullOrEmpty(header))
        {
            var encoding = System.Text.Encoding.GetEncoding("iso-8859-1");
            var credentialBytes = Convert.FromBase64String(header.ToString().Replace("Basic ", ""));
            var credentials = encoding.GetString(credentialBytes).Split(':');
            var user = credentials[0];
            var pass = credentials[1];

            return user == _login && pass == _password;
        }

        return false;
    }
}
