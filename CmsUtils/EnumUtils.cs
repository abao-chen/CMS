namespace CmsUtils
{

    #region 订单模块枚举

    /// <summary>
    /// 订单状态
    /// </summary>
    public enum OrderStatusEnum
    {
        /// <summary>
        /// 未支付
        /// </summary>
        WaittingPayment = 2,

        /// <summary>
        /// 3：已支付
        /// </summary>
        WaittingDelivery = 3,

        /// <summary>
        /// 订单完成
        /// </summary>
        OrderComplete = 3,

        /// <summary>
        /// 订单取消
        /// </summary>
        OrderCancel = 4,

        /// <summary>
        /// 订单取消
        /// </summary>
        OrderFail = 5
    }

    /// <summary>
    /// 支付方式
    /// </summary>
    public enum PayWayEnum
    {
        /// <summary>
        /// 支付宝
        /// </summary>
        Alipay = 1,

        /// <summary>
        /// 网银支付
        /// </summary>
        InternetBanking = 2,

        /// <summary>
        /// 优惠券
        /// </summary>
        Coupon = 3,

        /// <summary>
        /// 财付通
        /// </summary>
        Tenpay = 4
    }

    #endregion

    public enum SQLType
    {
        Insert,
        Update,
        DeleteUpdate
    }
}
