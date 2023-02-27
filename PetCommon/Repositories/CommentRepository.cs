using Microsoft.EntityFrameworkCore;
using Pet.Common.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pet.Common.Repositories.Interfaces
{
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context): base(context) 
        {
            _context = context;
        }       
        public async Task AddAttachmentAsync(Guid commentId, Attachment attachment)
        {
            var comment = await GetFirstWhereAsync(c => c.Id == commentId);
            if (comment == null)
            {
                throw new ArgumentException("Comment not found.");
            }

            if (comment.Attachments == null)
            {
                comment.Attachments = new List<Attachment>();
            }

            comment.Attachments.Add(attachment);

            _context.Update(comment);
            await _context.SaveChangesAsync();
        }
    }
}
