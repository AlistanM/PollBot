using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PollBot.Bot;
using PollBot.Configuration;
using PollBot.Data;
using PollBot.Services;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace PollBot
{
    class Program
    {
        static public long BotId { get; private set; }

        static async Task Main(string[] args)
        {

            var provider = DI.ConfigureServices();
            await ApplyMigrations(provider);

            provider.GetService<DictGeneratorService>().GenerateDictonary();

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { },
            };

            var bot = provider.GetService<ITelegramBotClient>();
            bot.StartReceiving(
                provider.GetService<CommandHandler>(),
                receiverOptions,
                cancellationToken
            );

            var botInfo = await bot.GetMeAsync();
            BotId = botInfo.Id;

            await Jobs.JobsConfigurator.Start(provider);
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);
            await Task.Delay(-1);

        }

        private static async Task ApplyMigrations(IServiceProvider provider)
        {
            var db = provider.GetService<DataContext>();
            await db.ApplyMigrations();
        }

    }
}

