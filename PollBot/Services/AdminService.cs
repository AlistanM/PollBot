using Microsoft.EntityFrameworkCore;
using PollBot.CommandInfo;
using PollBot.Configuration;
using PollBot.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace PollBot.Services
{
    public class AdminService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly DataContext _db;
        private readonly AppSettings _settings;

        public AdminService(ITelegramBotClient botClient, DataContext db, AppSettings settings)
        {
            _botClient = botClient;
            _db = db;
            _settings = settings;
        }


        [Commands("GetChatList")]
        public async Task GetChatList(CommandDetail details)
        {
            var chats = await _db.ChatPolls.ToListAsync();
            var list = "id           название\n";

            foreach (var chat in chats)
            {
                list += chat.ChatId + "           " + chat.ChatTitle + "\n";
            }

            await _botClient.SendTextMessageAsync(_settings.AdminId, list);
        }

        [Commands]
        public async Task Test(CommandDetail details)
        {
            await _botClient.SendTextMessageAsync(_settings.AdminId, "тестовый метод сработал");
        }
    }

}
