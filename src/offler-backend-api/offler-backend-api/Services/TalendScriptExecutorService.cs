using Microsoft.Extensions.Logging;
using offler_db_context.Context;
using offler_script_runner.Models;
using offler_script_runner.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace offler_backend_api.Services
{
    public interface ITalendScriptExecutorService
    {
        PowershellScriptResultDto LaunchScriptRequestByCode(string code);
    }
    public class TalendScriptExecutorService : ITalendScriptExecutorService
    {
        private readonly OfflerDbContext _context;
        private readonly IPowershellScriptLauncherService _launcher;
        private readonly ILogger<TalendScriptExecutorService> _logger;
        public TalendScriptExecutorService(OfflerDbContext context
            , IPowershellScriptLauncherService launcher
            , ILogger<TalendScriptExecutorService> logger)
        {
            _context = context;
            _launcher = launcher;
            _logger = logger;
        }
        public PowershellScriptResultDto LaunchScriptRequestByCode(string code)
        {
            var ent = _context.TalendConfig.FirstOrDefault(x => x.ScriptCode == code);

            if (ent == null)
            {
                return new PowershellScriptResultDto("", -1, "Script definition not found.");
            }
            else
            {
                return LaunchScript(ent.ScriptCode);
            }
        }
        private PowershellScriptResultDto LaunchScript(string code)
        {
            var ent = _context.TalendConfig.FirstOrDefault(x => x.ScriptCode == code);

            var result = _launcher.StartPowershellScript(ent.ScriptPath, ent.ScriptName);

            if (result.ExitCode != -1)
            {
                return result;
            }
            else
            {
                throw new System.Exception($"Script execution error: {result.Error}");
            }

        }
    }
}
