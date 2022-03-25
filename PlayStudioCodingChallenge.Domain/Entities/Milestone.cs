using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayStudioCodingChallenge.Domain.Entities
{
    public class Milestone
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public int PointToComplete { get; set; }
        public int Award { get; set; }
        public bool IsComplete { get; set; } = false;
    }
}
