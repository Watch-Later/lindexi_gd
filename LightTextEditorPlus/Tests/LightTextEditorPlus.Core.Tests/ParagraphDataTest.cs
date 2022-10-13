using LightTextEditorPlus.Core.Carets;
using LightTextEditorPlus.Core.Document.Segments;
using MSTest.Extensions.Contracts;

namespace LightTextEditorPlus.Core.Tests;

[TestClass]
public class ParagraphDataTest
{
    [ContractTestCase]
    public void GetRunIndex()
    {
        "�������������� 0 ��ȡ����� RunIndex ���Ի�ȡ���׸�".Test(() =>
        {
            // Arrange
            var textEditor = TestHelper.GetTextEditorCore();
            textEditor.AppendText("123");

            // Action
            var paragraphData = textEditor.DocumentManager.TextRunManager.ParagraphManager.GetParagraphData(new CaretOffset(0));
            var runIndex = paragraphData.GetRunIndex(new ParagraphOffset(0));

            // Assert
            Assert.AreEqual(0, runIndex.ParagraphIndex);
        });
    }
}