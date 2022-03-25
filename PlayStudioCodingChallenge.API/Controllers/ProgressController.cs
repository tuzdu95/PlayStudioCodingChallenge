using Microsoft.AspNetCore.Mvc;
using PlayStudioCodingChallenge.Application.DTOs;
using PlayStudioCodingChallenge.Application.Services.QuestService;
using PlayStudioCodingChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayStudioCodingChallenge.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgressController : ControllerBase
    {
        private readonly IQuestService _questService;
        public ProgressController(IQuestService questService)
        {
            _questService = questService;
        }
        [HttpPost]
        public async Task<UpdateQuestProgressResponse> UpdateQuestProgress(UpdateQuestProgressRequest updateQuestProgressRequest)
        {
            var result = await _questService.UpdateQuestProgress(updateQuestProgressRequest);
            return result;
        }
    }
}
