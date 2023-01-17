using AWASP.WebUI.Services.Interfaces;
using AWASP.WebUI.Utils;

namespace AWASP.WebUI.Services
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
