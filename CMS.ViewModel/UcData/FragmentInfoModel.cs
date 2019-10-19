using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Model;
using System.Data;
using Common;

namespace CMS.ViewModel
{
    public class FragmentInfoModel
    {
        public List<HtmlOptionItem> Categories;
		public Fragment Fragment;

		public FragmentInfoModel() { }

		public FragmentInfoModel(DataTable dt, Fragment fragment)
		{
			if( dt == null )
				throw new ArgumentNullException("dt");
			if( fragment == null )
				throw new ArgumentNullException("fragment");
			this.Fragment = fragment;
			this.Categories = ConvertCategoryList(dt, fragment.FragmentId);
		}

		public static List<HtmlOptionItem> ConvertCategoryList(DataTable dt, int ID)
		{
			if( dt == null )
				throw new ArgumentNullException("dt");

			List<HtmlOptionItem> categories = new List<HtmlOptionItem>(dt.Rows.Count);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                categories.Add(new HtmlOptionItem {
                    Text = dt.Rows[i]["Content"].ToString(),
                    Value = dt.Rows[i]["FragmentId"].ToString(),
                    Selected = dt.Rows[i]["FragmentId"].ToString() == ID.ToString()
                });
            }
			return categories;
		}



	}
}
