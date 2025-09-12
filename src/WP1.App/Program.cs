// WP1 Deployment Example Application
using WP1.App.Services;

Console.WriteLine("WP1 Deployment Example - Starting Application");

var appService = new AppService();
var result = appService.GetWelcomeMessage();
Console.WriteLine(result);

Console.WriteLine("Application running successfully. Press any key to exit...");
Console.ReadKey();
