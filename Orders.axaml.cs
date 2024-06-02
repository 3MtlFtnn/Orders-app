using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using MsBox.Avalonia;
using MySql.Data.MySqlClient;

namespace MyApp;

public partial class Orders : Window
{
    private string _Login;
    public Orders(string user)
    {
        _Login = user;
        InitializeComponent();
        if(orders == null){
            Orders_text.Text = "Error: orders not found";
        }else{
            LoadOrders();
        }
    }

    public async void Search_func(object sender, RoutedEventArgs e)
    {
        string connectionString = "Server=192.168.192.155;Port=3306;Database=OrdersApp;Uid=root;Pwd=220819998008Max;";
        string selectQuery = "SELECT o.EqType, o.EqModel, o.ProblemDesc, o.StartDate, o.Completion_Date, u.FIO FROM Orders o JOIN Users u ON o.Client_ID = u.User_ID WHERE o.Request_ID = @Request_ID";

        int requestId = int.Parse(Search_text.Text); 

        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Request_ID", requestId);

                    connection.Open();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string eqType = reader.GetString("EqType");
                            string eqModel = reader.GetString("EqModel");
                            string problemDesc = reader.GetString("ProblemDesc");
                            string startDate = reader.GetDateTime("StartDate").ToString("yyyy-MM-dd");
                            string completionDate = reader.GetDateTime("Completion_Date").ToString("yyyy-MM-dd");
                            string userName = reader.GetString("FIO");
                            Window windows_preview = new OrderPreview(eqType,  eqModel,  problemDesc, userName, startDate,  completionDate, _Login);
                            windows_preview.Show();                        
                        }
                        else
                        {
                            var box = MessageBoxManager.GetMessageBoxStandard("Caption", "Нет совпадений");
                            await box.ShowAsync();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
    public void LoadOrders(){
        string connectionString = "Server=192.168.192.155;Port=3306;Database=OrdersApp;Uid=root;Pwd=220819998008Max;";
        string selectQuery = "SELECT eqModel, EqType, Request_ID FROM Orders";

        try{
            using(MySqlConnection connection = new MySqlConnection(connectionString)){
                using(MySqlCommand command = new MySqlCommand(selectQuery, connection)){
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader()){
                        while (reader.Read()){
                            string eqModel = reader.GetString("EqModel");
                            string eqType = reader.GetString("EqType");
                            int Request_ID = reader.GetInt32("Request_ID");
                            orders.Items.Add($" {Request_ID} - {eqType} - {eqModel} ");
                        }
                    }
                }
            }
        }catch (Exception ex){
            Console.WriteLine("Error: "+ex.Message);
        }
    }
    public void Updateclick(object sender, RoutedEventArgs e){
        orders.Items.Clear();
        LoadOrders();
    }

    public void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
       
        if (sender == null)
        {
            Console.WriteLine("Error: sender is null in ListBox_SelectionChanged.");
            return;
        }

        var listBox = sender as ListBox;

        if (listBox?.SelectedItem != null)
        {
            var selectedOrder = listBox.SelectedItem.ToString();
            string[] parts = selectedOrder.Split(" - ");

            if (parts.Length < 2)
            {
                Console.WriteLine("Ошибка: ожидаемая строка формата 'Тип - Модель', но получена: " + selectedOrder);
                return;
            }
            if(!int.TryParse(parts[0].Trim(), out int Request_ID)){
                Console.WriteLine("Не удалось преобразовать");
            }
            string eqType = parts[1].Trim();
            string eqModel = parts[2].Trim();

            LoadOrderDetailsFromDatabase(eqType, eqModel, Request_ID);
        }
    }

    public void LoadOrderDetailsFromDatabase(string eqType, string eqModel, int Request_ID)
    {
        string connectionString = "Server=192.168.192.155;Port=3306;Database=OrdersApp;Uid=root;Pwd=220819998008Max;";
        string selectQuery = @"SELECT o.ProblemDesc, o.StartDate, o.Completion_Date, u.FIO 
                            FROM Orders o 
                            JOIN Users u ON o.Client_ID = u.User_ID 
                            WHERE o.EqType = @EqType AND o.Request_ID = @Request_ID AND o.EqModel = @EqModel";
        
        try{
            using (var connection = new MySqlConnection(connectionString))
            {
                using (var command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@EqType", eqType);
                    command.Parameters.AddWithValue("@EqModel", eqModel);
                    command.Parameters.AddWithValue("@Request_ID", Request_ID);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string problemDesc = reader.IsDBNull(reader.GetOrdinal("ProblemDesc")) ? string.Empty : reader.GetString("ProblemDesc");
                            string startDate = reader.IsDBNull(reader.GetOrdinal("StartDate")) ? string.Empty : reader.GetDateTime("StartDate").ToString("yyyy-MM-dd");
                            string completionDate = reader.IsDBNull(reader.GetOrdinal("Completion_Date")) ? string.Empty : reader.GetDateTime("Completion_Date").ToString("yyyy-MM-dd");
                            string userName = reader.IsDBNull(reader.GetOrdinal("FIO")) ? string.Empty : reader.GetString("FIO");
                            Console.WriteLine($"eqType: {eqType}, eqModel: {eqModel}, problemDesc: {problemDesc}, userName: {userName}, startDate: {startDate}, completionDate: {completionDate}");

                            if (eqType != null && eqModel != null && problemDesc != null && userName != null && startDate != null && completionDate != null)
                            {
                                OrderPreview infoWindow = new OrderPreview(eqType, eqModel, problemDesc, userName, startDate, completionDate, _Login);
                                infoWindow.SizeToContent = SizeToContent.WidthAndHeight;
                                infoWindow.ShowDialog(this);
                     
                            }
                            else
                            {
                                Console.WriteLine("Одно из полей равно null.");
                            }
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка при загрузке деталей заказа из базы данных: " + ex.Message);
        }
    
        
    }

    public void createOrderButton(object sender, RoutedEventArgs e){
       // Window newWindow = new createOrderWindow();
        //newWindow.Show();
    }
    private void back(object sender, RoutedEventArgs e){
        var main_win = new MainWindow();
        main_win.Show();
        this.Close();
    }
}