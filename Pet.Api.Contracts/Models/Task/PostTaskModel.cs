using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pet.Api.Contracts.Models.Task
{
    public class PostTaskModel : TaskModel
    {
        public Guid TaskId { get; set; }
    }
}
