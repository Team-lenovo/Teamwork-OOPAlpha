namespace AcademyInvaders.Models.Contracts
{
    public interface IBoss : IShootable, IPrintable, IMoveable
    {
        int Health { get; set; }
    }
}