using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotaBlog.Core.Commands
{
    public class CommandValidationResult
    {
        public IReadOnlyCollection<string> Errors { get; set; }
        public bool Success => (Errors == null || Errors.Count == 0);
    }
}
