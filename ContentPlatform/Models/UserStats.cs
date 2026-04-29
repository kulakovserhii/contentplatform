namespace ContentPlatform.Models
{
    public class UserStats
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int MusicReviewsCount { get; set; }
        public int FilmsReviewsCount { get; set; }
        public int GameReviewsCount { get; set; }
        public int BookReviewsCount { get; set; }
        public int TvShowReviewsCount { get; set; }
        public int LikeReviewsCount { get; set; }
        public int LikeReviewsRetrievedCount { get; set; }
        public int PerfectiRatingCount { get; set; }
        public int LowRatingsCount { get; set; }
    }
}
