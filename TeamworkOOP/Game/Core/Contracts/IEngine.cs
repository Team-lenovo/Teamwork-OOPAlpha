using AcademyInvaders.Models;
using AcademyInvaders.Models.Contracts;

namespace AcademyInvaders.Core.Contracts
{
    public interface IEngine
    {
        void Run();

        int GameSpeed { get; set; }

        void PlayOffline(IPlayer offlinePlayer);

        void PlayOnline(IClient client);
    }
}
