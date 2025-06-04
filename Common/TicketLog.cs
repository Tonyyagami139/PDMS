using System;

namespace Common
{
    public class TicketLog
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string ModifyUserName { get; set; }
        public DateTime ModifyTime { get; set; }
    }
}
