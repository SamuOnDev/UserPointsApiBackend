namespace UserPointsApiBackend.Models.DataModels
{
    public enum Rank
    {
        Bronze,
        Silver,
        Gold
    }

    public class UserPoint : User
    {
        public Rank Rank { get; set; } = Rank.Bronze;
        public float? Points { get; set; }
        public float? TotalPoints { get; set; }
    }
}
