using PetCommon.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCommon
{
    public interface IUnitOfWork : IDisposable
    {
        ITaskRepository TaskRepository { get; }
        ICommentRepository CommentRepository { get; }
        Task<int> SaveChangesAsync();
    }

}
