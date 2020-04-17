using DAL;
using GalaSoft.MvvmLight.Command;
using Ninject;
using Repositories;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TNPASerch.ViewModel
{
    public class WatchFilesViewModel : BaseViewModel
    {
        public ICommand ShowDataFileInfoCommand { get; set; }

        private readonly IFileRepository _fileRepository;

        private readonly Tnpa _tnpa;

        private ObservableCollection<DataFileInfo> _dataFileInfos;
        public ObservableCollection<DataFileInfo> DataFileInfos
        {
            get { return _dataFileInfos; }
            set
            {
                _dataFileInfos = value;
                OnPropertyChanged();
            }
        }

        private DataFileInfo _selectedDataFileInfo;
        public DataFileInfo SelectedDataFileInfo
        {
            get { return _selectedDataFileInfo; }
            set
            {
                _selectedDataFileInfo = value;
                OnPropertyChanged();
            }
        }

        public WatchFilesViewModel(Tnpa tnpa)
        {
            _tnpa = tnpa ?? throw new ArgumentNullException(nameof(tnpa));
            ShowDataFileInfoCommand = new RelayCommand(ShowDataFileInfo);
            _fileRepository = App.Container.Get<IFileRepository>();
            GetDataFileInfos();
        }

        private void ShowDataFileInfo()
        {
            if (SelectedDataFileInfo != null)
            {
                var resolt = _fileRepository.OpenFile(SelectedDataFileInfo.Path);
                if (!resolt)
                {
                    YesMessage("Не возмоно открыть файл", "Ошибка");
                }
            }
        }

        private void GetDataFileInfos()
        {
            DataFileInfos = new ObservableCollection<DataFileInfo>(_tnpa.Files);
        }
    }
}
