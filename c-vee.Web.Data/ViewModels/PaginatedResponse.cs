using System;
using System.Collections.Generic;
using System.Text;

namespace c_vee.Web.Data.ViewModels
{
    public class PaginatedResponse <T>
    {
        public PageMetaData PageMetaData { get; set; }
        public IEnumerable<T> ResponseData { get; set; } = new List<T>();

    }
}
