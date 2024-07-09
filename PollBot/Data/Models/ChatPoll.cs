using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PollBot.Data.Models
{
    [Table("ChatPolls")]
    public class ChatPoll
    {
        [Key]
        public long ChatId { get; set; }

        public string ChatTitle { get; set; }

        public int? LastPollId { get; set; }

        public DateTime? LastPollTime { get; set; }

        public string? PollName { get; set; }

        public string? PollOptions { get; set; }

    }
}
