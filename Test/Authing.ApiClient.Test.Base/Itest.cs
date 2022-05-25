using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Test.Base
{
    interface IInterface
    {
        public int Test { get; set; }

        EventHandler TestEventHandler(int test);
        string this[int index]
        {
            get;
            set;
        }
    } 
}
