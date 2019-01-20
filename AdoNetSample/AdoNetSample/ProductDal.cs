using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetSample
{
    public class ProductDal
    {
        SqlConnection connection=new SqlConnection(@"server=(localdb)\mssqllocaldb;initial catalog=ETrade;integrated security=true");
        public List<Product> GetAll()
        {
            if (connection.State==ConnectionState.Closed) //eğer bağlantı kapalıysa aç
            {
               connection.Open();
            }

            SqlCommand command=new SqlCommand("select * from Products",connection); //sql sorgusu

            SqlDataReader reader = command.ExecuteReader(); // sorguyu çalıştırdık

            List<Product> products=new List<Product>();

            while (reader.Read()) //readerdan gelenler tek tek okunur
            {
                Product product = new Product
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    StockAmount = Convert.ToInt32(reader["StockAmount"]),
                    UnitPrice = Convert.ToDecimal(reader["UnitPrice"])
                };

                products.Add(product); //listeye gelen nesneleri ekledik
            }

            reader.Close();
            connection.Close();
            return products; //listeyi geri döndürdük
        }
    }
}
