using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PollBot.CommandInfo
{
    public class CommandsAttribute : Attribute
    {
        public string commandName;
        public CommandsAttribute([CallerMemberName] string name = null)
        {
            //Console.WriteLine(name);
            commandName = name;
        }
    }
}
