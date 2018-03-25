using LedisLibrary.LedisStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LedisLibrary.Commands.ListCollectionCommand
{
    public class LRangeCommand : BaseCommand, IMyCommand, ICommandFactory
    {
        public string Key { get; set; }
        public int Start { get; set; }
        public int Stop { get; set; }

        public string CommandName
        {
            get { return "LRANGE"; }
        }

        public dynamic Excute()
        {
            var listCollection = DataLedisStore.GetDataStore().LedisCollection;
            return listCollection.LRANGE(Key, Start, Stop).ToList();
        }

        public IMyCommand MakeCommand(List<string> arguments)
        {
            int start;
            int stop;
            var args = arguments[2].Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            if (!ValidCommand.ValidParameter(arguments) || (arguments.Count != 3) 
                || !int.TryParse(args[0], out start) || !int.TryParse(args[1], out stop))
                return new InValidCommand();
            return new LRangeCommand { Key = arguments[1], Start = start, Stop = stop };
        }
    }
}
