using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using MvvmCross.Binding.Droid.ResourceHelpers;
using MvvmCross.Binding.Droid.Views;
using MvvmCross.Droid.Support.V7.RecyclerView;

namespace MvvmCross.Android.Controls.SectionedRecyclerView
{
    public class MvxSectionedRecyclerView : MvxRecyclerView
    {
        public MvxSectionedRecyclerView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public MvxSectionedRecyclerView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            //TODO: create the adapter with parameters from attributes
            //var itemTemplateId = MvxAttributeHelpers.ReadListItemTemplateId(context, attrs);
            var headerTemplateId = MvxAttributeHelpers.ReadAttributeValue(context, attrs, MvxAndroidBindingResource.Instance
                                                                            .ExpandableListViewStylableGroupId,
                                                   MvxAndroidBindingResource.Instance
                                                                            .GroupItemTemplateId);
            IMvxRecyclerAdapter headerAdapter = new MvxSectionedRecyclerAdapter(false, headerTemplateId);
            Adapter = headerAdapter;
        }

        public MvxSectionedRecyclerView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
        }

        public MvxSectionedRecyclerView(Context context, IAttributeSet attrs, int defStyle, IMvxRecyclerAdapter adapter) : base(context, attrs, defStyle, adapter)
        {
        }
    }
}