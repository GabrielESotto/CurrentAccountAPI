using Microsoft.AspNetCore.Mvc;
using Nuuvify.CommonPack.Extensions.Implementation;
using Nuuvify.CommonPack.Extensions.Notificator;
using Nuuvify.CommonPack.Middleware;
using Nuuvify.CommonPack.Middleware.Abstraction.Results;

namespace CurrentAccount.Api.v1
{
    public class BaseController : BaseCustomController
    {
        protected BaseController() { }

        protected virtual async new Task<IActionResult> Response(
            StatusCodeResult codigoRetornoSucesso,
            StatusCodeResult codigoRetornoErro,
            object result,
            IEnumerable<NotificationR> notifications)
        {
            if (notifications.NotNullOrZero())
            {
                var errorType = notifications.FirstOrDefault(x =>
                    x.Property != null &&
                    x.Property.StartsWith("ApiExterna"));

                if (int.TryParse(errorType?.Message, out int codigoErro))
                {
                    codigoRetornoErro = new StatusCodeResult(codigoErro);
                }

                var retornoPadronizado = StatusCode(codigoRetornoErro.StatusCode,
                    new ReturnStandardErrors<NotificationR>
                    {
                        Success = false,
                        Errors = notifications
                    });

                return await Task.FromResult(retornoPadronizado);
            }
            else
            {
                var retornoPadronizado = StatusCode(StatusCodes.Status204NoContent, null);

                if (!IsNull(result))
                {
                    var tipo = StatusCodeProducesResponseTypeAction(codigoRetornoSucesso);
                    var retornoSucesso = GetInstanceResponse(tipo, result);
                    retornoPadronizado = StatusCode(codigoRetornoSucesso.StatusCode, retornoSucesso);
                }

                return await Task.FromResult(retornoPadronizado);
            }
        }
    }
}
