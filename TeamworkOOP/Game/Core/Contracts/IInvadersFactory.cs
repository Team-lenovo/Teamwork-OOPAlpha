using AcademyInvaders.Models;
using AcademyInvaders.Models.Contracts;
using System;

namespace AcademyInvaders.Core.Contracts
{
    public interface IInvadersFactory
    {
        IPlayer CreatePlayer(PlayerState state, WeaponChoice weapon, ConsoleColor color, Position playerPosition, int lives, int health, int score);
        IBullet CreateBullet(string playerName, Position currPlayerPosition, Size BulletSize);
        IEnemy CreateEnemy(Position position, int health, Size? size, ConsoleColor color, int rand);

    }
}