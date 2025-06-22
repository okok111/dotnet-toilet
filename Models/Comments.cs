namespace ToiletApp.Models
{
    public class Comments
    {
        public int Id { get; set; }
        public DateTime UseDate { get; set; }
        public int Rate { get; set; }
        public string Details { get; set; } = null!;
        public int ToiletsId { get; set; }
        public Toilets Toilets { get; set; } = null!;
    }
}
