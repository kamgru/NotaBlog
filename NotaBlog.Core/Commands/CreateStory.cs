﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NotaBlog.Core.Commands
{
    public class CreateStory : ICommand
    {
        public Guid EntityId { get; set; }
    }
}
