using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCommon.Storage
{
    public class Attachment:Entity
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string FilePath { get; set; }
        public Guid CommentId { get; set; }

        public Comment Comment { get; set; }
    }
}
