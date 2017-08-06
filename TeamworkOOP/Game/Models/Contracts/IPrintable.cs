using System;

namespace AcademyInvaders.Models.Contracts
{
    public interface IPrintable
    {
        Position PlayerPosition { get; }

        ConsoleColor Color { get; }
    }
}
