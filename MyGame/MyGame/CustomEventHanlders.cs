using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Page1CustomEventArgs : EventArgs
    {
        public string MyParam { get; set; }

        public Page1CustomEventArgs(string param)
        {
            MyParam = param;
        }
    }

    public class Page2CustomEventArgs : EventArgs
    {
        public string MyParamForPage2 { get; set; }

        public Page2CustomEventArgs(string param)
        {
            MyParamForPage2 = param;
        }
    }

}
