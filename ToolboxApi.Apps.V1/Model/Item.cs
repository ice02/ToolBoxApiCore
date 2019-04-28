using System;
using System.Collections.Generic;
using System.Text;

namespace ToolboxApi.Apps.V1.Model
{
    public class Item
    {
        public int Att1 { get; set; }
        public string Att2 { get; set; }

        private string v1;
        private int v2;

        public Item(string v1, int v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }
    }
}
