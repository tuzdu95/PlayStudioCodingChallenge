using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayStudioCodingChallenge.Domain.Entities
{
    public class Quest
    {
        public string Id { get; set; }
        public Player Player { get; set; }
        public string Name { get; set; }
        public int PointEarned { get; set; }
        public int PointToComplete { get; set; }
        public int NumberOfMilestones { get; set; }
        public bool IsComplete { get; set; } = false;
        public bool IsActive { get; set; }
        public IEnumerable<Milestone> Milestones { get; set; }
    }
}
