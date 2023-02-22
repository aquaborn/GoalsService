using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCommon.Storage
{
    public class Goal:Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public Guid ProjectId { get; set; }
        public Guid? ParentTaskId { get; set; }

        public Project Project { get; set; }
        public ICollection<Goal> SubTasks { get; set; }
        public Goal ParentTask { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
