using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MineSweeper.Lib.PTL
{
    public class MineSweeperRepository : Repository<MineSweeper>, IMineSweeperRepository
    {
        #region Members
        public ApplicationContext _context => context;
        #endregion

        #region Ctor
        public MineSweeperRepository(ApplicationContext context) : base(context)
        {
        }

        #endregion

        #region  SQL Server Stored Procedures
        public IEnumerable<MineSweeper> SP_GetAllMineInput()
        {
            return entityDBSet.FromSqlRaw("EXECUTE dbo.SP_MineSweeper_GetMineSweeperInput").ToList();
            //return  _context.MineSweeper.FromSqlRaw("EXECUTE dbo.SP_MineSweeper_GetMineSweeperInput").ToList();
        }

        public IEnumerable<MineSweeper> SP_GetMineInput(long id)
        {
            return entityDBSet.FromSqlRaw($"EXECUTE dbo.SP_MineSweeper_GetMineSweeperInput {id}").ToList();
            //return _context.MineSweeper.FromSqlRaw($"EXECUTE dbo.SP_MineSweeper_GetMineSweeperInput {id}").ToList();
        }
        #endregion
    }
}
