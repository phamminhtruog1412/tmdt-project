using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Model.ViewModel;

namespace Model.DAO
{
    public class UserDAO
    {
        private Dbcontext db = null;
        public UserDAO()
        {
            db = new Dbcontext();
        }

        public bool IsExists(string userName)
        {
            var res = db.Users.SingleOrDefault(x => x.UserName == userName);
            if (res == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public IEnumerable<UserViewModel> ListUserViewModel(string searchString)
        {
            var model = from a in db.Users
                        join b in db.Roles
                        on a.RoleID equals b.RoleID
                        select new UserViewModel()
                        {
                            UserID = a.UserID,
                            UserName = a.UserName,
                            Password = a.Password,
                            Name = a.Name,
                            Phone = a.Phone,
                            Email = a.Email,
                            Address = a.Address,
                            CreatedDate = a.CreatedDate,
                            RoleID = a.RoleID,
                            RoleName = b.RoleName,
                            Status = a.Status
                        };
            var searchModel = model;
            if (searchString != null)
            {
                searchModel = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString));
            }

            return searchModel.OrderByDescending(x => x.CreatedDate).ToList();
        }

        public bool Create(User model)
        {
            try
            {
                db.Users.Add(model);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public User GetByID(long id)
        {
            return db.Users.Find(id);
        }

        public User GetByUsername(string username)
        {
            return db.Users.SingleOrDefault(x => x.UserName == username);
        }

        public bool Edit(User user)
        {
            try
            {
                var entity = db.Users.Find(user.UserID);
                if (!string.IsNullOrEmpty(user.Password))
                {
                    entity.Password = user.Password;
                }
                entity.Name = user.Name;
                entity.Phone = user.Phone;
                entity.Address = user.Address;
                entity.Email = user.Email;
                entity.RoleID = user.RoleID;
                entity.Status = user.Status;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool Delete(long id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public int Login(string username, string password, int role)
        {
            var res = db.Users.SingleOrDefault(x => x.UserName == username && x.RoleID == role);
            if (res == null)
            {
                return 0;
            }
            else
            {
                if (res.Status == false)
                {
                    return 1;
                }
                else
                {
                    if (res.Password != password)
                    {
                        return 2;
                    }
                    else return 3;
                }

            }



        }

        public bool ChangePassword(string username, string oldPass, string newPass)
        {
            var user = GetByUsername(username);
            if (user.Password == oldPass)
            {
                user.Password = newPass;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public int Count()
        {
            return db.Users.Count();
        }
    }
}
