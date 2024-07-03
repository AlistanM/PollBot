using Microsoft.EntityFrameworkCore;
using PollBot.Data;
using Quartz;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PollBot.Jobs
{
    public class CompletePoll : IJob
    {
        private readonly ITelegramBotClient _botClient;
        private readonly DataContext _db;

        public CompletePoll(ITelegramBotClient botClient, DataContext db)
        {
            _botClient = botClient;
            _db = db;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var chats = await _db.ChatPolls.ToListAsync();

            if (chats.Count > 0)
            {
                foreach (var chat in chats)
                {
                    if (chat.LastPollId != null && chat.LastPollTime.Value.AddHours(4) > DateTime.Now)
                    {
                        var msg = await _botClient.StopPollAsync(chat.ChatId, (int)chat.LastPollId);
                        var voterCount = 0;

                        for (int i = 0; i < msg.Options.Length - 1; i++)
                        {
                            voterCount += msg.Options[i].VoterCount;

                            if (voterCount >= 6)
                            {
                                var text = msg.Options[i].Text;
                                var regex = new Regex(@"\b(?:[01][0-9]|2[0-3]):[0-5][0-9]\b");
                                var match = regex.Matches(text).Cast<Match>().Select(x => x.Value).FirstOrDefault();

                                await _botClient.SendTextMessageAsync(chat.ChatId, $"Сбор гусей в {match} мск");
                                break;
                            }

                            else
                            {
                                await _botClient.SendTextMessageAsync(chat.ChatId, $"Сегодня гусей не собираем");
                                break;
                            }
                            
                        }
                    }
                }
            }

        }
    }
}
