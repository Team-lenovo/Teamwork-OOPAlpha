using System.Collections.Generic;

namespace AcademyInvaders.Models.Contracts
{
    public interface IBoss : IShootable, IPrintable, IMoveable
    {
        int Health { get; set; }

        List<IBullet> ShootedBullets { get; set; }
    }
}