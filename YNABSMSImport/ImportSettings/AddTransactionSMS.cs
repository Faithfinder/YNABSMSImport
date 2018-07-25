﻿using System;
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
    internal class AddTransactionSMS : ISMSTemplate
    {
        public Guid AccountID { get; set; }
        public Guid BudgetID { get; set; }
        public string RegExPattern { get; set; }
    }
}