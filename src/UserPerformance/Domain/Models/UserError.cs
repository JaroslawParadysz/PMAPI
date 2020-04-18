using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class UserError
    {
        public Guid UserErrorId { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Notes { get; set; }
    }
}
