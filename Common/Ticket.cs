using System;

namespace Common
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string Assignee { get; set; }
        public string Attachments { get; set; }
        public string CreateUserName { get; set; }
        public DateTime CreateTime { get; set; }
        public string ModifyUserName { get; set; }
        public DateTime ModifyTime { get; set; }
    }
}
