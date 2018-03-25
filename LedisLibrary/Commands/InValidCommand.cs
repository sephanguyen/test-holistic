

namespace LedisLibrary.Commands
{
    public class InValidCommand : IMyCommand
    {
        public dynamic Excute()
        {
            return "Parame InValid";
        }
    }
}
