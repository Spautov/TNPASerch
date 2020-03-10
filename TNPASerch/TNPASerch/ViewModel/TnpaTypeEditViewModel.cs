using DAL;
using GalaSoft.MvvmLight.Command;
using Repositories;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TNPASerch.View;

namespace TNPASerch.ViewModel
{
    public class TnpaTypeEditViewModel : BaseViewModel
    {
        public ICommand AddTypeCommand { get; set; }
        public ICommand RemoveTypeCommand { get; set; }
        public ICommand EditTypeCommand { get; set; }

        private readonly IRepository _repository;

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

        public TnpaTypeEditViewModel(TnpaTypeEditView window)
        {
            _repository = SQLiteRepository.GetRepository();
            AddTypeCommand = new RelayCommand(AddType);
            RemoveTypeCommand = new RelayCommand(RemoveType);
            EditTypeCommand = new RelayCommand(EditType);
            GetTnpaTypsAsync();
        }

        private void EditType()
        {
            if (SelectedTnpaType != null)
            {
                AddTextView addTextView = new AddTextView
                {
                    Owner = App.Current.MainWindow
                };
                AddTextViewModel addTextViewModel = new AddTextViewModel()
                {
                    TextValue = SelectedTnpaType.Name,
                    Title = "Введите наименование типа"
                };
                addTextView.DataContext = addTextViewModel;
                addTextView.ShowDialog();
                if (addTextView.DialogResult == true)
                {
                    string textresoult = addTextViewModel.TextValue.Trim(' ');
                    if (!String.IsNullOrEmpty(textresoult) || String.IsNullOrWhiteSpace(textresoult))
                    {
                        if (_repository.FindTnpaTypeByName(textresoult) != null)
                        {
                            YesMessage($"Тип {textresoult} уже существует");
                            return;
                        }
                        else
                        {
                            SelectedTnpaType.Name = textresoult;
                            _repository.Update(SelectedTnpaType);
                            GetTnpaTypsAsync();
                        }
                    }
                }
            }
        }

        private void RemoveType()
        {
            if (SelectedTnpaType != null)
            {
                var resoult = YesCancelMessage($"Вы действительно желаете удалить тип {SelectedTnpaType.Name}?");

                if (resoult)
                {
                    try
                    {
                        _repository.DeleteTnpaType(SelectedTnpaType.Id);
                        YesMessage($"Тип {SelectedTnpaType.Name} успешно удален");
                        GetTnpaTypsAsync();
                    }
                    catch (Exception ex)
                    {
                        YesMessage(ex.Message, "Ошибка");
                    }
                }
            }
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
            AddTextView addTextView = new AddTextView
            {
                Owner = App.Current.MainWindow
            };
            AddTextViewModel addTextViewModel = new AddTextViewModel
            {
                Title = "Введите наименование типа"
            };
            addTextView.DataContext = addTextViewModel;
            addTextView.ShowDialog();
            if (addTextView.DialogResult == true)
            {
                string textresoult = addTextViewModel.TextValue.Trim(' ');
                if (!String.IsNullOrEmpty(textresoult) || String.IsNullOrWhiteSpace(textresoult))
                {
                    TnpaType tnpaType = new TnpaType
                    {
                        Name = textresoult
                    };

                    try
                    {
                        _repository.Create(tnpaType);
                        GetTnpaTypsAsync();
                        YesMessage($"Тип {textresoult} успешно добавлен");
                    }
                    catch (Exception ex)
                    {
                        YesMessage(ex.Message, "Ошибка");
                    }
                   
                }
            }
        }

        private async void GetTnpaTypsAsync()
        {
            var colllectTnpaType = await _repository.GetTnpaTypeListAsunc();
            TnpaTypes = new ObservableCollection<TnpaType>(colllectTnpaType);
        }
    }
}
