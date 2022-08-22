using InventoryControl.Domain.Entities;
using InventoryControl.Models.Entities;

namespace InventoryControl.Application.Interfaces
{
    public interface IAtendimentoService
    {
        Task<Atendimento> CreateAtendimento(AtendimentoModel model);

        Task<List<AtendimentoModel>> SeachAgendamentos(DateTime dataInicio, DateTime dataFim);
    }
}