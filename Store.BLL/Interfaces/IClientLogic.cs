using System;
using System.Collections.Generic;
using Store.DAL.Entities;

namespace Store.BLL.Interfaces
{
    public interface IClientLogic
    {
        IEnumerable<ClientProfile> GetAll();
        ClientProfile Get(string id);
        IEnumerable<ClientProfile> Find(Func<ClientProfile, bool> predicate);
        void Add(ClientProfile clientProfile);
        void Delete(int id);
        void Edit(ClientProfile clientProfile);
    }
}