using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Ntreev.Library.Psd;

namespace Psd_Vaidation.view
{
    public partial class ImageIconForm : Form
    {
        public ImageIconForm()
        {
            InitializeComponent();
        }

        public static string PassValue { get; set; }

        private void ImageIconForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = PassValue;

            pictureBox1.BackColor = Color.Transparent;
            pictureBox2.BackColor = Color.Transparent;
            pictureBox3.BackColor = Color.Transparent;
            //pictureBox4.BackColor = Color.Transparent;

            pictureBox1.Location = new Point(0, 30);
            //pictureBox2.Location = new Point(0, 30);
            pictureBox3.Location = new Point(0, 0);

            //pictureBox1.Parent = pictureBox3;
            //pictureBox1.Parent = pictureBox2;

            pictureBox1.Controls.Add(pictureBox3);
            pictureBox2.Controls.Add(pictureBox1);

            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            //pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
        }

        byte[] data1;
        byte[] data2;
        byte[] data3;
        private void button1_Click(object sender, EventArgs e)
        {
            var selectPath = textBox1.Text;
            var path = @"D:\PSD 자체검즘 샘플\OEM_AM";
            var files = Directory.GetFiles(selectPath, "*.psd", SearchOption.AllDirectories); //나중에 selectPath로 변경해주어야한다.

            foreach (var file in files)
            {
                var doc = PsdDocument.Create(file);


                var aWidth1 = 0;
                var aHeight1 = 0;
                var aWidth2 = 0;
                var aHeight2 = 0;
                var aWidth3 = 0;
                var aHeight3 = 0;

                for (int i = 0; i < doc.Childs[0].Childs.Length; i++)
                {
                    if ("고속도로icon" == doc.Childs[0].Childs[i].Name)
                    {
                        aWidth1 = doc.Childs[0].Childs[i].Childs[1].Childs[1].Width;
                        aHeight1 = doc.Childs[0].Childs[i].Childs[1].Childs[1].Height;
                        data1 = doc.Childs[0].Childs[i].Childs[1].Childs[1].Channels[0].Data;

                        aWidth2 = doc.Childs[0].Childs[i].Childs[1].Childs[2].Width;
                        aHeight2 = doc.Childs[0].Childs[i].Childs[1].Childs[2].Height;
                        data2 = doc.Childs[0].Childs[i].Childs[1].Childs[2].Channels[0].Data;

                        aWidth3 = doc.Childs[0].Childs[i].Childs[1].Childs[3].Width;
                        aHeight3 = doc.Childs[0].Childs[i].Childs[1].Childs[3].Height;
                        data3 = doc.Childs[0].Childs[i].Childs[1].Childs[3].Channels[0].Data;
                    }
                }
                pictureBox1.Image = CreateBitmap(aWidth1, aHeight1, data1);
                pictureBox2.Image = CreateBitmap(aWidth2, aHeight2, data2);
                pictureBox3.Image = CreateBitmap(aWidth3, aHeight3, data3);

            }
        }




