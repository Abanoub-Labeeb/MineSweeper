using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweeper.Lib.PTL
{
    public interface IDBFunctionsResultRepository : IRepository<DBFunctionsResult>
    {
        #region  SQL Server DB Functions
        public int FN_MineSweeper_InputCount();
        #endregion

    }
}
