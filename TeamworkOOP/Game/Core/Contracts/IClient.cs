using System.Net.Sockets;

using AcademyInvaders.Models;
using AcademyInvaders.Models.Contracts;

namespace AcademyInvaders.Core.Contracts
{
    public interface IClient
    {
        TcpClient Client { get; set; }

        IPlayer ClientPlayer { get; set; }

        string PlayerName{ get; set; }

        void ConnectClient();

        void SendData(string output);

        string ReadServerData();
    }
}
