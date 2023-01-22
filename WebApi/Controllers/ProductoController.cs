using Aplication.Productos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ProductoController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<ProductoDto>>> GetProductos()
        {
            return await mediator.Send(new GetProductos.Execute());
        }

        [HttpGet("{codigo}")]
        public async Task<ActionResult<ProductoDto>> GetProductosById (string codigo ) {
            return await mediator.Send ( new GetProductosById.Execute { codigo = codigo } );
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> SetProductos(SetProductos.Execute data)
        {
            return await mediator.Send(data);
        }

        [HttpDelete("{codigo}")]
        public async Task<ActionResult<Unit>> DropProducto (string codigo ) {
            return await mediator.Send ( new DropProductos.Execute{codigo = codigo });
        }

        [HttpPut("{codigo}")]
        public async Task<ActionResult<Unit>> EditProducto ( string codigo, EditProductos.Execute data) {
            data.codigo = codigo;
            return await mediator.Send(data);
        }
    }
}
