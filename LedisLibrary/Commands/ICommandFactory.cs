
using System.Collections.Generic;

namespace LedisLibrary.Commands
{
    public interface ICommandFactory
    {
        string CommandName { get; }

        IMyCommand MakeCommand(List<string> arguments);
    }
}
