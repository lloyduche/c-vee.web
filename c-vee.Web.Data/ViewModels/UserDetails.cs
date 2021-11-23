using System;
using System.Collections.Generic;
using System.Text;

namespace c_vee.Web.Data.ViewModels
{
    public class UserDetails
    {
        public string id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string jobTitle { get; set; }
        public string photoUrl { get; set; }

        public string PublicId { get; set; }
        public string email { get; set; }
        public string Phone { get; set; }
        public IEnumerable<projectVm> Projects { get; set; }
        public IEnumerable<ExperienceVm> Experience { get; set; }
        public IEnumerable<EducationVm> Education { get; set; }
        public SocialVm social { get; set; }
    }
}
