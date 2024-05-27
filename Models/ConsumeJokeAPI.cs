namespace JokesWebApp.Models
{
    public class ConsumeJokeAPI
    {
        public int Id { get; set; }
        public bool Error { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public string Setup { get; set; }
        public string Delivery { get; set; }
        public bool Safe { get; set; }
        public string Lang { get; set; }

    }
}
