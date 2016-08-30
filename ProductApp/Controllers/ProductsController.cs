using ProductApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using MySql.Data.MySqlClient;


namespace ProductApp.Controllers
{
    public class ProductsController : ApiController
    {
        MySqlCommand sqlCommand;
        DataTable productsTable = new DataTable();

        private MySqlConnection dbConnection()
        {
            //SQL Connection infomation
            string strConnectInfo = "Server=localhost;User=root;Password=1234;Database=mydb;Pooling=false";
            MySqlConnection sqlConnect = new MySqlConnection(strConnectInfo);
            //Open service
            sqlConnect.Open();
            return sqlConnect;
        }

        public IHttpActionResult Get()
        {
            //String SQL Command
            string strSQL = "SELECT * FROM PRODUCT";

            //Execute SQL command
            using(MySqlDataAdapter dataAdapter = new MySqlDataAdapter(strSQL, dbConnection())) {
                //Fill data into DataTable
                dataAdapter.Fill(productsTable);
            }

            //Init Response Message
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

            string strResponse = "<b1>Hello World!</b><br>";
            
            //Fetch data from DataTable
            foreach (DataRow row in productsTable.Rows)
            {
                var id = row["id"].ToString();
                var name = row["name"].ToString();
                var category = row["category"].ToString();
                var price = row["price"].ToString();

                //Prepare string for response message
                strResponse += (id + " " + name + " " + category + " " + price + "<br>");
            }
            //Add content from string response
            response.Content = new StringContent(strResponse);

            return ResponseMessage(response);
        }
    }
}