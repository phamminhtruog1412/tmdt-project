using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class MenuDAO
    {
        private Dbcontext db = null;
        public MenuDAO()
        {
            db = new Dbcontext();
        }

        public List<Menu> GetListByID(int typeID)
        {
            return db.Menus.Where(x => x.MenuTypeID == typeID).ToList();
        }
    }
}
