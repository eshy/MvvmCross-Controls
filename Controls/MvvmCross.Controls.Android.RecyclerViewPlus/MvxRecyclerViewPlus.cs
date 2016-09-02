using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using MvvmCross.Droid.Support.V7.RecyclerView;

namespace MvvmCross.Controls.Android.RecyclerViewPlus
{
    public class MvxRecyclerViewPlus : MvxRecyclerView
    {

        public MvxRecyclerViewPlus(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public MvxRecyclerViewPlus(Context context, IAttributeSet attrs) : base(context, attrs)
        {

            IMvxRecyclerAdapter headerAdapter = new MvxRecyclerAdapterPlus();

            Adapter = headerAdapter;

        }

        public MvxRecyclerViewPlus(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
        }

        public MvxRecyclerViewPlus(Context context, IAttributeSet attrs, int defStyle, IMvxRecyclerAdapter adapter) : base(context, attrs, defStyle, adapter)
        {
        }
    }
}