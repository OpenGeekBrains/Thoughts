namespace Thoughts.UI.MVC.WebModels
{
    public class ShortUrlWebModel
    {   
        public int Id { get; set; }  
        public string OriginalUrl { get; set; } 
        public string GetUrl { get; set; }
        public string GetAlias { get; set; }
    }
}
