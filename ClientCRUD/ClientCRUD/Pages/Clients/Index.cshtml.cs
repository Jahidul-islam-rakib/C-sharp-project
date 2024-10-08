using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ClientCRUD.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> ListClients = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
                //String ConnectionString = @"Data Source=.\\sqlexpress;Initial Catalog=clientCRUD;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
				String ConnectionString = "Data Source=.\\SQLEXPRESS;Database =clientCRUD ;Integrated Security=True;MultipleActiveResultSets=True";


				using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    String query = " select * from Clients";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader()) 
                            {
                                 while (reader.Read())
                                     {
                                        ClientInfo clientInfo = new ClientInfo();
                                        clientInfo.id = "" + reader.GetInt32(0);
                                        clientInfo.name =  reader.GetString(1) ;
                                        clientInfo.email =  reader.GetString(2);
                                        clientInfo.phone = reader.GetString(3);
                                        clientInfo.address =  reader.GetString(4);
                                        clientInfo.created_at =reader.GetDateTime(5).ToString();
                                        ListClients.Add(clientInfo);


                            }
                        }
                    }
                }

            }
            catch (Exception ex) { 
                Console.WriteLine("\n\nThe ERROR is : " + ex.ToString());
            }


        }

        public class ClientInfo
        {
            public string id { get; set; }
            public string name { get; set; }
			public string email { get; set; }
			public string phone { get; set; }
			public string address { get; set; }
			public string created_at { get; set; }
		}
    }
}
