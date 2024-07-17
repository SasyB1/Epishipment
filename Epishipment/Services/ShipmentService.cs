using Epishipment.Models;
using Epishipment.Models.Dto;
using Microsoft.Data.SqlClient;
namespace Epishipment.Services
{
    public class ShipmentService
    {
        private readonly IConfiguration _config;


        public ShipmentService(IConfiguration config)
        {
            _config = config;
        }

        public Shipment GetShipment(int ShipmentId)
        {
            try
            {
                using (
                    SqlConnection conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"))
                )
                {
                    conn.Open();
                    const string SELECT_CMD = "SELECT ShipmentId,CustomerId,ShipmentDate,ShipmentDestinationCity,ShipmentNumber,ShipmentWeight,ShipmentPrice,ShipmentDateExpected FROM Shipments WHERE ShipmentId = @ShipmentId";
                    using (SqlCommand cmd = new SqlCommand(SELECT_CMD, conn))
                    {
                        cmd.Parameters.AddWithValue("@ShipmentId",ShipmentId);
                      
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                               return new Shipment
                                {
                                    ShipmentId = reader.GetInt32(0),
                                    CustomerId = reader.GetInt32(1),
                                    ShipmentDate = reader.GetDateTime(2),
                                    ShipmentDestinationCity = reader.GetString(3),
                                    ShipmentNumber = reader.GetString(4),
                                    ShipmentWeight = reader.GetDecimal(5),
                                    ShipmentPrice = reader.GetDecimal(6),
                                   ShipmentDateExpected = reader.GetDateTime(7)
                                };
                                
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

        public Shipment GetShipmentByTrackNumberAndTaxId(string shipmentNumber , int TaxId)
        {
            try
            {
                using (
                    SqlConnection conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"))
                )
                {
                    conn.Open();
                    const string SELECT_BY_TRACKANDTAX_CMD = "SELECT ShipmentId,CustomerId,ShipmentDate,ShipmentDestinationCity,ShipmentNumber,ShipmentWeight,ShipmentPrice,ShipmentDateExpected FROM Shipments inner join Customers on Shipments.CustomerId = Customers.CustomerId  WHERE ShipmentNumber = @ShipmentNumber and Customers.TaxId = @TaxId " ;
                    using (SqlCommand cmd = new SqlCommand(SELECT_BY_TRACKANDTAX_CMD, conn))
                    {
                        cmd.Parameters.AddWithValue("@ShipmentNumber", shipmentNumber);
                        cmd.Parameters.AddWithValue("@TaxId", TaxId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                               return new Shipment
                                {
                                   ShipmentId = reader.GetInt32(0),
                                   CustomerId = reader.GetInt32(1),
                                   ShipmentDate = reader.GetDateTime(2),
                                   ShipmentDestinationCity = reader.GetString(3),
                                   ShipmentNumber = reader.GetString(4),
                                   ShipmentWeight = reader.GetDecimal(5),
                                   ShipmentPrice = reader.GetDecimal(6),
                                   ShipmentDateExpected = reader.GetDateTime(7)
                               };
                                
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

        public List<Shipment> GetShipments()
        {
            List<Shipment> shipments = new List<Shipment>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    conn.Open();
                    const string SELECT_ALL_CMD = "SELECT ShipmentId, CustomerId, ShipmentDate, ShipmentDestinationCity, ShipmentNumber, ShipmentWeight, ShipmentPrice, ShipmentDateExpected FROM Shipments";
                    using (SqlCommand cmd = new SqlCommand(SELECT_ALL_CMD, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                shipments.Add(new Shipment
                                {
                                    ShipmentId = reader.GetInt32(0),
                                    CustomerId = reader.GetInt32(1),
                                    ShipmentDate = reader.GetDateTime(2),
                                    ShipmentDestinationCity = reader.GetString(3),
                                    ShipmentNumber = reader.GetString(4),
                                    ShipmentWeight = reader.GetDecimal(5),
                                    ShipmentPrice = reader.GetDecimal(6),
                                    ShipmentDateExpected = reader.GetDateTime(7)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Si è verificato un errore durante il tentativo di recuperare le spedizioni", ex);
            }
            return shipments;
        }


        public List<Shipment> GetShipmentsByDate(DateTime date)
        {
            List<Shipment> shipments = new List<Shipment>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    conn.Open();
                    const string SELECT_BY_DATE_CMD = "SELECT ShipmentId, CustomerId, ShipmentDate, ShipmentDestinationCity, ShipmentNumber, ShipmentWeight, ShipmentPrice, ShipmentDateExpected FROM Shipments WHERE CAST(ShipmentDate AS DATE) = @ShipmentDate";
                    using (SqlCommand cmd = new SqlCommand(SELECT_BY_DATE_CMD, conn))
                    {
                        cmd.Parameters.AddWithValue("@ShipmentDate", date.Date);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                shipments.Add(new Shipment
                                {
                                    ShipmentId = reader.GetInt32(0),
                                    CustomerId = reader.GetInt32(1),
                                    ShipmentDate = reader.GetDateTime(2),
                                    ShipmentDestinationCity = reader.GetString(3),
                                    ShipmentNumber = reader.GetString(4),
                                    ShipmentWeight = reader.GetDecimal(5),
                                    ShipmentPrice = reader.GetDecimal(6),
                                    ShipmentDateExpected = reader.GetDateTime(7)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Si è verificato un errore durante il tentativo di recuperare le spedizioni per data", ex);
            }
            return shipments;
        }


        public List<Shipment> GetShipmentsByStatus(string status)
        {
            List<Shipment> shipments = new List<Shipment>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    conn.Open();
                    const string SELECT_BY_STATUS_CMD = "SELECT ShipmentId, CustomerId, ShipmentDate, ShipmentDestinationCity, ShipmentNumber, ShipmentWeight, ShipmentPrice, ShipmentDateExpected FROM Shipments WHERE Status = @Status";
                    using (SqlCommand cmd = new SqlCommand(SELECT_BY_STATUS_CMD, conn))
                    {
                        cmd.Parameters.AddWithValue("@Status", status);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                shipments.Add(new Shipment
                                {
                                    ShipmentId = reader.GetInt32(0),
                                    CustomerId = reader.GetInt32(1),
                                    ShipmentDate = reader.GetDateTime(2),
                                    ShipmentDestinationCity = reader.GetString(3),
                                    ShipmentNumber = reader.GetString(4),
                                    ShipmentWeight = reader.GetDecimal(5),
                                    ShipmentPrice = reader.GetDecimal(6),
                                    ShipmentDateExpected = reader.GetDateTime(7)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Si è verificato un errore durante il tentativo di recuperare le spedizioni per stato", ex);
            }
            return shipments;
        }


        public List<Shipment> GetShipmentsByCity(string city)
        {
            List<Shipment> shipments = new List<Shipment>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    conn.Open();
                    const string SELECT_BY_CITY_CMD = "SELECT ShipmentId, CustomerId, ShipmentDate, ShipmentDestinationCity, ShipmentNumber, ShipmentWeight, ShipmentPrice, ShipmentDateExpected FROM Shipments WHERE ShipmentDestinationCity = @City";
                    using (SqlCommand cmd = new SqlCommand(SELECT_BY_CITY_CMD, conn))
                    {
                        cmd.Parameters.AddWithValue("@City", city);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                shipments.Add(new Shipment
                                {
                                    ShipmentId = reader.GetInt32(0),
                                    CustomerId = reader.GetInt32(1),
                                    ShipmentDate = reader.GetDateTime(2),
                                    ShipmentDestinationCity = reader.GetString(3),
                                    ShipmentNumber = reader.GetString(4),
                                    ShipmentWeight = reader.GetDecimal(5),
                                    ShipmentPrice = reader.GetDecimal(6),
                                    ShipmentDateExpected = reader.GetDateTime(7)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Si è verificato un errore durante il tentativo di recuperare le spedizioni per città", ex);
            }
            return shipments;
        }



    }
}
