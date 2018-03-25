using LedisLibrary.LedisStore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LedisLibrary.Commands.CommonCommand
{
    public class RestoreCommand : BaseCommand, IMyCommand, ICommandFactory
    {
        public string CommandName
        {
            get { return "RESTORE"; }
        }

        public dynamic Excute()
        {
            return DataLedisStore.GetDataStore().RestoreDB();
        }

        public IMyCommand MakeCommand(List<string> arguments)
        {
            return new RestoreCommand();
        }
    }
}
