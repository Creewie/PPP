using System.Collections.ObjectModel;
using Avalonia.Controls;

namespace EventCalendar;

public class Event
{
    public string Name { get; set; }
    public string Day { get; set; }
    public bool IsImportant { get; set; }
}

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        _events = new() {
            new Event { Name = "Zrobienie Pięciu Prostych Programów", Day = "Niedziela", IsImportant = true },
            new Event { Name = "Początek tej katastrofy :(", Day = "Poniedziałek", IsImportant = false },
            new Event { Name = "Urodziny ziomeczka! (Chlanie)", Day = "Piątek", IsImportant = true },
        };
        Events.ItemsSource = _events;
    }

    private readonly ObservableCollection<Event> _events = [];
}