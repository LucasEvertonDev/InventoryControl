using InventoryControl.Models.Entities;

namespace InventoryControl.Application.Interfaces
{
    public interface IPasswordHashService
    {
        HashSalt EncryptPassword(string password);
        bool VerifyPassword(string enteredPassword, string storedPassword);
    }
}