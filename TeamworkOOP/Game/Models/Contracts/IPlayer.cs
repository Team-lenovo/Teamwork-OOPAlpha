using System.Collections.Generic;

namespace AcademyInvaders.Models.Contracts
{
    public interface IPlayer : IMoveable, IPrintable
    {
        int Score { get; set; }
        void MoveOnLine(int pressedKey);
        List<IBullet> ShootedBullets { get; set; }
        int Lives { get; set; }
        int Health { get; set; }
        WeaponChoice Weapon { get; set; }
        PlayerState State { get; set; }
        string Skin { get; set; }
    }
}