using InventoryControl.Api.Factorys.Interfaces;
using InventoryControl.Models.Dto;
using InventoryControl.Models.Entities;

namespace InventoryControl.Api.Factorys
{
    public class AtendimentoModelFactory : IAtendimentoModelFactory 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageModel"></param>
        /// <returns></returns>
        public AtendimentoDTO ConvertModelToDto(AtendimentoModel atendimentoModel)
        {
            var list = new List<AssociacaoServicosAtendimentoModel>();

            atendimentoModel.MapServicosAtendimen.ForEach(item =>
            {
                list.Add(new AssociacaoServicosAtendimentoModel
                {
                    AtendimentoId = item.AtendimentoId,
                    AtendimentoIdExterno = item.AtendimentoIdExterno,
                    Id = item.Id,
                    IdExterno = item.IdExterno,
                    ServicoIdExterno = item.ServicoIdExterno,
                    ServicoId = item.ServicoId,
                    ValorCobrado = item.ValorCobrado,
                });
            });

            return new AtendimentoDTO()
            {
                Id = atendimentoModel.Id,
                Data = atendimentoModel.Data.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"),
                ClienteAtrasado = atendimentoModel.ClienteAtrasado,
                ClienteId  = atendimentoModel.ClienteId,
                ClienteIdExterno = atendimentoModel.ClienteIdExterno,
                IdExterno = atendimentoModel.IdExterno,
                ObservacaoAtendimento = atendimentoModel.ObservacaoAtendimento,
                Situacao = atendimentoModel.Situacao ,
                ValorAtendimento = atendimentoModel.ValorAtendimento,
                ValorPago = atendimentoModel.ValorPago,
                ServicosAssociados = list
            };
        }
    }
}
