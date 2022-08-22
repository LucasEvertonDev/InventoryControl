using InventoryControl.Models.Entities;
using InventoryControl.WebUI.ViewModels.Atendimentos;

namespace InventoryControl.WebUI.Factories.Interfaces
{
    public interface IAtendimentoModelFactory
    {
        Task<AtendimentoViewModel> PrepareAtendimentoViewModel();

        Task<AtendimentoViewModel> PrepareAtendimentoViewModel(AtendimentoViewModel atendimentoViewModel);

        Task<AtendimentoModel> PrepareAtendimentoModelDto(AtendimentoViewModel viewModel);

        Task<List<CalendarioViewModel>> PrepareCalendaViewModel(List<AtendimentoModel> listModel);
    }
}