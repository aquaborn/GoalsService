using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pet.Api.Contracts.Models.Comment
{
    public class CommentModel
    {
        public string Text { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid UserId { get; set; }
    }
}
