using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace kr.bbon.Xamarin.Forms.Controls
{
    /// <summary>
    /// 출력여부를 제어할 수 있는 <see cref="ToolbarItem"/> 확장 뷰 컨트롤입니다.
    /// </summary>
    public class VisibilityToolBarItem : ToolbarItem
    {
        public static readonly BindableProperty IsVisibleProperty = BindableProperty.Create(
            nameof(IsVisible),
            typeof(bool),
            typeof(VisibilityToolBarItem),
            defaultValue: default(bool),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: OnIsVisibleChanged);

        public VisibilityToolBarItem() : base()
        {
            this.InitVisibility();
        }

        public bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }

        //public ContentPage Parent { set; get; }

        private async void InitVisibility()
        {
            await Task.Delay(100);
            OnIsVisibleChanged(this, false, IsVisible);
        }

        private static void OnIsVisibleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var item = bindable as VisibilityToolBarItem;
            var oldBooleanValue = (bool)oldValue;
            var newBooleanValue = (bool)newValue;

            if (item.Parent != null && item.Parent is ContentPage)
            {
                var contentPage = (ContentPage)item.Parent;
                var items = contentPage.ToolbarItems;

                if (newBooleanValue && !items.Contains(item))
                {
                    items.Add(item);
                }
                else if (!newBooleanValue && items.Contains(item))
                {
                    items.Remove(item);
                }
            }
        }

    }
}
