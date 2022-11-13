using PackageManager.Server.Model;

namespace PackageManager.Server.Controllers;

/// <summary>
/// ��ȡ��������
/// </summary>
/// <param name="ClientVersion"></param>
/// <param name="PackageIdOrNamePattern">���� Id �ţ�����������</param>
public record GetPackageRequest(string? ClientVersion, string PackageIdOrNamePattern)
{
}

public record GetPackageResponse(string Message, PackageInfo? PackageInfo)
{
    public bool IsNotFound => PackageInfo is null;
}