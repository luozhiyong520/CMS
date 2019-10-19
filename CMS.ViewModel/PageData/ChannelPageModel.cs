using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Model;

namespace CMS.ViewModel
{
    public class ChannelPageModel
    {

        public Channel Channel { get; set; }
        public string strhtml { get; set; }
        public string strAll { get; set; }
        public List<Channel> List;

        public ChannelPageModel()
		{
            this.Channel = new Channel();
		}
    }
}
