using Microsoft.EntityFrameworkCore;
using Pet.Common.Storage;

namespace Pet.Common.Repositories.Interfaces
{
    public class ProjectRepository : RepositoryBase<Goal>, ITaskRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context):base(context) 
        {
            _context = context;
        }      
    }
}
