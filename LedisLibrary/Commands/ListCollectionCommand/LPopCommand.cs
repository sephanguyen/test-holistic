using LedisLibrary.LedisStore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LedisLibrary.Commands.ListCollectionCommand
{
    public class LPopCommand : BaseCommand, IMyCommand, ICommandFactory
    {
        public string Key { get; set; }
        public List<string> Values { get; set; }
        public string CommandName
        {
            get { return "LPOP"; }
        }

        public dynamic Excute()
        {
            var listCollection = DataLedisStore.GetDataStore().LedisCollection;
            return listCollection.LPOP(Key);
        }

        public IMyCommand MakeCommand(List<string> arguments)
        {
            if (!ValidCommand.ValidParameter(arguments) || (arguments.Count != 2))
                return new InValidCommand();
            return new LPopCommand { Key = arguments[1] };
        }
    }
}
