using DAL;
using GalaSoft.MvvmLight.Command;
using Ninject;
using Repositories;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TNPASerch.View;
using CommunityToolkit.Mvvm.Input;

namespace TNPASerch.ViewModel
{
    public class TnpaTypeEditViewModel : BaseViewModel
    {
        public IAsyncRelayCommand AddTypeCommand { get; set; }
        public ICommand RemoveTypeCommand { get; set; }
        public IAsyncRelayCommand EditTypeCommand { get; set; }

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

        public TnpaTypeEditViewModel()
        {
            _repository = App.Container.Get<IRepository>();
            AddTypeCommand = new AsyncRelayCommand(AddTypeAsync);
            RemoveTypeCommand = new CommunityToolkit.Mvvm.Input.RelayCommand(RemoveType);
            EditTypeCommand = new AsyncRelayCommand(EditType);
            GetTnpaTypsAsync();
        }

        private async Task EditType()
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
                            await _repository.UpdateAsync(SelectedTnpaType);
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

        private async Task AddTypeAsync()
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
                        await _repository.CreateAsync(tnpaType);
                        GetTnpaTypsAsync();
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
