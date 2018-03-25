using LedisLibrary.LedisStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LedisLibrary.Commands.SetCollectionCommand
{
    public class SMemberCommand : BaseCommand, IMyCommand, ICommandFactory
    {
        public string Key { get; set; }
        public string CommandName
        {
            get { return "SMEMBERS"; }
        }

        public dynamic Excute()
        {
            var setCollection = DataLedisStore.GetDataStore().LedisCollection;
            return setCollection.SMEMBERS(Key).ToList();
        }

        public IMyCommand MakeCommand(List<string> arguments )
        {
            if (!ValidCommand.ValidParameter(arguments) || (arguments.Count != 2))
                return new InValidCommand();
            return new SMemberCommand { Key = arguments[1] };
        }
    }
}
