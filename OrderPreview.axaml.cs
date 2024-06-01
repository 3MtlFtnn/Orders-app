using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace MyApp;

public partial class OrderPreview : Window
{
    public OrderPreview(string selectedOrder, string eqModel, string problem, string userName, string startDate, string completionData)
    {
        InitializeComponent();

        if(Info!=null){
            Info.Text = "Тип устройства: "+selectedOrder;
            Info_2.Text = "Модель устройства:" +eqModel;
            Info_3.Text = "Описание проблемы: " + problem;
            Info_4.Text = "Пользователь: "+userName;
            Info_5.Text = "Дата начала работ: "+startDate;
            Info_6.Text = "Дата окончания работ: "+completionData;
        }else{
            Info.Text = "No orders!"; 
        }
    }
}