        byte[] fileData1;
        byte[] fileData2;
        byte[] fileData3;
        byte[] fileData4;
        public Bitmap CreateBitmap(int Width, int Height, byte[] xyArray)
        {
            var selectPath = textBox1.Text;
            var path = @"D:\PSD 자체검즘 샘플\OEM_AM";
            var files = Directory.GetFiles(selectPath, "*.psd", SearchOption.AllDirectories); //나중에 selectPath로 변경해주어야한다.

            byte ColorR1 = 0;
            byte ColorG1 = 0;
            byte ColorB1 = 0;

            byte ColorR2 = 0;
            byte ColorG2 = 0;
            byte ColorB2 = 0;

            byte ColorR3 = 0;
            byte ColorG3 = 0;
            byte ColorB3 = 0;

            foreach (var file in files)
            {
                var doc = PsdDocument.Create(file);



                var aWidth = 0;
                var aHeight = 0;


                for (int i = doc.Childs[0].Childs.Length-1; i>=0; i--)
                {
                    if ("고속도로icon" == doc.Childs[0].Childs[i].Name)
                    {
                        fileData1 = doc.Childs[0].Childs[i].Childs[1].Childs[1].Channels[0].Data;
                        fileData2 = doc.Childs[0].Childs[i].Childs[1].Childs[2].Channels[0].Data;
                        fileData3 = doc.Childs[0].Childs[i].Childs[1].Childs[3].Channels[0].Data;
                        fileData4 = doc.Childs[0].Childs[i].Childs[1].Childs[0].Channels[0].Data;

                        for (int h = doc.Childs[0].Childs[i].Childs[1].Childs.Length - 1; h >= 0; h--)
                        {
                            int nIndex4 = 0;

                            for (aWidth = 0; aWidth <= doc.Childs[0].Childs[i].Childs[1].Childs[h].Width - 1; aWidth++)
                            {
                                for (aHeight = 0; aHeight <= doc.Childs[0].Childs[i].Childs[1].Childs[h].Height - 1; aHeight++)
                                {
                                    if (doc.Childs[0].Childs[i].Childs[1].Childs[h].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[0].Childs[i].Childs[1].Childs[h].Channels[0].Data[nIndex4]) break;
                                    nIndex4++;
                                }
                                if (doc.Childs[0].Childs[i].Childs[1].Childs[h].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[0].Childs[i].Childs[1].Childs[h].Channels[0].Data[nIndex4]) break;
                            }

                            if (doc.Childs[0].Childs[i].Childs[1].Childs[h].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[0].Childs[i].Childs[1].Childs[h].Channels[0].Data[nIndex4])
                            {
                                ColorR1 = doc.Childs[0].Childs[i].Childs[1].Childs[1].Channels[1].Data[nIndex4];
                                ColorG1 = doc.Childs[0].Childs[i].Childs[1].Childs[1].Channels[2].Data[nIndex4];
                                ColorB1 = doc.Childs[0].Childs[i].Childs[1].Childs[1].Channels[3].Data[nIndex4];

                                ColorR2 = doc.Childs[0].Childs[i].Childs[1].Childs[2].Channels[1].Data[nIndex4];
                                ColorG2 = doc.Childs[0].Childs[i].Childs[1].Childs[2].Channels[2].Data[nIndex4];
                                ColorB2 = doc.Childs[0].Childs[i].Childs[1].Childs[2].Channels[3].Data[nIndex4];

                                ColorR3 = doc.Childs[0].Childs[i].Childs[1].Childs[3].Channels[1].Data[nIndex4];
                                ColorG3 = doc.Childs[0].Childs[i].Childs[1].Childs[3].Channels[2].Data[nIndex4];
                                ColorB3 = doc.Childs[0].Childs[i].Childs[1].Childs[3].Channels[3].Data[nIndex4];
                            }
                        }
                    }
                }
            }

            try
            {
                Bitmap Canvas = new Bitmap(Width, Height, PixelFormat.Format8bppIndexed);

                Rectangle rect = new Rectangle(0, 0, Width, Height);
                //bitmap 객체의 rawData를 사용하기 위해 메모리를 lock
                BitmapData CanvasData = Canvas.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

                //rawData를 bitmap 객체에 기록
                //unsafe
                //{
                //byte* Ptr = (byte*)CanvasData.Scan0.ToPointer();
                //for (int y = 0; y < Height; y++)
                //{
                //for (int x = 0; x < Width; x++, Ptr++)
                //{
                //*Ptr = xyArray[x * y];
                //}
                //}
                //}
                Marshal.Copy(xyArray, 0, CanvasData.Scan0, xyArray.Length);
                // 메모리접근끝나면 언록해제
                Canvas.UnlockBits(CanvasData);

                ColorPalette cv = Canvas.Palette;

                if (xyArray.Length == fileData1.Length)
                {
                    for (int i = 0; i < 256; i++)
                    {
                        cv.Entries[i] = Color.FromArgb(i, ColorR1, ColorG1, ColorB1);
                        Canvas.Palette = cv;
                    }
                }

                if (xyArray.Length == fileData2.Length)
                {
                    for (int i = 0; i < 256; i++)
                    {
                        cv.Entries[i] = Color.FromArgb(i, ColorR2, ColorG2, ColorB2);
                        Canvas.Palette = cv;
                    }
                }

                if (xyArray.Length == fileData3.Length)
                {
                    for (int i = 0; i < 256; i++)
                    {
                        cv.Entries[i] = Color.FromArgb(i, 255, 255, 255);
                        Canvas.Palette = cv;
                    }
                }

                //if (xyArray.Length == fileData4.Length)
                //{
                //for (int i = 0; i < 256; i++)
                //{
                //cv.Entries[i] = Color.FromArgb(i, ColorR4, ColorG4, ColorB4);
                //Canvas.Palette = cv;
                //}
                //}

                return Canvas;
            }
            catch (Exception)
            {
                MessageBox.Show("bit Format error");
                return null;
            }
        }

        // 텍스트레이어 10 나오는 부분. 픽셀포멧 문제로 인해 잠시 주석처리.
        //byte[] fileData33;
        //public Bitmap CreateBitmap1(int Width, int Height, byte[] xyArray)
        //{
        //    var selectPath = textBox1.Text;
        //    var path = @"D:\PSD 자체검즘 샘플\OEM_AM";
        //    var files = Directory.GetFiles(selectPath, "*.psd", SearchOption.AllDirectories); //나중에 selectPath로 변경해주어야한다.

        //    byte ColorR3 = 0;
        //    byte ColorG3 = 0;
        //    byte ColorB3 = 0;

        //    foreach (var file in files)
        //    {
        //        var doc = PsdDocument.Create(file);



        //        var aWidth = 0;
        //        var aHeight = 0;

        //        for (int i = doc.Childs[0].Childs.Length-1; i >= 0; i--)
        //        {
        //            if ("고속도로icon" == doc.Childs[0].Childs[i].Name)
        //            {
        //                fileData33 = doc.Childs[0].Childs[i].Childs[1].Childs[3].Channels[0].Data;

