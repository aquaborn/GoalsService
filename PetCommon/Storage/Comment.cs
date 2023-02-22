using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PetCommon.Storage
{
    /// <summary>
    /// Комментарий пользователя
    /// </summary>
    public class Comment:Entity
    {
        public string Text { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid UserId { get; set; }
        public Guid TaskId { get; set; }

        public User User { get; set; }
        public Goal Task { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
    }

}
