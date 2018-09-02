using System.Globalization;
using System.Text;

namespace System
{
    /// <summary>
    /// 值格式化扩展
    /// </summary>
    public static class ValueToStringHelper
    {
        /// <summary>
        /// 拆分到单词(每个标点或大写字母作为分隔符)
        /// </summary>
        /// <param name="word">单词</param>
        /// <returns>复数形式</returns>
        public static string[] SpliteWord(this string word)
        {
            return StringHelper.SpliteWord(word);
        }

        /// <summary>
        /// 一个字符器转为名称
        /// </summary>
        /// <param name="word">单词</param>
        /// <param name="toFirstUpper">是否首字母大写</param>
        /// <returns>字符器</returns>
        public static string ToName(this string word, bool toFirstUpper = true)
        {
            return StringHelper.ToName(word, toFirstUpper);
        }

        /// <summary>
        /// 到一个单词的复数形式
        /// </summary>
        /// <param name="word">单词</param>
        /// <returns>复数形式</returns>
        public static string ToPluralism(this string word)
        {
            return StringHelper.ToPluralism(word);
        }

        /// <summary>
        ///   数据是不是相等
        /// </summary>
        /// <param name="a"> </param>
        /// <param name="b"> </param>
        /// <returns> </returns>
        public static bool IsEquals(this string a, string b)
        {
            bool aNull = string.IsNullOrWhiteSpace(a);
            bool bNull = string.IsNullOrWhiteSpace(b);
            return aNull && bNull || (aNull == bNull && a.Equals(b, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// 到用空文本替换的文本
        /// </summary>
        /// <param name="str"></param>
        /// <param name="nullString"></param>
        /// <returns></returns>
        public static string ToNullString(this string str, string nullString = null)
        {
            return String.IsNullOrWhiteSpace(str) ? nullString : str;
        }

        /// <summary>
        /// 到百分比显示
        /// </summary>
        /// <param name="d"></param>
        /// <param name="nullString">为0时表示的文本</param>
        /// <returns></returns>
        public static string ToPercent(this decimal d, string nullString)
        {
            return d == 0 ? (nullString ?? "0") : String.Format("{0:F2}%", d * 100.0M);
        }

        /// <summary>
        /// 到百分比显示
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string ToPercent(this decimal d)
        {
            return String.Format("{0:F2}%", d * 100.0M);
        }

        /// <summary>
        /// 到没有多余小数位的文本
        /// </summary>
        /// <param name="d"></param>
        /// <param name="nullString">为0时表示的文本</param>
        /// <param name="dia">最长小数位(默认为10)</param>
        /// <returns></returns>
        public static string ToNullString(this decimal d, int dia, string nullString = null)
        {
            if (d == 0M)
                return nullString ?? "0";
            if (dia <= 0)
                dia = 10;
            return d.ToString(String.Format("F{0}", dia)).TrimEnd('0').TrimEnd('.');
        }

        /// <summary>
        /// 到没有多余小数位的文本
        /// </summary>
        /// <param name="d"></param>
        /// <param name="nullString">为0时表示的文本</param>
        /// <param name="dia">最长小数位(默认为10)</param>
        /// <returns></returns>
        public static string ToNullString(this decimal? d, int dia, string nullString = null)
        {
            if (d == null
                || d.Value == 0M)
                return nullString;
            if (dia <= 0)
                dia = 10;
            return d.Value.ToString(String.Format("F{0}", dia)).TrimEnd('0').TrimEnd('.');
        }

        /// <summary>
        /// 到可为空的时间格式化
        /// </summary>
        /// <param name="v"></param>
        /// <param name="fmt">格式化</param>
        /// <param name="nullString">为0时表示的文本</param>
        /// <returns></returns>
        public static string ToNullString(this DateTime v, string fmt, string nullString)
        {
            return Equals(v, DateTime.MinValue) ? nullString : v.ToString(fmt);
        }

        /// <summary>
        /// 到可为空的时间格式化
        /// </summary>
        /// <param name="v"></param>
        /// <param name="fmt">格式化</param>
        /// <param name="nullString">为0时表示的文本</param>
        /// <returns></returns>
        public static string ToNullString(this DateTime? v, string fmt, string nullString)
        {
            return v == null || Equals(v.Value, DateTime.MinValue) ? nullString : v.Value.ToString(fmt);
        }
        /// <summary>
        /// 到缺省值用空文本表示的值
        /// </summary>
        /// <param name="v"></param>
        /// <param name="nullString">为0时表示的文本</param>
        /// <returns></returns>
        public static string ToNullString<T>(this T v, string nullString = null) where T : struct
        {
            return Equals(v, default(T)) ? (nullString ?? v.ToString()) : v.ToString();
        }

        /// <summary>
        /// 到没有多余小数位的文本
        /// </summary>
        /// <param name="v"></param>
        /// <param name="nullString">为0时表示的文本</param>
        /// <returns></returns>
        public static string ToNullString<T>(this T? v, string nullString = null) where T : struct
        {
            return v == null || Equals(v.Value, default(T)) ? (nullString ?? v.ToString()) : v.Value.ToString();
        }
        /// <summary>
        /// 到固定长度显示
        /// </summary>
        /// <param name="d">小数</param>
        /// <param name="len">总长度</param>
        /// <param name="dit">小数位</param>
        /// <returns></returns>
        public static string ToFixLenString(this decimal d, int len, int dit)
        {
            string s = d.ToString("F" + dit);
            StringBuilder sb = new StringBuilder();
            int l = len - s.Length;
            if (l > 0)
                sb.Append(' ', l);
            sb.Append(s);
            return sb.ToString();
        }
        /// <summary>
        /// 到固定长度显示
        /// </summary>
        /// <param name="d">小数</param>
        /// <param name="len">总长度</param>
        /// <param name="dit">小数位</param>
        /// <returns></returns>
        public static string ToFixLenString(this float d, int len, int dit)
        {
            string s = d.ToString("F" + dit);
            StringBuilder sb = new StringBuilder();
            int l = len - s.Length;
            if (l > 0)
                sb.Append(' ', l);
            sb.Append(s);
            return sb.ToString();
        }
        /// <summary>
        /// 到固定长度显示
        /// </summary>
        /// <param name="d">小数</param>
        /// <param name="len">总长度</param>
        /// <param name="dit">小数位</param>
        /// <returns></returns>
        public static string ToFixLenString(this double d, int len, int dit)
        {
            string s = d.ToString("F" + dit);
            StringBuilder sb = new StringBuilder();
            int l = len - s.Length;
            if (l > 0)
                sb.Append(' ', l);
            sb.Append(s);
            return sb.ToString();
        }
        /// <summary>
        /// 到固定长度显示
        /// </summary>
        /// <param name="d">对象</param>
        /// <param name="len">总长度</param>
        /// <returns></returns>
        public static string ToFixLenString(this int d, int len)
        {
            string s = d.ToString(CultureInfo.InvariantCulture);
            StringBuilder sb = new StringBuilder();
            int l = len - s.Length;
            if (l > 0)
                sb.Append(' ', l);
            sb.Append(s);
            return sb.ToString();
        }
        /// <summary>
        /// 到固定长度显示
        /// </summary>
        /// <param name="d">对象</param>
        /// <param name="len">总长度</param>
        /// <returns></returns>
        public static string ToFixLenString<T>(this T d, int len)
        {
            string s = d.ToString();
            StringBuilder sb = new StringBuilder();
            int l = len - s.Length;
            if (l > 1)
                sb.Append(' ', l / 2);
            sb.Append(s);
            if (l > 0)
                sb.Append(' ', l - l / 2);
            return sb.ToString();
        }
    }
}
