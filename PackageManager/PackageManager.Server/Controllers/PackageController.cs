using Microsoft.AspNetCore.Mvc;
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
    public IActionResult Get([FromQuery] GetPackageRequest? request)
    {
        return Ok(new GetPackageResponse($"Hello Version={request}"));
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
    public IActionResult Put([FromBody] PutPackageRequest request)
    {
        if (HttpContext.Request.Headers.TryGetValue("Token", out var value) &&
            string.Equals(value.ToString(), TokenConfiguration.Token, StringComparison.Ordinal))
        {
            // ֤����Ȩ�޿�������
            PackageManagerContext.LatestPackageDbSet.Add(request.PackageInfo);
            PackageManagerContext.PackageStorageDbSet.Add(request.PackageInfo);
            PackageManagerContext.SaveChangesAsync();
            return Ok();
        }

        return NotFound();
    }
}

public static class TokenConfiguration
{
    public const string Token = "B44A0A9E-D6C8-434F-9215-894A30BC5674";
}

public record PutPackageRequest(PackageInfo PackageInfo);