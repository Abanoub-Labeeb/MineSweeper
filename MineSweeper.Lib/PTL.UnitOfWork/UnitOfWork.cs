using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace MineSweeper.Lib.PTL
{
    public class UnitOfWork : IUnitOfWork
    {

        public IMineSweeperRepository MineSweeper { get; private set; }
        public IDBFunctionsResultRepository DBFunctionsResult { get; private set; }

        protected readonly ApplicationContext _context;
        private readonly ILogger<UnitOfWork> _logger;

        public UnitOfWork(ApplicationContext context, ILogger<UnitOfWork> logger)
        {
            MineSweeper       = new MineSweeperRepository(context);
            DBFunctionsResult = new DBFunctionsResultRepository(context);
            _context    = context;
            _logger     = logger;
        }

        
        public int Complete()
        {
            int result = 0;

            try {
                _context.SaveChanges();
                result = 1;
            } catch(Microsoft.EntityFrameworkCore.DbUpdateException ex) {
                _logger.LogError(Messages.COMMIT_UOM_ERROR + "\n" + ex.StackTrace, ex);
                _logger.LogError(Messages.COMMIT_UOM_ERROR + "\n" + ex.InnerException, ex);
                result = 0;
            } catch (Exception ex)
            {
                _logger.LogError(Messages.COMMIT_UOM_ERROR + "\n" + "Stack Trace" + "\n" + ex.StackTrace, ex);
                _logger.LogError(Messages.COMMIT_UOM_ERROR + "\n" + "Stack Trace" + "\n" + ex.InnerException, ex);
                result = 0;
            }

            return result; 
        }

        public void Dispose()
        {
            try
            {
                _context.Dispose();
            }
            catch (Exception ex)
            {
                _logger.LogError(Messages.DISPOSE_UOM_ERROR + "\n" + ex.StackTrace, ex);
            }
            
        }
    }
}
