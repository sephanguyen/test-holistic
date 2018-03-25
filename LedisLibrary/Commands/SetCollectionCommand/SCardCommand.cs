using LedisLibrary.LedisStore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LedisLibrary.Commands.SetCollectionCommand
{
    public class SCardCommand : BaseCommand, IMyCommand, ICommandFactory
    {
        public string Key { get; set; }
        public string CommandName
        {
            get { return "SCARD"; }
        }

        public dynamic Excute()
        {
            var setCollection = DataLedisStore.GetDataStore().LedisCollection;
            return setCollection.SCARD(Key);
        }

        public IMyCommand MakeCommand(List<string> arguments)
        {
            if (!ValidCommand.ValidParameter(arguments) || (arguments.Count != 2))
                return new InValidCommand();
            return new SCardCommand { Key = arguments[1] };
        }
    }
}
