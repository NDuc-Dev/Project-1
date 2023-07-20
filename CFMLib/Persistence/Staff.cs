namespace Persistence
{
    public class Staff
    {
        public int StaffId { set; get; }
        public string StaffName { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public int StaffStatus { set; get; }

        public Staff()
        {
            StaffName = "";
            UserName = "";
            Password = "";
        }

    }
}