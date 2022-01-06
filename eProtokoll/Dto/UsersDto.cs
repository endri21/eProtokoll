namespace eProtokoll.Dto
{
    public class UsersDto : Response
    {
        public int id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public bool isActive { get; set; }
        public string token { get; set; }
        public string role { get; set; }
    }
}
