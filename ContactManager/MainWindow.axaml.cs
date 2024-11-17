using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ContactManager;

namespace ContactMenager;

public class Contact {
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Number { get; set; }
    public string Email { get; set; }
    public bool IsFavourite { get; set; }
    public ICommand DeleteCommand { get; set; }
}

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        AddContact("Krzysiu", "Mayanowski", "420 549 911", "autobrumbrum@gmail.com", true);
        AddContact("Bartłomiej", "Nemendricki", "420 420 420", "cpunendricki@gmail.com", true);
        AddContact("Stefan", "Leoński", "600 100 100", "bocian.porzyczki@gmail.com");
        AddContact("Phelipe", "Mountan", "420 549 911", "gorol@gmail.com");
        AddContact("Dominik", "Winiarz", "0 1975 1995", "partyjniakwiniarz@gmail.com", true);

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

    private void AddContact(string name, string surname, string number, string email, bool isFavourite = false) {
        var contact = new Contact() {
            Name = name,
            Surname = surname,
            Number = number,
            Email = email,
            IsFavourite = isFavourite
        };
        contact.DeleteCommand = new Command(() => {
            _kontakty.Remove(contact);
            Filter();
        });
        _kontakty.Add(contact);
        Filter();
    }

    private void CategoryChanged(object? sender, SelectionChangedEventArgs e)
    {
        Filter();
    }

    private void SearchChange(object? sender, TextChangedEventArgs e)
    {
        Filter();
    }

    private void AddContact(object? sender, RoutedEventArgs e)
    {
        AddContact(Name.Text, Surname.Text, Number.Text, Email.Text);
        Name.Text = string.Empty;
        Surname.Text = string.Empty;
        Number.Text = string.Empty;
        Email.Text = string.Empty;
        
    }
    
}