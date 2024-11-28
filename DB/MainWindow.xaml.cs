using Microsoft.Data.SqlClient;
using System;
using System.Data.Common;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Microsoft.IdentityModel.Tokens;

namespace DB
{
    public partial class MainWindow : Window
    {
        private string connectionString;
        private SqlConnection _connection;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            string serverName = @"DBSRV\ag2024";
            string databaseName = DatabaseName_TextBox.Text;
            if (string.IsNullOrEmpty(DatabaseName_TextBox.Text))
            {
                databaseName = "BoryakinPA_LAB1";
            }

            connectionString = $"Server={serverName};Database={databaseName};Integrated Security=True;TrustServerCertificate=True;";


            try
            {
                _connection = new SqlConnection(connectionString);
                _connection.Open();
                ConnectionStatusText.Text = "Подключен";
                LoadDatabases();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка соединения: {ex.Message}");
            }
        }

        private void LoadDatabases()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT name FROM sys.databases";
                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DatabaseComboBox.Items.Clear();

                        while (reader.Read())
                        {
                            DatabaseComboBox.Items.Add(reader["name"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки баз данных: {ex.Message}");
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ShowTables_Click(object sender, RoutedEventArgs e)
        {
            // Логика для отображения таблиц
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Да поможет вам Бог с этим приложением");
        }

        private void LoadTables_Click(object sender, RoutedEventArgs e)
        {
            if (_connection == null || _connection.State != ConnectionState.Open)
            {
                MessageBox.Show("Сперва нужно подключиться");
                return;
            }

            try
            {
                DataTable tables = _connection.GetSchema("Tables");
                TablesListBox.Items.Clear();

                foreach (DataRow row in tables.Rows)
                {
                    TablesListBox.Items.Add(row["TABLE_NAME"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки таблиц: {ex.Message}");
            }
        }

        private void ShowTableData_Click(object sender, RoutedEventArgs e)
        {
            if (TablesListBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите таблицу");
                return;
            }

            string selectedTable = TablesListBox.SelectedItem.ToString();
            string query = $"SELECT * FROM [{selectedTable}]";

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, _connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                TableDataGrid.ItemsSource = dataTable.DefaultView;

                string primaryKeyQuery = @"
            SELECT COLUMN_NAME 
            FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC 
            JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KU 
            ON TC.CONSTRAINT_TYPE = 'PRIMARY KEY' 
            AND TC.CONSTRAINT_NAME = KU.CONSTRAINT_NAME 
            WHERE TC.TABLE_NAME = @tableName";

                using (SqlCommand command = new SqlCommand(primaryKeyQuery, _connection))
                {
                    command.Parameters.AddWithValue("@tableName", selectedTable);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        PrimaryKeyListBox.Items.Clear();

                        while (reader.Read())
                        {
                            PrimaryKeyListBox.Items.Add(reader["COLUMN_NAME"].ToString());
                        }
                    }
                }

                TableDetailsText.Text = $"Выбранная таблица: {selectedTable}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных таблицы: {ex.Message}");
            }
        }



        private void DatabaseComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatabaseComboBox.SelectedItem == null)
            {
                return;
            }

            string selectedDatabase = DatabaseComboBox.SelectedItem.ToString();
            try
            {
                _connection.ChangeDatabase(selectedDatabase);
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Ошибка смены базы данных: {ex.Message}\nВозможно база данных Вам не принадлежит");
            }

            LoadTables();
        }

        private void LoadTables()
        {
            try
            {
                DataTable tables = _connection.GetSchema("Tables");

                TablesListBox.Items.Clear();

                foreach (DataRow row in tables.Rows)
                {
                    TablesListBox.Items.Add(row["TABLE_NAME"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных таблицы: {ex.Message}");
            }
        }

        private void TablesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Логика для обработки изменения выбора таблицы
        }
    }
}
