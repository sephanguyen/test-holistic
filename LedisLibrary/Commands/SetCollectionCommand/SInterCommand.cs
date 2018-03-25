using LedisLibrary.Helpers;
using LedisLibrary.LedisStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LedisLibrary.Commands.SetCollectionCommand
{
    public class SInterCommand : BaseCommand, IMyCommand, ICommandFactory
    {
        public List<string> Keys { get; set; }

        public string CommandName
        {
            get { return "SINTER"; }
        }
        public dynamic Excute()
        {
            var setCollection = DataLedisStore.GetDataStore().LedisCollection;
            return setCollection.SINTER(Keys).ToList();
        }

        public IMyCommand MakeCommand(List<string> arguments)
        {
            if (!ValidCommand.ValidParameter(arguments) || (arguments.Count < 2))
                return new InValidCommand();
            var keys = new List<string> { arguments[1] };
            if (arguments.Count > 2)
            {
                keys.AddRange(arguments[2].ConvertParamToList());
            }
            return new SInterCommand { Keys = keys };
        }
    }
}
