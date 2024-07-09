using PollBot.CommandInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PollBot.Services
{
    public class DictGeneratorService
    {
        public Dictionary<string, string> CommandDictionary = new();

        public void GenerateDictonary()
        {
            var methods = typeof(AdminService).GetMethods().Where(m => m.GetCustomAttribute<CommandsAttribute>() != null);

            foreach (var item in methods)
            {
                CommandDictionary.Add(item.GetCustomAttribute<CommandsAttribute>().commandName, item.Name);               
            }

            //Console.WriteLine(string.Join("\n", CommandDictionary.Select(x => $"{x.Key}: {x.Value}")));

        }
    }
}
