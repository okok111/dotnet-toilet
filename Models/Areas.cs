namespace ToiletApp.Models
{
    public class Areas 
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Toilets> Toilets { get; set; } = null!;
    }
}
