using PlayStudioCodingChallenge.Application.DTOs;
using PlayStudioCodingChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayStudioCodingChallenge.Application.Services.QuestService
{
    public interface IQuestService
    {
        Task<UpdateQuestProgressResponse> UpdateQuestProgress(UpdateQuestProgressRequest updateQuestProgressRequest);
        Task<GetQuestStateResponse> GetQuestState(string playerId);
        Task UpdateQuest(Quest quest);
    }
}
