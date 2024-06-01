using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using Avalonia.Controls.Notifications;
using MsBox.Avalonia;
using Avalonia.Media;
using MySql.Data.MySqlClient;
using System;

namespace MyApp;

public partial class MyApp : Window
{

    public MyApp()
    {
        InitializeComponent();
        signUpButton.Foreground = Brushes.WhiteSmoke;
    }
    private async void Check_Password(object sender, TextChangedEventArgs e)
    {
        var pass1 = Password_1.Text;
        var pass2 = Password_2.Text;
        var name = Name.Text;
        if (pass1 != pass2)
        {
            var box = MessageBoxManager.GetMessageBoxStandard("Caption", "Пароль не совпадает");
            await box.ShowWindowAsync();

        }else if(pass1==null || pass2== null){
            var box = MessageBoxManager.GetMessageBoxStandard("Caption", "Пароль не должен быть пустым");
            await box.ShowAsync();
            
        }else if(name == null){
            var box = MessageBoxManager.GetMessageBoxStandard("Caption", "Имя не должен быть пустым");
            await box.ShowAsync();
        }
    }
    private void CreateProfilerButton_Click(object sender, RoutedEventArgs e)
    {
        string connectionString = "Server=192.168.192.155;Port=3306;Database=OrdersApp;Uid=root;Pwd=220819998008Max;";

        string fio = FIO.Text;
        string phone = Number.Text;
        string login = Name.Text;
        string password = Password_1.Text;
        string jobTitle = Job.Text;
        Check_Password(this,null);

        string insertQuery = "INSERT INTO Users (FIO, Phone, Login, Password, Type) VALUES (@fio, @phone, @login, @password, @jobTitle)";

        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@fio", fio);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@jobTitle", jobTitle);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0 && fio!=null)
                    {
                        signUpButton.Foreground = Brushes.Green;
                        signUpButton.Content = "Добавлено";
                    }else if(fio == ""){
                        signUpButton.Foreground = Brushes.DarkRed;
                        signUpButton.Content = "Ошибка";
                    }
                    else
                    {
                        signUpButton.Foreground = Brushes.DarkRed;
                        signUpButton.Content = "Ошибка";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка: " + ex.Message);
        }
    }

    private void back(object sender, RoutedEventArgs e){
        var main_win = new MainWindow();
        main_win.Show();
        this.Close();
    }
    private void ToMain(object sender, RoutedEventArgs e)
    {
        Check_Password(this, null);
    }
}