namespace UserPointsApiBackend.Models.DataModels
{
    public class UserWithPoint
    {
        public int Id { get; set; }
        public Rank Rank { get; set; }
        public float? Points { get; set; }
        public float? TotalPoints { get; set; }
    }
}
