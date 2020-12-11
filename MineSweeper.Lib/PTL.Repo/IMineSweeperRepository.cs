using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweeper.Lib.PTL
{
    public interface IMineSweeperRepository : IRepository<MineSweeper>
    {

        #region  MineSweeper Functions

        #endregion

        #region  SQL Server Stored Procedures
        public IEnumerable<MineSweeper> SP_GetAllMineInput();
        public IEnumerable<MineSweeper> SP_GetMineInput(long id);
        #endregion
    }
}
