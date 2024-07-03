using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollBot.Data.Models
{
    [Table("ChatPolls")]
    public class ChatPoll
    {
        [Key]
        public long ChatId {  get; set; }

        public int? LastPollId { get; set; }

        public DateTime? LastPollTime { get; set; }
    }
}
