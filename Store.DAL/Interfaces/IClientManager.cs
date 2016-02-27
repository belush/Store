using System;
using Store.DAL.Entities;

namespace Store.DAL.Interfaces
{
    public interface IClientManager : IDisposable
    {
        void Create(ClientProfile item);
    }
}