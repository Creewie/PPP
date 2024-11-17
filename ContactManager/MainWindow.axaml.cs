using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;

namespace ContactMenager;

public class Contact {
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Number { get; set; }
    public string Email { get; set; }
    public bool IsFavourite { get; set; }
}

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        _kontakty = new(){
            new Contact(){Name = "Krzysiu", Surname = "Mayanowski", Number = "420 549 911", Email = "autobrumbrum@gmail.com", IsFavourite = true},
            new Contact(){Name = "Bartłomiej", Surname = "Nemendricki", Number = "420 420 420", Email = "cpunendricki@gmail.com", IsFavourite = true},
            new Contact(){Name = "Stefan", Surname = "Leoński", Number = "600 100 100", Email = "bocian.porzyczki@gmail.com", IsFavourite = false},
            new Contact(){Name = "Phelipe", Surname = "Mountan", Number = "420 549 911", Email = "gorol@gmail.com", IsFavourite = false},
            new Contact(){Name = "Dominik", Surname = "Winiarz", Number = "0 1975 1995", Email = "partyjniakwiniarz@gmail.com", IsFavourite = true},
        };
        Kontakty.ItemsSource = filteredContacts;
        Category.ItemsSource = new string[]{"Wszystkie", "Ulubione"};
        Category.SelectedIndex = 0;
        Filter();
    }
    private readonly ObservableCollection<Contact> _kontakty = [];
    private readonly ObservableCollection<Contact> filteredContacts = [];
    
    private void Filter() {
        var selectedCategory = Category.SelectedItem as string;
        var filtered = string.IsNullOrEmpty(selectedCategory) || selectedCategory=="Wszystkie"
            ? _kontakty 
            : _kontakty.Where(p => p.IsFavourite);
        
        filtered = filtered.Where(p => p.Name.Contains(Search.Text ?? string.Empty));
        filteredContacts.Clear();
        foreach (var product in filtered)
        {
            filteredContacts.Add(product);
        }
    }

    private void CategoryChanged(object? sender, SelectionChangedEventArgs e)
    {
        Filter();
    }

    private void SearchChange(object? sender, TextChangedEventArgs e)
    {
        Filter();
    }
}