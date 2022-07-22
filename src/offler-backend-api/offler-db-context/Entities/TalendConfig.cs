using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offler_db_context.Entities
{
    public class TalendConfig
    {   
        [Key]
        public int ID { get; set; }
        public string ScriptCode { get; set; }
        public string ScriptPath { get; set; }
        public string ScriptName { get; set; }
    }
}
