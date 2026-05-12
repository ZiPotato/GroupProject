using System.Diagnostics;
using OrderTrackingBlazor.Components;

namespace OrderTrackingBlazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(new WebApplicationOptions
                {
                    Args = args,
                    ContentRootPath = AppContext.BaseDirectory
                });

                const string defaultUrl = "http://127.0.0.1:5161";

                var configuredUrls =
                    builder.Configuration["urls"] ??
                    Environment.GetEnvironmentVariable("ASPNETCORE_URLS") ??
                    defaultUrl;

                builder.WebHost.UseUrls(configuredUrls);

                var urls = configuredUrls
                    .Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                var startupUrl =
                    urls.FirstOrDefault(url => url.StartsWith("http://", StringComparison.OrdinalIgnoreCase)) ??
                    urls.FirstOrDefault() ??
                    defaultUrl;

                builder.Services.AddRazorComponents()
                    .AddInteractiveServerComponents();

                builder.Logging.ClearProviders();
                builder.Logging.AddConsole();

                var app = builder.Build();

                if (!app.Environment.IsDevelopment())
                {
                    app.UseExceptionHandler("/Error");
                }

                app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
                app.UseAntiforgery();

                app.MapStaticAssets();
                app.MapRazorComponents<App>()
                    .AddInteractiveServerRenderMode();

                if (!Debugger.IsAttached && Environment.UserInteractive)
                {
                    app.Lifetime.ApplicationStarted.Register(() =>
                    {
                        try
                        {
                            Process.Start(new ProcessStartInfo
                            {
                                FileName = startupUrl,
                                UseShellExecute = true
                            });
                        }
                        catch
                        {
                        }
                    });
                }

                app.Run();
            }
            catch (Exception ex)
            {
                var message = $"Startup failed:{Environment.NewLine}{ex}";
                Console.Error.WriteLine(message);

                try
                {
                    File.WriteAllText(
                        Path.Combine(AppContext.BaseDirectory, "startup-error.txt"),
                        message);
                }
                catch
                {
                }

                throw;
            }
        }
    }
}
