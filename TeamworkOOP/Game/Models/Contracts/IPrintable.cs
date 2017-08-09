using System;

namespace AcademyInvaders.Models.Contracts
{
    public interface IPrintable
    {
        Position ObjectPosition { get; }

        ConsoleColor Color { get; set; }
    }
}
