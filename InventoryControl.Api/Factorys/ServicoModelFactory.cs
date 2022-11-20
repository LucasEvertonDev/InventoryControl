using InventoryControl.Api.Factorys.Interfaces;
using InventoryControl.Models.DTOs;
using InventoryControl.Models.Entities;

namespace InventoryControl.Api.Factorys
{
    public class ServicoModelFactory : IServicoModelFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="servicoDTO"></param>
        /// <returns></returns>
        public ServicoModel ConvertDtoToModel(ServicoDTO servicoDTO)
        {
            return new ServicoModel()
            {
                Id = servicoDTO.Id,
                Nome = servicoDTO.Nome,
                Descricao = servicoDTO.Descricao,
                IdExterno = servicoDTO.IdExterno,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageModel"></param>
        /// <returns></returns>
        public ServicoDTO ConvertModelToDto(ServicoModel servicoModel)
        {
            return new ServicoDTO()
            {
                Id = servicoModel.Id,
                Descricao = servicoModel.Descricao,
                IdExterno = servicoModel.IdExterno,
                Nome = servicoModel.Nome
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="servicoDtos"></param>
        /// <returns></returns>
        public List<ServicoModel> ConvertListDtoToListModel(List<ServicoDto> servicoDtos)
        {
            var servicoModels = new List<ServicoModel>();
            if (servicoDtos != null && servicoDtos.Count > 0)
            {
                servicoDtos.ForEach(servico =>
                {
                    servicoModels.Add(this.ConvertDtoToModel(servico));
                });
            }
            return servicoModels;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageDTOs"></param>
        /// <returns></returns>
        public List<ServicoDTO> ConvertListModelToListDto(List<ServicoModel> servicoModels)
        {
            var servicoDTOs = new List<ServicoDTO>();
            if (servicoModels != null && servicoModels.Count > 0)
            {
                servicoModels.ForEach(servicoModel =>
                {
                    servicoDTOs.Add(this.ConvertModelToDto(servicoModel));
                });
            }
            return servicoDTOs;
        }
    }
}
