﻿using NotaBlog.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotaBlog.Core.Entities
{
    public class Story
    {
        public Guid Guid { get; set; }
        public PublicationStatus PublicationStatus { get; set; }
        public DateTime Created { get; set; }
    }

    public enum PublicationStatus
    {
        Draft, Published, Withdrawn
    }
}
