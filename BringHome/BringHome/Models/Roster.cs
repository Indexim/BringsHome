namespace BringHome.Models
{
    public class Roster
    {
        public int Id { get; set; }
        public string NIK { get; set; }
        public DateTime Tanggal { get; set; }
        public string Status { get; set; }
        public int Shift { get; set; } = 0;
    }
}
