using System;

namespace Main.Models
{
    public class ErrorViewModel
    {
        //Changed comment
        //Changed 1
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}