using System.Collections.Generic;
using System.Net.Sockets;
using AcademyInvaders.Models.Contracts;

namespace AcademyInvaders.Core.Contracts
{
    public interface IServer
    {
        Dictionary<string, TcpClient> Clients { get; }

        Dictionary<string, IPlayer> Players { get; }

        void Start();

        void LoopClients();

        string ChooseOpponent(IClient client);
    }
}
