using LedisLibrary.LedisStore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LedisLibrary.Commands.StringCollectionCommand
{
    public class GetCommand : BaseCommand, IMyCommand, ICommandFactory
    {
        public string Key { get; set; }
        public string CommandName
        {
            get { return "GET"; }
        }

        public dynamic Excute()
        {
            var stringCollection = DataLedisStore.GetDataStore().LedisCollection;
            return stringCollection.GET(Key);
        }

        public IMyCommand MakeCommand(List<string> arguments)
        {
            if (!ValidCommand.ValidParameter(arguments) || (arguments.Count != 2))
                return new InValidCommand();
            return new GetCommand { Key = arguments[1] };
        }
    }
}
