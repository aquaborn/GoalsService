using PetCommon.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCommon.Repositories.Interfaces
{
    public interface ICommentRepository: IRepositoryBase<Comment>
    {
        Task AddAttachmentAsync(Guid commentId, Attachment attachment);
    }
}
