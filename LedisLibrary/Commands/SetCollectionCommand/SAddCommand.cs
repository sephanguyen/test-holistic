using LedisLibrary.Helpers;
using LedisLibrary.LedisStore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LedisLibrary.Commands.SetCollectionCommand
{
    class SAddCommand : BaseCommand, IMyCommand, ICommandFactory
    {
        public string CommandName
        {
            get { return "SADD"; }
        }

        public string Key { get; set; }
        public HashSet<string> Values { get; set; }

        public dynamic Excute()
        {
            var setCollection = DataLedisStore.GetDataStore().LedisCollection;
            return setCollection.SADD(Key, Values);
        }

        public IMyCommand MakeCommand(List<string> arguments)
        {
            if (!ValidCommand.ValidParameter(arguments) || (arguments.Count != 3))
                return new InValidCommand();
            return new SAddCommand { Key = arguments[1], Values = arguments[2].ConvertParamToHashSet() };
        }
    }
}
