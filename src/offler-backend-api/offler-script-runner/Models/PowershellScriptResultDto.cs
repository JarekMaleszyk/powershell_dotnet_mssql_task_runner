using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offler_script_runner.Models
{
    public class PowershellScriptResultDto
    {
        public PowershellScriptResultDto(string result = "default", int exitCode = 0, string error = "none")
        {
            Result = result;
            ExitCode = exitCode;
            Error = error;
            FinishTime = DateTime.Now;
        }
        public string Result { get; set; }
        public int ExitCode { get; set; }
        public string Error { get; set; }
        public DateTime? FinishTime { get; set; }
    }
}
