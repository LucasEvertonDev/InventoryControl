using InventoryControl.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Application.Utils
{
    public class LogicalException : Exception
    {
        public LogicalException(string message) : base(message)
        { }
    }
}
