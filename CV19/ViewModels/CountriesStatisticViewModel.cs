using CV19.Services;
using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
    internal class CountriesStatisticViewModel : ViewModel
    {
        private MainWindowViewModel MainModel { get; }

        private DataService _DataService;


        public CountriesStatisticViewModel( MainWindowViewModel MainModel )
        {
            MainModel = MainModel;

            _DataService = new DataService();

        }
    }
}