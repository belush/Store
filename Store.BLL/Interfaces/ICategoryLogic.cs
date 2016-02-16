using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.DAL.Entities;

namespace Store.BLL.Interfaces
{
    public interface ICategoryLogic
    {
        IEnumerable<Category> GetAll();
        Category Get(int? id);
        IEnumerable<Category> Find(Func<Category, bool> predicate);
        void Add(Category category);
        void Delete(int id);
        void Edit(Category category);
    }
}
