﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    public interface IClientHandler
    {
        void HandleClient(TcpClient client);
    }
}
