using System.Collections.Generic;

namespace NotaBlog.Api.Dto
{
    public class Result
    {
        public bool Success { get; set; }
        public IReadOnlyCollection<string> Errors { get; set; }
    }
}
