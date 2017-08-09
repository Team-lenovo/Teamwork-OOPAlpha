namespace AcademyInvaders.Models.Contracts
{
    public interface IEnemy : IPrintable, IMoveable, ISizeable
    {
        int Health { get; set; }
    }
}