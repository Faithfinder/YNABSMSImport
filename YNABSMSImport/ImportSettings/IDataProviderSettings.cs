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
    internal interface IDataProviderSettings
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Sender { get; set; }
        List<SMSTemplate> Templates { get; set; }

        SMSTemplate ChooseTemplate(string messageText);
    }
}