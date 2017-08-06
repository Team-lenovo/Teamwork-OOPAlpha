using AcademyInvaders.Models;

namespace AcademyInvaders.Core.Contracts
{
    public interface IEngine
    {
        void Run();

        int GameSpeed { get; set; }

        void PlayOffline(Player offlinePlayer);

        void PlayOnline(IClient client);
    }
}
