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
        if (!string.IsNullOrEmpty(request?.PackageIdOrNamePattern))
        {
            // �ж����°汾���Ƿ�֧��
            // ��ǰ�Ŀͻ��˰汾���ڵ������֧�ֿͻ��˰汾
            var clientVersionValue = long.MaxValue;
            if (Version.TryParse(request.ClientVersion, out var clientVersion))
            {
                clientVersionValue = clientVersion.VersionToLong();
            }

            var packageInfo = await FindPackageInfoAsync(request.PackageIdOrNamePattern, clientVersionValue);

            if (packageInfo != null)
            {
                return Ok(new GetPackageResponse("Success", packageInfo));
            }
            else
            {
                // ���Խ�������ķ�ʽ�������Ҳ��������
                // ������Щ��Ը����Ϊ�ɼ��ģ��ǻ��ǲ������ˣ�ֻ������ð� Id �ķ�ʽ�ṩ
                // ��������Ͳ��ٴ���
            }
        }

        return Ok(new GetPackageResponse($"NotFound {request}", null));
    }

    /// <summary>
    /// ��ȡ���еİ��ĸ���
    /// </summary>
    /// <param name="updateAllPackageRequest"></param>
    /// <returns></returns>
    [HttpPost(nameof(UpdateAllPackage))]
    public async Task<IActionResult> UpdateAllPackage(UpdateAllPackageRequest updateAllPackageRequest)
    {
        var clientVersionValue = 0L;
        if (Version.TryParse(updateAllPackageRequest.ClientVersion, out var clientVersion))
        {
            clientVersionValue = clientVersion.VersionToLong();
        }

        // ��ʵ�ͻ��˰汾����������ڿͻ��˰汾
        // �����ֻ��һ���ж�
        if (clientVersionValue <= 0)
        {
            return Ok(new UpdateAllPackageResponse(ResponseErrorCode.DoNotSupportClientVersion.Code,
                ResponseErrorCode.DoNotSupportClientVersion.Message, new List<PackageInfo>(0)));
        }

        var result = new List<PackageInfo>();
        foreach (var updatePackageRequest in updateAllPackageRequest.PackageList ?? new List<UpdatePackageRequest>(0))
        {
            // �ƺ�һ���������е��
            var packageInfo = await FindPackageInfoAsync(updatePackageRequest.PackageId, clientVersionValue);

            if (packageInfo is not null)
            {
                if (packageInfo.Version > updatePackageRequest.CurrentPackageVersion)
                {
                    result.Add(packageInfo);
                }
            }
        }

        return Ok(new UpdateAllPackageResponse(ResponseErrorCode.Ok.Code, ResponseErrorCode.Ok.Message, result));
    }

    /// <summary>
    /// �о���ҳ�����а�
    /// </summary>
    /// <returns></returns>
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
            var currentPackageInfo =
                await PackageManagerContext.LatestPackageDbSet.FirstOrDefaultAsync(t => t.PackageId == packageId);
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

    private async Task<PackageInfo?> FindPackageInfoAsync(string packageId, long clientVersion)
    {
        var packageInfo = await
            PackageManagerContext.LatestPackageDbSet.FirstOrDefaultAsync(t => t.PackageId == packageId);

        if (packageInfo != null)
        {
            // �ж�һ�¿ͻ��˰汾

            // �ж����°汾���Ƿ�֧��
            // ��ǰ�Ŀͻ��˰汾���ڵ������֧�ֿͻ��˰汾

            if (clientVersion >= packageInfo.SupportMinClientVersion)
            {
                return packageInfo;
            }
        }

        // �����ж�һ����ʷ�汾
        var storagePackageInfo = await PackageManagerContext.PackageStorageDbSet
            .Where(t => t.PackageId == packageId)
            .Where(t => clientVersion >= t.SupportMinClientVersion).OrderByDescending(t => t.Version)
            .FirstOrDefaultAsync();

        return storagePackageInfo;
    }
}