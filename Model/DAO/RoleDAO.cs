using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Model.DAO
{
    public class RoleDAO
    {
        private Dbcontext db = null;
        public RoleDAO()
        {
            db = new Dbcontext();
        }

        public IEnumerable<Role> GetListAll(string searchString)
        {
            IQueryable<Role> roles = db.Roles;
            if (searchString != null)
            {
                roles = roles.Where(x=>x.RoleName.Contains(searchString));
            }
            return roles.OrderBy(x=>x.RoleID).ToList();
        }

        public bool Create(Role role)
        {
            db.Roles.Add(role);
            db.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var role = db.Roles.Find(id);
            db.Roles.Remove(role);
            db.SaveChanges();
            return true;
        }

        public Role GetByID(int id)
        {
            return db.Roles.Find(id);
        }
        public bool Edit(Role role)
        {
            try
            {
                var newRole = GetByID(role.RoleID);
                newRole.RoleName = role.RoleName;
                db.SaveChanges();
                return true;
            }
            catch 
            {
                return false;
            }


        }
    }
}
