using Epishipment.Models;
using Epishipment.Models.Dto;
using Microsoft.Data.SqlClient;
namespace Epishipment.Services
{
    public class CustomerService
    {
        private readonly IConfiguration _config;

        public CustomerService(IConfiguration config)
        {
            _config = config;
        }

        public Customer AddCustomer(CustomerDto customer)
        {
            try
            {
                using var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
                conn.Open();
                const string INSERT_CMD = "INSERT INTO Customers (CustomerName, CustomerSurname,CustomerBusinessName,CustomerTaxIdCode,CustomerVATNumber,CustomerType,UserId) VALUES (@CustomerName, @CustomerSurname,@CustomerBusinessName,@CustomerTaxIdCode,@CustomerVATNumber,@CustomerType,@UserId)";
                using var cmd = new SqlCommand(INSERT_CMD, conn);
                cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                cmd.Parameters.AddWithValue("@CustomerSurname", customer.CustomerSurname);
                cmd.Parameters.AddWithValue("@CustomerBusinessName", customer.CustomerBusinessName);
                cmd.Parameters.AddWithValue("@CustomerTaxIdCode", customer.CustomerTaxIdCode);
                cmd.Parameters.AddWithValue("@CustomerVATNumber", customer.CustomerVATNumber);
                cmd.Parameters.AddWithValue("@CustomerType", customer.CustomerType);
                cmd.Parameters.AddWithValue("@UserId", customer.UserId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while trying to add the customer", ex);
            }
            return null;
        }

        public Customer GetCustomer(int customerId)
        {
            try
            {
                using (var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    conn.Open();

                    const string SELECT_CMD = "SELECT CustomerId, CustomerName, CustomerSurname, CustomerBusinessName, CustomerTaxIdCode, CustomerVATNumber, CustomerType, UserId FROM Customers WHERE CustomerId = @CustomerId";

                    using (var cmd = new SqlCommand(SELECT_CMD, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerId", customerId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var customer = new Customer
                                {
                                    CustomerId = reader.GetInt32(0),
                                    CustomerName = reader.GetString(1),
                                    CustomerSurname = reader.GetString(2),
                                    CustomerBusinessName = reader.GetString(3),
                                    CustomerTaxIdCode = reader.GetString(4),
                                    CustomerVATNumber = reader.GetString(5),
                                    CustomerType = Enum.Parse<CustomerType>(reader.GetString(6)),
                                    UserId = reader.GetInt32(7)
                                };
                                return customer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while trying to get the customer", ex);
            }
            return null;
        }

        public List<Customer> GetCustomers()
        {
            try
            {
                List<Customer> customers = new List<Customer>();
                using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    conn.Open();
                    const string SELECT_CMD = "SELECT * FROM Customers";
                    using (SqlCommand cmd = new SqlCommand(SELECT_CMD, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Customer customer = new Customer();
                                customer.CustomerId = reader.GetInt32(0);
                                customer.CustomerName = reader.GetString(1);
                                customer.CustomerSurname = reader.GetString(2);
                                customer.CustomerBusinessName = reader.GetString(3);
                                customer.CustomerTaxIdCode = reader.GetString(4);
                                customer.CustomerVATNumber = reader.GetString(5);
                                customer.CustomerType = Enum.Parse<CustomerType>(reader.GetString(6));
                                customers.Add(customer);
                            }
                        }
                    }
                }
                return customers;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nel recupero dei clienti", ex);
            }
        }


    }
}
