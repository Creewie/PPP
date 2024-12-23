using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace ProductCatalog;

public class Product{
    private string _imagePath;
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
    public string Category { get; set; }
    public string ImagePath
    {
        get => _imagePath;
        set {
            _imagePath = value;
            try {
                ImageBitmap = new Bitmap(AssetLoader.Open(new Uri(value)));
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }
        }
    }
    public Bitmap? ImageBitmap { get; set; } 
}
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        _products = new () {
            new Product{ Name = "Laptop Alienware", Price = 17000m, IsAvailable = true, Category = "Elektronika", ImagePath="avares://ProductCatalog/Assets/laptop.png" },
            new Product{ Name = "Torba Louis Vuitton", Price = 80000m, IsAvailable = false, Category = "Torby", ImagePath="avares://ProductCatalog/Assets/torba.png" },
            new Product{ Name = "Mullermilch Orzechowy", Price = 3.75m, IsAvailable = true, Category = "Żywność", ImagePath="avares://ProductCatalog/Assets/mullermilch.png" },
            new Product{ Name = "iPhone 15", Price = 3500m, IsAvailable = true, Category = "Elektronika", ImagePath="avares://ProductCatalog/Assets/iphone15.png" },
            new Product{ Name = "Maya X", Price = 549m, Category = "Myszki/Kobiety", ImagePath = "avares://ProductCatalog/Assets/maya.png" },
        };
        var kategorie = _products.Select(p=>p.Category).Distinct().ToList();
        kategorie.Insert(0,"Wszystko");
        Category.ItemsSource = kategorie;
        Products.ItemsSource = filteredProducts;
        Category.SelectedIndex = 0;
    }
    
    private readonly ObservableCollection<Product> _products = [];
    private readonly ObservableCollection<Product> filteredProducts = [];


    private void Filter() {
        var selectedCategory = Category.SelectedItem as string;
        
        var filtered = string.IsNullOrEmpty(selectedCategory) || selectedCategory=="Wszystko"
            ? _products 
            : _products.Where(p => p.Category == selectedCategory);
        
        filtered = filtered.Where(p => p.Name.Contains(Search.Text ?? string.Empty));
        filteredProducts.Clear();
        foreach (var product in filtered)
        {
            filteredProducts.Add(product);
        }
    }

    private void SearchChange(object? sender, TextChangedEventArgs e)
    {
        Filter();
    }
    
    private void CategoryChanged(object? sender, SelectionChangedEventArgs e)
    {
        Filter();
    }
}