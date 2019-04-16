using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalkAspNetCoreDemo.Models.ViewModels
{
    public class ResultMessage
    {
        public ResultType Type { get; set; }

        public string Message { get; set; }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(Message);
        }
    }

    public enum ResultType
    {
        Info = 1,
        Warning = 2,
        Error = 3,
        Success = 4
    }
}
