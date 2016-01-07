﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace produproperty
{
    class viewModel : notify_property
    {
        public viewModel()
        {
            _m = new model();

            text = "选择要保存位置";
            writetext = true;
        }
        public bool firstget
        {
            set;
            get;
        }
        public bool updateproper
        {
            set;
            get;
        }
        public string text
        {
            set
            {
                _text = value;
                OnPropertyChanged();
            }
            get
            {
                return _text;
            }
        }

        public Action<int, int> selectchange
        {
            set;
            get;
        }

        public int select
        {
            set
            {
                _select = value;
            }
            get
            {
                return _select;
            }
        }

        public void property()
        {
            text = _m.property(text, firstget, updateproper);
            //Random ran = new Random();
            //int n = 'z' - 'a' + 1;
            //List<char> str = new List<char>();
            //StringBuilder t = new StringBuilder();            
            //for (int i = 0; i < n; i++)
            //{
            //    str.Add((char)('a' + i));
            //}
            //for (int i = 0; i < n; i++)
            //{
            //    str.Add((char)('A' + i));
            //}
            //n = 10;
            //for (int i = 0; i < n; i++)
            //{
            //    str.Add((char)('0' + i));
            //}
            //n = 100000;
            //for (int i = 0; i < n; i++)
            //{
            //    t.Append(str[ran.Next() % str.Count]);
            //}
            //text = t.ToString();
        }

        /// <summary>
        /// 拖入图片
        /// </summary>
        public async void dropimg(object sender, Windows.UI.Xaml.DragEventArgs e)
        {
            if (writetext)
            {
                return;
            }

            var defer = e.GetDeferral();

            try
            {
                DataPackageView dataView = e.DataView;
                // 拖放类型为文件存储。
                if (dataView.Contains(StandardDataFormats.StorageItems))
                {
                    var files = await dataView.GetStorageItemsAsync();
                    StorageFile file = files.OfType<StorageFile>().First();
                    if (file.FileType == ".png" || file.FileType == ".jpg")
                    {
                        // 拖放的是图片文件。
                        //BitmapImage bitmap = new BitmapImage();
                        //await bitmap.SetSourceAsync(await file.OpenAsync(FileAccessMode.Read));
                        //ximg.ImageSource = bitmap;
                        imgfolder(file);
                    }
                }
            }
            finally
            {
                defer.Complete();
            }
        }

        public async void folderaddress()
        {
            Windows.Storage.Pickers.FolderPicker picker = new Windows.Storage.Pickers.FolderPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation =
               Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".md");
            var folder = await picker.PickSingleFolderAsync();
            if (folder != null)
            {
                _folder = folder;
                folderaddresstext();
            }
        }


        public async Task file_open()
        {
            Windows.Storage.Pickers.FileOpenPicker picker = new Windows.Storage.Pickers.FileOpenPicker();
            //显示方式
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            //选择最先的位置
            picker.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            //后缀名
            picker.FileTypeFilter.Add(".txt");
            picker.FileTypeFilter.Add(".md");

            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                reminder = "选择 " + file.Name;
                _file = file;
                fileaddresstext();
            }
        }

        /// <summary>
        /// 获取存储文件位置
        /// </summary>
        public async void fileaddress()
        {
            //获取保存文件
            Windows.Storage.Pickers.FileSavePicker picker = new Windows.Storage.Pickers.FileSavePicker();
            //显示方式
            //picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            //选择最先的位置
            picker.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            //后缀名
            picker.SuggestedFileName = "博客";
            picker.DefaultFileExtension = ".md";
            picker.FileTypeChoices.Add("博客", new List<string>() { ".md", ".txt" });


            StorageFile file = await picker.PickSaveFileAsync();
            if (file != null)
            {
                reminder = "选择 " + file.Name;
                _file = file;
                _folder = await file.GetParentAsync();
                fileaddresstext();
            }

        }
        /// <summary>
        /// 保存文件
        /// </summary>
        public void storage()
        {

        }



        public bool writetext
        {
            set
            {
                _writetext = value;
                OnPropertyChanged();
            }
            get
            {
                return _writetext;
            }
        }

        private string _text;
        private model _m;
        private StorageFolder _folder;
        private StorageFile _file;
        private bool _writetext;
        private int _select;



        private void fileaddresstext()
        {
            writetext = false;
            text = "#" + _file.DisplayName + "#\r\n";
            //selectchange(2);
            if (_folder == null)
            {
                reminder = "没有找到保存文件夹";
                folderaddress();
            }
        }

        private void folderaddresstext()
        {
            //writetext = false;
            //string str = "输入标题";
            //text = "#" + str + "#\r\n";
            //selectchange(1, str.Length);        
        }

        private async void imgfolder(StorageFile file)
        {
            string str = "image";
            StorageFolder image = null;
            try
            {
                image = await _folder.GetFolderAsync(str);
            }
            catch
            {


            }
            if (image == null)
            {
                image = await _folder.CreateFolderAsync(str, CreationCollisionOption.OpenIfExists);
            }
            file = await file.CopyAsync(image, file.Name, NameCollisionOption.GenerateUniqueName);

            str = $"![这里写图片描述](image/{file.Name})";

            text = text.Insert(select, str);

            selectchange(select + 2, 7);
        }
    }
}
