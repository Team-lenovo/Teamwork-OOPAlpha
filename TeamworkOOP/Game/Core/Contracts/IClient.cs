using System.Net.Sockets;

using AcademyInvaders.Models;

namespace AcademyInvaders.Core.Contracts
{
    public interface IClient
    {
        TcpClient Client { get; }

        Player ClientPlayer { get; set; }

        void ConnectClient();

        void SendData(string output);

        string ReadServerData();
    }
}
