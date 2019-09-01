using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helper;
using Helper.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebSocketServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private ChatRoomHandler _webSocketHandler { get; set; }

        public AccountController(ChatRoomHandler webSocketHandler)
        {
            _webSocketHandler = webSocketHandler;
        }

        //Bu haliyle kullanılamaz. Çünkü istek http olduğundan webSocket kabul işlemi yapılamıyor.
        [Route("Register")]
        [HttpGet]
        public async Task<IActionResult> Register(string name)
        {
            //var socket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            //await _webSocketHandler.OnConnected(socket, name);
            //var result = WebSocketApiHelper.TakeWebSocketRequest(_webSocketHandler, HttpContext, socket, name);
            return Ok(true);
        }
    }
}