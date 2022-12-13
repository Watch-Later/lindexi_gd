using System.Windows.Media;
using dotnetCampus.UITest.WPF;
using LightTextEditorPlus.Utils;
using MSTest.Extensions.Contracts;

namespace LightTextEditorPlus.Tests;

[TestClass]
public class ConverterTest
{
    [UIContractTestCase]
    public void Equals()
    {
        "����������ɫֵ��ͬ�Ĵ�ɫ��ˢ���������".Test(() =>
        {
            var brush1 = new SolidColorBrush(Color.FromRgb(0x02, 0x03, 0x00));
            var brush2 = new SolidColorBrush(Color.FromRgb(0x02, 0x03, 0x00));

            var result = Converter.AreEquals(brush1, brush2);
            Assert.AreEqual(true, result);
        });

        "������������ͬ��Ԥ�贿ɫ��ˢ�����ز����".Test(() =>
        {
            var brush1 = Brushes.White;
            var brush2 = Brushes.Black;

            var result = Converter.AreEquals(brush1, brush2);
            Assert.AreEqual(false, result);
        });

        "���������� Null �Ļ�ˢ���������".Test(() =>
        {
            Brush brush1 = null;
            Brush brush2 = null;

            var result = Converter.AreEquals(brush1, brush2);
            Assert.AreEqual(true, result);
        });
        "������һ�� Null �Ļ�ˢ��һ��Ԥ��Ĵ�ɫ�����ز����".Test(() =>
        {
            Brush brush1 = null;
            var brush2 = Brushes.Black;

            var result = Converter.AreEquals(brush1, brush2);
            Assert.AreEqual(false, result);
        });

        "����һ��Ԥ��Ĵ�ɫ����һ�� Null �Ļ�ˢ�����ز����".Test(() =>
        {
            var brush1 = Brushes.Black;
            Brush brush2 = null;

            var result = Converter.AreEquals(brush1, brush2);
            Assert.AreEqual(false, result);
        });

        "��������Ԥ��Ĵ�ɫ�������ж����".Test(() =>
        {
            var brush1 = Brushes.Black;
            var brush2 = Brushes.Black;

            var result = Converter.AreEquals(brush1, brush2);
            Assert.AreEqual(true, result);
        });
    }
}