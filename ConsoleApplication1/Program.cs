using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        string[] databases =
        {
            "dbo.AssignedDacNumber",
            "dbo.Company",
            "dbo.Contact",
            "dbo.DACDetail",
            "dbo.DACHeader",
            "dbo.DACHistory",
            "dbo.DACMultiplier",
            "dbo.DACOption",
            "dbo.GenerateDistributeFile",
            "dbo.GeneratedPDFDistribution",
            "dbo.JobDetail",
            "dbo.JobDetailHistory",
            "dbo.JobHeader",
            "dbo.JobHeaderHistory",
            "dbo.JobMultiplier",
            "dbo.JobMultiplierHIstory",
            "dbo.JobOption",
            "dbo.JobOptionHistory",
            "dbo.JobHeader",
            "dbo.JobHeaderHistory",
            "dbo.JobMultiplier",
            "dbo.JobMulitiplierHistory",
            "dbo.JobOption",
            "dbo.JobOptionHistory",
            "dbo.OptionDefault",
            "dbo.OrderNote",
            "dbo.ShipToAddress",
            "dbo.UserDefault"
        };
        //static string ServerName, Database, UID, PWD;
        
        static void Main(string[] args)
        {
            const string SERVER_Literal = "Server";
            const string DATABASE_Literal = "Database";
            const string UID_Literal = "Uid";
            const string PWD_Literal = "Pwd";
            const string SEMI_COLON = ";";
            const string WHITESPACE = " ";
            const string EQUAL = "=";
            string connection_setup_file = @"C:\Users\ccole\Documents\Visual Studio 2015\Projects\ConsoleApplication1\ConsoleApplication1\bin\Debug\connection_file.txt";
            string ServerName="", Database="", UID="", PWD="", connectionstr ="", table = "", Databases=""; //Has to be initialized
            Connection_setup(ref connection_setup_file, ref ServerName, ref Database, ref UID, ref PWD, ref Databases, ref table);
       
           
            connectionstr = SERVER_Literal +EQUAL+ ServerName+SEMI_COLON+WHITESPACE+ 
                            DATABASE_Literal+EQUAL+Database+SEMI_COLON+WHITESPACE+
                            UID_Literal+EQUAL+UID+SEMI_COLON+WHITESPACE+
                            PWD_Literal+EQUAL+PWD+SEMI_COLON+WHITESPACE;
            Console.WriteLine("{0}", connectionstr);
            GetData_From_Server(ref connectionstr, ref Databases, ref table);
            
            

            
        }

        private static void Connection_setup(ref string connection_setup_file, 
                                             ref string ServerName, 
                                             ref string Database, 
                                             ref string UID, 
                                             ref string PWD, 
                                             ref string Databases, 
                                             ref string table)
        {
            string text = System.IO.File.ReadAllText(connection_setup_file);
            //System.Console.WriteLine("Contents of WriteText.txt = {0}", text);
            // Display the file contents by using a foreach loop.
            string[] lines = System.IO.File.ReadAllLines(connection_setup_file);
            ServerName = lines[0];
            Database = lines[1];
            UID = lines[2];
            PWD = lines[3];
            Databases = lines[4];
            table = lines[5];
            //Console.WriteLine("{0},{1},{2},{3},{4},{5}", ServerName, Database, UID, PWD, Databases, table);
            //throw new NotImplementedException();
        }


        public static void GetData_From_Server(ref string connectionString, ref string Databases, ref string table)
        {
            table = "dbo." + table;
            string queryString = "SELECT * FROM "+Databases+"."+table;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                //string linqquery = Databases + "." + table;
                //var query = from linqquery select *;
                //Call Read before accessing data
                while(reader.Read())
                {
                    ReadSingleRow((IDataRecord)reader, );
                }
                reader.Close();
            }
        }
        
      
        public static void ReadSingleRow(IDataRecord record, string outputFile)
        {
            Console.WriteLine(String.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", record[0], 
                                                                                      record[1], 
                                                                                      record[2], 
                                                                                      record[3], 
                                                                                      record[4], 
                                                                                      record[5], 
                                                                                      record[6], 
                                                                                      record[7]));
        }



    }
}
