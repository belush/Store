using System;
using System.Collections.Generic;
using Store.BLL.DTO;
//using Store.DAL.Entities;

namespace Store.BLL.Interfaces
{
    public interface IClientLogic
    {


        IEnumerable<UserDTO> GetAll();
        UserDTO Get(string id);
        void Add(UserDTO user);
        void Delete(int id);
        void Edit(UserDTO user);
    }
}