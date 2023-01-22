using Aplication.Encriptacion;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers {
    public class EncriptacionController : BaseController {
        [HttpPost]
        public async Task<ActionResult<string>> Encriptar (Encriptar.Execute data) {
            return await mediator.Send(data);
        }

        [HttpPost("desencriptar")]
        public async Task<ActionResult<string>> Desencriptar ( Desencriptar.Execute data ) {
            return await mediator.Send ( data );
        }
    }
}
