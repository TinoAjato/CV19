using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Services;
using CV19.ViewModels.Base;
using System.Collections.Generic;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class CountriesStatisticViewModel : ViewModel
    {
        private MainWindowViewModel MainModel { get; }

        private DataService _DataService;


        #region Contries:IEnumerable<CountryInfo> - Статистика по странам
        private IEnumerable<CountryInfo> _Contries;
        /// <summary>Статистика по странам</summary> 
        public IEnumerable<CountryInfo> Contries {
            get => _Contries;
            private set => Set( ref _Contries, value );
        }
        #endregion


        #region Команды

        public ICommand RefreshDataCommand { get; }
        private void OnRefreshDataCommandExecuted( object p )
        {
            Contries = _DataService.GetData();
        }

        #endregion

        public CountriesStatisticViewModel( MainWindowViewModel MainModel )
        {
            this.MainModel = MainModel;

            _DataService = new DataService();

            #region Команды

            RefreshDataCommand = new LambdaCommand( OnRefreshDataCommandExecuted );



            #endregion

        }
    }
}