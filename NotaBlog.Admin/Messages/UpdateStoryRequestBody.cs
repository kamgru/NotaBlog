using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotaBlog.Admin.Messages
{
    public class UpdateStoryRequestBody
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
