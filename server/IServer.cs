﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    public interface IServer
    {
        event Action ServerStarted;

        void Start();
        void Stop();
    }
}
