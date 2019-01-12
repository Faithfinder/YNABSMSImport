using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
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
            fabAddSetting.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(EditSettingActivity));
                StartActivity(intent);
            };
        }

        private void SetUpContextMenu(object sender, View.CreateContextMenuEventArgs e)
        {
            var menuInfo = e.MenuInfo as AdapterView.AdapterContextMenuInfo;
            var setting = settings[menuInfo.Position];
            e.Menu.SetHeaderTitle(setting.Name);

            foreach (var pair in SettingsContextMenuWithLabels())
                e.Menu.Add(0, (int)pair.Key, 0, pair.Value);
        }

        private void SettingsList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var setting = settings[e.Position];
            EditSetting(setting);
        }

        private void EditSetting(UserSetting setting)
        {
            var intent = new Intent(this, typeof(EditSettingActivity));
            intent.PutExtra("SettingID", setting.Id.ToString());
            StartActivity(intent);
        }

        public override bool OnContextItemSelected(IMenuItem item)
        {
            var menuInfo = item.MenuInfo as AdapterView.AdapterContextMenuInfo;
            var setting = settings[menuInfo.Position];
            switch ((SettingsContextMenu)item.ItemId)
            {
                case SettingsContextMenu.Edit:
                    EditSetting(setting);
                    break;
                case SettingsContextMenu.Delete:
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

        private Dictionary<SettingsContextMenu, int> SettingsContextMenuWithLabels()
        {
            return new Dictionary<SettingsContextMenu, int>
            {
                {SettingsContextMenu.Edit, Resource.String.settings_context_menu_edit },
                {SettingsContextMenu.Delete, Resource.String.settings_context_menu_delete }
            };
        }
    }
}