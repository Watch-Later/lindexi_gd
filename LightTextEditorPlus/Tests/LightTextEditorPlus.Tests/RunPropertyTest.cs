using dotnetCampus.UITest.WPF;
using MSTest.Extensions.Contracts;

namespace LightTextEditorPlus.Tests;

[TestClass]
public class RunPropertyTest
{
    [UIContractTestCase]
    public void Equals()
    {
        "��ȡ������Ĭ�ϵ� RunProperty �����ж���ȣ��������".Test(() =>
        {
            var (mainWindow, textEditor) = TestFramework.CreateTextEditorInNewWindow();
            var runPropertyCreator = textEditor.TextEditorPlatformProvider.GetPlatformRunPropertyCreator();

            // ��ȡ������Ĭ�ϵ� RunProperty ����
            var runProperty1 = runPropertyCreator.GetDefaultRunProperty();
            var runProperty2 = runPropertyCreator.GetDefaultRunProperty();

            // �ж���ȣ��������
            var equals = runProperty1.Equals(runProperty2);
            Assert.AreEqual(true,equals);
        });
    }
}