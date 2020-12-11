using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MineSweeper.Models;
using MineSweeper.PTL.MineSweeperLogic;
using MineSweeper.ServiceProviders;


namespace MineSweeper.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MineSweeperAPIController : ControllerBase
    {

        private IUnitOfWorkServiceProvider _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<MineSweeperAPIController> _logger;


        public MineSweeperAPIController(IUnitOfWorkServiceProvider unitOfWork, IMapper mapper, ILogger<MineSweeperAPIController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper     = mapper;
            _logger     = logger;
        }

        // GET: api/MineSweeperAPI
        [HttpGet]
        public IEnumerable<MineSweeper.Lib.PTL.MineSweeper> Get()
        {
            return _unitOfWork.GetUnitOfWork().MineSweeper.SP_GetAllMineInput();
        }

        // GET: api/MineSweeperAPI/5
        [HttpGet("{id}", Name = "Get")]
        public IEnumerable<MineSweeper.Lib.PTL.MineSweeper> Get(long id)
        {
            return _unitOfWork.GetUnitOfWork().MineSweeper.SP_GetMineInput(id);
        }

        // POST: api/MineSweeperAPI
        [HttpPost]
        public ActionResult<MineSweeper.Lib.PTL.MineSweeper> Post([FromBody] MineSweeperDTO entityDTO)
        {
             MineSweeper.Lib.PTL.MineSweeper entity = _mapper.Map<MineSweeper.Lib.PTL.MineSweeper>(entityDTO);
             
             //Add Required Info to Model
             entity.AddedDate    = DateTime.Now;
             entity.ModifiedDate = DateTime.Now;
             //get client IP address
             entity.IPAddress    = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            
             _unitOfWork.GetUnitOfWork().MineSweeper.Insert(entity);

            //commit changes
            if (_unitOfWork.GetUnitOfWork().Complete() == 1)
            {
                return entity;
            }
            else {
                return Result(StatusCodes.Status422UnprocessableEntity, "MineSweeper input record can not be created, Please check log.");
            }
            
        }

        // PUT: api/MineSweeperAPI
        [HttpPut]
        public ActionResult<MineSweeper.Lib.PTL.MineSweeper> Put([FromBody] MineSweeperDTO entityDTO)
        {
            MineSweeper.Lib.PTL.MineSweeper entity = _mapper.Map<MineSweeper.Lib.PTL.MineSweeper>(entityDTO);

            //Add Required Info to Model
            entity.ModifiedDate = DateTime.Now;
            //get client IP address
            entity.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();

            _unitOfWork.GetUnitOfWork().MineSweeper.Update(entity);

            //commit changes
            if (_unitOfWork.GetUnitOfWork().Complete() == 1)
            {
                return Result(StatusCodes.Status200OK, "MineSweeper input record updated.");
            }
            else
            {
                return Result(StatusCodes.Status400BadRequest, "MineSweeper input record can not be updated, Please check log.");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult<MineSweeper.Lib.PTL.MineSweeper> Delete(int id)
        {
            MineSweeper.Lib.PTL.MineSweeper entity = new Lib.PTL.MineSweeper();
            entity.Id = id;

            _unitOfWork.GetUnitOfWork().MineSweeper.Delete(entity);

            //commit changes
            if (_unitOfWork.GetUnitOfWork().Complete() == 1)
            {
                return Result(StatusCodes.Status204NoContent, "MineSweeper input record deleted.");
            }
            else
            {
                return Result(StatusCodes.Status400BadRequest, "MineSweeper input record can not be deleted, Please check log.");
            }
        }


        #region Named APIs
        // GET: /minesweeper_inputcount
        [HttpGet("/minesweeper_inputcount")]
        public ActionResult<int> MineSweeperInputCount()
        {
            return _unitOfWork.GetUnitOfWork().DBFunctionsResult.FN_MineSweeper_InputCount();
        }

        #endregion

        #region Helper Methods
        private static ActionResult Result(int statusCode, string reason) => new ContentResult
        {
            StatusCode = statusCode,
            Content = $"Status Code: {statusCode}; {statusCode}; {reason}",
            ContentType = "text/plain",
        };

        #endregion
    }
}
