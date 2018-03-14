namespace FirstUnitTest.BL.BE.Ticket
{
    /// <summary>
    /// TicketSlaveStatusEnum
    /// </summary>
    public enum TicketSlaveStatusEnum
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal,

        /// <summary>
        /// 無效資料
        /// </summary>
        InvalidationData,

        /// <summary>
        /// 條碼長度超過上限
        /// </summary>
        CodeLengthOverLimit,

        /// <summary>
        /// 無效條碼格式
        /// </summary>
        InvalidationCodeFormat,

        /// <summary>
        /// 同份檔案條碼重複
        /// </summary>
        ExistInSameFile,

        /// <summary>
        /// 驗證完成
        /// </summary>
        Finish
    }
}
