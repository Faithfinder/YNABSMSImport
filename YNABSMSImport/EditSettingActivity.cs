using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using System;
using System.Threading.Tasks;
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
            button.Click += SaveSettingClickAsync;
        }

        private async void SaveSettingClickAsync(object sender, EventArgs e)
        {
            var editText = FindViewById<EditText>(Resource.Id.SomeText);
            setting.Name = editText.Text;
            var saveSettingTask = new SettingsManager().SaveSettingAsync(setting);
            await saveSettingTask.ContinueWith((arg) =>
            {
                NotificationHelper.ShowToast(this, "Saved", ToastLength.Short);
            });
        }
    }
}