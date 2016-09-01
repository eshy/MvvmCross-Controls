using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Android.Views;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Binding.ExtensionMethods;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platform;

namespace MvvmCross.Android.Controls.SectionedRecyclerView
{
    public class MvxSectionedRecyclerAdapter : MvxRecyclerAdapter
    {
        private const string LogTag = nameof(MvxSectionedRecyclerAdapter);
        private readonly bool _showHeaderForEmptySections;
        private readonly int _headerLayoutId;
        private readonly Dictionary<int,int> _headerLocationMap = new Dictionary<int, int>();
        private const int HeaderViewType = -1;
        
        public MvxSectionedRecyclerAdapter(bool showHeaderForEmptySections, int headerLayoutId)
        {
            _showHeaderForEmptySections = showHeaderForEmptySections;
            _headerLayoutId = headerLayoutId;
        }

        //override ItemCount Get to return count of items in sections + section headers/footers
        public override int ItemCount
        {
            get
            {
                var count = 0;
                for (var s=0; s < GetSectionCount(); s++)
                {
                    var itemCount = GetSectionItemCount(s);
                    if (_showHeaderForEmptySections || itemCount > 0)
                    {
                        //TODO: Save the index of this section
                        _headerLocationMap[count] = s;
                        count += itemCount;
                    }
                }

                return count;
            }
        }

        private int GetSectionCount()
        {
            try
            {
                return ItemsSource?.Count() ?? 0;
            }
            catch (Exception ex)
            {
                Mvx.TaggedError(LogTag, "Exception in {0} ({1})", nameof(GetSectionCount), ex);
            }
            return 0;
        }

        private int GetSectionItemCount(int sectionIndex)
        {
            try
            {
                var sectionItems = ItemsSource?.ElementAt(sectionIndex) as IEnumerable;
                return sectionItems?.Count() ?? 0;
            }
            catch (Exception ex)
            {
                Mvx.TaggedError(LogTag, "Exception in {0} ({1})", nameof(GetSectionItemCount), ex);
            }
            return 0;
        }

        private bool IsHeader(int position)
        {
            return _headerLocationMap.ContainsKey(position);
        }

        //override GetItem to return the correct item based on the section number
        public override object GetItem(int position)
        {
            var sectionHeader = _headerLocationMap.LastOrDefault(i => position >= i.Key);
            //Mvx.TaggedTrace(LogTag, "position {0}, key {1}, value {2}", position, sectionHeader.Key, sectionHeader.Value);
            var section = ItemsSource.ElementAt(sectionHeader.Value) as IEnumerable;
            var itemIndex = position - sectionHeader.Key - 1;
            if (position == sectionHeader.Key)
            {
                //This is a header, no item for it. Return the first item for now
                itemIndex = 0;
            }

            //Mvx.TaggedTrace(LogTag, "position {0}, key {1}, value {2}, itemIndex {3}, items in section {4}", position, sectionHeader.Key, sectionHeader.Value, itemIndex, section.Count());
            return itemIndex >=0 ? section.ElementAt(itemIndex) : null;
        }

        

        //override GetItemViewType to return Header/Footer if necessary (used in Inflate to figure out the resource id)
        public override int GetItemViewType(int position)
        {
            if (IsHeader(position))
            {
                return HeaderViewType;
            }
            return base.GetItemViewType(position);
        }

        //override InflateViewForHolder, if it's a header/footer, use the appropariate layout id. If not call base (which will use single template or custom template selector)
        protected override View InflateViewForHolder(ViewGroup parent, int viewType, IMvxAndroidBindingContext bindingContext)
        {
            if (viewType== HeaderViewType)
            {
                return bindingContext.BindingInflate(_headerLayoutId, parent, false);
            }
            return base.InflateViewForHolder(parent, viewType, bindingContext);
        }
    }
}