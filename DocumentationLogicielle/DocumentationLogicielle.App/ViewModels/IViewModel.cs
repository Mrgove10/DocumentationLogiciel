using System.Windows.Input;
using DocumentationLogicielle.App.Views;
using DocumentationLogicielle.Models;
using DocumentationLogicielle.Services;

namespace DocumentationLogicielle.App.ViewModels
{
    /// <summary>
    /// Interface for the view model files
    /// </summary>
    /// <typeparam name="T">The type of the window. Ex: <see cref="BoardWindow"/></typeparam>
    /// <typeparam name="Y">The command type for the go back command : <see cref="ICommand"/> or <see cref="IAsyncCommand"/></typeparam>
    public interface IViewModel<T, Y>
    {
        /// <summary>
        /// Command to go back to the precedent page
        /// </summary>
        Y GoBackCommand { get; }

        /// <summary>
        /// The name of the current user
        /// </summary>
        string CurrentUserName { get; set; }

        /// <summary>
        /// Property which correspond to the current page
        /// </summary>
        T CurrentPage { get; set; }

        /// <summary>
        /// Services to interact with the table "User" (<see cref="User"/>)
        /// </summary>
        UserServices UserServices { get; set; }

        /// <summary>
        /// Services to interact with the table "Alert" (<see cref="Alert"/>)
        /// </summary>
        AlertServices AlertServices { get; set; }

        /// <summary>
        /// Services to interact with the table "Material" (<see cref="Material"/>)
        /// </summary>
        MaterialServices MaterialServices { get; set; }

        /// <summary>
        /// Services to interact with the table "Product" (<see cref="Product"/>)
        /// </summary>
        ProductServices ProductServices { get; set; }

        /// <summary>
        /// Services to interact with the table "MaterialsProduct" (<see cref="MaterialsProduct"/>)
        /// </summary>
        MaterialsProductServices MaterialsProductServices { get; set; }

        /// <summary>
        /// Services to interact with the table "Sale" (<see cref="Sale"/>)
        /// </summary>
        SaleServices SaleServices { get; set; }
    }
}
