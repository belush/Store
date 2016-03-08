using System.Collections.Generic;
using Store.BLL.DTO;

//using Store.DAL.Entities;

namespace Store.BLL.Interfaces
{
    public interface IGoodLogic
    {
        IEnumerable<GoodDTO> GetAll();
        GoodDTO Get(int? id);
        void Add(GoodDTO good);
        void Delete(int id);
        void Edit(GoodDTO good);
        IEnumerable<GoodDTO> Search(string search, FilterModelDTO filter);
    }
}