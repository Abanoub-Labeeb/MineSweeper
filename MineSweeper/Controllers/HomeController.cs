using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MineSweeper.Models;
using MineSweeper.PTL.MineSweeperLogic;
using MineSweeper.ServiceProviders;

namespace MineSweeper.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWorkServiceProvider _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IUnitOfWorkServiceProvider unitOfWork, IMapper mapper, ILogger<HomeController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper     = mapper;
            _logger     = logger;
        }

        public IActionResult Index()
        {
            List<MineSweeperDTO> mineSweeperDTOs = new List<MineSweeperDTO>();

            IEnumerable<MineSweeper.Lib.PTL.MineSweeper> mineSweeperInputList = _unitOfWork.GetUnitOfWork().MineSweeper.SP_GetAllMineInput();
           
            foreach (var mineSweeper in mineSweeperInputList)
            {
                MineSweeperDTO mineSweeperDTO = _mapper.Map<MineSweeperDTO>(mineSweeper);
                mineSweeperDTO.Output         = MineSweeperBL.ParseMineSweeperInput(mineSweeper.Input);
                mineSweeperDTOs.Add(mineSweeperDTO);
            }
            
            return View(mineSweeperDTOs);
        }

        
        public IActionResult MineSweeperInputList()
        {
            return View();
        }

        public IActionResult MineSweeperInputCreate()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
