using BowlingSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BowlingSimulator.Controllers
{
    public class BowlingController : ApiController
    {
        private Game game;
        private GameRecord games;

        public HttpResponseMessage New(string id,[FromBody] string name)
        {
            games = new GameRecord();
            var gameId = games.AddGame();

            return Request.CreateResponse(HttpStatusCode.Created, gameId);
        }

        public HttpResponseMessage AddFrame(string id, [FromBody] Frame frame)
        {
            if (frame == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Input");
            games = new GameRecord();
            game = games.FindGameByGameId(id);
            try
            {
                game.AddFrame(frame);
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }

            games.Save();

            return Request.CreateResponse(HttpStatusCode.Created, game);
        }
    }
}
