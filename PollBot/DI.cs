using Microsoft.Extensions.DependencyInjection;
using PollBot.Bot;
using PollBot.Data;
using PollBot.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            return services.BuildServiceProvider();
        }

        private static void AddBot(this IServiceCollection services)
        {
            services.AddTransient<ITelegramBotClient>(x => new TelegramBotClient("###"));
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
    }
}
