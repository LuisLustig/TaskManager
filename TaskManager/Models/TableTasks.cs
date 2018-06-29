using System;
using System.Collections.Generic;

namespace TaskManager.Models
{
    public partial class TableTasks
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }
    }
}
