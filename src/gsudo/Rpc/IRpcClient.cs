﻿using System.Threading.Tasks;

namespace gsudo.Rpc
{
    internal interface IRpcClient
    {
        Task<Connection> Connect(int? clientPid, bool failFast);
    }
}