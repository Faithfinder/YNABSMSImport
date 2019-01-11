using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using YNABSMSImport.ImportSettings;

namespace YNABSMSImport
{
    [Activity(Label = "@string/manage_settings_title", Theme = "@style/AppTheme")]
    public class ManageSettingsActivity : AppCompatActivity
    {
        private List<UserSetting> settings;
        private UserSettingsListAdapter adapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.manage_settings);
            SetUpListViewAsync();
            SetUpFabAddSetting();
        }

        private async void SetUpListViewAsync()
        {
            var userSettings = await new SettingsManager().GetAllAsync();

            settings = new List<UserSetting>();
            var settingsListView = FindViewById<ListView>(Resource.Id.lvSettingsList);
            adapter = new UserSettingsListAdapter(this, settings);
            settingsListView.Adapter = adapter;
            settingsListView.ItemClick += SettingsList_ItemClick;
            RegisterForContextMenu(settingsListView);
            settingsListView.ContextMenuCreated += SetUpContextMenu;

            settings.AddRange(userSettings);

            adapter.NotifyDataSetChanged();
        }

        private void SetUpFabAddSetting()
        {
            var fabAddSetting = FindViewById<FloatingActionButton>(Resource.Id.fabAddSetting);
            fabAddSetting.Click += async (sender, e) => { //TODO creation
                var setting = UserSetting.TemporaryOtkritie();
                await new SettingsManager().SaveSettingAsync(setting);
                settings.Add(setting);
                adapter.NotifyDataSetChanged();
            };
        }

        private void SetUpContextMenu(object sender, View.CreateContextMenuEventArgs e)
        {
            var menuInfo = e.MenuInfo as AdapterView.AdapterContextMenuInfo;
            var setting = settings[menuInfo.Position];
            e.Menu.SetHeaderTitle(setting.Name);

            foreach (var pair in SettingsContextMenu())
                e.Menu.Add(0, pair.Key, 0, pair.Value);
        }

        private void SettingsList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var setting = settings[e.Position];
            EditSetting(setting);
        }

        private static void EditSetting(UserSetting setting)
        {
            Toast.MakeText(Application.Context, "Edit " + setting.Name, ToastLength.Long).Show(); //TODO editing
        }

        public override bool OnContextItemSelected(IMenuItem item)
        {
            var menuInfo = item.MenuInfo as AdapterView.AdapterContextMenuInfo;
            var setting = settings[menuInfo.Position];
            switch (item.ItemId)
            {
                case 0:
                    EditSetting(setting);
                    break;
                case 1:
                    DeleteSetting(setting, menuInfo.Position);
                    break;
                default:
                    break;
            }
            return base.OnContextItemSelected(item);
        }

        private void DeleteSetting(UserSetting setting, int position)
        {
            settings.RemoveAt(position);
            new SettingsManager().DeleteSetting(setting);
            adapter.NotifyDataSetChanged();
        }

        private Dictionary<int, int> SettingsContextMenu()
        {
            return new Dictionary<int, int>
            {
                {0, Resource.String.settings_context_menu_edit },
                {1, Resource.String.settings_context_menu_delete }
            };
        }
    }
}