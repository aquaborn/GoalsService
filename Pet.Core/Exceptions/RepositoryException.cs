using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pet.Core.Exceptions
{
    public  class RepositoryException : Exception
    {
        public string EntityName { get; }

        public RepositoryException(string message, Exception inner, string entityName)
            : base(message, inner)
        {
            EntityName = entityName;
        }
    }
}
