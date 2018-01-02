using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotaBlog.Admin.Models
{
    public class UpdateStatusModel
    {
        public StoryStatus StoryStatus { get; set; }
    }

    public enum StoryStatus
    {
        Unpublished = 0,
        Published = 1 
    }
}
