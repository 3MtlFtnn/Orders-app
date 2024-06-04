using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using MyApp.Variables;
namespace MyApp;

public partial class CreateOrder : Window
{
    public string Type_device;
    public string Master_Selected;
    public CreateOrder()
    {
        InitializeComponent();
    }
    public void EqTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
       var combobox = sender as ComboBox;
       if(combobox?.SelectedItem!=null){
            Type_device = (combobox.SelectedItem as ComboBoxItem).Content.ToString();
        }
    }
    public void Master_SelectionChanged(object sender, SelectionChangedEventArgs e){
        var combobox = sender as ComboBox;
        if(combobox?.SelectedItem!=null){
            Master_Selected = (combobox.SelectedItem as ComboBoxItem).Content.ToString();
        }
    }
    public void SaveOrderButton_Click(object sender, RoutedEventArgs e){
        string connectionString = "Server=192.168.192.155;Port=3306;Database=OrdersApp;Uid=root;Pwd=220819998008Max";
        string eqModel = txtEqModel.Text;
        string problem = txtProblemDesc.Text;
        string insertQuery = "INSERT INTO Orders (Master_ID, Status, Client_ID, EqModel, EqType, ProblemDesc, StartDate, Completion_Date) VALUES (@Master_ID, @Status, @Client_ID, @eqModel, @Type_device, @problem, @StartDate, @Completion_Date)";
        try{
            using (MySqlConnection connection = new MySqlConnection(connectionString)){
                using(MySqlCommand command = new MySqlCommand(insertQuery, connection)){
                    command.Parameters.AddWithValue("@problem", problem);
                    command.Parameters.AddWithValue("@Master_ID", "2");
                    command.Parameters.AddWithValue("@Status", "pococococ");
                    command.Parameters.AddWithValue("@StartDate", "2000-12-12");
                    command.Parameters.AddWithValue("@Completion_Date", "2000-12-12");
                    command.Parameters.AddWithValue("@eqModel", eqModel);
                    command.Parameters.AddWithValue("@Type_device", Type_device);
                    command.Parameters.AddWithValue("@Client_ID", Variables.Variables.Login_global);
                
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }catch(Exception ex){
            Console.WriteLine(ex);
        }
    }
}