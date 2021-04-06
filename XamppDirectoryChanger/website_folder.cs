using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamppDirectoryChanger
{
    [Serializable]
    public class website_folder
    {
        public string name;
        public string url;
        public website_folder(string name, string url)
        {
            this.name = name;
            this.url = url;
        }
     

    }
}
