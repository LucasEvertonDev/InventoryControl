using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Domain.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        IEnumerable<T> FindAll();
        T? FindById(int? id);
        T Insert(T domain);
        T Update(T domain);
        T Delete(T domain);
    }
}
