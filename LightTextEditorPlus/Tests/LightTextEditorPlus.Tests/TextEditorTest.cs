using System.Windows;
using dotnetCampus.UITest.WPF;
using MSTest.Extensions.Contracts;

namespace LightTextEditorPlus.Tests;

[TestClass]
public class TextEditorTest
{
    [UIContractTestCase]
    public void AppendText()
    {
        "���յ��ı���׷�� 123 �ַ�����������ʾ�� 123 ���ı�".Test(async () =>
        {
            var (mainWindow, textEditor) = TestFramework.CreateTextEditorInNewWindow();

            textEditor.TextEditorCore.AppendText("123");

            await Task.Delay(TimeSpan.FromSeconds(1));

            mainWindow.Close();
        });
    }
}