using System.Collections.Generic;
using Store.BLL.DTO;

//using Store.DAL.Entities;

namespace Store.BLL.Interfaces
{
    public interface IColorLogic
    {
        IEnumerable<ColorDTO> GetAll();
        ColorDTO Get(int? id);
        void Add(ColorDTO colorDto);
        void Delete(int id);
        void Edit(ColorDTO colorDto);
    }
}