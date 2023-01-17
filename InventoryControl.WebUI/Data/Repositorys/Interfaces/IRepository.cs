using AWASP.WebUI.Data.Domains;

namespace AWASP.WebUI.Data.Repositorys.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        /// <summary>
        /// 
        /// </summary>
        Task<IQueryable<T>> Itens { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task Save();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> FindAll();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T>? FindById(int? id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        Task<T> Insert(T domain);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        Task<T> Update(T domain);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        Task<T> Delete(T domain);
    }
}
