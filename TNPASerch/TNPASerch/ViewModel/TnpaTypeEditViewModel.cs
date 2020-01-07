using DAL;
using DbWorker;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TNPASerch.View;

namespace TNPASerch.ViewModel
{
    public class TnpaTypeEditViewModel : BaseViewModel
    {
        public ICommand AddTypeCommand { get; set; }
        public ICommand RemoveTypeCommand { get; set; }
        public ICommand EditTypeCommand { get; set; }
        public ICommand CancelTypeCommand { get; set; }

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

        public TnpaTypeEditViewModel(TnpaTypeEditView window): base(window)
        {
            _dbContext = new TnpaDbContext();
            _lockDb = new object();
            AddTypeCommand = new RelayCommand(AddType);
            RemoveTypeCommand = new RelayCommand(RemoveType);
            EditTypeCommand = new RelayCommand(EditType);
            CancelTypeCommand = new RelayCommand(Close);
            GetTnpaTypsAsync();
        }

        private void EditType()
        {
            if (SelectedTnpaType != null)
            {
                AddTextView addTextView = new AddTextView();
                AddTextViewModel addTextViewModel = new AddTextViewModel(addTextView)
                {
                    TextValue = SelectedTnpaType.Name
                };
                addTextView.DataContext = addTextViewModel;
                addTextView.ShowDialog();
                if (addTextView.DialogResult == true)
                {
                    string textresoult = addTextViewModel.TextValue.Trim(' ');
                    if (!String.IsNullOrEmpty(textresoult) || String.IsNullOrWhiteSpace(textresoult))
                    {
                        var collect = _dbContext.TnpaTypes.Select(a => a).Where(x => x.Name.ToUpper().Equals(textresoult.ToUpper()));
                        if (collect.ToList().Count() > 0)
                        {
                            YesMessage($"Тип {textresoult} уже существует");
                            return;
                        }
                        else
                        {
                            SelectedTnpaType.Name = textresoult;

                            _dbContext.Update(SelectedTnpaType);
                            _dbContext.SaveChanges();
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
                var nameType = SelectedTnpaType.Name;
                
                var resoult = YesCancelMessage($"Вы действительно желаете удалить тип {nameType}?");

                if (resoult)
                {
                    var resoulCollect = _dbContext.Tnpas.Select(x => x).Where(el => el.TnpaTypeId == SelectedTnpaType.Id);
                    if (resoulCollect.Count() > 0)
                    {
                        YesMessage($"Тип {nameType} невозможно удалить, " +
                            $"так как существуют ТНПА связанные с ним. Сначала нужно удалить соответствующие ТНПА, " +
                            $"а затем тип.", "Ошибка");
                        return;
                    }

                    _dbContext.TnpaTypes.Remove(SelectedTnpaType);
                    _dbContext.SaveChanges();
                    GetTnpaTypsAsync();
                    YesMessage($"Тип {nameType} успешно удален");

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
            AddTextView addTextView = new AddTextView();
            AddTextViewModel addTextViewModel = new AddTextViewModel(addTextView);
            addTextView.DataContext = addTextViewModel;
            addTextView.ShowDialog();
            if (addTextView.DialogResult == true)
            {
                string textresoult = addTextViewModel.TextValue.Trim(' ');
                if (!String.IsNullOrEmpty(textresoult) || String.IsNullOrWhiteSpace(textresoult))
                {
                    var collect = _dbContext.TnpaTypes.Select(a => a).Where(x => x.Name.ToUpper().Equals(textresoult.ToUpper()));
                    if (collect.ToList().Count() > 0)
                    {
                        YesMessage($"Тип {textresoult} уже существует");
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
                        GetTnpaTypsAsync();
                        YesMessage($"Тип {textresoult} успешно добавлен");
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
    }
}
