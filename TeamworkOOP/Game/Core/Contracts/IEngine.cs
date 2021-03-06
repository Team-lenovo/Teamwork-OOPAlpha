﻿using AcademyInvaders.Models;
using AcademyInvaders.Models.Contracts;
using System.Collections.Generic;

namespace AcademyInvaders.Core.Contracts
{
    public interface IEngine
    {
        List<object> GameObjects { get; set; }

        int GameSpeed { get; set; }

        void Run();

        void PlayOffline(IPlayer offlinePlayer);

        void PlayOnline(IClient client);

        void MirrorOpponent(IPlayer opponent);

        void HitCheck(IPlayer player, List<IEnemy> enemies, IPlayer opponent = null, IBoss boss = null);
    }
}
