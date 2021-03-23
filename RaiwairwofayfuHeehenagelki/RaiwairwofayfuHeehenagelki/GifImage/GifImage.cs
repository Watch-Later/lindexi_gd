
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace RaiwairwofayfuHeehenagelki.GifImage
{

    /// <summary>
    ///     ��GifImage - ����Gif����
    ///     ��л jillzhang �ṩ�㷨
    /// </summary>
    internal class GifImage
    {
        #region ����ͼƬ�ĳ��� 

        /// <summary>
        ///     ����ͼƬ�ĳ���
        /// </summary>
        internal short Width => LogicalScreenDescriptor.Width;

        #endregion

        #region ����ͼƬ�ĸ߶�

        /// <summary>
        ///     ����ͼƬ�ĸ߶�
        /// </summary>
        internal short Height => LogicalScreenDescriptor.Height;

        #endregion

        #region Gif�ĵ�ɫ��

        /// <summary>
        ///     Gif�ĵ�ɫ��
        /// </summary>
        internal Color[] Palette
        {
            get
            {
                var act = PaletteHelper.GetColor32s(GlobalColorTable);
                act[LogicalScreenDescriptor.BgColorIndex] = Color.FromArgb(0,0,0,0);
                return act;
            }
        }

        #endregion

     
        /// <summary>
        ///    ��ʼ6�ֽ���GIF���� �ļ��汾�ţ�GIF���� �ǡ�GIF�����ļ��汾��Ϊ "87a"��"89a"
        /// </summary>
        internal string Header { get; set; } = "";

       

        #region ȫ����ɫ�б�

        /// <summary>
        ///     ȫ����ɫ�б�
        /// </summary>
        internal byte[] GlobalColorTable { get; set; }

        #endregion

        #region ȫ����ɫ��������

        /// <summary>
        ///     ȫ����ɫ��������
        /// </summary>
        internal Hashtable GlobalColorIndexedTable { get; } = new Hashtable();

        #endregion

        #region ע����չ�鼯��

        /// <summary>
        ///     ע�Ϳ鼯��
        /// </summary>
        internal List<CommentEx> CommentExtensions { get; set; } = new List<CommentEx>();

        #endregion

        #region Ӧ�ó�����չ�鼯��

        /// <summary>
        ///     Ӧ�ó�����չ�鼯��
        /// </summary>
        internal List<ApplicationEx> ApplictionExtensions { get; set; } = new List<ApplicationEx>();

        #endregion

        #region ͼ���ı���չ����

        /// <summary>
        ///     ͼ���ı���չ����
        /// </summary>
        internal List<PlainTextEx> PlainTextExtensions { get; set; } = new List<PlainTextEx>();

        #endregion

        #region �߼���Ļ����

        /// <summary>
        ///     �߼���Ļ����
        /// </summary>
        internal LogicalScreenDescriptor LogicalScreenDescriptor { get; set; }

        #endregion

        #region ����������֡����

        /// <summary>
        ///     ����������֡����
        /// </summary>
        internal List<GifFrame> Frames { get; set; } = new List<GifFrame>();

        #endregion
    }

}