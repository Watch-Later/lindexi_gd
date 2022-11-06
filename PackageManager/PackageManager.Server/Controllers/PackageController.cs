using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PackageManager.Server.Context;
using PackageManager.Server.Model;

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
                return Ok(new GetPackageResponse("Success", packageInfo));
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

            PackageManagerContext.LatestPackageDbSet.Add(request.PackageInfo);
            PackageManagerContext.PackageStorageDbSet.Add(request.PackageInfo);
            await PackageManagerContext.SaveChangesAsync();
            return Ok();
        }

        return NotFound();
    }
}

public record PutPackageRequest(PackageInfo PackageInfo);