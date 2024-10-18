namespace SOA_CA1.Services.Interfaces
{
    public interface IUserService
    {
        public bool ValidateLogin(string username, string password);
        public bool RegisterUser(string username, string password);
    }
}
