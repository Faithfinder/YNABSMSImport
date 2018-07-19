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

namespace YNABConnector.YNABObjectModel
{
    internal class BudgetSummary
    {
        public DateFormat date_format;
        public Guid id;
        public DateTime last_modified_on;
        public string name;
    }
}