using BowlingSimulator.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BowlingSimulator.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BowlingController : ApiController
    {
        private Game game;
        private GameRecord games;
        [HttpPost]
        public HttpResponseMessage NewGame([FromBody]Player player)
        {
            games = new GameRecord();

            Game game;

            try
            {
                game = games.AddGame(player.Name);
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }

            var responseMessage = Request.CreateResponse(HttpStatusCode.Created, game);

            string segments = "";

            for(var i = 0; i < Request.RequestUri.Segments.Length - 1 ; i++)
            {
                segments += Request.RequestUri.Segments[i];
            }

            string responseLocation = Request.RequestUri.Scheme + "://" + Request.RequestUri.Authority + segments + game.Id;

            responseMessage.Headers.Location = new Uri(responseLocation);
            return responseMessage;
        }

        [HttpPost]
        public HttpResponseMessage AddFrame(string gameId, [FromBody]Frame frame)
        {
            if (frame == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Input");
            games = new GameRecord();
            game = games.FindGameByGameId(gameId);
            if (game == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,"Game doesn't exist");

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
