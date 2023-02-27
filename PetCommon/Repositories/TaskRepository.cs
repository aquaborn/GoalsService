using Microsoft.EntityFrameworkCore;
using Pet.Common.Storage;

namespace Pet.Common.Repositories.Interfaces
{
    public class TaskRepository : RepositoryBase<Goal>, ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }      
    }
}
