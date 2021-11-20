using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Model.DAO
{
    public class ProductCategoryDAO
    {
        private Dbcontext db = null;
        public ProductCategoryDAO()
        {
            db = new Dbcontext();
        }

        public List<ProductCategory> GetList(string searchString)
        {
            IQueryable<ProductCategory> productcategorys = db.ProductCategories;
            if (searchString != null)
            {
                productcategorys = productcategorys.Where(x => x.CategoryName.Contains(searchString));
            }
            return productcategorys.OrderBy(x=>x.CategoryID).ToList();
        }

        public bool Create(ProductCategory pc)
        {
            try
            {
                db.ProductCategories.Add(pc);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ProductCategory GetByID(long id)
        {
            return db.ProductCategories.Find(id);
        }
        public bool Edit(ProductCategory pc)
        {
            try
            {
                var newPC = db.ProductCategories.Find(pc.CategoryID);
                newPC.CategoryName = pc.CategoryName;
                newPC.MetaTitle = pc.MetaTitle;
                newPC.CreatedDate = pc.CreatedDate;
                newPC.Status = pc.Status;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsForeignKey(long id) {
            var res = db.Products.Count(x => x.CategoryID == id);
            if (res > 0)
            {
                return true;
            }
            else {
                return false;
            }
        }

        public bool Delete(long id)
        {
            try
            {
                var pc = db.ProductCategories.Find(id);
                db.ProductCategories.Remove(pc);
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
