using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LudoGameEngine;
using WebAPI.Models;
using System.Collections.Specialized;
using System.Net;

namespace WebAPI.Controllers
{
    [Route("api/[Controller]")]
    public class LudoController : Controller
    {
        public ILudoContext context;
        public LudoController(ILudoContext _context)
        {
            context = _context;
        }

        // POST: api/ludo/createnewgame
        [HttpPost("createnewgame")]
        public IActionResult CreateNewGame()
        {
            Guid g = context.AddGame();
            return Ok(g);
        }

        // DELETE: api/ludo/{gameID}/removegame
        [HttpDelete("{id}/removegame")]
        public IActionResult RemoveGame(Guid id)
        {
            if (!context.RemoveGame(id))
            {
                return NotFound(id);
            }
            else
            {
                return Ok();
            }
        }

        // POST: api/ludo/{gameID}/players/addplayer?name={input}&colorID={input}
        [HttpPost("{id}/players/addplayer")]
        public IActionResult AddPlayer(Guid id, string name, int colorID)
        {
            if (context.AddPlayer(id, name, colorID) == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(context.GetGameDetail(id).GetPlayer(colorID));
            }
        }

        // DELETE: api/ludo{gameID}/players
        [HttpDelete("{id}/players")]
        public IActionResult RemovePlayer(Guid id, int colorID)
        {
            if (!context.RemovePlayer(id, colorID))
            {
                return NotFound(new KeyValuePair<Guid, int>(id, colorID));
            }

            return Ok(new KeyValuePair<Guid, int>(id, colorID));
        }

        // GET: api/Ludo/getallgames
        [HttpGet("getallgames")]
        public IActionResult GetAllGames()
        {
            return Ok(context.GetAllGames());
        }

        // GET: api/ludo/{gameID}/getgamedetails
        [HttpGet("{id}/getgamedetails")]
        public IActionResult GetGameDetails(Guid id)
        {
            if (context.GetGameDetail(id) == null)
            {
                return NotFound(id);
            }

            return Ok(context.GetGameDetail(id));
        }

        // GET: api/ludo/{gameID}/players/getplayers
        [HttpGet("{id}/players/getplayers")]
        public IActionResult GetAllPlayers(Guid id)
        {
            if (context.GetAllPlayers(id) == null)
            {
                return NotFound(id);
            }

            return Ok(context.GetAllPlayers(id));
        }

        // GET: api/ludo/{gameID}/players?colorID={input}
        [HttpGet("{id}/players")]
        public IActionResult GetPlayerDetails(Guid id, int colorID)
        {
            if (context.GetPlayerDetail(id, colorID) == null)
            {
                return NotFound(id);
            }

            return Ok(context.GetPlayerDetail(id, colorID));
        }

        // PUT: api/ludo/{gameID}/startgame
        [HttpPut("{id}/startgame")]
        public IActionResult StartGame(Guid id)
        {
            return Ok(context.StartGame(id));
        }

        // GET: api/ludo/{gameID}/rolldice
        [HttpGet("{id}/rolldice")]
        public IActionResult RollDice(Guid id)
        {
            return Ok(context.RollDice(id));
        }

        // PUT: api/ludo/{gameID}/movepiece
        [HttpPut("{id}/movepiece")]
        public IActionResult MovePiece(Guid id, int pieceId, int numberOfFields)
        {
            return Ok(context.MovePiece(id, pieceId, numberOfFields));
        }

        // PUT: api/ludo/{gameID}/endturn
        [HttpPut("{id}/endturn")]
        public IActionResult EndTurn(Guid id)
        {
            context.EndTurn(id);
            return Ok();
        }

        // GET: api/ludo/{gameID}/getwinner
        [HttpGet("{id}/getwinner")]
        public IActionResult GetWinner(Guid id)
        {
            return Ok(context.GetWinner(id));
        }
    }
}