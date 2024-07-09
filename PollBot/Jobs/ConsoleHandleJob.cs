using PollBot.Configuration;
using PollBot.Services;
using Quartz;
using Telegram.Bot;

namespace PollBot.Jobs
{
    public class ConsoleHandleJob : IJob
    {
        private readonly ITelegramBotClient _botClient;
        private readonly AppSettings _appSettings;
        private readonly ConsoleParserService _consoleParesrService;

        public ConsoleHandleJob(ITelegramBotClient botClient, AppSettings appSettings, ConsoleParserService consoleParserService)
        {
            _botClient = botClient;
            _appSettings = appSettings;
            _consoleParesrService = consoleParserService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var adminId = _appSettings.AdminId;

            while (true)
            {
                var command = Console.ReadLine();

                _consoleParesrService.GetConsoleCommand(command);

                //await _botClient.SendTextMessageAsync(adminId, $"Выполнена команда {command}");
            }

        }
    }
}
