using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace YNABSMSImport.ImportSettings
{
    internal class IgnoreSMS : ISMSTemplate
    {
        public string RegExPattern { get; set; }
    }
}