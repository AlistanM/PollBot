using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PollBot.Data;
using PollBot.Data.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using File = System.IO.File;

namespace PollBot.Jobs
{
    public class PollCreateJob : IJob
    {
        private readonly ITelegramBotClient _botClient;
        private readonly DataContext _db;
        public PollCreateJob(ITelegramBotClient botClient, DataContext db) { 
            _botClient = botClient;
            _db = db;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var quest = new[] { "Смешная нарезка гусей?", "Пускаем гуся на жаркое?", "Нафаршируем гусей перчиком?", "Фуагра для хорошего завершения дня?", "Га-га-га-га" };
            try
            {
                var time = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText("PollTime.json"));

                var chats = await _db.ChatPolls.ToListAsync();

                if(chats.Count > 0)
                {
                    foreach(var chat in chats)
                    {
                        if (chat.LastPollTime == null || chat.LastPollTime.Value.AddHours(12) < DateTime.Now) 
                        {
                            var msg = await _botClient.SendPollAsync(chat.ChatId, quest[new Random().Next(quest.Length)], time.Select(x => $"Могу в {x} по мск").Concat(new[] { "Я пидор и с полной уверностью это заявляю!!!" }), isAnonymous: false);
                            chat.LastPollTime = DateTime.Now.AddHours(-1);
                            chat.LastPollId = msg.MessageId;
                            _db.SaveChanges();
                        }
                    }
                }

                Console.WriteLine("Poll was create");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
