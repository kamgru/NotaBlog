﻿using System;

namespace NotaBlog.Api.ViewModels
{
    public class StoryLeadViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string LeadContent { get; set; }
        public DateTime Published { get; set; }
        public string SeName { get; set; }
    }
}
