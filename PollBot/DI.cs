using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PollBot.Bot;
using PollBot.Configuration;
using PollBot.Data;
using PollBot.Jobs;
using System.Reflection;
using Telegram.Bot;

namespace PollBot
{
    public static class DI
    {
        public static IServiceProvider Provider { get; private set; }

        public static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddDbContext<DataContext>(ServiceLifetime.Transient);
            services.AddBot();
            services.AddJobs();
            services.AddServices();
            services.AddConfiguration();

            return services.BuildServiceProvider();
        }

        private static void AddBot(this IServiceCollection services)
        {
            services.AddTransient<ITelegramBotClient>(provider => 
                new TelegramBotClient(provider.GetService<AppSettings>().TelegramToken));
        }

        private static void AddJobs(this IServiceCollection services)
        {
            services.AddTransient<PollCreateJob>();
            services.AddTransient<CompletePoll>();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<CommandHandler>();
        }

        private static void AddConfiguration(this IServiceCollection services)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .AddUserSecrets(Assembly.GetExecutingAssembly())
                .Build();

            services.AddSingleton(config);
            services.AddSingleton(provider => 
                provider
                    .GetService<IConfiguration>()
                    .GetRequiredSection("AppSettings")
                    .Get<AppSettings>());
        }
    }
}
