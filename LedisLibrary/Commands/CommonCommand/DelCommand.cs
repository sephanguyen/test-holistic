using LedisLibrary.LedisStore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LedisLibrary.Commands.CommonCommand
{
    public class DelCommand : BaseCommand, IMyCommand, ICommandFactory
    {
        public string Key { get; set; }
        public string CommandName
        {
            get { return "DEL"; }
        }

        public dynamic Excute()
        {
            return DataLedisStore.GetDataStore().DelKey(Key);
        }

        public IMyCommand MakeCommand(List<string> arguments)
        {
            if (!ValidCommand.ValidParameter(arguments) || arguments.Count != 2)
                return new InValidCommand();
            return new DelCommand { Key = arguments[1] };
        }
    }
}
