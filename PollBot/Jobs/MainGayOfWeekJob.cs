using PollBot.Data;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace PollBot.Jobs
{
    internal class MainGayOfWeekJob : IJob
    {
        private readonly ITelegramBotClient _botClient;
        private readonly DataContext _db;

        public MainGayOfWeekJob(ITelegramBotClient botClient, DataContext db)
        {
            _botClient = botClient;
            _db = db;
        }

        public Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
