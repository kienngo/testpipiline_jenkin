using System;
using System.Collections.Generic;
using System.Text;
using Wiseman.PJC.Gen2.Http.Message.ErrorTypes;
using Wiseman.PJC.Gen2.Http.Message.Interfaces;

namespace Wiseman.PJC.Service.GroupSettings.Http.Message.ErrorContents
{
    /// <summary>
    /// 標準エラープロパティを実装したエラーコンテンツクラス
    /// </summary>
    /// <remarks>
    /// <para>参考用です</para>
    /// </remarks>
    public class AllStandardErrorResponseContent :
        IErrorResponseContent,
        IRequiredProperty,
        IMaxLengthProperty,
        IMinLengthProperty,
        IMailAddressProperty,
        IPhoneNumberProperty,
        IAlphaProperty,
        IAlphaNurmericProperty,
        IUpperCaseProperty,
        ILowerCaseProperty,
        IUrlFormatProperty,
        IDateFormatProperty,
        ITimeFormatProperty,
        IDateTimeFormatProperty,
        IDateTimeRangeProperty,
        IIntegerValueProperty,
        IDoubleValueProperty,
        IMaxIntegerProperty,
        IMinIntegerProperty,
        IMaxDoubleProperty,
        IMinDoubleProperty,
        IUserIDFormatProperty,
        INanProperty,
        IContainsEnumProperty,
        IContainsEnumOfStringProperty,
        ICollectionCountProperty,
        IStringLengthByteProperty,
        IRangeProperty,
        IStringLengthProperty,
        IRegularExpressionProperty,
        IZenkakuProperty,
        IHankakuProperty,
        IPostalCodeProperty,
        IFileStoragePersistKeyProperty,
        IIllegalCustomHeaderProperty,
        IBindErrorProperty,
        IExternalReferenceErrorProperty
    {
        #region 標準Validatorエラー
        /// <summary>必須エラー</summary>
        public ValidationError ErrorCode0001 { get; set; }
        /// <summary>入力可能文字数エラー</summary>
        public ValidationError ErrorCode0002 { get; set; }
        /// <summary>最小文字列長エラー</summary>
        public ValidationError ErrorCode0003 { get; set; }
        /// <summary>数値範囲エラー</summary>
        public ValidationError ErrorCode0004 { get; set; }
        /// <summary>文字列長範囲エラー</summary>
        public ValidationError ErrorCode0005 { get; set; }
        /// <summary>文字列形式エラー</summary>
        public ValidationError ErrorCode0006 { get; set; }
        /// <summary>全角英数字制約エラー</summary>
        public ValidationError ErrorCode0007 { get; set; }
        /// <summary>半角英数字制約エラー</summary>
        public ValidationError ErrorCode0008 { get; set; }
        /// <summary>郵便番号形式エラー</summary>
        public ValidationError ErrorCode0009 { get; set; }
        /// <summary>メールアドレス形式エラー</summary>
        public ValidationError ErrorCode0010 { get; set; }
        /// <summary>電話番号形式エラー</summary>
        public ValidationError ErrorCode0011 { get; set; }
        /// <summary>アルファベット制約エラー</summary>
        public ValidationError ErrorCode0012 { get; set; }
        /// <summary>英数制約エラー</summary>
        public ValidationError ErrorCode0013 { get; set; }
        /// <summary>大文字制約エラー</summary>
        public ValidationError ErrorCode0014 { get; set; }
        /// <summary>小文字制約エラー</summary>
        public ValidationError ErrorCode0015 { get; set; }
        /// <summary>URL形式エラー</summary>
        public ValidationError ErrorCode0016 { get; set; }
        /// <summary>日付形式エラー</summary>
        public ValidationError ErrorCode0017 { get; set; }
        /// <summary>時刻形式エラー</summary>
        public ValidationError ErrorCode0018 { get; set; }
        /// <summary>日時形式エラー</summary>
        public ValidationError ErrorCode0019 { get; set; }
        /// <summary>日時範囲エラー</summary>
        public ValidationError ErrorCode0020 { get; set; }
        /// <summary>整数値制約エラー</summary>
        public ValidationError ErrorCode0021 { get; set; }
        /// <summary>小数値制約エラー</summary>
        public ValidationError ErrorCode0022 { get; set; }
        /// <summary>整数最大値エラー</summary>
        public ValidationError ErrorCode0023 { get; set; }
        /// <summary>整数最小値エラー</summary>
        public ValidationError ErrorCode0024 { get; set; }
        /// <summary>少数最大値エラー</summary>
        public ValidationError ErrorCode0025 { get; set; }
        /// <summary>少数最小値エラー</summary>
        public ValidationError ErrorCode0026 { get; set; }
        /// <summary>ユーザID形式エラー</summary>
        public ValidationError ErrorCode0027 { get; set; }
        /// <summary>数値制約エラー</summary>
        public ValidationError ErrorCode0028 { get; set; }
        /// <summary>列挙体制約エラー</summary>
        public ValidationError ErrorCode0029 { get; set; }
        /// <summary>列挙体文字列エラー</summary>
        public ValidationError ErrorCode0030 { get; set; }
        /// <summary>コレクション要素０件エラー</summary>
        public ValidationError ErrorCode0031 { get; set; }
        /// <summary>文字列バイト長超過エラー</summary>
        public ValidationError ErrorCode0032 { get; set; }
        /// <summary>ファイルストレージの永続化キー不正エラー</summary>
        public ValidationError ErrorCode0033 { get; set; }
        /// <summary>カスタムヘッダー必須エラー</summary>
        public ValidationError ErrorCode0098 { get; set; }
        #endregion

        /// <summary>Bindエラー</summary>
        public BindError ErrorCode0099 { get; set; }
        /// <summary>外部参照エラー</summary>
        public ExternalReferenceError ErrorCode1000 { get; set; }
    }
}
