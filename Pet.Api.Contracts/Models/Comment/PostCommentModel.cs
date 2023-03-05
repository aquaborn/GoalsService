using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pet.Api.Contracts.Models.Comment
{
    public class PostCommentModel : CommentModel
    {
        public Guid Id { get; set; }

        public Guid TaskId { get; set; }
    }
}
