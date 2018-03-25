
namespace LedisLibrary.Commands
{
    public class NotFoundCommand : IMyCommand
    {
        public string Name { get; set; }


        public dynamic Excute()
        {
            return "Not found command";
        }
    }
}
