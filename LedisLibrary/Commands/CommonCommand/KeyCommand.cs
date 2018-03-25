using LedisLibrary.LedisStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LedisLibrary.Commands.CommonCommand
{
    public class KeyCommand : BaseCommand, IMyCommand, ICommandFactory
    {
        public string CommandName
        {
            get { return "KEYS"; }
        }
        public string Pattern { get; set; }
        public dynamic Excute()
        {
            return DataLedisStore.GetDataStore().GetKeys(Pattern);
        }

        public IMyCommand MakeCommand(List<string> arguments)
        {
            if (!ValidCommand.ValidParameter(arguments) || (arguments.Count != 2))
                return new InValidCommand();
            return new KeyCommand { Pattern = arguments[1] };
        }
    }
}
