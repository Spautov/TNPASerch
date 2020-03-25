using DAL;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using Ninject;
using Repositories;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TNPASerch.ViewModel
{
    public class EditFilesViewModel : BaseViewModel
    {
        public ICommand AddDataFileInfoCommand { get; set; }
        public ICommand RemoveDataFileInfoCommand { get; set; }
        public ICommand ShowDataFileInfoCommand { get; set; }

        private IFileRepository _fileRepository;

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

        public EditFilesViewModel(Tnpa tnpa)
        {
            _tnpa = tnpa ?? throw new ArgumentNullException(nameof(tnpa));

            AddDataFileInfoCommand = new RelayCommand(AddDataFileInfo);
            RemoveDataFileInfoCommand = new RelayCommand(RemoveDataFileInfo);
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

        private void RemoveDataFileInfo()
        {
            if (SelectedDataFileInfo != null)
            {
                var res = YesNoCancelMessage("Вы действительно желаете удалить файл?", "?");
                if (res)
                {
                    var resolt = _fileRepository.RemoveFile(SelectedDataFileInfo.Path);

                    if (resolt)
                    {
                        _tnpa.Files.Remove(SelectedDataFileInfo);
                    }
                    else
                    {
                        YesMessage("Ошибка удаления файла", "Ошибка");
                    }
                    GetDataFileInfos();
                }
            }
        }

        private void AddDataFileInfo()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text files (*.doc,*.docx,*.pdf,*.txt)| *.doc;*.docx;*.pdf;*.txt",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    var files = _fileRepository.AddFile(openFileDialog.FileName);
                    DataFileInfo dataFileInfo = new DataFileInfo
                    {
                        Path = files,
                        Tnpa = _tnpa,
                        HashCode = _fileRepository.CalculateHashCodeFile(files)
                    };
                    _tnpa.Files.Add(dataFileInfo);
                    GetDataFileInfos();
                }
                catch (Exception)
                {
                    YesMessage("Не удалось добавить файл", "Ошибка");
                }
            }
        }

        private void GetDataFileInfos()
        {
            DataFileInfos = new ObservableCollection<DataFileInfo>(_tnpa.Files);
        }
    }
}
