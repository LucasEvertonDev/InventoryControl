using InventoryControl.Application.Interfaces;
using InventoryControl.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Application.Services
{
    public class Service : IService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void LogicalException(string message)
        {
            throw new LogicalException(message);
        }
    }
}
