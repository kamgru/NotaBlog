using System;
using System.Collections.Generic;
using System.Text;

namespace NotaBlog.Core.Commands
{
    public class UpdateBlogInfo : ICommand
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
