using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows;
namespace POE2
{
    
    public class DatabaseConnection
    {
        private string connectionstring = "server=localhost;database=CyberAwarenessBot;uid=root;password=1234578Tm@";
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionstring);
        }
        public bool AddTask(string title, string description, string reminder, string status)

        {

            try

            {

                using (MySqlConnection conn = new MySqlConnection(connectionstring))

                {

                    conn.Open();

                    string query =

                    @"INSERT INTO Tasks

            (Title, Description, Reminder, Status)

            VALUES

            (@title,@description,@reminder,@status)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@title", title);

                    cmd.Parameters.AddWithValue("@description", description);

                    cmd.Parameters.AddWithValue("@reminder", reminder);

                    cmd.Parameters.AddWithValue("@status", status);

                    cmd.ExecuteNonQuery();

                    return true;

                }

            }

            catch (Exception ex)

            {

                MessageBox.Show(ex.Message);

                return false;

            }
        }
        public List<Task> GetTasks()

        {

            List<Task> tasks = new List<Task>();

            try

            {

                using (MySqlConnection conn = new MySqlConnection(connectionstring))

                {

                    conn.Open();

                    string query = "SELECT * FROM Tasks";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())

                    {

                        Task task = new Task();

                        task.TaskID = reader.GetInt32("TaskID");

                        task.Title = reader.GetString("Title");

                        task.Description = reader.GetString("Description");

                        task.Reminder = reader.GetString("Reminder");

                        task.Status = reader.GetString("Status");

                        tasks.Add(task);

                    }

                }

            }

            catch

            {

            }

            return tasks;

        }
        public bool DeleteTask(string title)

        {

            try

            {

                using (MySqlConnection conn = new MySqlConnection(connectionstring))

                {

                    conn.Open();

                    string query =

                    "DELETE FROM Tasks WHERE Title=@title";

                    MySqlCommand cmd =

                    new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@title", title);

                    cmd.ExecuteNonQuery();

                    return true;

                }

            }

            catch (Exception ex)

            {

                MessageBox.Show(ex.Message);

                return false;

            }

        }

        public bool CompleteTask(string title)

        {

            try

            {

                using (MySqlConnection conn = new MySqlConnection(connectionstring))

                {

                    conn.Open();

                    string query =

                    "UPDATE Tasks SET Status='Completed' WHERE Title=@title";

                    MySqlCommand cmd =

                    new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@title", title);

                    cmd.ExecuteNonQuery();

                    return true;

                }

            }

            catch (Exception ex)

            {

                MessageBox.Show(ex.Message);

                return false;

            }

        }

    }


    }


