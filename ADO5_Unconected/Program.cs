using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO5_Unconected
{
    internal class Program
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
        private static DataSet dataSet;
        private static SqlDataAdapter dataAdapter;
        private static DataTable dataTable;

        public static void AddInfo(string[] values)
        {
            dataSet = new DataSet();
            
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                dataAdapter = new SqlDataAdapter("Select * from Info", connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(dataAdapter);

                dataAdapter.InsertCommand = builder.GetInsertCommand();
                dataAdapter.UpdateCommand = builder.GetUpdateCommand();
                dataAdapter.DeleteCommand = builder.GetDeleteCommand();

                dataAdapter.Fill(dataSet, "Info");
                dataTable = dataSet.Tables["Info"];

                if (dataTable.Columns.Count - 1 != values.Length)
                {
                    Console.WriteLine("You have too much or less values in your array");
                    return;
                }

                DataRow newRow = dataTable.NewRow();

                newRow["CoffeName"] = values[0];
                newRow["Country"] = values[1];
                newRow["Type"] = values[2];
                newRow["Description"] = values[3];
                newRow["Gramm"] = Convert.ToInt32(values[4]);
                newRow["Cost"] = Convert.ToDecimal(values[5]);

                dataTable.Rows.Add(newRow);
                dataAdapter.Update(dataTable);
                Console.WriteLine("Data was inserted successfully!");

            }
        }

        public static void AlterInfo(string columnName, object value, int id)
        {
            dataSet = new DataSet();

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                dataAdapter = new SqlDataAdapter($"Select * from Info where Id = {id}", connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(dataAdapter);

                dataAdapter.UpdateCommand = builder.GetUpdateCommand();

                dataAdapter.Fill(dataSet, "Info");
                dataTable = dataSet.Tables["Info"];

                if (!dataTable.Columns.Contains(columnName))
                {
                    Console.WriteLine("Column is not exists");
                    return;
                }

                if (dataTable.Rows.Count == 0)
                {
                    Console.WriteLine("Id not found");
                    return;
                }

                DataRow row = dataTable.Rows[0];


                Type type = dataTable.Columns[columnName].DataType;

                if (value.GetType() != type)
                {
                    Console.WriteLine("Wrong type of value");
                    return;
                }

                row[columnName] = value;

                dataAdapter.Update(dataTable);
                Console.WriteLine("Data was inserted successfully!");

            }
        }

        public static void DeleteInfo(object value, string columnName)
        {
            dataSet = new DataSet();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string query = $"Select * from Info where {columnName} = @value";

                dataAdapter = new SqlDataAdapter(query, connection);

                dataAdapter.SelectCommand.Parameters.AddWithValue("@value", value);

                SqlCommandBuilder builder = new SqlCommandBuilder(dataAdapter);

                dataAdapter.DeleteCommand = builder.GetDeleteCommand();

                dataAdapter.Fill(dataSet, "Info");
                dataTable = dataSet.Tables["Info"];

                if (dataTable.Rows.Count == 0)
                {
                    Console.WriteLine("value not found");
                    return;
                }

                DataRow row = dataTable.Rows[0];

                row.Delete();
                dataAdapter.Update(dataTable);
                Console.WriteLine("Data was deleted successfully!");

            }
        }

        public static void ShowAllInfo()
        {
            dataSet = new DataSet();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                dataAdapter = new SqlDataAdapter("SELECT * FROM Info", connection);

                dataAdapter.Fill(dataSet, "Info");

                dataTable = dataSet.Tables["Info"];

                foreach (DataRow row in dataTable.Rows)
                {
                    Console.WriteLine(
                        $"Id: {row["Id"]}, " +
                        $"Coffee: {row["CoffeName"]}, " +
                        $"Country: {row["Country"]}, " +
                        $"Type: {row["Type"]}, " +
                        $"Description: {row["Description"]}, " +
                        $"Gramm: {row["Gramm"]}, " +
                        $"Cost: {row["Cost"]}"
                    );
                }
            }
        }
        public static void ShowCoffeeNames()
        {
            dataSet = new DataSet();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                dataAdapter = new SqlDataAdapter(
                    "SELECT CoffeName FROM Info",
                    connection
                );

                dataAdapter.Fill(dataSet, "Info");

                dataTable = dataSet.Tables["Info"];

                foreach (DataRow row in dataTable.Rows)
                {
                    Console.WriteLine(row["CoffeName"]);
                }
            }
        }

        public static void ShowArabica()
        {
            dataSet = new DataSet();

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                dataAdapter = new SqlDataAdapter("SELECT * FROM Info where CoffeName = 'Arabica'", connection);

                dataAdapter.Fill(dataSet, "Info");

                dataTable = dataSet.Tables["Info"];

                foreach (DataRow row in dataTable.Rows)
                {
                    Console.WriteLine(
                        $"Id: {row["Id"]}, " +
                        $"Coffee: {row["CoffeName"]}, " +
                        $"Country: {row["Country"]}, " +
                        $"Type: {row["Type"]}, " +
                        $"Description: {row["Description"]}, " +
                        $"Gramm: {row["Gramm"]}, " +
                        $"Cost: {row["Cost"]}"
                    );
                }

            }
        }

        public static void ShowRobusta()
        {
            dataSet = new DataSet();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                dataAdapter = new SqlDataAdapter(
                    "SELECT * FROM Info WHERE Type = 'Robusta'",
                    connection
                );

                dataAdapter.Fill(dataSet, "Info");

                dataTable = dataSet.Tables["Info"];

                foreach (DataRow row in dataTable.Rows)
                {
                    Console.WriteLine(
                        $"Id: {row["Id"]}, " +
                        $"Coffee: {row["CoffeName"]}, " +
                        $"Country: {row["Country"]}, " +
                        $"Type: {row["Type"]}, " +
                        $"Description: {row["Description"]}, " +
                        $"Gramm: {row["Gramm"]}, " +
                        $"Cost: {row["Cost"]}"
                    );
                }
            }
        }

        public static void ShowBlend()
        {
            dataSet = new DataSet();

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                dataAdapter = new SqlDataAdapter("SELECT * FROM Info WHERE Type = 'Blend'", connection);
                dataAdapter.Fill(dataSet, "Info");

                dataTable = dataSet.Tables["Info"];

                foreach (DataRow row in dataTable.Rows)
                {
                    Console.WriteLine(
                        $"Id: {row["Id"]}, " +
                        $"Coffee: {row["CoffeName"]}, " +
                        $"Country: {row["Country"]}, " +
                        $"Type: {row["Type"]}, " +
                        $"Description: {row["Description"]}, " +
                        $"Gramm: {row["Gramm"]}, " +
                        $"Cost: {row["Cost"]}"
                    );
                }

            }
        }

        public static void Show200Gramm()
        {
            dataSet = new DataSet();
            
            using( SqlConnection connection = new SqlConnection(connectionString))
            {
                dataAdapter = new SqlDataAdapter("SELECT * FROM Info where Gramm <=200", connection);
                dataAdapter.Fill(dataSet, "Info");

                dataTable = dataSet.Tables["Info"];

                foreach (DataRow row in dataTable.Rows)
                {
                    Console.WriteLine(
                        $"Id: {row["Id"]}, " +
                        $"Coffee: {row["CoffeName"]}, " +
                        $"Country: {row["Country"]}, " +
                        $"Type: {row["Type"]}, " +
                        $"Description: {row["Description"]}, " +
                        $"Gramm: {row["Gramm"]}, " +
                        $"Cost: {row["Cost"]}"
                    );
                }
            }
        }

        public static void ShowMinCost()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(
                    "SELECT MIN(Cost) FROM Info",
                    connection
                );

                object result = command.ExecuteScalar();

                Console.WriteLine($"Minimum cost: {result}");
            }
        }

        public static void ShowMaxCost()
        {
            dataSet = new DataSet();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                dataAdapter = new SqlDataAdapter(
                    "SELECT MAX(Cost) AS MaxCost FROM Info",
                    connection
                );

                dataAdapter.Fill(dataSet, "Info");

                dataTable = dataSet.Tables["Info"];

                Console.WriteLine(dataTable.Rows[0]["MaxCost"]);
            }
        }
    
        public static void ShowAverageCost()
        {
            dataSet = new DataSet();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                dataAdapter = new SqlDataAdapter(
                    "SELECT AVG(Cost) AS AvgCost FROM Info",
                    connection
                );

                dataAdapter.Fill(dataSet, "Info");

                dataTable = dataSet.Tables["Info"];

                Console.WriteLine(dataTable.Rows[0]["AvgCost"]);
            }
        }

        public static void ShowMinCostCount()
        {
            dataSet = new DataSet();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                dataAdapter = new SqlDataAdapter(
                    "SELECT COUNT(*) AS MinCostCount FROM Info WHERE Cost = (SELECT MIN(Cost) FROM Info)",
                    connection
                );

                dataAdapter.Fill(dataSet, "Info");

                dataTable = dataSet.Tables["Info"];

                Console.WriteLine(dataTable.Rows[0]["MinCostCount"]);
            }
        }

        public static void ShowMaxCostCount()
        {
            dataSet = new DataSet();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                dataAdapter = new SqlDataAdapter(
                    "SELECT COUNT(*) AS MaxCostCount FROM Info WHERE Cost = (SELECT MAX(Cost) FROM Info)",
                    connection
                );

                dataAdapter.Fill(dataSet, "Info");

                dataTable = dataSet.Tables["Info"];

                Console.WriteLine(dataTable.Rows[0]["MaxCostCount"]);
            }
        }

        public static void ShowCoffeeTypesCount()
        {
            dataSet = new DataSet();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                dataAdapter = new SqlDataAdapter(
                    "SELECT Type, COUNT(*) AS Amount FROM Info GROUP BY Type",
                    connection
                );

                dataAdapter.Fill(dataSet, "Info");

                dataTable = dataSet.Tables["Info"];

                foreach (DataRow row in dataTable.Rows)
                {
                    Console.WriteLine(
                        $"{row["Type"]}: {row["Amount"]}"
                    );
                }
            }
        }


        static void Main(string[] args)
        {
            //string[] values = { "Arabica", "Brazil", "Coffee", "Strong", "500", "12,50" };

            //AddInfo(values);

            //AlterInfo("Gramm", 1000, 1);

            //DeleteInfo(1000, "Gramm");

            Console.WriteLine("All coffee information:");
            ShowAllInfo();

            Console.WriteLine("\nCoffee names:");
            ShowCoffeeNames();

            Console.WriteLine("\nArabica coffe:");
            ShowArabica();

            Console.WriteLine("\nRobusta coffe:");
            ShowRobusta();

            Console.WriteLine("\nBlend coffe:");
            ShowBlend();

            Console.WriteLine("\nGramm less 200:");
            Show200Gramm();

            Console.WriteLine("\nMin Cost:");
            ShowMinCost();

            Console.WriteLine("\nMaximum coffee cost:");
            ShowMaxCost();

            Console.WriteLine("\nAverage coffee cost:");
            ShowAverageCost();

            Console.WriteLine("\nCount of coffee with minimum cost:");
            ShowMinCostCount();

            Console.WriteLine("\nCount of coffee with maximum cost:");
            ShowMaxCostCount();

            Console.WriteLine("\nCount of Arabica, Robusta and Blend:");
            ShowCoffeeTypesCount();





        }
    }
}




/*
CREATE DATABASE Coffeshop
go

Use Coffeshop
go

create Table Info(
Id int not null primary key identity(1,1),
CoffeName nvarchar(100) not null check(CoffeName <> ''),
Country nvarchar(100) not null check(Country <> ''),
[Type] nvarchar(100) not null check([Type] <> ''),
Description nvarchar(200) not null check(Description <> ''),
Gramm int not null check(Gramm>0),
Cost decimal not null check(Cost>0)
)
 */
