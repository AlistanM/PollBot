﻿using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PollBot.Bot;
using PollBot.Data;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace PollBot
{
    class Program
    {
        static public ITelegramBotClient bot = new TelegramBotClient("###");
        static public long BotId { get; private set; }

        static async Task Main(string[] args)
        {

            var provider = DI.ConfigureServices();
            await ApplyMigrations(provider);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { },
            };
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

