using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MineSweeper.Lib.PTL
{
    public class DBFunctionsResultRepository : Repository<DBFunctionsResult>, IDBFunctionsResultRepository
    {

        #region Members
        public ApplicationContext _context => context;
        #endregion

        #region Ctor
        public DBFunctionsResultRepository(ApplicationContext context) : base(context)
        {
        }

        #endregion

        #region SQL Server Functions 

        [DbFunction("FN_MineSweeper_InputCount", "dbo")]
        public int FN_MineSweeper_InputCount()
        {
            return entityDBSet
                .FromSqlInterpolated($"select  dbo.FN_MineSweeper_InputCount() as InputCount")
                .FirstOrDefault().InputCount;
        }

        #endregion

    }
}
