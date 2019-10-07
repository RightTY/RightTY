using System;

namespace DotnetCore.Models
{
#pragma warning disable 1591
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
#pragma warning restore 1591
