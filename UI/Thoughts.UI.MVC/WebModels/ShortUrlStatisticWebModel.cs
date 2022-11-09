namespace Thoughts.UI.MVC.WebModels
{
    public class ShortUrlStatisticWebModel
    {   
        public int Id { get; set; }  
        public string OriginalUrl { get; set; } 
        public string AliasUrl { get; set; }
        public int Statistic { get; set; }
        public DateTimeOffset LastReset { get; set; }
    }
}
