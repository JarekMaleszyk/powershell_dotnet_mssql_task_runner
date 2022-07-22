using AutoMapper;
using offler_backend_api.Models.TalendConfig;
using offler_db_context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace offler_backend_api.Configs
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateTalendConfigDto, TalendConfig>();
            CreateMap<UpdateTalendConfigDto, TalendConfig>();
            CreateMap<TalendConfig, ReadTalendConfigDto>();
        }
    }
}
