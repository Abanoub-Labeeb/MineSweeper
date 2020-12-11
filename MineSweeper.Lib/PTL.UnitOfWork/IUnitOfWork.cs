using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweeper.Lib.PTL
{
    public interface IUnitOfWork : IDisposable
    {
        IMineSweeperRepository MineSweeper { get; }
        int Complete();
    }
}
