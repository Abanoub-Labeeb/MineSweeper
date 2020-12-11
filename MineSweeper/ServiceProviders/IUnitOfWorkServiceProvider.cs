using MineSweeper.Lib.PTL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineSweeper.ServiceProviders
{
    public interface IUnitOfWorkServiceProvider
    {
        UnitOfWork GetUnitOfWork();
        int GetInstanceUniqueID();
    }
}
