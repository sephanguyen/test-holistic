using LedisLibrary.LedisStore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LedisLibrary.Commands.CommonCommand
{
    public class SaveCommand : BaseCommand, IMyCommand, ICommandFactory
    {
        public string CommandName
        {
            get { return "SAVE"; }
        }

        public dynamic Excute()
        {
            return DataLedisStore.GetDataStore().SaveDB();
        }

        public IMyCommand MakeCommand(List<string> arguments)
        {
            return new SaveCommand();
        }
    }
}
