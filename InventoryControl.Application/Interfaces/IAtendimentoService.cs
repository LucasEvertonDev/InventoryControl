using InventoryControl.Domain.Entities;
using InventoryControl.Models.Entities;

namespace InventoryControl.Application.Interfaces
{
    public interface IAtendimentoService
    {
        Task<Atendimento> CreateAtendimento(AtendimentoModel model);

        Task<Atendimento> UpdateAtendimento(AtendimentoModel model);

        Task<List<AtendimentoModel>> SeachAgendamentos(DateTime dataInicio, DateTime dataFim);

        Task<List<AtendimentoModel>> SeachAgendamentos(AtendimentoModel model, DateTime dataInicio, DateTime datFim);

        Task<AtendimentoModel> FindById(int id);
    }
}