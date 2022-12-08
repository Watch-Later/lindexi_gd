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
            using var context = TestFramework.CreateTextEditorInNewWindow();
            var textEditor = context.TextEditor;

            textEditor.TextEditorCore.AppendText("123");

            await TestFramework.FreezeTestToDebug();
        });
    }
}