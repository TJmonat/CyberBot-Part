using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace POE2
{
   
        public class TestConnection

        {
        //This class is used to test and check if MySql linked to our visual studio
            public static void Test()

            {

                DatabaseConnection db = new DatabaseConnection();

                try

                {

                    MySqlConnection connection = db.GetConnection();

                    connection.Open();

                    MessageBox.Show(

                        "Connected to MySQL successfully!",

                        "Database",

                        MessageBoxButton.OK,

                        MessageBoxImage.Information);

                    connection.Close();

                }

                catch (Exception ex)

                {

                    MessageBox.Show(

                        ex.Message,

                        "Connection Failed",

                        MessageBoxButton.OK,

                        MessageBoxImage.Error);

                }

            }
        }
}

