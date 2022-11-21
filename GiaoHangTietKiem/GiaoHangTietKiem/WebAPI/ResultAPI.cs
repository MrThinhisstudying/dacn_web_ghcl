using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiaoHangTietKiem.WebAPI
{
    public class ResultAPI
    {
        public bool result { get; set; }
        public string message { get; set; }

        public Object obj {get;set;}

        public ResultAPI() { }

        public ResultAPI(bool result, string message)
        {
            this.message = message;this.result = result;
        }
        public ResultAPI(bool result, string message,Object obj)
        {
            this.message = message; this.result = result;
            this.obj = obj;
        }
    }
}