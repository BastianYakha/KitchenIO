﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassLibrary.Objects;
using Frontend.RequestSenders;
using KitchenIO.Objects;

namespace Frontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ProductRequests ProductRequestMaker = new ProductRequests();
        InventoryRequests InventoryerquestMaker = new InventoryRequests();

        ObservableCollection<ProductRef> ProductList = new ObservableCollection<ProductRef>();
        ObservableCollection<Product> InventoryList = new ObservableCollection<Product>();

        public MainWindow()
        {
            InitializeComponent();
            dataGrid.ItemsSource = ProductList;
            ProductdataGrid.ItemsSource = InventoryList;
            UpdateProductRefs();
            UpdateInventory();
        }

        public async void UpdateProductRefs()
        {
            ProductList.Clear();
            List<ProductRef> PL = await ProductRequestMaker.PullProductRefs();
            foreach(ProductRef product in PL)
            {
                ProductList.Add(product);
            }
        }

        public async void UpdateInventory()
        {
            InventoryList.Clear();
            List<Product> PL = await InventoryerquestMaker.GetInventory();

            foreach(Product product in PL)
            {
                InventoryList.Add(product);
            }
        }

        public async void createProductRefButton(object sender, RoutedEventArgs e)
        {
            ProductRef newProductRef = new ProductRef();
            newProductRef.Id = Guid.NewGuid();
            newProductRef.Name = testName.Text;
            newProductRef.Barcode = Convert.ToInt32(testbarcode.Text);
            newProductRef.Price = Convert.ToDouble(testPrice.Text);
            newProductRef.Type = Convert.ToInt32(testType.Text);

            string result = await ProductRequestMaker.PushProductRef(newProductRef);

            testName.Text = "";
            testbarcode.Text = "";
            testPrice.Text = "";
            testType.Text = "";


            UpdateProductRefs();
        }

        public async void AddProductButton(object sender, RoutedEventArgs e)
        {

            Product newProduct = new Product();

            //int Barcode = Convert.ToInt32(newProductbarcode.Text);
            ProductRef foundProductRef = await ProductRequestMaker.GetProductRefByBarcode(Convert.ToInt32(newProductbarcode.Text));

            if(foundProductRef != null)
            {
                newProduct.ProductId = foundProductRef.Id;
                newProduct.Id = Guid.NewGuid();
                newProduct.Amount = Convert.ToDouble(newProductAmount.Text);
                newProduct.Weight = Convert.ToDouble(newProductWeight.Text);
                DateTime newDate = EpDate.SelectedDate.Value;
                newProduct.EP = newDate;

                InventoryerquestMaker.AddToInventorie(newProduct);

            }
            UpdateInventory();
        }
    }
}