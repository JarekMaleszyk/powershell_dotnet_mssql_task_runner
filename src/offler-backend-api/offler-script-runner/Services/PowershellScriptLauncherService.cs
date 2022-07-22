using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using offler_script_runner.Models;

namespace offler_script_runner.Services
{
    public interface IPowershellScriptLauncherService
    {
        PowershellScriptResultDto StartPowershellScript(string path, string script);
    }
    public class PowershellScriptLauncherService : IPowershellScriptLauncherService
    {
        private readonly ILogger<PowershellScriptLauncherService> _logger;
        public PowershellScriptLauncherService(ILogger<PowershellScriptLauncherService> logger)
        {
            _logger = logger;
        }
         public PowershellScriptResultDto StartPowershellScript(string path, string script)
        {
            var powershellScriptResult = new PowershellScriptResultDto();

            try
            {
                using (PowerShell ps = PowerShell.Create())
                {
                    ps.AddCommand("Set-ExecutionPolicy")
                      .AddParameter("Scope", "Process")
                      .AddParameter("ExecutionPolicy", "Bypass")
                      .Invoke();

                    _logger.LogDebug("Powershell Initialized....");

                    var combined = Path.Combine(path, script);

                    var results = ps.AddCommand(combined)
                        .Invoke();

                    foreach (var result in results)
                    {
                        _logger.LogInformation($"PowershellScriptLauncher result {result}");
                        powershellScriptResult.Result = result.ToString();
                        powershellScriptResult.ExitCode = 1;
                        powershellScriptResult.FinishTime = DateTime.Now;
                    }
                    // Also report non-terminating errors, if any.
                    foreach (var error in ps.Streams.Error)
                    {
                        _logger.LogInformation($"PowershellScriptLauncher error {error}");
                        powershellScriptResult.Error = error.ToString();
                        powershellScriptResult.ExitCode = -1;
                        powershellScriptResult.FinishTime = DateTime.Now;
                    }
                }

                return powershellScriptResult;
            }
            catch (Exception ex)
            {
                powershellScriptResult.Error = "Startup error" + ex.Message;
                powershellScriptResult.ExitCode = -1;
                powershellScriptResult.FinishTime = DateTime.Now;
                return powershellScriptResult;
            }
        }
    }
}
