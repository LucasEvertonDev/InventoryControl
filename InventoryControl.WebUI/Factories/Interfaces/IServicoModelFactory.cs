using InventoryControl.Models.Entities;
using InventoryControl.WebUI.ViewModels.Servicos;

namespace InventoryControl.WebUI.Factories.Interfaces
{
    public interface IServicoModelFactory
    {
        Task<ConsultarServicosViewModel> PrepareConsultaServicosModel(List<ServicoModel> servicos);
        Task<ServicoModel> PrepareServicoModelDto(ServicoViewModel servicoViewModel);
        Task<ServicoViewModel> PrepareServicoViewModel();
        Task<ServicoViewModel> PrepareServicoViewModel(ServicoModel servicoModel);
    }
}