using Microsoft.AspNetCore.Mvc;
using PlayStudioCodingChallenge.Application.DTOs;
using PlayStudioCodingChallenge.Application.Services.QuestService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayStudioCodingChallenge.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StateController : ControllerBase
    {
        private readonly IQuestService _questService;
        public StateController(IQuestService questService)
        {
            _questService = questService;
        }
        [HttpGet]
        public async Task<GetQuestStateResponse> UpdateQuestProgress(string playerId)
        {
            var result = await _questService.GetQuestState(playerId);
            return result;
        }
    }
}
