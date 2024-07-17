using Epishipment.Models;
using Epishipment.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
namespace Epishipment.Services
{
    public class UserService
    {
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public User GetUser(LoginDto loginDto)
        {
            try
            {
                using (
                    SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))
                )
                {
                    conn.Open();
                    const string SELECT = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";
                    using (SqlCommand cmd = new SqlCommand(SELECT, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", loginDto.Username);
                        cmd.Parameters.AddWithValue("@Password", loginDto.Password);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                               User user = new User
                                {
                                    UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                    Username = reader.GetString(reader.GetOrdinal("Username")),
                                    Password = reader.GetString(reader.GetOrdinal("Password"))
                                };
                                return user;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while trying to get the user", ex);
            }
            return null;
        }

        public User AddUser(RegisterDto registerDto)
        {
            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                conn.Open();
                const string INSERT_CMD = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)";
                using (SqlCommand cmd = new SqlCommand(INSERT_CMD, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", registerDto.Username);
                    cmd.Parameters.AddWithValue("@Password", registerDto.Password);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while trying to add the user", ex);
            }
            return null;
        }


    }
}
