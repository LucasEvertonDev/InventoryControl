using InventoryControl.Domain.Entities;

namespace InventoryControl.Application.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(Usuario user);
    }
}