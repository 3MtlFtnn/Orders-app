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
using System.Collections.Generic;
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

            Grid newGrid = new Grid();
            newGrid.RowDefinitions.Add(new RowDefinition());
            newGrid.RowDefinitions.Add(new RowDefinition());
            newGrid.RowDefinitions.Add(new RowDefinition());
            newGrid.RowDefinitions.Add(new RowDefinition());
            newGrid.RowDefinitions.Add(new RowDefinition());
            newGrid.RowDefinitions.Add(new RowDefinition());
            newGrid.RowDefinitions.Add(new RowDefinition());
            newGrid.RowDefinitions.Add(new RowDefinition());
            newGrid.ColumnDefinitions.Add(new ColumnDefinition());
            newGrid.ColumnDefinitions.Add(new ColumnDefinition());
            newGrid.Margin = new Thickness(25);


            TextBlock info = new TextBlock();
            info.HorizontalAlignment = HorizontalAlignment.Left;
            info.VerticalAlignment = VerticalAlignment.Center;
            info.FontSize = 18;
            info.Margin = new Thickness(10);
            info.Text = "Тип устройства: ";
            Grid.SetRow(info, 0);
            Grid.SetColumn(info, 0);
            newGrid.Children.Add(info);

            TextBox infoTextBox = new TextBox();
            infoTextBox.Width = 200;
            infoTextBox.Margin = new Thickness(10);
            infoTextBox.Text = selectedOrder;
            Grid.SetRow(infoTextBox, 0);
            Grid.SetColumn(infoTextBox, 1);
            newGrid.Children.Add(infoTextBox);

            TextBlock info_2 = new TextBlock();
            info_2.HorizontalAlignment = HorizontalAlignment.Left;
            info_2.VerticalAlignment = VerticalAlignment.Center;
            info_2.FontSize = 18;
            info_2.Margin = new Thickness(10);
            info_2.Text = "Модель устройства: ";
            Grid.SetRow(info_2, 1);
            Grid.SetColumn(info_2, 0);
            newGrid.Children.Add(info_2);

            TextBox info_2TextBox = new TextBox();
            info_2TextBox.Width = 200;
            info_2TextBox.Text = eqModel;
            info_2TextBox.Margin = new Thickness(10);
            Grid.SetRow(info_2TextBox, 1);
            Grid.SetColumn(info_2TextBox, 1);
            newGrid.Children.Add(info_2TextBox);

            TextBlock info_3 = new TextBlock();
            info_3.HorizontalAlignment = HorizontalAlignment.Left;
            info_3.VerticalAlignment = VerticalAlignment.Center;
            info_3.FontSize = 18;
            info_3.Margin = new Thickness(10);
            info_3.Text = "Описание проблемы: ";
            Grid.SetRow(info_3, 2);
            Grid.SetColumn(info_3, 0);
            newGrid.Children.Add(info_3);

            TextBox info_3TextBox = new TextBox();
            info_3TextBox.Width = 200;
            info_3TextBox.Margin = new Thickness(10);
            info_3TextBox.Text = problem;
            Grid.SetRow(info_3TextBox, 2);
            Grid.SetColumn(info_3TextBox, 1);
            newGrid.Children.Add(info_3TextBox);

            TextBlock info_4 = new TextBlock();
            info_4.HorizontalAlignment = HorizontalAlignment.Left;
            info_4.VerticalAlignment = VerticalAlignment.Center;
            info_4.FontSize = 18;
            info_4.Margin = new Thickness(10);
            info_4.Text = "Пользователь: ";
            Grid.SetRow(info_4, 3);
            Grid.SetColumn(info_4, 0);
            newGrid.Children.Add(info_4);

            TextBox info_4TextBox = new TextBox();
            info_4TextBox.Width = 200;
            info_4TextBox.Text = userName;
            info_4TextBox.Margin = new Thickness(10);
            Grid.SetRow(info_4TextBox, 3);
            Grid.SetColumn(info_4TextBox, 1);
            newGrid.Children.Add(info_4TextBox);

            TextBlock info_5 = new TextBlock();
            info_5.HorizontalAlignment = HorizontalAlignment.Left;
            info_5.VerticalAlignment = VerticalAlignment.Center;
            info_5.FontSize = 18;
            info_5.Margin = new Thickness(10);
            info_5.Text = "Дата начала работ: ";
            Grid.SetRow(info_5, 4);
            Grid.SetColumn(info_5, 0);
            newGrid.Children.Add(info_5);

            dateTextBox.Margin = new Thickness(10);
            Grid.SetRow(dateTextBox, 4);
            dateTextBox.Text = startDate;
            Grid.SetColumn(dateTextBox, 1);
            newGrid.Children.Add(dateTextBox);

            TextBlock info_6 = new TextBlock();
            info_6.HorizontalAlignment = HorizontalAlignment.Left;
            info_6.VerticalAlignment = VerticalAlignment.Center;
            info_6.FontSize = 18;
            info_6.Margin = new Thickness(10);
            info_6.Text = "Дата окончания работ: ";
            Grid.SetRow(info_6, 5);
            Grid.SetColumn(info_6, 0);
            newGrid.Children.Add(info_6);

            TextBox info_6TextBox = new TextBox();
            info_6TextBox.Width = 200;
            info_6TextBox.Margin = new Thickness(10);
            info_6TextBox.Text = completionData;
            Grid.SetRow(info_6TextBox, 5);
            Grid.SetColumn(info_6TextBox, 1);
            newGrid.Children.Add(info_6TextBox);

            TextBlock info_7 = new TextBlock();
            info_7.HorizontalAlignment = HorizontalAlignment.Left;
            info_7.VerticalAlignment = VerticalAlignment.Center;
            info_7.FontSize = 18;
            info_7.Margin = new Thickness(10);
            info_7.Text = "Статус: ";
            Grid.SetRow(info_7, 6);
            Grid.SetColumn(info_7, 0);
            newGrid.Children.Add(info_7);

            statusBox.Margin = new Thickness(10);
            Grid.SetRow(statusBox, 6);
            
            statusBox.Text = status;
            Grid.SetColumn(statusBox, 1);
            newGrid.Children.Add(statusBox);

            Button submitButton = new Button();
            submitButton.Content = "Сохранить";
            submitButton.Width = 100;
            submitButton.HorizontalAlignment = HorizontalAlignment.Center;
            submitButton.VerticalAlignment = VerticalAlignment.Bottom;
            submitButton.Click += SubmitButton_Click;
            submitButton.Margin = new Thickness(10);
            Grid.SetRow(submitButton, 8);
            Grid.SetColumn(submitButton, 1);
            submitButton.Background = Brushes.AliceBlue;
            newGrid.Children.Add(submitButton);

            this.Content = newGrid;
        }

        if (Info!=null){
            Info.Text = "Тип устройства: "+ selectedOrder;
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
        Dictionary<string, int> myDictionary = new Dictionary<string, int>();
        myDictionary.Add("Вася", 1);
        myDictionary.Add("Антон", 2);
        myDictionary.Add("Наталья", 3);
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