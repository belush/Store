using System.Collections.Generic;
using Store.DAL.Entities;

namespace Store.BLL.Interfaces
{
    public interface IStatusLogic
    {
        IEnumerable<Status> GetAll();
        void Add(Status status);
        Status Get(int? id);
        void Edit(Status status);
        void Delete(int id);
        //IEnumerable<Status> Find(Func<Status, bool> predicate);
    }
}