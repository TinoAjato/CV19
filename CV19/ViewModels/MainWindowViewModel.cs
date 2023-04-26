using CV19.ViewModels.Base;

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



    }
}