using LedisLibrary.Commands.CommonCommand;
using LedisLibrary.Commands.ListCollectionCommand;
using LedisLibrary.Commands.SetCollectionCommand;
using LedisLibrary.Commands.StringCollectionCommand;
using System.Collections.Generic;

namespace LedisLibrary.Commands
{
    public class CommonStore
    {
        private static CommonStore _instance = new CommonStore();

        public IEnumerable<ICommandFactory> ListCommand { get; private set; }
        
        private CommonStore()
        {
            ListCommand = new ICommandFactory[]
                {
                    new GetCommand(),
                    new SetCommand(),
                    new SAddCommand(),
                    new SCardCommand(),
                    new SInterCommand(),
                    new SMemberCommand(),
                    new SRemCommand(),
                    new LLenCommand(),
                    new LPopCommand(),
                    new LRangeCommand(),
                    new RPopCommand(),
                    new RPushCommand(),
                    new DelCommand(),
                    new FLushCommand(),
                    new KeyCommand(),
                    new ExpireCommand(),
                    new TtlCommand(),
                    new SaveCommand(),
                    new RestoreCommand()
                };
        }

        public static CommonStore GetCommonStore()
        {
            return _instance;
        }
    }
}
