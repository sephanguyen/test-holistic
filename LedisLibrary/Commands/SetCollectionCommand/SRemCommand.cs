using LedisLibrary.Helpers;
using LedisLibrary.LedisStore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LedisLibrary.Commands.SetCollectionCommand
{
    public class SRemCommand : BaseCommand, IMyCommand, ICommandFactory
    {
        public string Key { get; set; }
        public HashSet<string> Values { get; set; }
        public string CommandName
        {
            get { return "SREM"; }
        }

        public dynamic Excute()
        {
            var setCollection = DataLedisStore.GetDataStore().LedisCollection;
            return setCollection.SREM(Key, Values);
        }

        public IMyCommand MakeCommand(List<string> arguments)
        {
            if (!ValidCommand.ValidParameter(arguments) || (arguments.Count != 3))
                return new InValidCommand();
            return new SRemCommand { Key = arguments[1], Values = arguments[2].ConvertParamToHashSet() };
        }
    }
}
