using DAL;
using DbWorker;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TNPASerch.View;

namespace TNPASerch.ViewModel
{
    public class TnpaTypeEditViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand AddTypeCommand { get; set; }
        public ICommand RemoveTypeCommand { get; set; }
        public ICommand EditTypeCommand { get; set; }

        private readonly TnpaDbContext _dbContext;
        private readonly object _lockDb;

        private ObservableCollection<TnpaType> _tnpaTypes;
        public ObservableCollection<TnpaType> TnpaTypes
        {
            get { return _tnpaTypes; }
            set
            {
                _tnpaTypes = value;
                OnPropertyChanged();
            }
        }

        public TnpaTypeEditViewModel()
        {
            _dbContext = new TnpaDbContext();
            _lockDb = new object();
            AddTypeCommand = new RelayCommand(AddType);
            GetTnpaTypsAsync();
        }

        private TnpaType _selectedTnpaType;
        public TnpaType SelectedTnpaType
        {
            get { return _selectedTnpaType; }
            set
            {
                _selectedTnpaType = value;
                OnPropertyChanged();
            }
        }

        private void AddType()
        {
            AddTextView addTextView = new AddTextView();
            AddTextViewModel addTextViewModel = new AddTextViewModel(addTextView);
            addTextView.DataContext = addTextViewModel;
            addTextView.ShowDialog();
            if (addTextView.DialogResult == true)
            {
                string textresoult = addTextViewModel.TextValue.Trim(' ');
                if (!String.IsNullOrEmpty(textresoult) || String.IsNullOrWhiteSpace(textresoult))
                {
                   var collect = _dbContext.TnpaTypes.Select(a=>a).Where(x => x.Name.ToUpper().Equals(textresoult.ToUpper()));
                    if (collect.ToList().Count() > 0)
                    {
                        MessageBox.Show($"Тип {textresoult} уже существует");
                        return;
                    }
                    else
                    {
                        TnpaType tnpaType = new TnpaType
                        {
                            Name = textresoult
                        };
                        _dbContext.TnpaTypes.Add(tnpaType);
                        _dbContext.SaveChanges();
                        MessageBox.Show($"Тип {textresoult} успешно добавлен");
                    }
                }
            }
        }

        private async void GetTnpaTypsAsync()
        {
            await Task.Run(() => GetTnpaTyps());
        }

        private void GetTnpaTyps()
        {
            lock (_lockDb)
            {
                var colllectTnpaType = _dbContext.TnpaTypes.Select(x => x);
                TnpaTypes = new ObservableCollection<TnpaType>(colllectTnpaType.ToList());
            }
        }
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
