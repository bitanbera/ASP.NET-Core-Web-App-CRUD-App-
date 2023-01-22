using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CRUD_App1_bb.Pages.Student
{
    public class IndexModel : PageModel
    {
        public List<StudentInfo> listStudents= new List<StudentInfo>();
        public void OnGet()
        {
            if (listStudents.Count > 0)
            {
                listStudents.Clear();
            }
            try
            {
                string connectionString = "Data Source=LAPTOP-C35DMLHN;Initial Catalog=bb;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM student";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentInfo studentinfo = new StudentInfo();

                                studentinfo.id = "" + reader.GetInt32(0);
                                studentinfo.name = reader.GetString(1);
                                studentinfo.email = reader.GetString(2);
                                studentinfo.phone = reader.GetString(3);
                                studentinfo.address = reader.GetString(4);
                                studentinfo.created_at= reader.GetDateTime(5).ToString();

                                listStudents.Add(studentinfo);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public class StudentInfo
    {
        public string id;
        public string name;
        public string email;
        public string phone;
        public string address;
        public string created_at;
    }
}
