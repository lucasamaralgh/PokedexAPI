using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Business.Core.Notifications
{
    public  class Notification
    {
        public string Message { get;}

        public Notification(string message)
        {
            Message = message;
        }
    }
}
