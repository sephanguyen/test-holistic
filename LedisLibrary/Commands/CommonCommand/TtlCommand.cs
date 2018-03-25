using LedisLibrary.LedisStore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LedisLibrary.Commands.CommonCommand
{
    public class TtlCommand : BaseCommand, IMyCommand, ICommandFactory
    {
        public string Key { get; set; }
        public string CommandName
        {
            get { return "TTL"; }
        }

        public dynamic Excute()
        {
            return DataLedisStore.GetDataStore().GetTtl(Key);
        }

        public IMyCommand MakeCommand(List<string> arguments)
        {
            if (!ValidCommand.ValidParameter(arguments) || (arguments.Count != 2))
                return new InValidCommand();
            return new TtlCommand { Key = arguments[1] };
        }
    }
}
