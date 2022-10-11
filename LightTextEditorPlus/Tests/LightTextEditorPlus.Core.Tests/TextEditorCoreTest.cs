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

    [ContractTestCase]
    public void AppendText()
    {
        "���ı��༭��׷��һ�δ��ı����ȴ��� DocumentChanging �ٴ��� DocumentChanged �¼�".Test(() =>
        {
            // Arrange
            var textEditorCore = TestHelper.GetTextEditorCore();
            var raiseCount = 0;

            textEditorCore.DocumentChanging += (sender, args) =>
            {
                // Assert
                Assert.AreEqual(0, raiseCount);
                raiseCount++;
            };

            textEditorCore.DocumentChanged += (sender, args) =>
            {
                // Assert
                Assert.AreEqual(1, raiseCount);
                raiseCount = 2;
            };

            // Action
            textEditorCore.AppendText(TestHelper.PlainNumberText);

            // Assert
            Assert.AreEqual(2, raiseCount);
        });

        // todo �����״�׷�ӵľ��� \r\n ����
        // todo ���Ǵ��� 123\r\n123 �ı�
        // todo ���Ǵ��� 123\r\n123\r\n �ı�

    }

}