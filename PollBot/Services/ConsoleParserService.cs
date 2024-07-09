using PollBot.CommandInfo;
using PollBot.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace PollBot.Services
{
    public class ConsoleParserService
    {
        private readonly AdminService _adminService;
        private readonly DictGeneratorService _dictGeneratorService;

        public ConsoleParserService(AdminService adminService, DictGeneratorService dictGeneratorService)
        {
            _adminService = adminService;
            _dictGeneratorService = dictGeneratorService;
        }

        public async void GetConsoleCommand(string command)
        {
            var m = typeof(AdminService).GetMethods();
            
            if (command != null && _dictGeneratorService.CommandDictionary.ContainsKey(command))
            {
                var method = typeof(AdminService).GetMethod(_dictGeneratorService.CommandDictionary[command]);
                await (Task)method.Invoke(_adminService, new object[] { new CommandDetail() });
            }
        }
    }
}
