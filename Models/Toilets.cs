namespace ToiletApp.Models
{
    public class Toilets
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Advise { get; set; } = null!;
        public string Url { get; set; } = null!;
        public double Lat { get; set; }
        public double Lng { get; set; }

        public int AreasId { get; set; }
        public Areas Areas { get; set; } = null!;
        public ICollection<Comments> Comments { get; set; } = null!;

    }
}
