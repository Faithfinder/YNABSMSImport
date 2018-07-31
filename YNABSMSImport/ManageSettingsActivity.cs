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
using YNABSMSImport.ImportSettings;

namespace YNABSMSImport
{
    [Activity(Label = "@string/manage_settings_title", Theme = "@style/AppTheme")]
    public class ManageSettingsActivity : ListActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Create your application here
            var settingsEnum = SettingsManager.GetAllAsync();
            var taskresult = settingsEnum.Result;
            var settings = taskresult.Select(p => p.Name).ToArray();
            ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, settings);
        }
    }
}