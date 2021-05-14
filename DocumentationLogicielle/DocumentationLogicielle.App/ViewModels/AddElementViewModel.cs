using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using DocumentationLogicielle.App.Templates;
using DocumentationLogicielle.App.Views;
using DocumentationLogicielle.Models;
using DocumentationLogicielle.Services;
using MaterialDesignThemes.Wpf;

namespace DocumentationLogicielle.App.ViewModels
{
    public class AddElementViewModel : IViewModel<AddElementWindow, IAsyncCommand>, INotifyPropertyChanged
    {
        #region Private properties

        private ElementTemplate elementTemplate;
        private NeededProductTemplate neededProductTemplate;
        private List<NeededProductTemplate> neededMaterials;
        private Visibility displayProduct;
        private Visibility displayMaterial;
        private Visibility buttonDisplay;

        #endregion

        #region Commands

        public IAsyncCommand GoBackCommand { get; }
        public IAsyncCommand AddElementCommand { get; }
        public ICommand AddNeededMaterialCommand { get; }
        public ICommand DeleteNeededMaterialCommand { get; }

        #endregion

        #region Inputs

        public string ElementLabel
        {
            get => elementTemplate.Label;
            set
            {
                elementTemplate.Label = value;
                IsLabelOk = !string.IsNullOrEmpty(value);
                OnPropertyChange();
                OnPropertyChange(nameof(IsLabelOk));
            }
        }

        public int ElementQuantity
        {
            get => elementTemplate.Quantity;
            set
            {
                elementTemplate.Quantity = value;
                IsQuantityOk = value != 0;
                OnPropertyChange();
                OnPropertyChange(nameof(IsQuantityOk));
            }
        }

        public int NeededMaterialQuantity
        {
            get => neededProductTemplate.QuantityNeeded;
            set
            {
                neededProductTemplate.QuantityNeeded = value;
                IsNeededMaterialQuantityOk = value != 0;
                OnPropertyChange();
                OnPropertyChange(nameof(IsNeededMaterialQuantityOk));
            }
        }

        public float ElementPrice
        {
            get => elementTemplate.Price;
            set
            {
                elementTemplate.Price = value;
                IsPriceOk = value != 0;
                OnPropertyChange();
                OnPropertyChange(nameof(IsPriceOk));
            }
        }

        public DateTime ProductAvailableDate
        {
            get => elementTemplate.AvailableUntil;
            set
            {
                elementTemplate.AvailableUntil = value;
                OnPropertyChange();
            }
        }

        public Brush ProductAvailableDateColor
        {
            get => elementTemplate.ColorDate;
            set
            {
                elementTemplate.ColorDate = value;
                OnPropertyChange();
            }
        }
        #endregion

        #region Visibility

        public Visibility DisplayProduct
        {
            get => displayProduct;
            set
            {
                displayProduct = value;
                OnPropertyChange();
            }
        }

        public Visibility DisplayMaterial
        {
            get => displayMaterial;
            set
            {
                displayMaterial = value;
                OnPropertyChange();
            }
        }

        public Visibility ButtonDisplay
        {
            get => buttonDisplay;
            set
            {
                buttonDisplay = value;
                OnPropertyChange();
            }
        }

        #endregion

        #region Validations

        public bool IsLabelOk { get; set; }
        public bool IsPriceOk { get; set; }
        public bool IsQuantityOk { get; set; }

        public bool IsNeededMaterialQuantityOk { get; set; }

        #endregion

        public List<NeededProductTemplate> NeededMaterials { get; set; }
        public string CurrentUserName { get; set; }
        public AddElementWindow CurrentPage { get; set; }

        #region Services

        public UserServices UserServices { get; set; }
        public AlertServices AlertServices { get; set; }
        public MaterialServices MaterialServices { get; set; }
        public ProductServices ProductServices { get; set; }
        public MaterialsProductServices MaterialsProductServices { get; set; }
        public SaleServices SaleServices { get; set; }

        #endregion

