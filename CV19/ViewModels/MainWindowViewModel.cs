using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Models.Decanat;
using CV19.ViewModels.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        /* ----------------------------------------------------------------------------------- */

        private readonly CountriesStatisticViewModel _CountriesStatistic;

        /* ----------------------------------------------------------------------------------- */


        #region StudentFilterText:string - Текст фильтра студентов
        private string? _StudentFilterText;
        /// <summary>Текст фильтра студентов</summary>
        public string StudentFilterText {
            get => _StudentFilterText;
            set {
                if (!Set( ref _StudentFilterText, value ))
                {
                    return;
                }
                _SelectedGroupStudents.View.Refresh();
            }
        }
        #endregion

        #region SelectedGroupStudents
        private readonly CollectionViewSource _SelectedGroupStudents = new CollectionViewSource();
        public ICollectionView? SelectedGroupStudents => _SelectedGroupStudents?.View;
        private void OnStudentFiltred( object sender, FilterEventArgs e )
        {
            if (e.Item is not Student student)
            {
                e.Accepted = false;
                return;
            }

            var text_filer = _StudentFilterText;
            if (string.IsNullOrEmpty( text_filer ))
            {
                return;
            }

            if (student.Name is null || student.Surname is null || student.Patronymic is null)
            {
                e.Accepted = false;
                return;
            }

            if (student.Name.Contains( text_filer, StringComparison.OrdinalIgnoreCase ))
            {
                return;
            }
            if (student.Surname.Contains( text_filer, StringComparison.OrdinalIgnoreCase ))
            {
                return;
            }
            if (student.Patronymic.Contains( text_filer, StringComparison.OrdinalIgnoreCase ))
            {
                return;
            }

            e.Accepted = false;
        }
        #endregion

        #region SelectedPageIndex:int - Номер выбранной вкладки
        private int _SelectedPageIndex = 0;
        /// <summary>Номер выбранной вкладки</summary>
        public int SelectedPageIndex {
            get => _SelectedPageIndex;
            set => Set( ref _SelectedPageIndex, value );
        }
        #endregion

        #region DataPoint:IEnumerable<DataPoint> - Набор данных для визуализации
        private IEnumerable<DataPoint>? _TestDataPoint;
        /// <summary>Набор данных для визуализации</summary>
        public IEnumerable<DataPoint>? TestDataPoint {
            get => _TestDataPoint;
            set => Set( ref _TestDataPoint, value );
        }
        #endregion

        #region Заголовок окна
        private string _title = "Анализ статистики";
        /// <summary>Заголовок окна</summary>
        public string Title {
            get => _title;
            set => Set( ref _title, value );
        }
        #endregion

        #region Status:string - Статус
        private string _Status = string.Empty;
        /// <summary>Статус</summary>
        public string Status {
            get => _Status;
            set => Set( ref _Status, value );
        }
        #endregion

        #region Команды

        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }
        private bool CanCloseApplicationCommandExecute( object p ) => true;
        private void OnCloseApplicationCommandExecuted( object p )
        {
            Application.Current.Shutdown();
        }
        #endregion

        #region ChangetTabIndexCommand
        public ICommand ChangetTabIndexCommand { get; }
        private bool CanChangetTabIndexCommandExecute( object p ) => SelectedPageIndex >= 0;
        private void OnChangetTabIndexCommandExecuted( object p )
        {
            if (p is null)
            {
                return;
            }
            SelectedPageIndex += Convert.ToInt32( p );
        }
        #endregion

        #endregion

        public IEnumerable<Student> TestStudents => Enumerable.Range( 1, App.IsDesignMode ? 10 : 100_000 ).Select( i => new Student() {
            Name = $"Name {i}",
            Surname = $"Surname {i}"
        } );


        public MainWindowViewModel()
        {
            _CountriesStatistic = new CountriesStatisticViewModel( this );

            #region Команды

            CloseApplicationCommand = new LambdaCommand( OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute );
            ChangetTabIndexCommand = new LambdaCommand( OnChangetTabIndexCommandExecuted, CanChangetTabIndexCommandExecute );
            #endregion

            /**********************************************************************************/
            var data_point = new List<DataPoint>( (int) (360 / 0.1) );
            for (var x = 0d; x <= 360; x += 0.1)
            {
                var y = Math.Sin( x * Math.PI / 180 );

                data_point.Add( new DataPoint { XValue = x, YValue = y } );
            }
            TestDataPoint = data_point;

            _SelectedGroupStudents.Filter += OnStudentFiltred;
        }
    }
}