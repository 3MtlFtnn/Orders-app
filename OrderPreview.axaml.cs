using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using MySql.Data.MySqlClient;
using MsBox.Avalonia;
namespace MyApp;



public partial class OrderPreview : Window
{
    private string _eqModel;
    private string _selectedOrder;
    public OrderPreview(string selectedOrder, string eqModel, string problem, string userName, string startDate, string completionData, string login)
    {

        _eqModel = eqModel;
        _selectedOrder = selectedOrder;
        InitializeComponent();

        if(Info!=null){
            Info.Text = "Тип устройства: "+selectedOrder;
            Info_2.Text = "Модель устройства:" +eqModel;
            Info_3.Text = "Описание проблемы: " + problem;
            Info_4.Text = "Пользователь: "+userName;
            Info_5.Text = "Дата начала работ: "+startDate;
            Info_6.Text = "Дата окончания работ: "+completionData;
            Delete.Content = "Удалить🧺";
        }else{
            Info.Text = "No orders!"; 
        }
    }
    private async void  DeleteOrder(object sender, RoutedEventArgs e)
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