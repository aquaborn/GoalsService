using Pet.Common.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pet.Common.Repositories.Interfaces
{
    public interface ICommentRepository: IRepositoryBase<Comment>
    {
        Task AddAttachmentAsync(Guid commentId, Attachment attachment);
    }
}
