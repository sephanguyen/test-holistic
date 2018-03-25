using System;
using System.Collections.Generic;
using System.Text;

namespace LedisLibrary.Commands.ValidCommand
{
    public class ValidParam : IValidParam
    {
        public bool ValidParameter(List<string> parameters)
        {
            foreach(string param in parameters) {
                if(string.IsNullOrEmpty(param)) {
                    return false;
                }
            }
            return true;
        }
    }
}
