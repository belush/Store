using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.BLL.DTO;
using Store.DAL.Entities;

namespace Store.BLL.Interfaces
{
    public interface ICategoryLogic
    {
        IEnumerable<CategoryDTO> GetAll();
        CategoryDTO Get(int? id);
        void Add(CategoryDTO category);
        void Delete(int id);
        void Edit(CategoryDTO category);
    }
}
