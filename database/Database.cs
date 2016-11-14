using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Collections;
using System.Windows.Forms;


// klasa, która zapewnia komunikację DB (procedury składowane)

namespace Astromal.SQL
{
    class Database
    {
        // pola klasy Database

        static SqlConnection cs;

        private SqlConnection conn = Connection;

        private SqlCommand cmd;

        private SqlDataReader Reader = null; 


        // connection string
        private static SqlConnection Connection
        {
            get
            {
                if (cs == null)
                {
                    // Connection string WINDOWS AUTH.
                    //string conn_string = "Data Source=localhost;Initial Catalog=DodatkiEPDM;Integrated Security=True";

                    //Connection string Astromal
                    string conn_string = "Server=WIN-CQ5CIER3QCD;Database=DodatkiEPDM;User Id=sa;Password=AMalPDM100$$$";

                    cs = new SqlConnection(@conn_string);
                }
                return cs;
            }
        }
        
 // otwarcie połączenia
        private void OpenConnection()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
            }
            catch
            {
                MessageBox.Show("Brak połączenia z bazą danych !");
            }
        }

        // zamknięcie połączenia
        private void CloseConnection()
        {
            if (conn.State == ConnectionState.Open)
            conn.Close();
        }

        // dodaje parametr do zapytania
        public void AddQueryParam(string param, object value)
        {
            cmd.Parameters.AddWithValue(param, value);
        }

        // Inicjacja zapytania (procedura składowana) - 1szy step
        public void ExecQuery(string proc)
        {
            cmd = new SqlCommand();
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = proc;
            cmd.Connection = conn;
        }

        // czytanie wartości zmiennych >> zwraca typ obj
        public object ReadVariable(string col = null)
        {
            object obj = null;

            try
            {
                if (col == null)
                    obj = Reader[0];
                else
                    obj = Reader[col];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            return obj;
        }

        // sprawdzanie czy istnieje rekord i pobieranie jego (tylko dla metody ReadVariable())
        public bool GetNextRow()
        {
            bool check = false;

            try
            {
                if (Reader == null)
                {
                    OpenConnection();
                    Reader = cmd.ExecuteReader();
                    check = Reader.Read();
                }
                else
                    check = Reader.Read();

                if (check == false && Reader != null)
                {
                    if (!Reader.IsClosed) Reader.Close();
                    cmd.Dispose();
                    CloseConnection();
                }
            }
            catch (Exception ex)
            {  
                check = false;
                MessageBox.Show(ex.Message.ToString());
            }

            return check;
        }
    // metoda, która zwraca wynik zapytania w postaci DataTable, tu nie trzeba używać metody GetNextRow() 
        public DataTable ReadTable()
        {
            DataTable dt = new DataTable();

            try
            {
                OpenConnection();
                Reader = cmd.ExecuteReader();
                dt.Load(Reader);

                if (!Reader.IsClosed) Reader.Close();
                cmd.Dispose();
                CloseConnection();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            return dt;
        }

        // metoda która potwierdza wykonanie INSERT / UPDATE / DELETE >> nie zwraca rekordów tylko true lub false
        public bool ConfirmQuery()
        {
            bool flag = false;
            try
            {
                OpenConnection();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                CloseConnection();
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
                MessageBox.Show(ex.Message.ToString());
            }

            return flag;
        }

        // metoda która potwierdza wykonanie INSERT / UPDATE / DELETE >> zwraca 1-szą kolumnę z 1-go wiersza
        public bool ConfirmQueryWithMessage(out string message)
        {
            message = "";
            bool flag = false;

            try
            {
                OpenConnection();
                message = cmd.ExecuteScalar().ToString();
                cmd.Dispose();
                CloseConnection();
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
                MessageBox.Show(ex.Message.ToString());
            }

            return flag;
        }

    }
}
