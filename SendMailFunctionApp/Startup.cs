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
        builder.ConfigurationBuilder.AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"), true, false);
        builder.ConfigurationBuilder.AddJsonFile(Path.Combine(context.ApplicationRootPath, $"appsettings.{context.EnvironmentName}.json"), true, false);
        //importante para que siempre lea el local setting o local publicado
        builder.ConfigurationBuilder.AddEnvironmentVariables();
    }
}
