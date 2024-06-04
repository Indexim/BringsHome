namespace BringHome.Models
{
    public class Roster
    {
        public int id { get; set; }
        public string? nik { get; set; }
        public DateTime? tanggal { get; set; }
        public string? status { get; set; }
        public int? shift { get; set; } = 0;
    }
}
