using Microsoft.EntityFrameworkCore;
using Pet.Common.Storage;

namespace Pet.Common.Repositories.Interfaces
{
    public class AttachmentRepository : RepositoryBase<Attachment>, IAttachmentRepository
    {
        private readonly AppDbContext _context;

        public AttachmentRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }      
    }
}
