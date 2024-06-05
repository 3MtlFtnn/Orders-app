using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Interactivity;
using Avalonia.Animation;
using MySql.Data.MySqlClient;
using System;
using MyApp.Variables;

namespace MyApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        signInButton.Background = Brushes.AliceBlue;
    }
    private void myButton_Click(object sender, RoutedEventArgs e)
    {
        var secwin = new MyApp();
        secwin.Show();
        this.Close();
    }
    private void CheckConnection_click(object sender, RoutedEventArgs e)
    {
        string Login = Login_textbox.Text;
        string Password = Password_textbox.Text;
        string connectionString = "Server=192.168.192.155;Port=3306;Database=OrdersApp;Uid=root;Pwd=220819998008Max";
        string selectQuery = "SELECT User_ID FROM Users WHERE Login = @login AND Password = @password";

        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                Test.Foreground = Brushes.Green;

                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@login", Login);
                    command.Parameters.AddWithValue("@password", Password);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
          
                            Test.Foreground = Brushes.Green;
                            statusLabel.Text = "Connected";
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Test.Foreground = Brushes.Red;
            statusLabel.Text = "No connect: " + ex.Message;
        }
    }
    private void Login_click(object sender, RoutedEventArgs e){
        string Login = Login_textbox.Text;
        string Password = Password_textbox.Text;
        string connectionString = "Server=192.168.192.155;Port=3306;Database=OrdersApp;Uid=root;Pwd=220819998008Max";
        string selectQuery = "SELECT User_ID FROM Users WHERE Login = @login AND Password = @password";
        string check_type_user = "SELECT Type FROM Users WHERE Login = @login AND Password = @password";
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                Test.Foreground = Brushes.Green;

                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@login", Login);
                    command.Parameters.AddWithValue("@password", Password);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
            
                            
                            Variables.Variables.Login_global = reader.GetInt32("User_ID").ToString();
                            Type_check();
                            Window newWindow = new Orders(Login);
                            newWindow.Show();
                            this.Close();
                        }else{
                            Test.Foreground = Brushes.Red;
                            statusLabel.Text = "Password incorrect";
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Test.Foreground = Brushes.Red;
            statusLabel.Text = "No connect: " + ex.Message;
        }
    }
    private void Type_check(){
        string Login = Login_textbox.Text;
        string Password = Password_textbox.Text;
        string connectionString = "Server=192.168.192.155;Port=3306;Database=OrdersApp;Uid=root;Pwd=220819998008Max";
        string check_type_user = "SELECT Type FROM Users WHERE Login = @login AND Password = @password";
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                Test.Foreground = Brushes.Green;

                using (MySqlCommand command = new MySqlCommand(check_type_user, connection))
                {
                    command.Parameters.AddWithValue("@login", Login);
                    command.Parameters.AddWithValue("@password", Password);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {                
                            Variables.Variables.Type_global = reader.GetString("Type");
                            Console.WriteLine(Variables.Variables.Type_global);
                            Window newWindow = new Orders(Login);
                            newWindow.Show();
                            this.Close();
                        }else{
                            Test.Foreground = Brushes.Red;
                            statusLabel.Text = "Password incorrect";
                        }
                    }

                }
            }
        }catch(Exception ex){
            Console.WriteLine(ex);
        }
    }
}
