using AWASP.WebUI.Factories.Interfaces;
using AWASP.WebUI.ViewModels;

namespace AWASP.WebUI.Factories
{
    public class SaveButtonsModelFactory : ISaveButtonsModelFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SaveButtonsModelFactory(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<SaveButtonsViewModel> PrepareSaveButtonsViewModel()
        {
            string controller = _httpContextAccessor.HttpContext.GetRouteValue("controller").ToString();
            string action = _httpContextAccessor.HttpContext.GetRouteValue("action").ToString();

            return Task.FromResult(new SaveButtonsViewModel
            {
                Controller = controller.ToLower(),
                Action = action.ToLower(),
                BotaoSalvarTexto = GetNameButtonSalvarByAction(action),
                BotaoVoltarTexto = "Voltar"
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        private string GetNameButtonSalvarByAction(string action)
        {
            if ("create".Equals(action.ToLower()))
            {
                return $"Cadastrar";
            }
            if ("edit".Equals(action.ToLower()))
            {
                return $"Editar";
            }
            return "";
        }
    }
}
