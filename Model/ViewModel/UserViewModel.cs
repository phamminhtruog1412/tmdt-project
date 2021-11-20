using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class UserViewModel
    {
        public long UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? RoleID { get; set; }
        public string RoleName { get; set; }
        public bool Status { get; set; }
    }
}
