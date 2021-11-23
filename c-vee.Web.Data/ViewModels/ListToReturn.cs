using System;
using System.Collections.Generic;
using System.Text;

namespace c_vee.Web.Data.ViewModels
{
    public class ListToReturn
    {
        public PagedListOfUser Data { get; set; } = new PagedListOfUser();

        public IEnumerable<string> Info { get; set; } = new List<string>();
    }
}
