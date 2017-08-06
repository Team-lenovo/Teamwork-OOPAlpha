using System;
using System.Collections.Generic;

using AcademyInvaders.Core.Contracts;
using AcademyInvaders.Models;

namespace AcademyInvaders.Core.Providers
{
    public class GameDB : IGameDB
    {
        private static GameDB instance = new GameDB();

        private GameDB()
        {
            this.Users = new Dictionary<Tuple<string, string>, Player>();
        }

        public Dictionary<Tuple<string, string>, Player> Users { get; private set; }

        public static GameDB Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
