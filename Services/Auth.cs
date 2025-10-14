public class Auth
{
    public bool LoggedIn { get; private set; } = false;
    public string? Email { get; private set; } = null;


    public void LogIn(string email)
    {
        LoggedIn = true;
        Email = email;
    }

    public void LogOut()
    {
        LoggedIn = false;
        Email = null;
    }
}