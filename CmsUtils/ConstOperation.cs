
namespace CmsUtils
{
    public class ConstOperation
    {
        #region Url

        /// <summary>
        /// 后台登录地址
        /// </summary>
        public static string UrlBackLogin = "/admin/login.aspx";

        /// <summary>
        /// 后台首页地址
        /// </summary>
        public static string UrlBackIndex = "/admin/main.aspx";

        #endregion

        #region 提示文字

        public static string TextValidateCodeError = "验证码输入不正确";
        public static string TextLoginError = "用户名或密码输入不正确";
        public static string TextLoginTimeOutError = "您没有登录或登录已过期，请重新登录！";
        public static string TextAddSuccess = "添加成功";
        public static string TextAddFail = "添加失败";
        public static string TextUpdateSuccess = "编辑成功";
        public static string TextUpdateFail = "编辑失败";
        public static string TextDelSuccess = "删除成功";
        public static string TextDelFail = "删除失败";
        public static string TextError = "更新失败";


        #endregion

        #region 颜色

        public static string ColorRed = "#ff0000";
        public static string ColorGray = "#999999";

        #endregion

        #region 上传图片路径（产品）
        /// <summary>
        /// 正常产品图片路径 
        /// </summary>
        public static string PhotoNormalProduct = "/Upload/Image/Product/Normal/";
        /// <summary>
        /// 前台列表产品图片路径
        /// </summary>
        public static string PhotoListProduct = "/Upload/Image/Product/List/";
        /// <summary>
        /// 后台列表产品图片路径 
        /// </summary>
        public static string PhotoBackListProduct = "/Upload/Image/Product/BackList/";

        #endregion

        #region 上传图片路径（友情链接）
        /// <summary>
        /// 链接图片路径 
        /// </summary>
        public static string PhotoFriendLink = "/Upload/Image/FriendLink/";
        #endregion

    }
}
