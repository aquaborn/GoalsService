using Pet.Common.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pet.Api.Contracts.Models.Project
{
    public class ProjectModel : PostProjectModel
    {
        public DateTime CreatedOn { get; set; }
    }
}
