using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PollBot.Data;
using PollBot.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace PollBot.Bot
{
    internal class CommandHandler : IUpdateHandler
    {
        private readonly DataContext _db;

        public CommandHandler(DataContext db)
        {
            _db = db;
        }

        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception);
            throw new NotImplementedException();
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message?.NewChatMembers?.Select(x => x.Id).Contains(Program.BotId) ?? false)
            {
                var id = await _db.ChatPolls.FirstOrDefaultAsync(x => x.ChatId == update.Message.Chat.Id);
                if(id != null) 
                {
                    _db.ChatPolls.Remove(id);
                    await _db.SaveChangesAsync();
                }
                var newChat = new ChatPoll() { ChatId =  update.Message.Chat.Id};
                _db.Add(newChat);
                await _db.SaveChangesAsync();
            }
        }
    }
}
