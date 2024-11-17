using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;

namespace ProductCatalog;

public class Product{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
    public string Category { get; set; }
    public string ImagePath { get; set; }
}
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        _products = new () {
            new Product{ Name = "Laptop", Price = 17000m, IsAvailable = true, Category = "Elektronika", ImagePath="/Assets/laptop.png" },
            new Product{ Name = "Torba", Price = 800m, IsAvailable = false, Category = "Torby", ImagePath="/Assets/laptop.png" },
            new Product{ Name = "Mullermilch", Price = 3.75m, IsAvailable = true, Category = "Żywność", ImagePath="/Assets/laptop.png" },
            new Product{ Name = "iPhone", Price = 17000m, IsAvailable = true, Category = "Elektronika", ImagePath="/Assets/laptop.png" },
            new Product{ Name = "Maya", Price = 549m, Category = "Myszki/Kobiety", ImagePath = "/Assets/maya.png" },
        };
        Category.ItemsSource = _products.Select(p=>p.Category).Distinct().ToList();
    }
    
    private readonly ObservableCollection<Product> _products = [];
    private readonly ObservableCollection<Product> filteredProducts = [];

    private void SearchChange(object? sender, TextChangedEventArgs e)
    {
        Console.WriteLine("Krzysiu git");
    }
}