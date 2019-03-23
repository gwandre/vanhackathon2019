using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Text;

namespace senseilabs_api.Controllers
{
    [Route("api/[controller]")]
    public class InnovationController : Controller
    {
        // GET api/values
        [HttpGet]
        public Models.Innovation Get()
        {
            double innovationRate = 0;

            // Create a new SqlConnection object with ConnectionString from Helpers class
            using (SqlConnection connection = new SqlConnection(Helpers.GetRDSConnectionString()))
            {
                // Open SQL Server database connection
                connection.Open();

                // Query to read the average Innovation Rate from database considering last month from current date
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT round((sum(((CAST(numberOfInnovations as Float) / CAST(numberOfProducts as Float)) * 100)) / count(*)), 1) as innovationRate");
                sb.Append("    FROM tbInnovationData");
                sb.Append("    WHERE [year] = year(dateAdd(month, -1, getDate()))");
                sb.Append("        AND [month] = month(dateAdd(month, -1, getDate()))");
                String sql = sb.ToString();

                // Define a SqlCommand to run the query
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // If the data reader has data to read
                        if (reader.HasRows)
                        {
                            // Read the next data
                            reader.Read();

                            // Set the innovationRate based on data from database
                            innovationRate = reader.GetDouble(0);
                        }
                    }
                }
            }
            
            // Return the innovation rate as a new object from Models
            return new Models.Innovation(innovationRate);
        }

        // GET api/values/{businessNumber}
        [HttpGet("{businessNumber}")]
        public Models.Innovation Get(string businessNumber)
        {
            double innovationRate = 0;

            // Create a new SqlConnection object with ConnectionString from Helpers class
            using (SqlConnection connection = new SqlConnection(Helpers.GetRDSConnectionString()))
            {
                // Open SQL Server database connection
                connection.Open();

                // Query to read the average Innovation Rate from database considering last month from current date
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT numberOfInnovations, numberOfProducts");
                sb.Append("    FROM tbInnovationData");
                sb.Append(String.Format("    WHERE businessNumber='{0}'", Helpers.clearBusinessNumber(businessNumber)));
                sb.Append("        AND [year] = year(dateAdd(month, -1, getDate()))");
                sb.Append("        AND [month] = month(dateAdd(month, -1, getDate()))");
                String sql = sb.ToString();

                // Define a SqlCommand to run the query
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // If the data reader has data to read
                        if (reader.HasRows)
                        {
                            // Read the next data
                            reader.Read();

                            // Calculate the innovationRate based on data from database calling a Helpers calss function
                            innovationRate = Helpers.innovationRate(reader.GetInt32(0), reader.GetInt32(1));
                        }
                    }
                }
            }

            // Return the innovation rate as a new object from Models
            return new Models.Innovation(innovationRate);
        }

        // POST api/values
        [HttpPost]
        public Models.Innovation Post([FromBody] Models.InnovationData reqParms)
        {
            int numberOfInnovations = reqParms.numberOfInnovations;
            int numberOfProducts = reqParms.numberOfProducts;

            // Create a new SqlConnection object with ConnectionString from Helpers class
            using (SqlConnection connection = new SqlConnection(Helpers.GetRDSConnectionString()))
            {
                // Open SQL Server database connection
                connection.Open();

                // Query to insert/update innovation data on database
                StringBuilder sb = new StringBuilder();
                sb.Append("declare");
                sb.Append("	@businessNumber as nvarchar(15),");
                sb.Append("	@year as int,");
                sb.Append("	@month as int,");
                sb.Append("	@numberOfInnovations as int,");
                sb.Append("	@numberOfProducts as int;");

                sb.Append(String.Format("set @businessNumber='{0}';", Helpers.clearBusinessNumber(reqParms.businessNumber)));
                sb.Append(String.Format("set @year={0};", reqParms.year.ToString()));
                sb.Append(String.Format("set @month={0};", reqParms.month.ToString()));
                sb.Append(String.Format("set @numberOfInnovations = {0};", numberOfInnovations));
                sb.Append(String.Format("set @numberOfProducts = {0};", numberOfProducts));

                sb.Append("if exists (select * from tbInnovationData where businessNumber=@businessNumber and [year]=@year and [month]=@month) begin;");
                sb.Append("	update tbInnovationData");
                sb.Append("		set numberOfInnovations=@numberOfInnovations,");
                sb.Append("			numberOfProducts=@numberOfProducts");
                sb.Append("		where businessNumber=@businessNumber");
                sb.Append("			and [year]=@year");
                sb.Append("			and [month]=@month;");
                sb.Append("end;");
                sb.Append("else begin;");
                sb.Append("   insert into tbInnovationData (businessNumber,[year],[month],numberOfInnovations,numberOfProducts)");
                sb.Append("	values (@businessNumber, @year, @month, @numberOfInnovations, @numberOfProducts);");
                sb.Append("end;");
                String sql = sb.ToString();

                // Define a SqlCommand to run the query
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // Execute NonQuery command into database
                    command.BeginExecuteNonQuery();
                }
            }

            return new Models.Innovation(numberOfInnovations, numberOfProducts);
        }
    }
}
