using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Services;
using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class CountriesStatisticViewModel : ViewModel
    {
        private MainWindowViewModel MainModel { get; }

        private DataService _DataService;


        #region Contries:IEnumerable<CountryInfo> - Статистика по странам
        private IEnumerable<CountryInfo> _Contries = Enumerable.Empty<CountryInfo>();
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

        public CountriesStatisticViewModel() : this( null )
        {
            if (!App.IsDesignMode)
            {
                throw new InvalidOperationException( "Вызов конструктора, непредназначенного для использования в обычном режиме" );
            }

            _Contries = Enumerable.Range( 1, 10 )
                .Select( i => new CountryInfo {
                    Name = $"Country {i}",
                    ProvinceCounts = Enumerable.Range( 1, 10 ).Select( j => new PlaceInfo {
                        Name = $"Province {j}",
                        Location = new Point( i, j ),
                        Counts = Enumerable.Range( 1, 10 )
                        .Select( k => new ConfirmedCount {
                            Date = DateTime.Now.Subtract( TimeSpan.FromDays( 100 - k ) ),
                            Count = k
                        } ).ToArray()
                    } ).ToArray()
                } ).ToArray();
        }

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