        public AddElementViewModel(AddElementWindow currentPage, UserServices userServices, AlertServices alertServices, MaterialServices materialServices, ProductServices productServices, MaterialsProductServices materialsProductServices, SaleServices saleServices, List<Material> materials)
        {
            CurrentPage = currentPage;
            CurrentUserName = $"Welcome {AppSettings.CurrentUser.Login} {(AppSettings.CurrentUser.Role == ERole.Administrator.ToString() ? "(admin)" : "")} !";
            NeededMaterials = new List<NeededProductTemplate>();

            UserServices = userServices;
            AlertServices = alertServices;
            MaterialServices = materialServices;
            ProductServices = productServices;
            MaterialsProductServices = materialsProductServices;
            SaleServices = saleServices;

            elementTemplate = new ElementTemplate();
            neededProductTemplate = new NeededProductTemplate();

            CurrentPage.NeededMaterialsComboBox.ItemsSource = materials.Select(x => x.Label);

            DisplayProduct = Visibility.Hidden;
            DisplayMaterial = Visibility.Hidden;
            ButtonDisplay = Visibility.Hidden;

            GoBackCommand = new AsyncCommand(GoBack, () => true);
            AddElementCommand = new AsyncCommand(CreateElement, CanCreateElement);
            AddNeededMaterialCommand = new CommandHandler(AddNeededMaterial, CanAddNeededMaterial);
            DeleteNeededMaterialCommand = new CommandHandler(DeleteNeededMaterial, () => true);
        }

        #region Property Changes

