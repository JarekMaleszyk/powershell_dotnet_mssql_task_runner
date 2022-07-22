using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace offler_backend_api.Models.TalendConfig
{
    public class ReadTalendConfigDto
    {
        public int ID { get; set; }
        public string ScriptCode { get; set; }
        public string ScriptPath { get; set; }
        public string ScriptName { get; set; }
    }
}
