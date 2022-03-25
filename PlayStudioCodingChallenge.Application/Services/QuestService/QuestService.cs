using PlayStudioCodingChallenge.Application.AbstractRepository;
using PlayStudioCodingChallenge.Application.DTOs;
using PlayStudioCodingChallenge.Domain.Constants;
using PlayStudioCodingChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayStudioCodingChallenge.Application.Services.QuestService
{
    public class QuestService : IQuestService
    {
        private readonly IQuestRepository _questRepository;
        private readonly IConfigurationRepository _configurationRepository;
        public QuestService(IQuestRepository questRepository, IConfigurationRepository configurationRepository)
        {
            _questRepository = questRepository;
            _configurationRepository = configurationRepository;
        }
        public async Task<UpdateQuestProgressResponse> UpdateQuestProgress(UpdateQuestProgressRequest updateQuestProgressRequest)
        {
            if (updateQuestProgressRequest.ChipAmountBet <= 0)
            {
                throw new Exception(ApplicationConstant.ZERO_CHIP_BET);
            }
            var currentQuest = await _questRepository.GetQuestByPlayerId(updateQuestProgressRequest.PlayerId);
            if (currentQuest == null)
            {
                throw new Exception(ApplicationConstant.QUEST_NOT_FOUND_ERROR);
            }
            var updateQuestProgressResponse = new UpdateQuestProgressResponse();
            var questPointsEarnedFromNewBet = await CalculateQuestPointsEarned(updateQuestProgressRequest);
            var currentTotalQuestPoint = currentQuest.PointEarned + questPointsEarnedFromNewBet;
            currentQuest = UpdateMileStoneStatus(currentQuest, currentTotalQuestPoint);
            var totalQuestPercentCompleted = (100 * currentQuest.PointEarned / currentQuest.PointToComplete);
            if (totalQuestPercentCompleted >= 100) currentQuest.IsComplete = true;
            await UpdateQuest(currentQuest);
            var response = new UpdateQuestProgressResponse
            {
                QuestPointsEarned = questPointsEarnedFromNewBet,
                TotalQuestPercentCompleted = totalQuestPercentCompleted > 100 ? 100: totalQuestPercentCompleted,
                MilestoneCompleted = currentQuest.Milestones.Where(m => m.IsComplete).Select(item => new MilestoneResponse { MilestoneIndex = item.Index, ChipsAwarded = item.Award })
            };
            return response;
        }

        public async Task<GetQuestStateResponse> GetQuestState(string playerId)
        {
            var quest = await _questRepository.GetQuestByPlayerId(playerId);
            if (quest == null)
            {
                throw new Exception(ApplicationConstant.QUEST_NOT_FOUND_ERROR);
            }
            var percentage = quest.PointEarned * 100 / quest.PointToComplete;
            var lastMilestoneComplete = quest.Milestones.Where(m => m.IsComplete)?.OrderByDescending(m => m.PointToComplete)?.FirstOrDefault();
            var questState = new GetQuestStateResponse {
                TotalQuestPercentCompleted = percentage > 100 ? 100 : percentage,
                LastMilestoneIndexCompleted = lastMilestoneComplete != null ? lastMilestoneComplete.Index: 0
            };
            return questState;
        }

        private async Task<int> CalculateQuestPointsEarned(UpdateQuestProgressRequest updateQuestProgressRequest)
        {
            var rateFromCurrentBet = await _configurationRepository.GetRateByAmountBet(updateQuestProgressRequest.ChipAmountBet);
            var bonusFromCurrentPlayerLevel = await _configurationRepository.GetBonusByLevel(updateQuestProgressRequest.PlayerLevel);
            return (int)(updateQuestProgressRequest.ChipAmountBet * (rateFromCurrentBet.Rate / 100) + updateQuestProgressRequest.PlayerLevel * bonusFromCurrentPlayerLevel.BonusRate);
        }

        private Quest UpdateMileStoneStatus(Quest currentQuest, int currentTotalQuestPoint)
        {
            var canCompleteMilestones = currentQuest.Milestones.Where(m => !m.IsComplete && m.PointToComplete <= currentTotalQuestPoint);
            currentQuest.PointEarned = currentTotalQuestPoint;
            if (canCompleteMilestones.Count() > 0)
            {
                foreach (var item in canCompleteMilestones)
                {
                    currentQuest.PointEarned += item.Award;
                    item.IsComplete = true;
                }
            }
            return currentQuest;
        } 

        public async Task UpdateQuest(Quest quest)
        {
            await _questRepository.UpdateAsync(quest.Id, quest);
        }
    }
}
