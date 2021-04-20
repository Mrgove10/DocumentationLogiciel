using DocumentationLogicielle.App.Views;

namespace DocumentationLogicielle.App.ViewModels
{
    public class BoardViewModel
    {
        public BoardWindow CurrentPage { get; set; }

        public BoardViewModel(BoardWindow currentPage)
        {
            CurrentPage = currentPage;
        }
    }
}