        /// <summary>
        /// Property which allow to update a field
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Specific method to update a field without refreshing
        /// </summary>
        /// <param name="propertyName">Name of the field</param>
        protected void OnPropertyChange([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public void GenerateForm()
        {
            if (CurrentPage.TypeElementComboBox.SelectedItem != null)
            {
                var itemSelected = ((ComboBoxItem)CurrentPage.TypeElementComboBox.SelectedItem).Content;
                switch (itemSelected.ToString())
                {
                    case "Material":
                        DisplayProduct = Visibility.Hidden;
                        DisplayMaterial = Visibility.Visible;
                        break;
                    case "Product":
                        DisplayProduct = Visibility.Visible;
                        DisplayMaterial = Visibility.Hidden;
                        ProductAvailableDate = DateTime.Today;
                        break;
                }

                ButtonDisplay = Visibility.Visible;
            }
        }

        public void DeleteNeededMaterial(object parameter)
        {
            var toDelete = NeededMaterials.First(x => x.Material == parameter.ToString());
            var indexToDelete = NeededMaterials.FindIndex(x => x.Material == parameter.ToString());
            NeededMaterials.Remove(toDelete);
            CurrentPage.NeededMaterialsList.Children.RemoveAt(indexToDelete);
            CurrentPage.NeededMaterialsList.RowDefinitions.RemoveAt(0);
            for (int i = indexToDelete; i < NeededMaterials.Count; i++)
            {
                CurrentPage.NeededMaterialsList.Children[i].SetValue(Grid.RowProperty, i);
            }
        }

        public void AddNeededMaterial(object parameter)
        {
            var materialSelected = CurrentPage.NeededMaterialsComboBox.SelectedItem.ToString();
            NeededMaterials.Add(new NeededProductTemplate{Material = materialSelected, QuantityNeeded = NeededMaterialQuantity});
            OnPropertyChange(nameof(NeededMaterials));

            CurrentPage.NeededMaterialsList.RowDefinitions.Add(new RowDefinition());
            Card card = new Card();
            StringBuilder sb = new StringBuilder();

            //Create card
            sb.Append(@"<materialDesign:Card Margin='1' Grid.Row='"+(NeededMaterials.Count-1)+@"' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' xmlns:materialDesign='http://materialdesigninxaml.net/winfx/xaml/themes'>
                                <Grid>
                                    <Grid.ColumnDefinitions>
                                        <ColumnDefinition Width = '2*'></ColumnDefinition>
                                        <ColumnDefinition Width = '*'></ColumnDefinition>
                                        <ColumnDefinition Width = '*'></ColumnDefinition>
                                        <ColumnDefinition Width = '*'></ColumnDefinition>
                                    </Grid.ColumnDefinitions >

                                    <TextBlock Grid.Column='0'
                                               Foreground='White'
                                               VerticalAlignment = 'Center'
                                               HorizontalAlignment='Center'
                                               FontSize='15'
                                               FontWeight='Bold'
                                               Text='"+materialSelected+@"'/>
                                    <TextBlock Grid.Column='1'
                                               Foreground='White'
                                               VerticalAlignment = 'Center'
                                               HorizontalAlignment='Center'
                                               FontSize='15'
                                               FontWeight='Bold'
                                               Text=':'/>
                                    <TextBlock Grid.Column = '2'
                                               VerticalAlignment = 'Center'
                                               HorizontalAlignment='Center'
                                               Foreground = 'White'
                                               FontSize = '15'
                                               Text = '"+NeededMaterialQuantity+@"' />

                                    <Border CornerRadius='5' 
                                            Grid.Column='3'
                                            BorderBrush='Red'>
                                        <Button Height='20'
                                                VerticalAlignment='Center'
                                                Background='Red'
                                                Command='{Binding DeleteNeededMaterialCommand}'
                                                CommandParameter='"+materialSelected+@"'
                                                FontSize='10'>
                                            -
                                        </Button>
                                    </Border>
                                </Grid>
                            </materialDesign:Card>
");


            card = (Card)XamlReader.Parse(sb.ToString());
            CurrentPage.NeededMaterialsList.Children.Add(card);
        }

        public async Task CreateElement()
        {
            try
            {
                var itemSelected = ((ComboBoxItem)CurrentPage.TypeElementComboBox.SelectedItem).Content.ToString();

                switch (itemSelected)
                {
                    case "Material":
                        MaterialServices.Create(new Material
                        {
                            Label = ElementLabel,
                            Quantity = ElementQuantity,
                            Price = ElementPrice
                        });
                        break;
                    case "Product":
                        ProductServices.Create(new Product
                        {
                            Label = ElementLabel,
                            Quantity = ElementQuantity,
                            Price = ElementPrice,
                            AvailableUntil = ProductAvailableDate
                        });
                        var list = new List<MaterialsProduct>();
                        foreach (var pair in NeededMaterials)
                        {
                            list.Add(new MaterialsProduct
                            {
                                IdMaterial = (await MaterialServices.GetByLabel(pair.Material)).Id,
                                IdProduct = (await ProductServices.GetByLabel(ElementLabel)).Id,
                                QuantityNeeded = pair.QuantityNeeded
                            });
                        }
                        MaterialsProductServices.AddNeededMaterials(list);
                        break;
                }

                DisplayProduct = Visibility.Hidden;
                DisplayMaterial = Visibility.Hidden;
                ButtonDisplay = Visibility.Hidden;
                ElementLabel = string.Empty;
                ElementPrice = 0;
                ElementQuantity = 0;
                ProductAvailableDate = DateTime.Today;
                CurrentPage.TypeElementComboBox.SelectedItem = null; 
                
                if (CurrentPage.ElementAddSnackbar.MessageQueue is { } messageQueue)
                {
                    var message = $"{itemSelected} '{ElementLabel}' has been added !";
                    Task.Factory.StartNew(() => messageQueue.Enqueue(message)).Wait();
                }
            }
            catch (Exception e)
            {
                if (CurrentPage.ElementAddSnackbar.MessageQueue is { } messageQueue)
                {
                    var message = $"A problem occurs, please retry or contact the support.";
                    CurrentPage.ElementAddSnackbar.Background = Brushes.Red;
                    Task.Factory.StartNew(() => messageQueue.Enqueue(message)).Wait();
                }
            }
            
        }

        public bool CanCreateElement()
        {
            return IsLabelOk && IsPriceOk && IsQuantityOk;
        }

        public bool CanAddNeededMaterial()
        {
            return IsNeededMaterialQuantityOk && !string.IsNullOrEmpty(CurrentPage.NeededMaterialsComboBox.SelectedItem.ToString());
        }

        /// <summary>
        /// Method to go back to the precedent page
        /// </summary>
        /// <returns></returns>
        private async Task GoBack()
        {
            BoardWindow page = new BoardWindow(UserServices, AlertServices, MaterialServices, ProductServices, MaterialsProductServices, SaleServices, await AlertServices.CountAlerts());
            page.Show();
            CurrentPage.Close();
        }
    }
}
