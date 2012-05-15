using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreepCollector.Models
{
    public class Creep
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public int? Level { get; set; }
        public string CreatorName { get; set; }
        public string CreatorEmail { get; set; }
        public string ConfirmCreatorEmail { get; set; }
        public Attack Weapon { get; set; }

        public Creep()
        {
            Weapon = new Attack();
        }
    }
}