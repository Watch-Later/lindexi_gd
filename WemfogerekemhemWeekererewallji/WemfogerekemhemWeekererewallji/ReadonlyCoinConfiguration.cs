using Microsoft.Extensions.Primitives;

namespace WemfogerekemhemWeekererewallji;

class ReadonlyCoinConfiguration : IConfigurationSource, IConfigurationProvider
{
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return this;
    }

    public bool TryGet(string key, out string value)
    {
        value = string.Empty;
        return false;
    }

    public void Set(string key, string value)
    {
        // ��֧��
    }

    public IChangeToken GetReloadToken()
    {
        return new CancellationChangeToken(CancellationToken.None);
    }

    public void Load()
    {
        // ��֧��
    }

    public IEnumerable<string> GetChildKeys(IEnumerable<string> earlierKeys, string parentPath)
    {
        // ���������������ʵ��������
        // 1. �������� IConfigurationProvider �Ľ�����й���
        // 2. ���ظ���ܲ㣬�� IConfigurationProvider �ṩ��������

        // �Լ����ԣ�
        // 1. ʲô�����������ص��� earlierKeys ������
        // 2. ֱ�ӷ��� Array.Empty<string>();
        // 3. ƴ�ӳ��µ��б��� 

        // ������������ṩ���������棬�������� Foo.F1=123; Foo.F2=123; Foo.F3=123 ����ֵ����
        // ����� ��·��(parentPath) �ǽ��� `Foo` ��ô��Ӧ�ý� `Foo.F1` �� `Foo.F2` �� `Foo.F3` ���� Key ��ϲ� earlierKeys ���з���
        // Ĭ�϶��ǲ��� Concat(earlierKeys) �ķ�ʽ���з��ص�
        // ��ʲô����²��ǲ���ֱ�� Concat(earlierKeys) �ķ�ʽ���ţ���Ҫ���˵�����������Ҫ������������

        if (string.IsNullOrEmpty(parentPath))
        {
            /*
            [WTTSTDIO, C:\Program Files (x86)\Windows Kits\10\Hardware Lab Kit\Studio\]
            [windir, C:\Windows]
            ...
            [APPDATA, C:\Users\lindexi\AppData\Roaming]
            [ALLUSERSPROFILE, C:\ProgramData]
            [AllowedHosts, *]
            [, ]
            [:ASPNETCORE_BROWSER_TOOLS, true]
            [:Foo.F3, ]
            [:Foo.F2, ]
            [:Foo.F1, ]
            [Foo.F3, ]
            [Foo.F2, ]
            [Foo.F1, ]
             */
            return new string[] { "Foo.F1", "Foo.F2", "Foo.F3" }.Concat(earlierKeys);

            /*
          [Foo.F3, ]
          [Foo.F2, ]
          [Foo.F1, ]
          [WTTSTDIO, C:\Program Files (x86)\Windows Kits\10\Hardware Lab Kit\Studio\]
          [windir, C:\Windows]
          ...
          [APPDATA, C:\Users\lindexi\AppData\Roaming]
          [ALLUSERSPROFILE, C:\ProgramData]
          [AllowedHosts, *]
          [, ]
          [:Foo.F3, ]
          [:Foo.F2, ]
          [:Foo.F1, ]
          [:ASPNETCORE_BROWSER_TOOLS, true]
           */
            return earlierKeys.Concat(new string[] { "Foo.F1", "Foo.F2", "Foo.F3" });
          
        }

        return earlierKeys;

        // ��ʱö���ǿհ�
        return Array.Empty<string>();
    }
}