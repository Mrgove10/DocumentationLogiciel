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
    /// <summary>
    /// View Model for the page "AddElement"
    /// </summary>
    public class AddElementViewModel : IViewModel<AddElementWindow, IAsyncCommand>, INotifyPropertyChanged
    {
        #region Private properties
        
        /// <summary>
        /// Private property for the template
        /// </summary>
        private readonly ElementTemplate _elementTemplate;

        /// <summary>
        /// Private property for the template
        /// </summary>
        private readonly NeededProductTemplate _neededProductTemplate;

        /// <summary>
        /// Private property to display product form
        /// </summary>
        private Visibility _displayProduct;

        /// <summary>
        /// Private property to display material form
        /// </summary>
        private Visibility _displayMaterial;

        /// <summary>
        /// Private property to display button
        /// </summary>
        private Visibility _buttonDisplay;

        #endregion

        #region Commands
        
        public IAsyncCommand GoBackCommand { get; }

        /// <summary>
        /// Command to add an element product or material
        /// </summary>
        public IAsyncCommand AddElementCommand { get; }

        /// <summary>
        /// Command to add a material to a product
        /// </summary>
        public ICommand AddNeededMaterialCommand { get; }

        /// <summary>
        /// Command to delete a material to a product
        /// </summary>
        public ICommand DeleteNeededMaterialCommand { get; }

        #endregion

        #region Inputs

        /// <summary>
        /// Label for an element
        /// </summary>
        public string ElementLabel
        {
            get => _elementTemplate.Label;
            set
            {
                _elementTemplate.Label = value;
                IsLabelOk = !string.IsNullOrEmpty(value);
                OnPropertyChange();
                OnPropertyChange(nameof(IsLabelOk));
            }
        }

        /// <summary>
        /// Quantity for an element
        /// </summary>
        public int ElementQuantity
        {
            get => _elementTemplate.Quantity;
            set
            {
                _elementTemplate.Quantity = value;
                IsQuantityOk = value != 0;
                OnPropertyChange();
                OnPropertyChange(nameof(IsQuantityOk));
            }
        }

        /// <summary>
        /// Quantity of a material of a product
        /// </summary>
        public int NeededMaterialQuantity
        {
            get => _neededProductTemplate.QuantityNeeded;
            set
            {
                _neededProductTemplate.QuantityNeeded = value;
                IsNeededMaterialQuantityOk = value != 0;
                OnPropertyChange();
                OnPropertyChange(nameof(IsNeededMaterialQuantityOk));
            }
        }

        /// <summary>
        /// Price of an element
        /// </summary>
        public float ElementPrice
        {
            get => _elementTemplate.Price;
            set
            {
                _elementTemplate.Price = value;
                IsPriceOk = value != 0;
                OnPropertyChange();
                OnPropertyChange(nameof(IsPriceOk));
            }
        }

        /// <summary>
        /// Date until a product is available
        /// </summary>
        public DateTime ProductAvailableDate
        {
            get => _elementTemplate.AvailableUntil;
            set
            {
                _elementTemplate.AvailableUntil = value;
                OnPropertyChange();
            }
        }

        /// <summary>
        /// Color for the date
        /// <remarks>Red if the date is passed</remarks>
        /// <remarks>Green if the date is in the future</remarks>
        /// </summary>
        public Brush ProductAvailableDateColor
        {
            get => _elementTemplate.ColorDate;
            set
            {
                _elementTemplate.ColorDate = value;
                OnPropertyChange();
            }
        }
        #endregion

        #region Visibility

        /// <summary>
        /// Allow to display the product form
        /// </summary>
        public Visibility DisplayProduct
        {
            get => _displayProduct;
            set
            {
                _displayProduct = value;
                OnPropertyChange();
            }
        }

        /// <summary>
        /// Allow to display the material form
        /// </summary>
        public Visibility DisplayMaterial
        {
            get => _displayMaterial;
            set
            {
                _displayMaterial = value;
                OnPropertyChange();
            }
        }

        /// <summary>
        /// Allow to display the button add element
        /// </summary>
        public Visibility ButtonDisplay
        {
            get => _buttonDisplay;
            set
            {
                _buttonDisplay = value;
                OnPropertyChange();
            }
        }

        #endregion

        #region Validations

        /// <summary>
        /// Boolean saying if the label is Ok
        /// </summary>
        public bool IsLabelOk { get; set; }

        /// <summary>
        /// Boolean saying if the price is Ok
        /// </summary>
        public bool IsPriceOk { get; set; }

        /// <summary>
        /// Boolean saying if the quantity is Ok
        /// </summary>
        public bool IsQuantityOk { get; set; }

        /// <summary>
        /// Boolean saying if the quantity of the material's product is Ok
        /// </summary>
        public bool IsNeededMaterialQuantityOk { get; set; }

        #endregion

        /// <summary>
        /// List which contains the materials of the product 
        /// </summary>
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

        /// <summary>
        /// Constructor of the view model
        /// </summary>
        /// <param name="currentPage">Page of the view mode</param>
        /// <param name="userServices">Services for the "User" table</param>
        /// <param name="alertServices">Services for the "Alert" table</param>
        /// <param name="materialServices">Services for the "Material" table</param>
        /// <param name="productServices">Services for the "Product" table</param>
        /// <param name="materialsProductServices">Services for the "MaterialProduct" table</param>
        /// <param name="saleServices">Services for the "Sale" table</param>
        /// <param name="materials">List of materials</param>
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

            _elementTemplate = new ElementTemplate();
            _neededProductTemplate = new NeededProductTemplate();

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

        /// <summary>
        /// Allow to generate the form when an element is selected (<see cref="Product"/> or <see cref="Material"/>)
        /// </summary>
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

        /// <summary>
        /// Allow to delete a material from a product
        /// </summary>
        /// <param name="parameter"></param>
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

        /// <summary>
        /// Allow to add a material to a product
        /// </summary>
        /// <param name="parameter"></param>
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

        /// <summary>
        /// Method to create an element (<see cref="Product"/> or <see cref="Material"/>)
        /// <remarks>if it's a product, create also the list of materials associated (<see cref="MaterialsProduct"/>)</remarks>
        /// </summary>
        /// <returns></returns>
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
                        if (ElementQuantity <= 10)
                        {
                            AlertServices.Create(new Alert
                            {
                                IsDismiss = false,
                                MaterialId = (await MaterialServices.GetByLabel(ElementLabel)).Id,
                                Title = $"Stock critique de {ElementLabel}",
                                Message = $"Le stock de {ElementLabel} est au plus bas. Contactez le fournisseur pour recommander du stock."
                            });
                        }
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

                if (CurrentPage.ElementAddSnackbar.MessageQueue is { } messageQueue)
                {
                    var message = $"{itemSelected} '{ElementLabel}' has been added !";
                    Task.Factory.StartNew(() => messageQueue.Enqueue(message)).Wait();
                }

                DisplayProduct = Visibility.Hidden;
                DisplayMaterial = Visibility.Hidden;
                ButtonDisplay = Visibility.Hidden;
                ElementLabel = string.Empty;
                ElementPrice = 0;
                ElementQuantity = 0;
                ProductAvailableDate = DateTime.Today;
                CurrentPage.TypeElementComboBox.SelectedItem = null;
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

        /// <summary>
        /// Method to check if an element can be added (<see cref="Product"/> or <see cref="Material"/>)
        /// - Label must not be empty
        /// - Price must be > 0
        /// - Quantity must be > 0
        /// </summary>
        /// <returns><b>true</b> if all element are ok, else <b>false</b></returns>
        public bool CanCreateElement()
        {
            return IsLabelOk && IsPriceOk && IsQuantityOk;
        }

        /// <summary>
        /// Method to check if a material can be added into a product (<see cref="MaterialsProduct"/>)
        /// - Quantity of the material must be > 0
        /// - Label must not be empty
        /// </summary>
        /// <returns></returns>
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
