using MSTest.Extensions.Contracts;

namespace LightTextEditorPlus.Core.Tests;

[TestClass]
public class TextEditorCoreTest
{
    [ContractTestCase]
    public void TestCreate()
    {
        "�����ı��Ĵ���".Test(() =>
        {
            var textEditorCore = new TextEditorCore(new TestPlatformProvider());

            // û���쳣���Ǿ��Ƿ���Ԥ��
            Assert.IsNotNull(textEditorCore);
        });
    }
}