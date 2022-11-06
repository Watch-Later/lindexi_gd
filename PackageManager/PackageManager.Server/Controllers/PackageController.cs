using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PackageManager.Server.Context;
using PackageManager.Server.Model;
using PackageManager.Server.Utils;

namespace PackageManager.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class PackageController : ControllerBase
{
    public PackageController(PackageManagerContext packageManagerContext)
    {
        PackageManagerContext = packageManagerContext;
    }

    private PackageManagerContext PackageManagerContext { get; }

    /// <summary>
    /// ��ȡ�����ػ����
    /// </summary>
    /// <param name="request"></param>
    /// <returns>
    /// ���ظ����İ������°汾�����°汾ָ���Ǵ���Ĳ�������֧�ֵ����°汾
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetPackageRequest? request)
    {
        if (!string.IsNullOrEmpty(request?.PackageId))
        {
            var packageInfo = await 
                PackageManagerContext.LatestPackageDbSet.FirstOrDefaultAsync(t => t.PackageId == request.PackageId);

            if (packageInfo != null)
            {
                // �ж����°汾���Ƿ�֧��
                // ��ǰ�Ŀͻ��˰汾���ڵ������֧�ֿͻ��˰汾
                var clientVersionValue = long.MaxValue;
                if (Version.TryParse(request.ClientVersion, out var clientVersion))
                {
                    clientVersionValue = clientVersion.VersionToLong();
                }

                if (clientVersionValue >= packageInfo.SupportMinClientVersion)
                {
                    return Ok(new GetPackageResponse("Success", packageInfo));
                }

                // ���򷵻���֧��������汾�����汾�ŵ���Դ
                var storagePackageInfo = await PackageManagerContext.PackageStorageDbSet
                    .Where(t=>t.PackageId== request.PackageId)
                    .Where(t => clientVersionValue >= t.SupportMinClientVersion).OrderByDescending(t => t.Version)
                    .FirstOrDefaultAsync();

                if (storagePackageInfo is not null)
                {
                    return Ok(new GetPackageResponse("Success", storagePackageInfo));
                }
            }
        }

        return Ok(new GetPackageResponse($"NotFound {request}",null));
    }


    // ��ȡ���еİ��ĸ���

    // �о���ҳ�����а�

    [HttpGet(nameof(GetPackageListInMainPage))]
    public IActionResult GetPackageListInMainPage()
    {
        var list = PackageManagerContext.LatestPackageDbSet.Where(t => t.CanShow).ToList();
        return Ok(list);
    }

    /// <summary>
    /// ���Ͱ�
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] PutPackageRequest request)
    {
        if (HttpContext.Request.Headers.TryGetValue("Token", out var value)
            // ֤����Ȩ�޿�������
            && string.Equals(value.ToString(), TokenConfiguration.Token, StringComparison.Ordinal))
        {
            // �ȴ� LatestPackageDbSet �����Ƴ����������еģ�Ȼ���ټ����µ�
            // ��˾��� LatestPackageDbSet ֻ������µ�
            var packageId = request.PackageInfo.PackageId;
            var currentPackageInfo = await PackageManagerContext.LatestPackageDbSet.FirstOrDefaultAsync(t => t.PackageId == packageId);
            if (currentPackageInfo != null)
            {
                PackageManagerContext.LatestPackageDbSet.Remove(currentPackageInfo);
            }

            var latestPackageInfo = new LatestPackageInfo();
            request.PackageInfo.CopyTo(latestPackageInfo);
            PackageManagerContext.LatestPackageDbSet.Add(latestPackageInfo);

            var storagePackageInfo = new StoragePackageInfo();
            request.PackageInfo.CopyTo(storagePackageInfo);
            PackageManagerContext.PackageStorageDbSet.Add(storagePackageInfo);
            await PackageManagerContext.SaveChangesAsync();
            return Ok();
        }

        return NotFound();
    }
}

public record PutPackageRequest(PackageInfo PackageInfo);

public class StringVersionComparer : IComparer<string>
{
    public int Compare(string? x, string? y)
    {
        // ���� ����1
        // ���� ����0
        // С�� ���ظ���
        if (x is null && y is null) return 0;
        if (x is null && y is not null) return -1;
        if (x is not null && y is null) return 1;

        var xSuccess = Version.TryParse(x, out var versionX);
        var ySuccess = Version.TryParse(y, out var versionY);

        if (!xSuccess && !ySuccess) return 0;
        if (!xSuccess && ySuccess) return -1;
        if (xSuccess && !ySuccess) return 1;

        if (versionX is not null && versionY is not null)
        {
            return versionX.CompareTo(versionY);
        }
        else
        {
            // �����ϲ����������߼�
            return 0;
        }
    }
}