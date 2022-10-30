#nullable disable

using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace PackageManager.Server.Model;

[Index(nameof(PackageId))]
public class PackageInfo
{
    [JsonIgnore]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { set; get; }

    public string PackageId { set; get; }

    /// <summary>
    /// �����ѺõĲ����
    /// </summary>
    public string Name { set; get; }

    public string Author { set; get; }
    public string Version { set; get; }
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

    public string SupportMinClientVersion { set; get; }
    public string SupportClientPlatform { set; get; }
}