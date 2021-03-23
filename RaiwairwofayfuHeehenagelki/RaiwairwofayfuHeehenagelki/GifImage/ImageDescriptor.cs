using System;
using System.Collections.Generic;

namespace RaiwairwofayfuHeehenagelki.GifImage
{
    /// <summary>
    ///     ͼ���ʶ��(Image Descriptor)һ��GIF�ļ��ڿ��԰������ͼ��
    ///     һ��ͼ�����֮�����������һ��ͼ��ı�ʶ����ͼ���ʶ����0x2C(',')
    ///     �ַ���ʼ���������������ͼ������ʣ�����ͼ��������߼���Ļ�߽��ƫ������
    ///     ͼ���С�Լ����޾ֲ���ɫ�б����ɫ�б��С����10���ֽ����
    /// </summary>
    internal class ImageDescriptor
    {

        internal byte[] GetBuffer()
        {
            var list = new List<byte> { GifExtensions.ImageDescriptorLabel };
            list.AddRange(BitConverter.GetBytes(XOffSet));
            list.AddRange(BitConverter.GetBytes(YOffSet));
            list.AddRange(BitConverter.GetBytes(Width));
            list.AddRange(BitConverter.GetBytes(Height));
            var m = 0;
            if (LocalColorTableFlag)
            {
                m = 1;
            }

            var i = 0;
            if (InterlaceFlag)
            {
                i = 1;
            }

            var s = 0;
            if (SortFlag)
            {
                s = 1;
            }

            var pixel = (byte) (Math.Log(LocalColorTableSize, 2) - 1);
            var packed = (byte) (pixel | (s << 5) | (i << 6) | (m << 7));
            list.Add(packed);
            return list.ToArray();
        }


        #region �ṹ�ֶ�      

        /// <summary>
        ///     X����ƫ����
        /// </summary>
        internal short XOffSet { set; get; }

        /// <summary>
        ///     X����ƫ����
        /// </summary>
        internal short YOffSet { set; get; }

        /// <summary>
        ///     ͼ����
        /// </summary>
        internal short Width { set; get; }

        /// <summary>
        ///     ͼ��߶�
        /// </summary>
        internal short Height { set; get; }

        /// <summary>
        ///     packed
        /// </summary>
        internal byte Packed { set; get; }

        /// <summary>
        ///     �ֲ���ɫ�б��־(Local Color Table Flag)
        ///     ��λʱ��ʶ������ͼ���ʶ��֮����һ���ֲ���ɫ�б�����������֮���һ��ͼ��ʹ�ã�
        ///     ֵ��ʱʹ��ȫ����ɫ�б�����pixelֵ��
        /// </summary>
        internal bool LocalColorTableFlag { set; get; }

        /// <summary>
        ///     ��֯��־(Interlace Flag)����λʱͼ������ʹ��������ʽ���У�����ʹ��˳�����С�
        /// </summary>
        internal bool InterlaceFlag { set; get; }

        /// <summary>
        ///     �����־(Sort Flag)�������λ��ʾ�����ŵľֲ���ɫ�б��������.
        /// </summary>
        internal bool SortFlag { set; get; }

        /// <summary>
        ///     pixel - �ֲ���ɫ�б��С(Size of Local Color Table)��pixel+1��Ϊ��ɫ�б��λ��
        /// </summary>
        internal int LocalColorTableSize { set; get; }

        #endregion
    }
}