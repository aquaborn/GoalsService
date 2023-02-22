using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCommon.Storage
{
    public class Project:Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }

        public ICollection<Goal> Goals { get; set; }
    }
}
