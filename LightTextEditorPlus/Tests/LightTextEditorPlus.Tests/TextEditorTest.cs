using System.Windows;
using dotnetCampus.UITest.WPF;
using MSTest.Extensions.Contracts;

namespace LightTextEditorPlus.Tests;

[TestClass]
public class TextEditorTest
{
    [UIContractTestCase]
    public void AppendTestAfterSetRunProperty()
    {
        "��׷��һ���ı������޸ĵ�ǰ������ԣ���׷��һ���ı������Է���Ԥ�ڵ���ʾ������ʽ��ͬ���ı�".Test(async () =>
        {
            using var context = TestFramework.CreateTextEditorInNewWindow();
            var textEditor = context.TextEditor;

            // ��׷��һ���ı�
            textEditor.TextEditorCore.AppendText("123");

            // ���޸ĵ�ǰ�������
            textEditor.SetRunProperty(runProperty => runProperty.FontSize = 15);

            // ��׷��һ���ı�
            textEditor.TextEditorCore.AppendText("123");

            // ���Է���Ԥ�ڵ���ʾ������ʽ��ͬ���ı�
            // �ȿ���ȥ��
            await TestFramework.FreezeTestToDebug();
        });
    }

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