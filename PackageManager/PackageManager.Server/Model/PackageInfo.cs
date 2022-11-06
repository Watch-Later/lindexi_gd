#nullable disable

using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using PackageManager.Server.Context;

namespace PackageManager.Server.Model;

[Index(nameof(PackageId))]
public class PackageInfo
{
    public string PackageId { set; get; }

    /// <summary>
    /// �����ѺõĲ����
    /// </summary>
    public string Name { set; get; }

    public string Author { set; get; }
    public long Version { set; get; }
    public string Description { set; get; }

    /// <summary>
    /// ͼ������ص�ַ
    /// </summary>
    public string IconUrl { set; get; }

    /// <summary>
    /// �Ƿ��ܹ�չʾ����
    /// </summary>
    public bool CanShow { set; get; }

    /// <summary>
    /// ������Ϣ���ɲ�Ҫ̫��Ŷ
    /// </summary>
    public string DownloadUrl { set; get; }

    public long SupportMinClientVersion { set; get; }

    public void CopyTo(PackageInfo packageInfo)
    {
        packageInfo.Author = Author;
        packageInfo.Name = Name;
        packageInfo.Version = Version;
        packageInfo.Description = Description;
        packageInfo.IconUrl = IconUrl;
        packageInfo.SupportMinClientVersion = SupportMinClientVersion;
        packageInfo.DownloadUrl = DownloadUrl;
        packageInfo.CanShow = CanShow;
        packageInfo.PackageId = PackageId;
    }
}