using Android.App;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using YNABSMSImport.ImportSettings;

namespace YNABSMSImport
{
    internal class UserSettingsListAdapter : BaseAdapter<UserSetting>
    {
        private List<UserSetting> items;
        private Activity context;

        public UserSettingsListAdapter(Activity context, List<UserSetting> items) : base()
        {
            this.items = items;
            this.context = context;
        }

        public override UserSetting this[int position] => items[position];

        public override int Count => items.Count;

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            if (view == null)
                view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = items[position].Name;
            return view;
        }
    }

}