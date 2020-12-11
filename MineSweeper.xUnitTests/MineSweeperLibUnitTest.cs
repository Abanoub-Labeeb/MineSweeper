using Microsoft.Extensions.Logging;
using MineSweeper.Lib.PTL;
using System;
using Xunit;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace MineSweeper.xUnitTests
{
    public class MineSweeperLibUnitTest
    {
        /*
         * MineSweeper Unit of work tests
        */
        [Fact]
        public void MineSweeper_Insert()
        {

            //arrange 
            ILoggerFactory loggerFactory = new LoggerFactory();
            ILogger<UnitOfWork> logger = loggerFactory.CreateLogger<UnitOfWork>();

            UnitOfWork _unitOfWork = new UnitOfWork(new ApplicationContext(), logger);
            MineSweeper.Lib.PTL.MineSweeper entity = new Lib.PTL.MineSweeper();

            entity.Input = "4 4\n*...\n....\n.*..\n....\n3 5\n**...\n.....\n.*...\n0 0";
            entity.AddedDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;
            entity.IPAddress = "::1";
            
            //act 
            _unitOfWork.MineSweeper.Insert(entity);
            int result = _unitOfWork.Complete();
            //assert
            Assert.Equal(1,result);
        }

        [Fact]
        public void MineSweeper_Update()
        {
            //arrange 
            ILoggerFactory loggerFactory = new LoggerFactory();
            ILogger<UnitOfWork> logger = loggerFactory.CreateLogger<UnitOfWork>();

            UnitOfWork _unitOfWork = new UnitOfWork(new ApplicationContext(), logger);
            MineSweeper.Lib.PTL.MineSweeper entity = _unitOfWork.MineSweeper.GetAll().FirstOrDefault();

            if (entity != null)
            {
                entity.RequestProcessTimes = 99;
                //act 
                _unitOfWork.MineSweeper.Update(entity);
                int result = _unitOfWork.Complete();
                //assert
                Assert.Equal(1, result);
            }
            else {
                // no data to update
                Assert.True(false);
            }

        }

        [Fact]
        public void MineSweeper_Delete()
        {
            //arrange 
            ILoggerFactory loggerFactory = new LoggerFactory();
            ILogger<UnitOfWork> logger = loggerFactory.CreateLogger<UnitOfWork>();

            UnitOfWork _unitOfWork = new UnitOfWork(new ApplicationContext(), logger);
            MineSweeper.Lib.PTL.MineSweeper entity = _unitOfWork.MineSweeper.GetAll().FirstOrDefault();

            if (entity != null)
            {
                //act 
                _unitOfWork.MineSweeper.Delete(entity);
                int result = _unitOfWork.Complete();
                //assert
                Assert.Equal(1, result);
            }
            else
            {
                // no data to update
                Assert.True(false, "no data to test on");
            }
        }

        /*
         * MineSweeper trigger tests
        */
        [Fact]
        public void MineSweeper_Trigger_ValidateInput()
        {
            //arrange 
            ILoggerFactory loggerFactory = new LoggerFactory();
            ILogger<UnitOfWork> logger = loggerFactory.CreateLogger<UnitOfWork>();

            UnitOfWork _unitOfWork = new UnitOfWork(new ApplicationContext(), logger);
            MineSweeper.Lib.PTL.MineSweeper entity = new Lib.PTL.MineSweeper();

            entity.Input = "4 4\n*...\n....\n.*..\n....\n3 5\n**...\n.....\n.*...\n";
            entity.AddedDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;
            entity.IPAddress = "::1";

            //act 
            _unitOfWork.MineSweeper.Insert(entity);
            int result = _unitOfWork.Complete();
            //assert
            Assert.Equal(0, result);
        }

        /*
         * Stored Procedure tests
        */
        [Fact]
        public void SP_MineSweeper_GetMineSweeperInput()
        {
            //arrange 
            ILoggerFactory loggerFactory = new LoggerFactory();
            ILogger<UnitOfWork> logger = loggerFactory.CreateLogger<UnitOfWork>();

            UnitOfWork _unitOfWork = new UnitOfWork(new ApplicationContext(), logger);
            MineSweeper.Lib.PTL.MineSweeper entity = _unitOfWork.MineSweeper.GetAll().FirstOrDefault();

            if (entity != null)
            {
                //act 
                MineSweeper.Lib.PTL.MineSweeper ms = _unitOfWork.MineSweeper.SP_GetMineInput(entity.Id).FirstOrDefault();
                
                //assert
                Assert.Equal(entity.Id, ms.Id);
            }
            else {
                //assert - no data to test on
                Assert.True(false, "no data to test on");
            }
            
        }

        [Fact]
        public void SP_GetAllMineInput()
        {
            //arrange 
            ILoggerFactory loggerFactory = new LoggerFactory();
            ILogger<UnitOfWork> logger = loggerFactory.CreateLogger<UnitOfWork>();

            UnitOfWork _unitOfWork = new UnitOfWork(new ApplicationContext(), logger);
            IEnumerable<MineSweeper.Lib.PTL.MineSweeper> entities = _unitOfWork.MineSweeper.GetAll();

            if (entities.Count() > 0)
            {
                //act 
                IEnumerable<MineSweeper.Lib.PTL.MineSweeper> ms = _unitOfWork.MineSweeper.SP_GetAllMineInput();

                //assert
                Assert.Equal(entities.Count(), ms.Count());
            }
            else
            {
                //assert - no data to test on
                Assert.True(false, "no data to test on");
            }
        }



        /*
         * Function tests
        */
        [Fact]
        public void FN_MineSweeper_InputCount()
        {
            //arrange 
            ILoggerFactory loggerFactory = new LoggerFactory();
            ILogger<UnitOfWork> logger = loggerFactory.CreateLogger<UnitOfWork>();

            UnitOfWork _unitOfWork = new UnitOfWork(new ApplicationContext(), logger);
            IEnumerable<MineSweeper.Lib.PTL.MineSweeper> entities = _unitOfWork.MineSweeper.GetAll();

            if (entities.Count() > 0)
            {
                //act 
                int msCount = _unitOfWork.DBFunctionsResult.FN_MineSweeper_InputCount();

                //assert
                Assert.Equal(entities.Count(), msCount);
            }
            else
            {
                //assert - no data to test on
                Assert.True(false, "no data to test on");
            }
        }
    }
}
