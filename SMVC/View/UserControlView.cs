using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMVC
{
    /// <summary>
    /// 用户控件视图的基类
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class UserControlView<TModel> : BaseUserControl
    {
        public TModel Model { get; set; }

        public override void SetModel(object model)
        {
            try
            {
                if (model != null)
                    Model = (TModel)model;
            }
            catch
            {
                throw new InvalidCastException("指定的类型与model不符");
            }
        }
    }
}
