namespace BlogManagement.Application.Contracts.Article
{
    public class ArticleViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string PageTitle { get; set; }
        public long AuthorId { get; set; }
        public string Author { get; set; }
        public string ShortDescription { get; set; }
        public string Picture { get; set; }
        public long CategoryId { get; set; }
        public bool IsRemoved { get; set; }
        public string CreationDate { get; set; }
        public string Category { get; set; }
    }
}