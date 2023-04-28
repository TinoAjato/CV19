using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Models.Decanat;
using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        public ObservableCollection<Group> Groups { get; }

        #region SelectedGroup:Group - Выбранная группа
        private Group _SelectedGroup;
        /// <summary>Выбранная группа</summary>
        public Group SelectedGroup {
            get => _SelectedGroup;
            set => Set( ref _SelectedGroup, value );
        }
        #endregion


        #region SelectedPageIndex:int - Номер выбранной вкладки
        private int _SelectedPageIndex;
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

        public MainWindowViewModel()
        {
            #region Команды

            CloseApplicationCommand = new LambdaCommand( OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute );
            ChangetTabIndexCommand = new LambdaCommand( OnChangetTabIndexCommandExecuted, CanChangetTabIndexCommandExecute );

            #endregion

            var data_point = new List<DataPoint>( (int) (360 / 0.1) );
            for (var x = 0d; x <= 360; x += 0.1)
            {
                var y = Math.Sin( x * Math.PI / 180 );

                data_point.Add( new DataPoint { XValue = x, YValue = y } );
            }
            TestDataPoint = data_point;

            /**********************************************************************************/
            var student_index = 1;
            var students = Enumerable.Range( 1, 10 ).Select( i => new Student() {
                Name = $"Name {student_index}",
                Surname = $"Surname {student_index}",
                Patronymic = $"Patronymic {student_index++}",
                Birthday = DateTime.Now,
                Rating = 0
            } );
            var groups = Enumerable.Range( 1, 20 ).Select( i => new Group() {
                Name = $"Group {i}",
                Students = new ObservableCollection<Student>( students )
            } );
            Groups = new ObservableCollection<Group>( groups );


        }



    }
}