
using LedisLibrary.Helpers;
using LedisLibrary.LedisStore;
using System.Collections.Generic;

namespace LedisLibrary.Commands.ListCollectionCommand
{
    public class RPushCommand : BaseCommand, IMyCommand, ICommandFactory
    {
        public string Key { get; set; }
        public List<string> Values { get; set; }
        public string CommandName
        {
            get { return "RPUSH"; }
        }

        public dynamic Excute()
        {
            var listCollection = DataLedisStore.GetDataStore().LedisCollection;
            return listCollection.RPUSH(Key, Values);
        }

        public IMyCommand MakeCommand(List<string> arguments)
        {
            if (!ValidCommand.ValidParameter(arguments) || (arguments.Count != 3))
                return new InValidCommand();
            return new RPushCommand { Key = arguments[1], Values = arguments[2].ConvertParamToList()};
        }
    }
}
