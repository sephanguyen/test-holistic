using System;
using System.Collections.Generic;
using System.Text;

namespace LedisLibrary.Commands.ValidCommand
{
    public interface IValidParam
    {
        bool ValidParameter(List<string> param);
    }
}
