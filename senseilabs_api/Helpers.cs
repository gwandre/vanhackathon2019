using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Text.RegularExpressions;

namespace senseilabs_api
{
    public class Helpers
    {
        /*
         * Return SQL Server Connection String securely stored into the System Variables
         */
        public static string GetRDSConnectionString()
        {
            string dbname = System.Environment.GetEnvironmentVariable("vanhackathon2019_RDS_DB_NAME");

            if (string.IsNullOrEmpty(dbname)) return null;

            string username = System.Environment.GetEnvironmentVariable("vanhackathon2019_RDS_USERNAME");
            string password = System.Environment.GetEnvironmentVariable("vanhackathon2019_RDS_PASSWORD");
            string hostname = System.Environment.GetEnvironmentVariable("vanhackathon2019_RDS_HOSTNAME");
            string port = System.Environment.GetEnvironmentVariable("vanhackathon2019_RDS_PORT");
            
            return "Data Source=" + hostname + ";Initial Catalog=" + dbname + ";User ID=" + username + ";Password=" + password + ";";
        }

        /*
         * Clear the input string to prevent SQL Injection and get a "clear" Business Id
         */
        public static string clearBusinessNumber(string businessNumber)
        {
            return Regex.Replace(businessNumber, @"[^a-zA-Z0-9]", "").ToUpper();
        }

        /*
         * Calculate the innovation rate
         */
        public static double innovationRate(int numberOfInnovations, int numberOfProducts)
        {
            return Math.Round((((double)numberOfInnovations / (double)numberOfProducts) * 100), 1);
        }
    }
}
