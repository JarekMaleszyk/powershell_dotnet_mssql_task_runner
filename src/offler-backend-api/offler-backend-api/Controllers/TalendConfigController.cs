using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using offler_backend_api.Models.TalendConfig;
using offler_backend_api.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace offler_backend_api.Controllers
{
    [Route("api/v1")]
    [ApiExplorerSettings(GroupName = "module2")]
    [ApiController]
    public class TalendConfigController : Controller
    {
        private readonly ITalendConfigService _service;
        public TalendConfigController(ITalendConfigService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [Route("talendconfig")]
        public ActionResult<ReadTalendConfigDto> GetOneTalendConfigDto([FromQuery] string code)
        {
            var TalendConfig = _service.ReadByCodeTalendConfig(code);

            return Ok(TalendConfig);
        }
        [HttpGet]
        [Route("talendconfigs")]
        public ActionResult<ICollection<ReadTalendConfigDto>> GetAllTalendConfigDto()
        {
            var TalendConfigCollection = _service.ReadAllTalendConfig();

            return Ok(TalendConfigCollection);
        }
        [HttpPost]
        [Route("talendconfig")]
        public ActionResult<ReadTalendConfigDto> CreateTalendConfig([FromBody] CreateTalendConfigDto dto)
        {
            var ent = _service.CreateTalendConfig(dto);

            return Ok(ent);
        }
        [HttpPut]
        [Route("talendconfig/{id}")]
        public ActionResult<ReadTalendConfigDto> UpdateTalendConfig([FromRoute] int id, [FromBody] UpdateTalendConfigDto dto)
        {
            var result = _service.UpdateTalendConfig(dto, id);

            return Ok(result);
        }
        [HttpDelete]
        [Route("talendconfig")]
        public ActionResult<bool> RemoveTalendConfig([FromBody] DeleteTalendConfigDto dto)
        {
            var result = _service.DeleteTalendConfig(dto);

            return Ok(result);
        }
    }
}
