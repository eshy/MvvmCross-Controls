using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using MvvmCross.Binding.Droid.ResourceHelpers;
using MvvmCross.Binding.Droid.Views;
using MvvmCross.Droid.Support.V7.RecyclerView;

namespace MvvmCross.Controls.Android.SectionedRecyclerView
{
    public class MvxSectionedRecyclerView : MvxRecyclerView
    {
        public MvxSectionedRecyclerView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public MvxSectionedRecyclerView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            //create the adapter with parameters from attributes
            var headerTemplateId = MvxAttributeHelpers.ReadAttributeValue(context, attrs,
                                            MvxAndroidBindingResource.Instance.ExpandableListViewStylableGroupId,
                                            MvxAndroidBindingResource.Instance.GroupItemTemplateId);
            IMvxRecyclerAdapter adapter = new MvxSectionedRecyclerAdapter(false, headerTemplateId);
            Adapter = adapter;
        }

        public MvxSectionedRecyclerView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
        }

        public MvxSectionedRecyclerView(Context context, IAttributeSet attrs, int defStyle, IMvxRecyclerAdapter adapter) : base(context, attrs, defStyle, adapter)
        {
        }
    }
}