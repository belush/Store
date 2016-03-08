using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.DAL.Entities;
using Store.DAL.Repositories;

namespace Store.DAL.Interfaces
{
    public interface IClientRepository : IRepository<ClientProfile> 
    {
        ClientProfile Get(string id);
    }
}
