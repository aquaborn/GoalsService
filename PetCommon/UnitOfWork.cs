using PetCommon.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCommon
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private ITaskRepository _taskRepository;
        private ICommentRepository _commentRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ITaskRepository TaskRepository
        {
            get
            {
                if (_taskRepository == null)
                {
                    _taskRepository = new TaskRepository(_context);
                }
                return _taskRepository;
            }
        }

        public ICommentRepository CommentRepository
        {
            get
            {
                if (_commentRepository == null)
                {
                    _commentRepository = new CommentRepository(_context);
                }
                return _commentRepository;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }


}
