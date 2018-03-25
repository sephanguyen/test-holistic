using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LedisLibrary.Commands;
using Microsoft.AspNetCore.Mvc;

namespace LedisServer.Controllers
{
    [Route("api/[controller]")]
    public class LedisController : Controller
    {
        // GET api/values

        [HttpPost]
        public dynamic QueryLedis(string query)
        {
            var args = query.Split(' ', 3, StringSplitOptions.RemoveEmptyEntries);
            
            var listCommand = CommonStore.GetCommonStore().ListCommand;
            var parser = new CommandParser(listCommand);
            var command = parser.ParseCommand(args.ToList());
            var result = command.Excute();
            return result;
        }

        
    }
}
