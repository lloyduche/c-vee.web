using System;
using System.Collections.Generic;
using System.Text;

namespace c_vee.Web.Data.ViewModels
{
    public class GlobalResponse<T>
    {
        public string Message { get; set; }
        public Dictionary<string, string> Errs { get; set; }
        public T Data { get; set; }
    }
}
