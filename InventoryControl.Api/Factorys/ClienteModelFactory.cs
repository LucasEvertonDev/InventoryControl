using InventoryControl.Api.Factorys.Interfaces;
using InventoryControl.Models.Dto;
using InventoryControl.Models.Entities;

namespace InventoryControl.Api.Factorys
{
    public class ClienteModelFactory : IClienteModelFactory
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clienteDTO"></param>
        /// <returns></returns>
        public ClienteModel ConvertDtoToModel(ClienteDTO clienteDTO)
        {
            return new ClienteModel()
            {
                Id = clienteDTO.Id,
                Nome = clienteDTO.Nome,
                IdExterno = clienteDTO.IdExterno,
                Cpf = clienteDTO.Cpf,
                DataNascimento = string.IsNullOrEmpty(clienteDTO.DataNascimento) ? (DateTime?)null : Convert.ToDateTime(clienteDTO.DataNascimento),
                Telefone = clienteDTO.Telefone,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageModel"></param>
        /// <returns></returns>
        public ClienteDTO ConvertModelToDto(ClienteModel clienteModel)
        {
            return new ClienteDTO()
            {
                Id = clienteModel.Id,
                DataNascimento = clienteModel.DataNascimento.HasValue ? clienteModel.DataNascimento.Value.ToString("yyyy-MM-dd hh:mm:ss").Replace("", "T") : "",
                Cpf = clienteModel.Cpf,
                IdExterno = clienteModel.IdExterno,
                Nome = clienteModel.Nome,
                Telefone = clienteModel.Telefone,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clienteDTOs"></param>
        /// <returns></returns>
        public List<ClienteModel> ConvertListDtoToListModel(List<ClienteDTO> clienteDTOs)
        {
            var clienteModels = new List<ClienteModel>();
            if (clienteDTOs != null && clienteDTOs.Count > 0)
            {
                clienteDTOs.ForEach(clienteDto =>
                {
                    clienteModels.Add(this.ConvertDtoToModel(clienteDto));
                });
            }
            return clienteModels;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageDTOs"></param>
        /// <returns></returns>
        public List<ClienteDTO> ConvertListModelToListDto(List<ClienteModel> clienteModels)
        {
            var clienteDTOs = new List<ClienteDTO>();
            if (clienteModels != null && clienteModels.Count > 0)
            {
                clienteModels.ForEach(clienteModel =>
                {
                    clienteDTOs.Add(this.ConvertModelToDto(clienteModel));
                });
            }
            return clienteDTOs;
        }
    }
}
