using Microsoft.EntityFrameworkCore;
using PetCommon.Storage;

namespace PetCommon.Repositories.Interfaces
{
    public class TaskRepository : RepositoryBase<Goal>, ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context):base(context) 
        {
            _context = context;
        }      
    }
}
