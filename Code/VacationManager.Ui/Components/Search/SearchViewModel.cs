using Caliburn.Micro;
using System.Collections.Generic;
using Ninject;
using VacationManager.Ui.Services;

namespace VacationManager.Ui.Components.Search
{
    public class SearchViewModel : Screen, ISearchViewModel
    {
        public static SearchStrings Localization
        {
            get
            {
                return new SearchStrings();
            }
        }

        [Inject]
        public IUiService UiService { get; set; }

        public string KeyWord { get; set; }

        public IEnumerable<IResult> Search()
        {
            yield return UiService.ShowMessageBox("Under construction ...", "Search is");
        }
    }
}