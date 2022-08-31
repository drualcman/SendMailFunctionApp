//configurar la funcion con un startup differente
[assembly: FunctionsStartup(typeof(SendMailFunctionApp.Startup))]
namespace SendMailFunctionApp;

/// Cuando implementa FunctionsStartup se convierte en la clase inicial de la aplicacion
internal class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddTransient<MailService>();
    }

    public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
    {
        base.ConfigureAppConfiguration(builder);
        var context = builder.GetContext();
        builder.ConfigurationBuilder.AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsetings.json"), true, false);
    }
}
