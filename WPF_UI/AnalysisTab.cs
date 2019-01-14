using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BackEnd;
using WPF_UI.Annotations;

namespace WPF_UI
{
    public class AnalysisTab : INotifyPropertyChanged
    {
        private static AnalysisTab _instance;

        public static IBackEnd ExpensesAnalysis;
        public static AnalysisTab Instance => _instance ?? (_instance = new AnalysisTab());

        private string _notification;
        public string NotificationMessage {
            get => _notification;
            set
            {
                _notification = value;
                OnPropertyChanged(NotificationMessage);
            }
        }
        
        private AnalysisTab()
        {
            ExpensesAnalysis = Analysis.Instance;
        }
        private void ThrowNotification(string message)
        {
            NotificationMessage = message;
        }

        public void LoadDataRange(DateTime starTime, DateTime endTime)
        {
            if (endTime > starTime)
            {
                ExpensesAnalysis.LoadDataRange(starTime, endTime);
            }
            else
            {
                ThrowNotification($"Selected Start time {starTime} bigger that End time {endTime}");
            }

        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
