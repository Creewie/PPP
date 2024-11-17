using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Interactivity;

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
        Day.ItemsSource = new []{"Poniedziałek","Wtorek","Środa","Czwartek","Piątek","Sobota","Niedziela"};
        Events.ItemsSource = _events;
    }

    private readonly ObservableCollection<Event> _events = [];

    private void AddEvent(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(Day.SelectedItem as string)) {
            return;
        }
        _events.Add(new Event(){Name = Name.Text ?? "Puste", Day = (Day.SelectedItem as string ?? String.Empty), IsImportant = false});
    }
}