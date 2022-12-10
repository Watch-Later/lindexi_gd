using System.Windows;
using System.Windows.Controls;

namespace LightTextEditorPlus.Tests;

public static class CSharpMarkupHelper
{
    public static T Row<T>(this T element, int row)
        where T : UIElement
    {
        Grid.SetRow(element, row);

        return element;
    }

    public static T Column<T>(this T element, int column)
        where T : UIElement
    {
        Grid.SetColumn(element, column);

        return element;
    }

    public static T Out<T>(this T obj, out T value)
        // ��������Ϊ object Ҳ���ԣ����ǻ���������߼�
        where T : DependencyObject
    {
        value = obj;
        return obj;
    }

    public static T Do<T>(this T obj, Action<T> action)
        // ��������Ϊ object Ҳ���ԣ����ǻ���������߼�
        where T : DependencyObject
    {
        action(obj);
        return obj;
    }
}