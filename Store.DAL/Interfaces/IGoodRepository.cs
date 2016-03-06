using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.DAL.Entities;

namespace Store.DAL.Interfaces
{
    public interface IGoodRepository : IRepository<Good>
    {
        IEnumerable<Good> Search(string search, FilterModel filter);
    }
}
