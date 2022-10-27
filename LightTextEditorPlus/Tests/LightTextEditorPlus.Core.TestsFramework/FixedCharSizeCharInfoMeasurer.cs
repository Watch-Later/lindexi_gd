using LightTextEditorPlus.Core.Document;
using LightTextEditorPlus.Core.Layout;
using LightTextEditorPlus.Core.Platform;
using LightTextEditorPlus.Core.Primitive;

namespace LightTextEditorPlus.Core.Tests;

/// <summary>
/// �̶��ַ�ʹ���ֺ���Ϊ�ߴ�Ĳ���
/// </summary>
/// �����ڵ�Ԫ�������Ӿ���ƽ̨�;��������Ӱ��
public class FixedCharSizeCharInfoMeasurer : ICharInfoMeasurer
{
    public CharInfoMeasureResult MeasureCharInfo(in CharInfo charInfo)
    {
        return new CharInfoMeasureResult(new Rect(0, 0, charInfo.RunProperty.FontSize, charInfo.RunProperty.FontSize));
    }
}