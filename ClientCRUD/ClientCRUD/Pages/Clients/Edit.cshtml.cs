using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Net.Security;
using static ClientCRUD.Pages.Clients.IndexModel;


namespace ClientCRUD.Pages.Clients
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
				String connectionString = "Data Source=.\\SQLEXPRESS;Database =clientCRUD ;Integrated Security=True;MultipleActiveResultSets=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String query = "Select * from clients where id = @id";

					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clientInfo.id = "" + reader.GetInt32(0);
								clientInfo.name =  reader.GetString(1);
								clientInfo.email = reader.GetString(2);
								clientInfo.phone = reader.GetString(3);
								clientInfo.address = reader.GetString(4);

							}
						}

					}
				}
			}
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost() 
        {
			// Capture form data
			clientInfo.id = Request.Form["id"];
			clientInfo.name = Request.Form["name"];
			clientInfo.email = Request.Form["email"];
			clientInfo.phone = Request.Form["phone"];
			clientInfo.address = Request.Form["address"];


			// Validate fields using string.IsNullOrEmpty()
			if (string.IsNullOrEmpty(clientInfo.id) ||
				string.IsNullOrEmpty(clientInfo.name) ||
				string.IsNullOrEmpty(clientInfo.email) ||
				string.IsNullOrEmpty(clientInfo.phone) ||
				string.IsNullOrEmpty(clientInfo.address))
			{
				errorMessage = "All the fields are required.";
				return;
			}

			try
			{
				String connectionString = "Data Source=.\\SQLEXPRESS;Database =clientCRUD ;Integrated Security=True;MultipleActiveResultSets=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String query = "update clients  SET name =@name, email= @email, phone=@phone, address= @address Where id = @id"; 

					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@name", clientInfo.name);
						command.Parameters.AddWithValue("@email", clientInfo.email);
						command.Parameters.AddWithValue("@phone", clientInfo.phone);
						command.Parameters.AddWithValue("@address", clientInfo.address);
						command.Parameters.AddWithValue("@id", clientInfo.id);

						command.ExecuteNonQuery();

					}
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return;
			}

			Response.Redirect("/Clients/Index");
		}

    }
}
