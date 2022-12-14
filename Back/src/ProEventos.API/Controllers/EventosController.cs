using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Persistence;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Application.Contratos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
               
        private readonly IEventoService _eventoService;

        public EventosController(IEventoService eventoService)
        {
            _eventoService = eventoService;            

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync(true);

                if (eventos == null)
                    return NotFound("Nenhum evento encontrado!");
                
                return Ok(eventos);
            }
            catch (Exception e)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar eventos. <->... Erro: {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = await _eventoService.GetAllEventosByIdAsync(id, true);

                if (evento == null) return NotFound("Nenhum evento encontrado!");

                return Ok(evento);
            }
            catch (Exception e)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar eventos. Erro: {e.Message}");
            }
        }

        [HttpGet("tema/{tema}")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var evento = await _eventoService.GetAllEventosByTemaAsync(tema, true);

                if (evento == null) return NotFound("Nenhum evento encontrado com o tema!");

                return Ok(evento);
            }
            catch (Exception e)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar eventos<>. Erro: {e.Message}");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
           try
            {
                var evento = await _eventoService.AddEventos(model);

                if (evento == null) return BadRequest("Não foi possível adcionar o evento!");

                return Ok(evento);
            }
            catch (Exception e)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar cadastrar eventos >>. Erro: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento model)
        {
             try
            {
                var evento = await _eventoService.UpdateEvento(id, model);

                if (evento == null) return BadRequest("Não foi possível atualizar o evento!");

                return Ok(evento);
            }
            catch (Exception e)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar atualizar evento. Erro: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
             try
            {
                if(await _eventoService.DeleteEvento(id))
                    return Ok("Evento deletado com sucesso!");
                else
                    return BadRequest("Não foi possível deletar o evento");
            }
            catch (Exception e)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar deletar o evento. Erro: {e.Message}");
            }
        }

    }

}
