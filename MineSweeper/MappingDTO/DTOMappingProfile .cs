using AutoMapper;
using MineSweeper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineSweeper.MappingDTO
{
    public class DTOMappingProfile : Profile
    {
        public DTOMappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<MineSweeperDTO, MineSweeper.Lib.PTL.MineSweeper>();
            CreateMap<MineSweeper.Lib.PTL.MineSweeper, MineSweeperDTO>();
        }
    }
}
