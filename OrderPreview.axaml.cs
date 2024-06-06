using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using MySql.Data.MySqlClient;
using MsBox.Avalonia;
using Avalonia.Layout;
using System.Globalization;
using Avalonia.Media;
namespace MyApp;



public partial class OrderPreview : Window
{
    private string _eqModel;
    private string _selectedOrder;
    private int _Req_id;
    private string Status_;
    TextBox dateTextBox = new TextBox();
    TextBox statusBox = new TextBox();
    public OrderPreview(string selectedOrder, string eqModel, string problem, string userName, string startDate, string completionData, string login, int Request_ID, string status)
    {
        _Req_id = Request_ID;
        
        InitializeComponent();
        _eqModel = eqModel;
        _selectedOrder = selectedOrder;
        if (Variables.Variables.Type_global == "Programmer")
        {
            dateTextBox.Watermark = "Дата в формате (yyyy-MM-dd)";
            dateTextBox.Width = 200;
            statusBox.Watermark = "Статус зявки";
            statusBox.Width = 200;
            Stackus.Children.Add(statusBox);
            Stackus.Children.Add(dateTextBox);
        
            Button submitButton = new Button();
            submitButton.Content = "Сохранить";
            submitButton.Width = 100;
            submitButton.HorizontalAlignment = HorizontalAlignment.Center;
            submitButton.Click += SubmitButton_Click;
            submitButton.Margin = new Thickness(0, 10, 0, 0);
            submitButton.Background = Brushes.AliceBlue;
            Stackus.Children.Add(submitButton);
            this.Content = Stackus;
        }
        if(Info!=null){
            Info.Text = "Тип устройства: "+selectedOrder;
            Info_2.Text = "Модель устройства:" +eqModel;
            Info_3.Text = "Описание проблемы: " + problem;
            Info_4.Text = "Пользователь: "+userName;
            Info_5.Text = "Дата начала работ: "+startDate;
            Info_6.Text = "Дата окончания работ: "+completionData;
            Info_7.Text = "Статус заявки: " + status;
            Delete.Content = "Удалить";
        }else{
            Info.Text = "No orders!"; 
        }
    }
    private async void SubmitButton_Click(object sender, RoutedEventArgs e)
    {
        var box = MessageBoxManager.GetMessageBoxStandard("Уведомление", "Время добавлено!", MsBox.Avalonia.Enums.ButtonEnum.Ok);
        var res = await box.ShowWindowDialogAsync(this);
        string connectionString = "Server=192.168.192.155;Port=3306;Database=OrdersApp;Uid=root;Pwd=220819998008Max";
        string insertQuery = "UPDATE Orders SET Completion_Date = @Completion_Date, Status = @Status WHERE Request_ID = @Request_ID";
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Completion_Date", dateTextBox.Text);
                    command.Parameters.AddWithValue("@Request_ID", _Req_id);
                    command.Parameters.AddWithValue("@Status", statusBox.Text);
                    connection.Open();
                    command.ExecuteNonQuery();
                    if(res == MsBox.Avalonia.Enums.ButtonResult.Ok){
                        this.Close();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private async void DeleteOrder(object sender, RoutedEventArgs e)
    {
        var box = MessageBoxManager.GetMessageBoxStandard("Подтверждение", "Хотите удалить?", MsBox.Avalonia.Enums.ButtonEnum.YesNo);
        var res = await box.ShowWindowAsync();
        if(res == MsBox.Avalonia.Enums.ButtonResult.Yes){
            string connectionString = "Server=192.168.192.155;Port=3306;Database=OrdersApp;Uid=root;Pwd=220819998008Max;";
            string deleteQuery = "DELETE FROM Orders WHERE EqType = @EqType AND EqModel = @EqModel";

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    using (var command = new MySqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@EqType", _selectedOrder);
                        command.Parameters.AddWithValue("@EqModel", _eqModel);

                        connection.Open();
                        int affectedRows = command.ExecuteNonQuery();

                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting order: " + ex.Message);
            }
        }else{
            this.Close();
        }
    }
}