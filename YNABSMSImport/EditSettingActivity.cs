using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using YNABSMSImport.ImportSettings;

namespace YNABSMSImport
{
    [Activity(Label = "EditSettingActivity")]
    public class EditSettingActivity : AppCompatActivity
    {
        private UserSetting setting;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.setting_form);
            setting = await GetSettingAsync();

            var editText = FindViewById<EditText>(Resource.Id.SomeText);

            editText.Text = setting.Name;
            SetupSaveSettingButton();
        }

        private async Task<UserSetting> GetSettingAsync()
        {
            var settingID = Intent.GetStringExtra("SettingID");

            if (settingID == null)
            {
                return UserSetting.TemporaryOtkritie();
            }
            else
            {
                var setting = await new SettingsManager().FindSettingByIdAsync(settingID);
                return setting ?? new UserSetting();
            }
        }

        private void SetupSaveSettingButton()
        {
            var button = FindViewById<Button>(Resource.Id.btn_SaveSetting);
            button.Click += async (sender, e) => {
                var editText = FindViewById<EditText>(Resource.Id.SomeText);
                setting.Name = editText.Text;
                await new SettingsManager().SaveSettingAsync(setting);
            };
        }
    }
}