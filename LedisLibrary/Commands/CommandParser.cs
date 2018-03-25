using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LedisLibrary.Commands
{
    public class CommandParser
    {
        readonly IEnumerable<ICommandFactory> availableCommands;
        public CommandParser(IEnumerable<ICommandFactory> availableCommands)
        {
            this.availableCommands = availableCommands;
        }

        public IMyCommand ParseCommand(List<string> args)
        {
            var requestedCommandName = args[0];

            var command = FindRequestedCommand(requestedCommandName);
            if (null == command)
                return new NotFoundCommand { Name = requestedCommandName };

            return command.MakeCommand(args);
        }

        ICommandFactory FindRequestedCommand(string commandName)
        {
            return availableCommands
                .FirstOrDefault(cmd => cmd.CommandName == commandName.ToUpper());
        }
    }
}
