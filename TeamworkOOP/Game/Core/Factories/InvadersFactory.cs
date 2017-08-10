using AcademyInvaders.Core.Contracts;
using AcademyInvaders.Models;
using AcademyInvaders.Models.Contracts;
using AcademyInvaders.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyInvaders.Core.Factories
{
    public class InvadersFactory : IInvadersFactory
    {
        private static InvadersFactory instance = new InvadersFactory();


        private InvadersFactory()
        {

        }

        public static InvadersFactory Instance
        {
            get
            {
                return instance;
            }
        }



        public IPlayer CreatePlayer(PlayerState state = PlayerState.Default, WeaponChoice weapon = WeaponChoice.Torpedo, ConsoleColor color = ConsoleColor.Cyan,
            Position playerPosition = null, int lives = 3, int health = 30, int score = 0)
        {
            if (playerPosition == null)
            {
                playerPosition = new Position((Console.WindowWidth - 4) / 2, Console.WindowHeight - 1);
            }
            return new Player(state, weapon, color, playerPosition, lives, health, score);
        }

        public IEnemy CreateEnemy(Position enemyPosition = null, int health = 2, Size? size = null, ConsoleColor color = ConsoleColor.Cyan, int randX = 0)
        {
            if (enemyPosition == null)
            {
                enemyPosition = new Position(Console.WindowWidth / 2, 1);
            }
            if (size == null)
            {
                size = new Size(4, 1);
            }
            return new Enemy(enemyPosition, health, size, color, randX);
        }

        public IBullet CreateBullet(string playerName, Position currPlayerPosition, Size BulletSize)
        {
            return new Bullet(playerName, currPlayerPosition, BulletSize);
        }

        public IClient CreateInvadersClient()
        {
            return new InvadersClient();
        }
    }
}
