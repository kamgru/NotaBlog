using System.Collections.Generic;

namespace NotaBlog.Api.Dto
{
    public class Result
    {
        public bool Success { get; set; }
        public IReadOnlyCollection<string> Errors { get; set; }

        public string ErrorMessage
        {
            get
            {
                if (Errors != null && Errors.Count > 0)
                {
                    return string.Join("\r\n", Errors);
                }
                return string.Empty;
            }
        }
    }
}
