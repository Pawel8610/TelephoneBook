using System.Data.SqlClient;

namespace TelephoneBook
{
    class Liczba
    {
       public string liczba(string textboxFirst, string textboxLast,string textboxMobile,string textboxEmail)
        {
           // SqlConnection con = new SqlConnection("Data Source=PAWEL-PC\\KURS; Initial Catalog=Phone; Integrated Security=true");
            SqlConnection con = new SqlConnection("Data Source=" + LogIn.ServerName + "; Initial Catalog=Phone; Integrated Security=true");
            con.Open();
            SqlCommand comm = new SqlCommand("SELECT ID from Mobiles WHERE (First='" + textboxFirst + "')and(Last='" + textboxLast + "')and(Mobile='" + textboxMobile + "')and(Email='" + textboxEmail + "')", con);
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                return reader.GetInt32(0).ToString();
            }
            else
            {
                return "";
                reader.Close();
            }
        }


    }
}
