namespace NewLife.Cube;
/// <summary>区域上下文</summary>
public class CmsAreaContext
{
  #region 当前上下文
        private static readonly AsyncLocal<CmsAreaContext> _current = new AsyncLocal<CmsAreaContext>();

        /// <summary>当前区域上下文</summary>
        public static CmsAreaContext Current
        {
            get => _current.Value;
            set => _current.Value = value;
        }
        #endregion

        #region 属性
        /// <summary>区域ID</summary>
        public int AreaId { get; set; }

        /// <summary>当前区域ID</summary>
        public static int CurrentId => Current?.AreaId ?? 0;
        #endregion
    
}