using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollBot.Data.Models
{
    [Table("MainGay")]
    public class MainGay
    {
        [Key]
        public long ChatId { get; set; }

        public string Name { get; set; }

        public DateTime Time { get; set; }
    }
}
