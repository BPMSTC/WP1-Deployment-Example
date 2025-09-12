namespace WP1.App.Services;

public class AppService
{
    public string GetWelcomeMessage()
    {
        return "Welcome to WP1 Deployment Example! The application is working correctly.";
    }

    public bool IsHealthy()
    {
        return true;
    }
}