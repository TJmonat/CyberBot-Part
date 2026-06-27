using System;
using System.Collections.Generic;
using System.Text;

namespace POE2
{
   public class Task
    {//we are using this class to get the taskid, title, details, reminddate and status of the tasks
        public int TaskID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Reminder { get; set; }
        public string Status { get; set; }
    }
}
