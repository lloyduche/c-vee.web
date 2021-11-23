using System;
using System.Collections.Generic;
using System.Text;

namespace c_vee.Web.Data.ViewModels
{
    public class ExperienceVm
    {
        public string Id { get; set; }
        public string jobTitle { get; set; }
        public string Description { get; set; }
        public string Employer { get; set; }
        public string Location { get; set; }
        public string yearStarted { get; set; }
        public string yearEnded { get; set; }
    }
}
