
using LedisLibrary.LedisStore;
using System.Collections.Generic;

namespace LedisLibrary.Commands.CommonCommand
{
    public class FLushCommand : BaseCommand, IMyCommand, ICommandFactory
    {
        public string CommandName
        {
            get { return "FLUSHDB"; }
        }

        public dynamic Excute()
        {
            return DataLedisStore.GetDataStore().FlushDB();
        }

        public IMyCommand MakeCommand(List<string> arguments)
        {
            return new FLushCommand();
        }
    }
}
