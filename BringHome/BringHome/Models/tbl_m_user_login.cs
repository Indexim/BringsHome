namespace BringHome.Models
{
    public class tbl_m_user_login
    {
        public int id { get; set; }
        public string? kategori_user_id { get; set; }
        public string? dept_code { get; set; }
        public string? nrp { get; set; }
        public string? password { get; set; }
        public string? fullname { get; set; }
        public string? created_by { get; set; }
        public string? created_at { get; set; }
        public string? updated_at { get; set; }
        public string? updated_by { get; set; }
        public string? last_login { get; set; }
        public string? ip { get; set; }
    }
}
