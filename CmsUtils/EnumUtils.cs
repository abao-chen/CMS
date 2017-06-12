namespace CmsUtils
{

    #region ����ģ��ö��

    /// <summary>
    /// ����״̬
    /// </summary>
    public enum OrderStatusEnum
    {
        /// <summary>
        /// δ֧��
        /// </summary>
        WaittingPayment = 2,

        /// <summary>
        /// 3����֧��
        /// </summary>
        WaittingDelivery = 3,

        /// <summary>
        /// �������
        /// </summary>
        OrderComplete = 3,

        /// <summary>
        /// ����ȡ��
        /// </summary>
        OrderCancel = 4,

        /// <summary>
        /// ����ȡ��
        /// </summary>
        OrderFail = 5
    }

    /// <summary>
    /// ֧����ʽ
    /// </summary>
    public enum PayWayEnum
    {
        /// <summary>
        /// ֧����
        /// </summary>
        Alipay = 1,

        /// <summary>
        /// ����֧��
        /// </summary>
        InternetBanking = 2,

        /// <summary>
        /// �Ż�ȯ
        /// </summary>
        Coupon = 3,

        /// <summary>
        /// �Ƹ�ͨ
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
