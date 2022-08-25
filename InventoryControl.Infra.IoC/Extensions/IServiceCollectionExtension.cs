using Microsoft.Extensions.DependencyInjection;

namespace InventoryControl.Infra.IoC.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddRange(this IServiceCollection current, IServiceCollection main)
        {
            foreach (var serv in main)
            {
                current.Add(serv);
            }

            return current;
        }
    }
}
