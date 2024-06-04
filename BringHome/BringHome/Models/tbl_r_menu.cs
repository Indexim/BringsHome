namespace BringHome.Models
{
    public class tbl_r_menu
    {
        public int id { get; set; }
        public string? kategori_user_id { get; set; }
        public string? title { get; set; }
        public string? type { get; set; }
        public string? link_controller { get; set; }
        public string? link_function { get; set; }
        public string? hidden { get; set; }
        public string? new_tab { get; set; }
        public string? insert_by { get; set; }
        public string? ip { get; set; }
        public string? created_at { get; set; }
        public string? updated_at { get; set; }
    }
}