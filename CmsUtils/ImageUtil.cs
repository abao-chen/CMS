using System.Drawing.Imaging;
using System.Drawing;

namespace CmsUtils
{
    public class ImageUtil
    {

        #region 图片地址

        /// <summary>
        /// 新闻类别图片路径
        /// </summary>
        public static string NewsClassImagePath = "/Upload/NewsClass/";
        /// <summary>
        /// 新闻图片路径
        /// </summary>
        public static string NewsImagePath = "/Upload/News/";

        #endregion

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="FromImagePath">源图路径(绝对路径)</param>
        /// <param name="ToImagePath">缩略图路径(绝对路径)</param>
        /// <param name="MaxWidth">最大宽度</param>
        /// <param name="MaxHeight">最大高度</param>
        public static void CreatePic(string FromImagePath, string ToImagePath, double MaxWidth, double MaxHeight)
        {
            Bitmap tmp = null;
            Graphics g = null;
            //double Max_width = 110, Max_height = 110;//假设最大宽度以及最大高度
            int Width = 0;//框条和卡纸的宽度
            int Height = 0;//框条和卡纸的高度
            double wmp = 1, hmp = 1, default_pparm = 1;//默认宽的比例，默认高对应的比例，最大宽度比例，最大高度比例，默认实际比例，最大实际比例
            System.Drawing.Image imgPic = null;//画芯
            imgPic = System.Drawing.Image.FromFile(FromImagePath);
            Width = imgPic.Width;
            Height = imgPic.Height;
            wmp = MaxWidth / Width;//最大宽度比例
            hmp = MaxHeight / Height;//最大高度比例
            default_pparm = wmp < hmp ? wmp : hmp;//默认实际比例


            if (default_pparm > 1)
            {
                default_pparm = 1;
            }
            Width = (int)(Width * default_pparm);
            Height = (int)(Height * default_pparm);

            tmp = new Bitmap((int)(Width), (int)(Height));//最大的容器
            g = Graphics.FromImage(tmp);
            //g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, tmp.Width, tmp.Height));
            Rectangle Boxs = new Rectangle(0, 0, imgPic.Width, imgPic.Height);
            Rectangle Boxd = new Rectangle(0, 0,
                (int)(Width), (int)(Height));//算图的起点
            g.DrawImage(imgPic, Boxd, Boxs, GraphicsUnit.Pixel);
            tmp.Save(ToImagePath, ImageFormat.Jpeg);
        }
    }
}
