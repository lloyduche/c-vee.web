using System;
using System.Collections.Generic;
using System.Text;

namespace c_vee.Web.Data.ViewModels
{
    public class PagedListOfUser
    {
        public PageMetaData PageMetaData { get; set; } = new PageMetaData();

        public IEnumerable<ListOfUsers> ResponseData { get; set; } = new List<ListOfUsers>();
    }
}
