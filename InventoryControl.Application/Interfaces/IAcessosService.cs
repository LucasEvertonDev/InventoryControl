using InventoryControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Application.Interfaces
{
    public interface IAcessosService
    { 
        Task<Acesso> FindById(int id);
        Task<Acesso> FindByName(string name);
    }
}
