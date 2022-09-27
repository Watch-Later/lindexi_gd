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


    [ContractTestCase]
    public void BuildTextLogger()
    {
        "�ı�����־���Բ�Ϊ�գ���ʹƽ̨���ؿ�".Test(() =>
        {
            // Arrange
            var testPlatformProvider = new TestPlatformProvider();

            // Action
            var textEditorCore = new TextEditorCore(testPlatformProvider);

            // Assert
            Assert.IsNotNull(textEditorCore.Logger);
        });
    }
}