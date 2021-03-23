using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Drawing.Color;

namespace RaiwairwofayfuHeehenagelki.GifImage
{
    /// <summary>
    ///     GIFͼ���ļ��Ľ�����
    /// </summary>
    internal class GifDecoder
    {
        /// <summary>
        ///     ��gifͼ���ļ����н���
        /// </summary>
        /// <param name="gifPath">gif�ļ�·��</param>
        internal static GifImage Decode(string gifPath)
        {
            using (var stream = new GifStream(new FileStream(gifPath, FileMode.Open)))
            {
                var gifImage = new GifImage();
                var graphics = new List<GraphicEx>();
                var frameCount = 0;

                //��ȡ�ļ�ͷ

                //��ʼ6�ֽ���GIF���� �ļ��汾�ţ�GIF���� �ǡ�GIF�����ļ��汾��Ϊ "87a"��"89a"
                //87a �� 1987 �棬89a �� 1989 ����ģ����Ծ���Ҫ���� 6 �ֽ�
                gifImage.Header = stream.ReadString(6);
                //��ȡ�߼���Ļ��ʾ��
                gifImage.LogicalScreenDescriptor = stream.GetLogicalScreenDescriptor();
                if (gifImage.LogicalScreenDescriptor.GlobalColorTableFlag)
                {
                    //��ȡȫ����ɫ�б�
                    gifImage.GlobalColorTable =
                        stream.ReadByte(gifImage.LogicalScreenDescriptor.GlobalColorTableSize * 3);
                }

                int nextFlag;
                while ((nextFlag = stream.Read()) != GifExtensions.Terminator)
                {
                    if (nextFlag == GifExtensions.ImageLabel)
                    {
                        ReadImage(stream, gifImage, graphics, frameCount);
                        frameCount++;
                    }
                    else if (nextFlag == GifExtensions.ExtensionIntroducer)
                    {
                        var gcl = stream.Read();
                        switch (gcl)
                        {
                            case GifExtensions.GraphicControlLabel:
                            {
                                var graphicEx = stream.GetGraphicControlExtension();
                                graphics.Add(graphicEx);
                                break;
                            }
                            case GifExtensions.CommentLabel:
                            {
                                var comment = stream.GetCommentEx();
                                gifImage.CommentExtensions.Add(comment);
                                break;
                            }
                            case GifExtensions.ApplicationExtensionLabel:
                            {
                                var applicationEx = stream.GetApplicationEx();
                                gifImage.ApplictionExtensions.Add(applicationEx);
                                break;
                            }
                            case GifExtensions.PlainTextLabel:
                            {
                                var textEx = stream.GetPlainTextEx();
                                gifImage.PlainTextExtensions.Add(textEx);
                                break;
                            }
                        }
                    }
                    else if (nextFlag == GifExtensions.EndIntroducer)
                    {
                        //�����ļ�β
                        break;
                    }
                }

                return gifImage;
            }
        }


        private static void ReadImage(GifStream stream, GifImage gifImage, List<GraphicEx> graphics,
            int frameCount)
        {
            var imageDescriptor = stream.GetImageDescriptor();
            var frame = new GifFrame { ImageDescriptor = imageDescriptor, LocalColorTable = gifImage.GlobalColorTable };
            if (imageDescriptor.LocalColorTableFlag)
            {
                frame.LocalColorTable = stream.ReadByte(imageDescriptor.LocalColorTableSize * 3);
            }

            var lzwDecoder = new LZWDecoder(stream);
            var dataSize = stream.Read();
            frame.ColorDepth = dataSize;
            var pixel = lzwDecoder.DecodeImageData(imageDescriptor.Width, imageDescriptor.Height, dataSize);
            frame.IndexedPixel = pixel;
            var blockSize = stream.Read();
            _ = new DataStruct(blockSize, stream);
            var graphicEx = graphics[frameCount];
            frame.GraphicExtension = graphicEx;
            frame.Image = GetImageFromPixel(pixel, imageDescriptor.Width, imageDescriptor.Height, frame.Palette,
                imageDescriptor.InterlaceFlag);
            gifImage.Frames.Add(frame);
        }

        private static ImageSource GetImageFromPixel(byte[] pixel, int logicalWidth,
            int logicalHeight, Color[] colorTable, bool interlaceFlag)
        {
            var dest = new int[logicalWidth * logicalHeight];
            var pointer = 0;
            var tempPointer = pointer;

            var offSet = 0;
            if (interlaceFlag)
            {
                #region ��֯�洢ģʽ

                var i = 0;
                var pass = 0; //��ǰͨ��            
                while (pass < 4)
                {
                    //�ܹ���4��ͨ��
                    if (pass == 1)
                    {
                        pointer = tempPointer;
                        pointer += 4 * logicalWidth;
                        offSet += 4 * logicalWidth;
                    }
                    else if (pass == 2)
                    {
                        pointer = tempPointer;
                        pointer += 2 * logicalWidth;
                        offSet += 2 * logicalWidth;
                    }
                    else if (pass == 3)
                    {
                        pointer = tempPointer;
                        pointer += 1 * logicalWidth;
                        offSet += 1 * logicalWidth;
                    }

                    var rate = 2;
                    if ((pass == 0) | (pass == 1))
                    {
                        rate = 8;
                    }
                    else if (pass == 2)
                    {
                        rate = 4;
                    }

                    while (i < pixel.Length)
                    {
                        dest[pointer] = colorTable[pixel[i++]].ToArgb();
                        pointer++;

                        offSet++;
                        if (i % logicalWidth == 0)
                        {
                            pointer += logicalWidth * (rate - 1);
                            offSet += logicalWidth * (rate - 1);
                            if (offSet >= pixel.Length)
                            {
                                pass++;
                                offSet = 0;
                                break;
                            }
                        }
                    }
                }

                #endregion
            }
            else
            {
                for (var i = 0; i < pixel.Length;)
                {
                    dest[pointer] = colorTable[pixel[i++]].ToArgb();
                    pointer++;
                }
            }

            return BitmapSource.Create(logicalWidth, logicalHeight, 96, 96, PixelFormats.Bgr32, null, dest, 4 * logicalWidth);
        }
    }
}