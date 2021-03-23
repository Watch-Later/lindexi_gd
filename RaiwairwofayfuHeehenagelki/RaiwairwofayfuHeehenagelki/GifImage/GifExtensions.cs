
namespace RaiwairwofayfuHeehenagelki.GifImage
{


    internal static class GifExtensions
    {
        /// <summary>
        ///     Extension Introducer 
        /// ͼ�ο�����չ ����һ��ͼ���(ͼ���ʶ��)���ı���չ���ǰ�棬�������Ƹ���������ĵ�һ��ͼ��
        /// </summary>
        internal const byte ExtensionIntroducer = 0x21;

        /// <summary>
        ///     lock Terminator 
        /// </summary>
        internal const byte Terminator = 0;


        /// <summary>
        ///     Application Extension Label 
        /// </summary>
        internal const byte ApplicationExtensionLabel = 0xFF;


        /// <summary>
        ///     Comment Label
        /// </summary>
        internal const byte CommentLabel = 0xFE;


        /// <summary>
        /// һ�� Gif �����ܶ��ͼƬ��ʹ�� 0x2C ˵��ͼƬ��ʼ
        /// </summary>
        internal const byte ImageDescriptorLabel = ImageLabel;

        /// <summary>
        ///     Plain Text Label
        /// </summary>
        internal const byte PlainTextLabel = 0x01;

        /// <summary>
        ///     Graphic Control Label 
        /// </summary>
        internal const byte GraphicControlLabel = 0xF9;

        /// <summary>
        /// һ�� Gif �����ܶ��ͼƬ��ʹ�� 0x2C ˵��ͼƬ��ʼ
        /// </summary>
        internal const byte ImageLabel = 0x2C;

       /// <summary>
        /// ��ʶGIF�ļ�����
       /// </summary>
        internal const byte EndIntroducer = 0x3B;
    }

}