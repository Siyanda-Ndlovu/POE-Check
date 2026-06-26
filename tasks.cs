using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace POE_Check
{
    public class tasks
    {
        string connection = @"Data source=(localdb)\MSSQLLocalDB;Database=master";

        public void CreateTableIfNotExists()
        {
            using (SqlConnection connect = new SqlConnection(connection))
            {
                try
                {
                    connect.Open();

                    string createTableQuery = @"
                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='tasks' AND xtype='U')
                        BEGIN
                            CREATE TABLE tasks (
                                task_id INT IDENTITY(1,1) PRIMARY KEY,
                                task_name NVARCHAR(100) NOT NULL,
                                task_description NVARCHAR(255),
                                task_dueDate NVARCHAR(50),
                                task_status NVARCHAR(20)
                            )
                        END";

                    SqlCommand createCommand = new SqlCommand(createTableQuery, connect);
                    createCommand.ExecuteNonQuery();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error creating table: " + error.Message);
                }
            }
        }

        public void insert_task(string name, string description, string dueDate, string status)
        {
            using (SqlConnection connects = new SqlConnection(connection))
            {
                connects.Open();

                string query = "INSERT INTO tasks (task_name, task_description, task_dueDate, task_status) VALUES (@name, @description, @dueDate, @status)";

                SqlCommand run_query = new SqlCommand(query, connects);
                run_query.Parameters.AddWithValue("@name", name);
                run_query.Parameters.AddWithValue("@description", description);
                run_query.Parameters.AddWithValue("@dueDate", dueDate);
                run_query.Parameters.AddWithValue("@status", status);
                run_query.ExecuteNonQuery();
            }
        }

        public void update_latest_task_reminder(string name, string dueDate)
        {
            using (SqlConnection connects = new SqlConnection(connection))
            {
                connects.Open();

                string query = @"
                    UPDATE tasks
                    SET task_dueDate = @dueDate,
                        task_status = 'pending'
                    WHERE task_id = (
                        SELECT TOP 1 task_id
                        FROM tasks
                        WHERE task_name = @name
                        ORDER BY task_id DESC
                    )";

                SqlCommand run_query = new SqlCommand(query, connects);
                run_query.Parameters.AddWithValue("@name", name);
                run_query.Parameters.AddWithValue("@dueDate", dueDate);
                run_query.ExecuteNonQuery();
            }
        }

        public void load_tasks(ListView view_task)
        {
            view_task.Items.Clear();

            using (SqlConnection connects = new SqlConnection(connection))
            {
                connects.Open();

                string query = "SELECT * FROM tasks ORDER BY task_id DESC";
                SqlCommand run_query = new SqlCommand(query, connects);
                SqlDataReader data_collect = run_query.ExecuteReader();

                bool data_found = false;

                while (data_collect.Read())
                {
                    data_found = true;

                    string task_id = data_collect["task_id"].ToString();
                    string task_name = data_collect["task_name"].ToString();
                    string task_description = data_collect["task_description"].ToString();
                    string task_dueDate = data_collect["task_dueDate"].ToString();
                    string task_status = data_collect["task_status"].ToString();

                    view_task.Items.Add(task_id + " " + task_name + " with " + task_description + " due on " + task_dueDate + " and is " + task_status);
                }

                if (!data_found)
                {
                    view_task.Items.Add("No task found");
                }
            }
        }

        public void update_taskStatus(int id)
        {
            using (SqlConnection connects = new SqlConnection(connection))
            {
                connects.Open();

                string query = "UPDATE tasks SET task_status='done' WHERE task_id=@id";
                SqlCommand run_query = new SqlCommand(query, connects);
                run_query.Parameters.AddWithValue("@id", id);
                run_query.ExecuteNonQuery();
            }
        }

        public void delete_task(int id)
        {
            using (SqlConnection connects = new SqlConnection(connection))
            {
                connects.Open();

                string query = "DELETE FROM tasks WHERE task_id=@id";
                SqlCommand run_query = new SqlCommand(query, connects);
                run_query.Parameters.AddWithValue("@id", id);
                run_query.ExecuteNonQuery();
            }
        }
    }
}
