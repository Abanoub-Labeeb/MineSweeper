using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MineSweeper.Lib.PTL;

namespace MineSweeper.ServiceProviders
{
    public class UnitOfWorkServiceProvider : IUnitOfWorkServiceProvider
    {
        private UnitOfWork UnitOfWork;
        
        public UnitOfWorkServiceProvider(ILogger<UnitOfWork> logger)
        {
            UnitOfWork = new UnitOfWork(new ApplicationContext(), logger);
        }


        public UnitOfWork GetUnitOfWork()
        {
            //return  unique id of the created instance in the constructor
            return UnitOfWork;
        }

        //return unique id for the instantiated obj. from this class
        public int GetInstanceUniqueID()
        {
            return GetHashCode();
        }
    }
}
