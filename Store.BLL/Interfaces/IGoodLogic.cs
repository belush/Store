using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.DAL.Entities;

namespace Store.BLL.Interfaces
{
    public interface IGoodLogic
    {
        IEnumerable<Good> GetAll();
        Good Get(int? id);
        IEnumerable<Good> Find(Func<Good, bool> predicate);
        void Add(Good good);
        void Delete(int id);
        void Edit(Good good);
    }
}
