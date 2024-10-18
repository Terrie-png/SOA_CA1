using Microsoft.AspNetCore.Authentication;
using SOA_CA1.Services.Interfaces;

namespace SOA_CA1.Services
{
    public class UserService : IUserService
    {

        private readonly string csvFilePath = "users.csv";
        public bool ValidateLogin(string username, string password)
        {
            if (File.Exists(csvFilePath))
            {
                var lines = File.ReadAllLines(csvFilePath);

                foreach (var line in lines.Skip(1))  // Skip header row
                {
                    var data = line.Split(',');

                    if (data[0] == username && data[1] == password)  // Match username and password
                    {
                        return true;
                    }
                }
            }  

            return false;
        }

        public bool RegisterUser(string username, string password, string email,int sex)
        {
            if (File.Exists(csvFilePath))
            {
                var lines = File.ReadAllLines(csvFilePath);

                foreach (var line in lines.Skip(1))
                {
                    var data = line.Split(',');

                    if (data[0] == username)
                    {
                        return false; 
                    }
                }
            }
            else
            {
                using (var writer = new StreamWriter(csvFilePath, true))
                {
                    writer.WriteLine("Username,Password,Email,Sex");
                }
            }

            string textSex = ((EnumSex)sex).ToString(); 

            using (var writer = new StreamWriter(csvFilePath, true))
            {
                writer.WriteLine($"{username},{password},{email},{textSex}");
            }

            return true; 
        }
    }
}
//login through reading csv file and register through writing to csv file
//using enum to check if the user had logged in or not before
//pagination
