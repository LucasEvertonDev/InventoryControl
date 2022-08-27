using InventoryControl.Models.Entities;
using InventoryControl.WebUI.ViewModels.Custos;

namespace InventoryControl.WebUI.Factories.Interfaces
{
    public interface ICustosModelFactory
    {
        Task<ConsultarCustosViewModel> PrepareConsultaCustosModel(List<CustosModel> custos);
        Task<CustosModel> PrepareCustosModelDto(CustosViewModel custoViewModel);
        Task<CustosViewModel> PrepareCustosViewModel();
        Task<CustosViewModel> PrepareCustosViewModel(CustosModel custoModel);
    }
}