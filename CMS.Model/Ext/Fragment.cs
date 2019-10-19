using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Model
{
   public partial class Fragment
    {
       public string ChannelName { get; set; }
       public Fragment(int FragmentId, string Content)
       {
           this.FragmentId = FragmentId;
           this.Content = Content;
       }
    }
}
