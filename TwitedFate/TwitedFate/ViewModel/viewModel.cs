﻿using Windows.Security.ExchangeActiveSyncProvisioning;

namespace TwitedFate.ViewModel
{
    public class viewModel : notify_property
    {
        public viewModel()
        {
            获取系统信息();
           
        }

        public void quedin()
        {
            Windows.UI.Xaml.Controls.Image image = xf.SelectedItem as Windows.UI.Xaml.Controls.Image;
            _m.writeasset桌面(image.Tag as Windows.Storage.StorageFile);
        }

        public void 获取系统信息()
        {
            Windows.System.Profile.AnalyticsVersionInfo analyticsVersion = Windows.System.Profile.AnalyticsInfo.VersionInfo;

            reminder = analyticsVersion.DeviceFamily;

            ulong v = ulong.Parse(Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamilyVersion);
            ulong v1 = ( v & 0xFFFF000000000000L ) >> 48;
            ulong v2 = ( v & 0x0000FFFF00000000L ) >> 32;
            ulong v3 = ( v & 0x00000000FFFF0000L ) >> 16;
            ulong v4 = ( v & 0x000000000000FFFFL );
            reminder = $"{v1}.{v2}.{v3}.{v4}";
            Windows.ApplicationModel.Package package = Windows.ApplicationModel.Package.Current;
            reminder = package.Id.Architecture.ToString();
            reminder = package.DisplayName;
            EasClientDeviceInformation eas = new EasClientDeviceInformation();
            reminder= eas.SystemManufacturer;
        }
        public Windows.UI.Xaml.Controls.FlipView xf;
        model _m = new model();
    }
}
