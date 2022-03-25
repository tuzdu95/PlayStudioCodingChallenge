namespace PlayStudioCodingChallenge.Application.DTOs
{
    public class UpdateQuestProgressRequest
    {
        public string PlayerId { get; set; }
        public int PlayerLevel { get; set; }
        public int ChipAmountBet { get; set; }
    }
}
