using LedisLibrary.LedisStore;
using System.Collections.Generic;

namespace LedisLibrary.Commands.StringCollectionCommand
{
    public class SetCommand : BaseCommand, IMyCommand, ICommandFactory
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string CommandName
        {
            get { return "SET"; }
        }

        public dynamic Excute()
        {
            var stringCollection = DataLedisStore.GetDataStore().LedisCollection;
            return stringCollection.SET(Key, Value);
        }

        public IMyCommand MakeCommand(List<string> arguments)
        {
            if (!ValidCommand.ValidParameter(arguments) || (arguments.Count != 3))
                return new InValidCommand();
            return new SetCommand { Key = arguments[1], Value = arguments[2]};
        }
    }
}