        //                int nIndex4 = 0;
        //                for (aWidth = 0; aWidth <= doc.Childs[0].Childs[i].Childs[1].Childs[3].Width - 1; aWidth++)
        //                {
        //                    for (aHeight = 0; aHeight <= doc.Childs[0].Childs[i].Childs[1].Childs[3].Height - 1; aHeight++)
        //                    {
        //                        if (doc.Childs[0].Childs[i].Childs[1].Childs[3].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[0].Childs[i].Childs[1].Childs[3].Channels[0].Data[nIndex4]) break;
        //                        nIndex4++;
        //                    }
        //                    if (doc.Childs[0].Childs[i].Childs[1].Childs[3].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[0].Childs[i].Childs[1].Childs[3].Channels[0].Data[nIndex4]) break;
        //                }

        //                if (doc.Childs[0].Childs[i].Childs[1].Childs[3].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[0].Childs[i].Childs[1].Childs[3].Channels[0].Data[nIndex4])
        //                {
        //                    ColorR3 = doc.Childs[0].Childs[i].Childs[1].Childs[3].Channels[1].Data[nIndex4];
        //                    ColorG3 = doc.Childs[0].Childs[i].Childs[1].Childs[3].Channels[2].Data[nIndex4];
        //                    ColorB3 = doc.Childs[0].Childs[i].Childs[1].Childs[3].Channels[3].Data[nIndex4];
        //                }
        //                /*
        //                for (int h = doc.Childs[0].Childs[i].Childs[1].Childs.Length - 1; h >= 0; h--)
        //                {
        //                    int nIndex4 = 0;

        //                    for (aWidth = 0; aWidth <= doc.Childs[0].Childs[i].Childs[1].Childs[h].Width - 1; aWidth++)
        //                    {
        //                        for (aHeight = 0; aHeight <= doc.Childs[0].Childs[i].Childs[1].Childs[h].Height - 1; aHeight++)
        //                        {
        //                            if (doc.Childs[0].Childs[i].Childs[1].Childs[h].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[0].Childs[i].Childs[1].Childs[h].Channels[0].Data[nIndex4]) break;
        //                            nIndex4++;
        //                        }
        //                        if (doc.Childs[0].Childs[i].Childs[1].Childs[h].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[0].Childs[i].Childs[1].Childs[h].Channels[0].Data[nIndex4]) break;
        //                    }

        //                    if (doc.Childs[0].Childs[i].Childs[1].Childs[h].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[0].Childs[i].Childs[1].Childs[h].Channels[0].Data[nIndex4])
        //                    {
        //                        ColorR3 = doc.Childs[0].Childs[i].Childs[1].Childs[3].Channels[1].Data[nIndex4];
        //                        ColorG3 = doc.Childs[0].Childs[i].Childs[1].Childs[3].Channels[2].Data[nIndex4];
        //                        ColorB3 = doc.Childs[0].Childs[i].Childs[1].Childs[3].Channels[3].Data[nIndex4];
        //                    }
        //                }
        //                */
        //            }
        //        }
        //    }

        //    try
        //    {
        //        Bitmap Canvas = new Bitmap(Width, Height, PixelFormat.Format8bppIndexed);

        //        Rectangle rect = new Rectangle(0, 0, Width, Height);
        //        //bitmap 객체의 rawData를 사용하기 위해 메모리를 lock
        //        BitmapData CanvasData = Canvas.LockBits(rect, ImageLockMode.ReadOnly, Canvas.PixelFormat);
        //        //BitmapData CanvasData = Canvas.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format16bppArgb1555);

        //        /*
        //        for(int nHeight = Height - 1; nHeight > 0; nHeight--)
        //        {
        //            for(int i = 0; i < nHeight; i++)
        //            {
        //                int nIndex = (Height - 1 - nHeight) * Width;
        //                for (int nCnt = 0; nCnt < 1; nCnt++)
        //                {
        //                    byte btFirst = xyArray[nIndex];
        //                    for (int j = 0; j < Width - 1; j++)
        //                    {
        //                        xyArray[nIndex + j] = xyArray[nIndex + j + 1];
        //                    }
        //                    xyArray[nIndex + Width - 1] = btFirst;
        //                }
        //            }
        //        }
        //        */

        //        Marshal.Copy(xyArray, 0, CanvasData.Scan0, xyArray.Length);
        //        // 메모리접근끝나면 언록해제
        //        Canvas.UnlockBits(CanvasData);

        //        //ColorPalette cv = Canvas.Palette;
        //        //if (xyArray.Length == fileData3.Length)
        //        //{
        //        //for (int i = 0; i < 256; i++)
        //        //{
        //        //cv.Entries[i] = Color.FromArgb(i, 255, 255, 255);
        //        //Canvas.Palette = cv;
        //        //}
        //        //}

        //        return Canvas;
        //    }
        //    catch (Exception)
        //    {
        //        MessageBox.Show("bit Format error");
        //        return null;
        //    }
        //}




    }
}