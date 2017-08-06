using System;
using System.Collections.Generic;

using AcademyInvaders.Models;

namespace AcademyInvaders.Core.Contracts
{
    public interface IGameDB
    {
        Dictionary<Tuple<string, string>, Player> Users { get; }
    }
}
