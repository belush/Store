using System.Collections.Generic;
using Store.BLL.DTO;
using Store.DAL.Entities;

namespace Store.BLL.Interfaces
{
    public interface IStatusLogic
    {
        StatusDTO Get(int? id);
        void Delete(int id);
        IEnumerable<StatusDTO> GetAll();
        void Edit(StatusDTO statusDto);
        void Add(StatusDTO statusDto);
    }
}