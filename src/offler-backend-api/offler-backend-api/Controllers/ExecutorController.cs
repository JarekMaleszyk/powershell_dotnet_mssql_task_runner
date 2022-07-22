using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using offler_backend_api.Models.Common;
using offler_backend_api.Services;
using offler_script_runner.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace offler_backend_api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ExecutorController : ControllerBase
    {
        private readonly IAttachmentFileService _fileService;
        private readonly ITalendScriptExecutorService _scriptService;
        public ExecutorController(IAttachmentFileService fileService
            , ITalendScriptExecutorService scriptService)
        {
            _fileService = fileService;
            _scriptService = scriptService;
        }
        [ApiExplorerSettings(GroupName = "module1")]
        [HttpPost("offler1")]
        public ActionResult<PowershellScriptResultDto> UploadFile1([Required] IFormFile formFiles, [FromQuery] ExecutionRequestDto code)
        {
            _fileService.UploadFileByCode(formFiles, code.ScriptCode);

            var result = _scriptService.LaunchScriptRequestByCode(code.ScriptCode);

            return Ok(result);
        }
    }
}
