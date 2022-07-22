using AutoMapper;
using offler_backend_api.Models.TalendConfig;
using offler_db_context.Context;
using offler_db_context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace offler_backend_api.Services
{
    public interface ITalendConfigService
    {
        ReadTalendConfigDto CreateTalendConfig(CreateTalendConfigDto dto);
        ICollection<ReadTalendConfigDto> ReadAllTalendConfig();
        ReadTalendConfigDto ReadByCodeTalendConfig(string scriptCode);
        ReadTalendConfigDto UpdateTalendConfig(UpdateTalendConfigDto dto, int id);
        bool DeleteTalendConfig(DeleteTalendConfigDto dto);
    }
    public class TalendConfigService : ITalendConfigService
    {
        private readonly OfflerDbContext _context;
        private readonly IMapper _mapper;
        public TalendConfigService(OfflerDbContext context
            , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public ReadTalendConfigDto CreateTalendConfig(CreateTalendConfigDto dto)
        {
            var check = _context.TalendConfig.Where(x => x.ScriptCode == dto.ScriptCode).Count();
            if (check > 0)
                throw new Exception($"CodeName must be unique");

            var newConfig = _mapper.Map<TalendConfig>(dto);

            _context.TalendConfig.Add(newConfig);
            _context.SaveChanges();

            var entity = _context.TalendConfig.Where(x => x.ScriptCode == dto.ScriptCode).FirstOrDefault();

            return _mapper.Map<ReadTalendConfigDto>(entity);
        }

        public bool DeleteTalendConfig(DeleteTalendConfigDto dto)
        {
            var ent = _context.TalendConfig.Where(x => x.ScriptCode == dto.ScriptCode).FirstOrDefault();
            if (ent == null)
            {
                return false;
                throw new Exception($"Talend configuration with code: {dto.ScriptCode} not found.");
            }
            else
            {
                _context.TalendConfig.Remove(ent);
                _context.SaveChanges();
                return true;
            }    

        }

        public ICollection<ReadTalendConfigDto> ReadAllTalendConfig()
        {
            var ent = _context.TalendConfig.OrderBy(x => x.ScriptName).ToList();

            return _mapper.Map<ICollection<ReadTalendConfigDto>>(ent);
        }

        public ReadTalendConfigDto ReadByCodeTalendConfig(string scriptCode)
        {
            var ent = _context.TalendConfig.OrderBy(x => x.ScriptName).Where(y => y.ScriptCode == scriptCode).FirstOrDefault();

            return _mapper.Map<ReadTalendConfigDto>(ent);
        }

        public ReadTalendConfigDto UpdateTalendConfig(UpdateTalendConfigDto dto, int id)
        {
            var ent = _context.TalendConfig.Where(x => x.ID == id).FirstOrDefault();
            if (ent == null)
            {
                   throw new Exception($"Talend configuration with code: {dto.ScriptCode} not found.");
            }
            else
            {
                ent.ScriptName = dto.ScriptName;
                ent.ScriptPath = dto.ScriptPath;

                _context.TalendConfig.Update(ent);
                _context.SaveChanges();
                return _mapper.Map<ReadTalendConfigDto>(ent);
            }
        }
    }
}
