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
using YNABSMSImport.ImportSettings;

namespace YNABSMSImport
{
    [Activity(Label = "@string/manage_settings_title", Theme = "@style/AppTheme")]
    public class ManageSettingsActivity : Activity
    {
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.manage_settings);
            var settingsList = FindViewById<ListView>(Resource.Id.lvSettingsList);
            var userSettings = await SettingsManager.GetAllAsync();
            settingsList.Adapter = new ArrayAdapter<UserSetting>(this, Android.Resource.Layout.SimpleListItem1, userSettings.ToArray());
        }
    }
}