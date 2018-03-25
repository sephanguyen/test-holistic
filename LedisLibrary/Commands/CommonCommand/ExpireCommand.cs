using LedisLibrary.LedisStore;
using System;
using System.Collections.Generic;

namespace LedisLibrary.Commands.CommonCommand
{
    class ExpireCommand : BaseCommand, IMyCommand, ICommandFactory
    {
        public string Key { get; set; }
        public int Second { get; set; }
        public string CommandName
        {
            get { return "EXPIRE"; }
        }

        public dynamic Excute()
        {
            return DataLedisStore.GetDataStore().Expire(Key, Second);
        }

        public IMyCommand MakeCommand(List<string> arguments)
        {
            int second;
            if (!ValidCommand.ValidParameter(arguments)|| arguments.Count != 3 || !int.TryParse(arguments[2], out second))
                return new InValidCommand();
            return new ExpireCommand { Key = arguments[1], Second = second };
        }
    }
}
