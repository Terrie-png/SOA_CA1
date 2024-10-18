namespace SOA_CA1
{
    public class UserSession
    {
        public string Username { get; set; }
        public bool IsLoggedIn { get; set; } = false;

        public void logout()
        {
            Username = null;
            IsLoggedIn = false;
        }
    }
}
