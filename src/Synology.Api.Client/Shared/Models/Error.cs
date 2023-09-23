using System.Collections.Generic;

namespace Synology.Api.Client.Shared.Models
{
    public class Error
    {
        public int Code { get; set; }

        public IEnumerable<Errors>? Errors { get; set; }
    }
}
