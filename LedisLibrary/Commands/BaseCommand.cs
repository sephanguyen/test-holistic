
using LedisLibrary.Commands.ValidCommand;

namespace LedisLibrary.Commands
{
    public abstract class BaseCommand
    {
        protected IValidParam ValidCommand { get; set; }

        public BaseCommand()
        {
            ValidCommand = new ValidParam();
        }
    }
}
