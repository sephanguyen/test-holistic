using LedisLibrary.Helpers;
using LedisLibrary.LedisStore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LedisLibrary.Commands.ListCollectionCommand
{
    public class LLenCommand : BaseCommand, IMyCommand, ICommandFactory
    {
        public string Key { get; set; }
        public string CommandName
        {
            get { return "LLEN"; }
        }

        public dynamic Excute()
        {
            var listCollection = DataLedisStore.GetDataStore().LedisCollection;
            return listCollection.LLEN(Key);
        }

        public IMyCommand MakeCommand(List<string> arguments)
        {
            if (!ValidCommand.ValidParameter(arguments) || (arguments.Count != 2))
                return new InValidCommand();
            return new LLenCommand { Key = arguments[1] };
        }
    }
}
