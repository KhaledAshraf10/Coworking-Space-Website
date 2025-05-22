using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace Workspace.Models
{
    public class DB
    {
        public SqlConnection con { get; set; }
        public DB()
        {
            string constr = "Data Source=LAPTOP-0HPFPI30;Initial Catalog=TheKingsDorm3;Integrated Security=True";


			con = new SqlConnection(constr);
        }

        // Eyad Tasks
        public bool checkemployeeid(employee e)
        {
            con.Open();
            string Q= "select count(*) from person where person.ID=" + e.ID+"";
            try
            {

             
                SqlCommand cmd = new SqlCommand(Q, con);
                int count = (int)cmd.ExecuteScalar();
                   con.Close();
                if (count > 0)
                {
                    return true;
                }
                else { return false; }
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
                con.Close();
             
                return true;

            }
            

        }
        public bool checkemployeeusername(employee e)
        {
            con.Open();
            string Q = "select count(*) from person where person.username = '" + e.username + "'";
            try
            {
                SqlCommand cmd = new SqlCommand(Q, con);
                int count = (int)cmd.ExecuteScalar();
                con.Close();
                if (count > 0)
                {
                    return true;
                }
                else { return false; }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                con.Close();
                return true;

            }
        }

            public bool checkemployePhoneNO(employee e)
            {
                con.Open();
                string Q = "select count(*) from person where person.username = '" + e.phoneno + "'";
                try
                {
                    SqlCommand cmd = new SqlCommand(Q, con);
                    int count = (int)cmd.ExecuteScalar();
                    con.Close();
                    if (count > 0)
                    {
                        return true;
                    }
                    else { return false; }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    con.Close();
                    return true;

                }

            }

        public DataTable getemployee(employee e)
        {
            DataTable dt = new DataTable();
            con.Open();
            string Q = "select* from person, employee where person.Id=" + e.ID + " and person.id=Employee_id";
            try
            {
                SqlCommand cmd = new SqlCommand(Q, con);
                dt.Load(cmd.ExecuteReader());
                return dt;





            }
            catch (SqlException x) { 


            Console.WriteLine(x.Message);
                return dt;   //empty table




            }
            finally
            {
                con.Close();    
            }






		}

		public DataTable getallemployees()
		{
			DataTable dt = new DataTable();
			con.Open();
			string Q = "select* from person, employee where  person.id=Employee_id";
			try
			{
				SqlCommand cmd = new SqlCommand(Q, con);
				dt.Load(cmd.ExecuteReader());
				return dt;





			}
			catch (SqlException x)
			{


				Console.WriteLine(x.Message);
				return dt;   //empty table




			}
			finally
			{
				con.Close();
			}






		}


		public bool insertemployee(employee e)
        {
            con.Open();
            string Q =
                        "insert into person VALUES(" + e.ID + ", '" + e.Fname + "', '" + e.Lname + "', '" + e.Email + "', '" + e.Pass + "', '" + e.username + "', '" + e.phoneno + "', null, 1)\r\n\r\ninsert into employee values(" + e.ID + ",'receptionist','" + e.city + "','" + e.AddressLine + "'," + e.age + ",'"+e.gender+"', '"+e.status+"',"+e.Salary+"); ";

              

			try
			{ SqlCommand cmd=new SqlCommand(Q, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
             

            }
            catch(SqlException ex){
                Console.WriteLine(ex.Message );
                con.Close();
                return false;   


            }






        }


		public DataTable getallCustomers()
		{
			DataTable dt = new DataTable();
			con.Open();
			string Q = "select* from person, Customer where  person.id=Customer_id ORDER BY Status DESC";
			try
			{
				SqlCommand cmd = new SqlCommand(Q, con);
				dt.Load(cmd.ExecuteReader());
				return dt;





			}
			catch (SqlException x)
			{


				Console.WriteLine(x.Message);
				return dt;   //empty table




			}
			finally
			{
				con.Close();
			}






		}


        public DataTable getcustomerid(CustomerUser C)
        {
            DataTable dt = new DataTable();
            con.Open();
            string Q = "select* from person, customer where person.Id=" + C.ID + " and person.id=Customer_ID ORDER BY Status DESC";
            try
            {
                SqlCommand cmd = new SqlCommand(Q, con);
                dt.Load(cmd.ExecuteReader());
                return dt;





            }
            catch (SqlException x)
            {


                Console.WriteLine(x.Message);
                return dt;   //empty table




            }
            finally
            {
                con.Close();
            }






        }














        public DataTable GetCustomerTable(string query)
        {
            DataTable DBTable = new DataTable();

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                DBTable.Load(cmd.ExecuteReader()); 
            }
            /*catch (Exception ex)
            {

            }*/
            finally
            {
                con.Close();
            }
            return DBTable;
        }

        public void InertIntoTable(string query)
        {

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }
            /*catch (Exception ex)
            {

            }*/
            finally
            {
                con.Close();
            }
        }

        public string GetString_Value(string query)
        {
			string value = null;
			try
			{
				con.Open();
				SqlCommand cmd = new SqlCommand(query, con);
				object result = cmd.ExecuteScalar();
				if (result != null)
				{
					value = result.ToString();
				}
			}
			/*catch (Exception ex)
            {

            }*/
			finally
			{
				con.Close();
			}
			return value;

		}
		public int GetInt_Value(string query)
		{
			int value = 0;
			try
			{
				con.Open();
				SqlCommand cmd = new SqlCommand(query, con);
				object result = cmd.ExecuteScalar();
				if (result != null)
				{
					value = (int)result;
				}
			}
			/*catch (Exception ex)
            {

            }*/
			finally
			{
				con.Close();
			}
			return value;

		}

		public void UpdatImage(string query)
        {
			try
			{
				con.Open();
				SqlCommand cmd = new SqlCommand(query, con);
				cmd.ExecuteNonQuery();
			}
			/*catch (Exception ex)
            {

            }*/
			finally
			{
				con.Close();
			}
		}
		public byte[] GetImage(string query)
		{
			byte[] imageData = null;

			try
			{
				con.Open();
				SqlCommand cmd = new SqlCommand(query, con);
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					// Check if the "ImageData" column is not null
					if (!reader.IsDBNull(reader.GetOrdinal("Person_Pic")))
					{
						imageData = (byte[])reader["Person_Pic"];
					}
				}
				reader.Close();
			}
			/*catch (Exception ex)
			{
				// Handle any exceptions
			}*/
			finally
			{
				con.Close();
			}

			return imageData;
		}






        // Kareem Tasks
        public Dictionary<string, int> GetNewCustomersLast30Days()
        {
            Dictionary<string, int> newCustomersData = new Dictionary<string, int>();
            string query = "SELECT " +
                           "    CONVERT(date, registration_date) AS registration_day, " +
                           "    COUNT(Customer_ID) AS new_customers_count " +
                           "FROM Customer " +
                           "WHERE registration_date >= DATEADD(day, -30, GETDATE()) " +
                           "GROUP BY CONVERT(date, registration_date) " +
                           "ORDER BY registration_day DESC";

            try
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string registrationDay = reader.GetDateTime(0).ToString("yyyy-MM-dd");
                        int newCustomersCount = reader.GetInt32(1);

                        newCustomersData.Add(registrationDay, newCustomersCount);
                    }
                    reader.Close();
                }
            }
            finally
            {
                con.Close();
            }

            return newCustomersData;
        }
        public Dictionary<string, int> GetSalesForTodayByRoomType()
        {
            Dictionary<string, int> salesData = new Dictionary<string, int>();
            string query = "SELECT r.type, COALESCE(SUM(r.Cost * br.time_Hours), 0) AS TotalSales " +
                "FROM Room r LEFT JOIN Book_Room br ON br.Room_ID = r.Room_id " +
                "WHERE CONVERT(date, br.Date_time) = CONVERT(date, GETDATE()) " +
                "GROUP BY r.type; ";

            try
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string roomType = reader.GetString(0);
                        int totalSales = reader.GetInt32(1);
                        salesData.Add(roomType, totalSales);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving sales data for today from the database: {ex}");
            }
            finally
            {
                con.Close();
            }
            return salesData;
        }
		// function that take quary and return value 




        // Khaled Tasks
        private object ExecuteScalarFunction(string Q)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Q, con);
                return cmd.ExecuteScalar();
            }
            catch (SqlException ex) { return ex; }
            finally { con.Close(); }
        }
		// function that return user type 
        public int GetUserType(string username)
        { 
            int Type = 0;
            string Q = "Select UserType From Person Where Username = '" + username + "'";
            //return (int)ExecuteScalarFunction(Q);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Q, con);
                Type = (int)cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {

            }
            finally
            {
                con.Close();

            }
            return Type;
        }


        // function that check that user in database or not 
        public bool CheckPassword(string username, string password)
        {
            string Q = "Select Password From Person Where Username = '" + username + "'";
            return (string)ExecuteScalarFunction(Q) == password;
        }


        public Dictionary<string, int> GetSalesByRoomType()
        {
            Dictionary<string, int> salesData = new Dictionary<string, int>();
            string query = "SELECT r.type, COALESCE(SUM(r.Cost * br.time_Hours), 0) AS TotalSales" +
                " FROM Room r LEFT JOIN Book_Room br ON br.Room_ID = r.Room_id WHERE MONTH(br.Date_time)" +
                " = MONTH(GETDATE()) AND YEAR(br.Date_time) = YEAR(GETDATE()) GROUP BY r.type; ";

            try
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string roomType = reader.GetString(0);
                        int totalSales = reader.GetInt32(1);
                        salesData.Add(roomType, totalSales);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving sales data from the database: {ex}");
            }
            finally
            {
                con.Close();
            }

            return salesData;
        }
        public Dictionary<string, int> GetAllTimeSalesByRoomType()
        {
            Dictionary<string, int> salesData = new Dictionary<string, int>();
            string query = "SELECT r.type, COALESCE(SUM(r.Cost * br.time_Hours), 0) AS TotalSales" +
                " FROM Room r LEFT JOIN Book_Room br ON br.Room_ID = r.Room_id GROUP BY r.type; ";

            try
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string roomType = reader.GetString(0);
                        int totalSales = reader.GetInt32(1);
                        salesData.Add(roomType, totalSales);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving sales data from the database: {ex}");
            }
            finally
            {
                con.Close();
            }

            return salesData;
        }
        public Dictionary<string, double> GetOccupancyByRoomTypeToday()
        {
            Dictionary<string, double> occupancyData = new Dictionary<string, double>();
            string query = "SELECT " +
                           "    Room.type, " +
                           "    AVG(CAST(Book_Room.time_Hours AS FLOAT) / Room.Capacity) AS Average_Occupancy_Rate " +
                           "FROM Book_Room " +
                           "INNER JOIN Room ON Book_Room.Room_ID = Room.Room_id " +
                           "WHERE CONVERT(date, Book_Room.Date_time) = CONVERT(date, GETDATE()) " +
                           "GROUP BY Room.type";

            try
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        string roomType = reader.GetString(0);
                        double occupancyRate = reader.GetDouble(1);

                        occupancyData.Add(roomType, occupancyRate);
                    }
                    reader.Close();
                }
            }
            finally
            {
                con.Close();
            }

            return occupancyData;
        }
        public Dictionary<string, double> GetOccupancyByRoomTypeYesterday()
        {
            Dictionary<string, double> occupancyData = new Dictionary<string, double>();
            string query = "SELECT " +
                           "    Room.type, " +
                           "    AVG(CAST(Book_Room.time_Hours AS FLOAT) / Room.Capacity) AS Average_Occupancy_Rate " +
                           "FROM Book_Room " +
                           "INNER JOIN Room ON Book_Room.Room_ID = Room.Room_id " +
                           "WHERE CONVERT(date, Book_Room.Date_time) = DATEADD(day, -1, CONVERT(date, GETDATE())) " +
                           "GROUP BY Room.type";

            try
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        string roomType = reader.GetString(0);
                        double occupancyRate = reader.GetDouble(1);

                        occupancyData.Add(roomType, occupancyRate);
                    }
                    reader.Close();
                }
            }
            finally
            {
                con.Close();
            }

            return occupancyData;
        }

        public Dictionary<string, Dictionary<DateTime, double>> GetOccupancyByRoomType()
        {
            Dictionary<string, Dictionary<DateTime, double>> occupancyData = new Dictionary<string, Dictionary<DateTime, double>>();
            string query = "SELECT " +
                           "    Room.type, " +
                           "    CONVERT(date, Book_Room.Date_time) AS Booking_Date, " +
                           "    AVG(CAST(Book_Room.time_Hours AS FLOAT) / Room.Capacity) AS Average_Occupancy_Rate " +
                           "FROM Book_Room " +
                           "INNER JOIN Room ON Book_Room.Room_ID = Room.Room_id " +
                           "GROUP BY Room.type, CONVERT(date, Book_Room.Date_time)";

            try
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        string roomType = reader.GetString(0);
                        DateTime bookingDate = reader.GetDateTime(1);
                        double occupancyRate = reader.GetDouble(2);

                        if (!occupancyData.ContainsKey(roomType))
                        {
                            occupancyData[roomType] = new Dictionary<DateTime, double>();
                        }
                        occupancyData[roomType].Add(bookingDate, occupancyRate);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred while retrieving occupancy data from the database: {ex}");
            }
            finally
            {
                con.Close();
            }

            return occupancyData;
        }

        public Dictionary<string, Dictionary<string, double[]>> GetEventsData()
        {
            var eventsData = new Dictionary<string, Dictionary<string, double[]>>();

            DateTime currentDate = DateTime.Now;
            DateTime oneMonthAgo = currentDate.AddMonths(-1);

            string query = "SELECT Title, NUM_Attendees, Cost, Date_Time " +
                            "FROM Event " +
                            "WHERE Date_Time >= @OneMonthAgo";

            try
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@OneMonthAgo", oneMonthAgo);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string title = reader.GetString(0);
                        int numAttendees = reader.GetInt32(1);
                        int costInt = reader.GetInt32(2);
                        DateTime date = reader.GetDateTime(3);

                        string dateString = date.ToString("yyyy-MM-dd");

                        double cost = Convert.ToDouble(costInt);

                        if (!eventsData.ContainsKey(title))
                        {
                            eventsData[title] = new Dictionary<string, double[]>();
                        }
                        eventsData[title][dateString] = new double[] { cost, numAttendees };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving event data from the database: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

            return eventsData;
        }


        public List<Dictionary<string, object>> GetCustomerSpending(int customerId)
        {
            List<Dictionary<string, object>> spendingData = new List<Dictionary<string, object>>();
            string query = "SELECT 'Book_Room' AS source, br.Date_time AS date, r.type AS room_type," +
                            "br.time_Hours AS duration, r.Cost * br.time_Hours AS payment_cost, br.Payment AS payment_type," +
                            "NULL AS event_title FROM Book_Room br INNER JOIN Room r ON br.Room_ID = r.Room_id " +
                            "WHERE br.Customer_ID = @customer_id AND NOT EXISTS" +
                            "( " +
                            "SELECT 1 FROM Book_Chair bc " +
                            "INNER JOIN Chair ch ON bc.Chair_ID = ch.Chair_ID " +
                            "WHERE bc.Customer_ID = br.Customer_ID AND bc.Date_time = br.Date_time) " +
                            "UNION ALL " +
                            "SELECT 'Book_Event' AS source, be.Date_time AS date, NULL AS room_type," +
                            "be.time_Hours AS duration, e.Cost* be.time_Hours AS payment_cost, be.Payment AS payment_type, " +
                            "e.Title AS event_title FROM Book_Event be INNER JOIN Event e ON be.Event_ID = e.Event_ID WHERE be.Customer_ID = @customer_id " +
                            "UNION ALL " +
                            "SELECT 'Book_Chair' AS source, bc.Date_time AS date, r.type AS room_type, bc.time_Hours AS duration," +
                            "r.Cost* bc.time_Hours AS payment_cost, bc.Payment AS payment_type, NULL AS event_title " +
                            "FROM Book_Chair bc " +
                            "INNER JOIN Chair ch ON bc.Chair_ID = ch.Chair_ID " +
                            "INNER JOIN Room r ON ch.Room_ID = r.Room_id " +
                            "WHERE bc.Customer_ID = @customer_id " +
                            "UNION ALL " +
                            "SELECT 'Bill' AS source, b.date_time AS date, NULL AS room_type, NULL AS duration, " +
                            "b.Cost_Products AS payment_cost, 'Products' AS payment_type, CAST(b.Bill_id AS NVARCHAR(50)) AS event_title " +
                            "FROM Bill b " +
                            "WHERE b.Customer_id = @customer_id " +
                            "ORDER BY date DESC;";

            try
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@customer_id", customerId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Dictionary<string, object> row = new Dictionary<string, object>();

                        row.Add("source", reader.GetString(0));
                        row.Add("date", reader.GetDateTime(1));
                        row.Add("room_type", reader.IsDBNull(2) ? null : reader.GetString(2));
                        row.Add("event_title", reader.IsDBNull(6) ? null : reader.GetString(6));
                        row.Add("duration", reader.IsDBNull(3) ? null : (object)reader.GetInt32(3));
                        row.Add("payment_cost", reader.IsDBNull(4) ? null : (object)reader.GetInt32(4));
                        row.Add("payment_type", reader.GetString(5));

                        spendingData.Add(row);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving spending data for customer {customerId} from the database: {ex}");
            }
            finally
            {
                con.Close();
            }

            return spendingData;
        }

        /*public Event ReadEvent(int ID)
        {
            Event u = new Event();
            DataTable dt = new DataTable();
            string Q = "Select * From Event Where Event_ID =" + ID;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Q, con);
                dt.Load(cmd.ExecuteReader());
                u.ID = (int)dt.Rows[0]["Event_ID"];
                u.NUM_Attendes = (int)dt.Rows[0]["NUM_Attendees"];
                u.Title = (string)dt.Rows[0]["Title"];
                u.Cost = (int)dt.Rows[0]["Cost"];
                u.RoomID = (int)dt.Rows[0]["Room_ID"];
                u.Time = (string)dt.Rows[0]["Date_Time"];

            }
            catch (SqlException ex) { }
            finally { con.Close(); }
            return u;
        }*/
        public DataTable ReadEvent()
        {
            DataTable dt = new DataTable();
            string Q = "Select * From Event";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Q, con);
                dt.Load(cmd.ExecuteReader());

            }
            catch (SqlException ex)
            {

            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public DataTable ReadEvent(int EventID)
        {
            DataTable dt = new DataTable();
            string Q = "Select * From Event Where Event_ID = "+EventID;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Q, con);
                dt.Load(cmd.ExecuteReader());

            }
            catch (SqlException ex)
            {

            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public int getNOAttendes(int EventID)
        {
            int Type = 0;
            string Q = "Select NUM_Attendees From Event Where Event_ID =  '" + EventID + "'";
            
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Q, con);
                Type = (int)cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {

            }
            finally
            {
                con.Close();

            }
            return Type;

            
        }
        public void incrementattendes(int EventID)
        {
            con.Open();
            string Q = "UPDATE Event SET NUM_Attendees = NUM_Attendees + 1 where Event_ID  = " + EventID;



            try
            {
                SqlCommand cmd = new SqlCommand(Q, con);
                cmd.ExecuteNonQuery();
                con.Close();
                


            }
            catch (SqlException ex)
            {}
            finally
            {
                con.Close();

            }
        }
        public int getNOOfEvents()
        {
            int Type = 0;
            string Q = "SELECT COUNT(*) FROM Event";

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Q, con);
                Type = (int)cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {

            }
            finally
            {
                con.Close();

            }
            return Type;

        }
        public bool insertEvent(Event e)
        {
            int newID = getNOOfEvents() + 1;
            string Q = "insert into Event VALUES(" + newID + ", " + e.NUM_Attendes + ", '" + e.Title + "', " + e.Cost + ", " + 1 + ", '" + e.Time + "')";

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Q, con);
                cmd.ExecuteNonQuery();
                
                return true;


            }
            catch (SqlException ex)
            {
                return false; 


            }
            finally
            {
                con.Close();

            }


        }
        public bool updateEvent(Event e)
        {
            int newID = getNOOfEvents() + 1;
            string Q = "Update Event Set NUM_Attendees =  " + e.NUM_Attendes + " , Title = '" + e.Title + "' , Cost = " + e.Cost + ", Room_ID = " + 1 + ", Date_Time = '" + e.Time + "' where Event_ID = "+ e.ID +"";

            
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Q, con);
                cmd.ExecuteNonQuery();

                return true;


            }
            catch (SqlException ex)
            {
                return false;


            }
            finally
            {
                con.Close();

            }


        }
        public bool DeleteEvent(int ID)
        {
            string Q = "Delete From Event Where Event_ID = " + ID;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Q, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (SqlException ex) { con.Close(); return false; }
            
        }

        public Event ReadEventt(int EventID)
        {
            Event e = new Event();
            DataTable dt = new DataTable();
            string Q = "Select * From Event Where Event_ID = " + EventID;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Q, con);
                dt.Load(cmd.ExecuteReader());
                e.ID = (int)dt.Rows[0]["Event_ID"];
                e.NUM_Attendes = (int)dt.Rows[0]["NUM_Attendees"];
                e.Title = (string)dt.Rows[0]["Title"];
                e.Cost = (int)dt.Rows[0]["Cost"];
                e.RoomID = (int)dt.Rows[0]["Room_ID"];
                e.Time = (DateTime)dt.Rows[0]["Date_Time"];

            }
            catch (SqlException ex)
            {

            }
            finally
            {
                con.Close();
            }
            return e;
        }


    }

}
