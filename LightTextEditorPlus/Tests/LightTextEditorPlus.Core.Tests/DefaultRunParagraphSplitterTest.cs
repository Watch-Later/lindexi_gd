using LightTextEditorPlus.Core.Document;
using MSTest.Extensions.Contracts;

namespace LightTextEditorPlus.Core.Tests;

[TestClass]
public class DefaultRunParagraphSplitterTest
{
    [ContractTestCase]
    public void Split()
    {
        "������ı��Ľ�β���������������з������Զ�������ն�".Test(() =>
        {
            // Arrange
            var textEditorCore = TestHelper.GetTextEditorCore();
            var splitter = textEditorCore.PlatformProvider.GetRunParagraphSplitter();

            // Action
            var textRun = new TextRun("123\r\n\r\n");
            var result = splitter.Split(textRun).ToList();

            // Assert
            Assert.AreEqual(3, result.Count);
        });


        "������ı��Ľ�β�������з������Զ��һ���ն�".Test(() =>
        {
            // Arrange
            var textEditorCore = TestHelper.GetTextEditorCore();
            var splitter = textEditorCore.PlatformProvider.GetRunParagraphSplitter();

            // Action
            var textRun = new TextRun("123\r\n");
            var result = splitter.Split(textRun).ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
        });

        "�����������������з����ı������Էֳ��ն�".Test(() =>
        {
            // Arrange
            var textEditorCore = TestHelper.GetTextEditorCore();
            var splitter = textEditorCore.PlatformProvider.GetRunParagraphSplitter();

            // Action
            var textRun = new TextRun("123\r\n\r\n\r\n123\r\n123");
            var result = splitter.Split(textRun).ToList();

            // Assert
            Assert.AreEqual(5, result.Count);
        });

        "�������������з����ı������Ը��ݻ��з����зֶ�".Test(() =>
        {
            // Arrange
            var textEditorCore = TestHelper.GetTextEditorCore();
            var splitter = textEditorCore.PlatformProvider.GetRunParagraphSplitter();

            // Action
            var textRun = new TextRun("123\r\n123\r\n123");
            var result = splitter.Split(textRun).ToList();

            // Assert
            Assert.AreEqual(3, result.Count);
        });

        "����һ���ı��������κλ��з������Էָ�֮�󣬷�����Ȼ��һ��".Test(() =>
        {
            // Arrange
            var textEditorCore = TestHelper.GetTextEditorCore();
            var splitter = textEditorCore.PlatformProvider.GetRunParagraphSplitter();

            // Action
            var result = splitter.Split(new TextRun("123")).ToList();

            // Assert
            Assert.AreEqual(1,result.Count);
        });
    }
}