using System.Diagnostics;
using System.Windows;

namespace LightTextEditorPlus.Tests;

public record TextEditTestContext(Window TestWindow, TextEditor TextEditor):IDisposable
{
    public void Dispose()
    {
        if (TestFramework.IsDebug())
        {
            // ����ڸ��ӵ��ԣ��Ǿ��Ȳ��˳���
            return;
        }

        TestWindow.Close();
    }
}