using System;

namespace API.InputDto
{
    public class UserErrorForCreationDto
    {
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string Notes { get; set; }
    }
}
