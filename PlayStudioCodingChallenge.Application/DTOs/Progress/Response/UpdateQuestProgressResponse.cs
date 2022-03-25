using PlayStudioCodingChallenge.Domain.Entities;
using System.Collections.Generic;

namespace PlayStudioCodingChallenge.Application.DTOs
{
    public class UpdateQuestProgressResponse
    {
        public double QuestPointsEarned { get; set; }
        public double TotalQuestPercentCompleted { get; set; }
        public IEnumerable<MilestoneResponse> MilestoneCompleted { get; set; }
    }
}
