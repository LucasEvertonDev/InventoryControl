using InventoryControl.WebUI.ViewModels.Atendimentos;

namespace InventoryControl.WebUI.Factories.Interfaces
{
    public interface IAtendimentoModelFactory
    {
        Task<AtendimentoViewModel> PrepareAtendimentoViewModel();
    }
}