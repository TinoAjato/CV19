using CV19.Infrastructure.Commands;
using CV19.ViewModels.Base;
using System.Windows;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Заголовок окна
        private string _title = "Анализ статистики";
        /// <summary>Заголовок окна</summary>
        public string Title {
            get => _title;
            set => Set( ref _title, value );
        }
        #endregion

        #region Status:string - Статус
        private string _Status = "Good!";
        /// <summary>Статус</summary>
        public string Status {
            get => _Status;
            set => Set( ref _Status, value );
        }
        #endregion

        #region Команды

        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }
        private bool CanCloseApplicationCommandExecute( object sender ) => true;
        private void OnCloseApplicationCommandExecuted( object sender )
        {
            Application.Current.Shutdown();
        }
        #endregion

        #endregion

        public MainWindowViewModel()
        {
            #region Команды

            CloseApplicationCommand = new LambdaCommand( OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute );

            #endregion
        }



    }
}