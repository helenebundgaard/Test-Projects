using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    public interface IClient
    {
        void SendMessage(string message);
        void ReceiveMessages();
    }
}
