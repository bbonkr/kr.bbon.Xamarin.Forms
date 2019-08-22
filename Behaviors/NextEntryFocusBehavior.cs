using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace kr.bbon.Xamarin.Forms.Behaviors
{
    public class NextEntryFocusBehavior : BindableBehavior<Entry>
    {
        public static readonly BindableProperty NextEntryProperty = BindableProperty.Create(nameof(NextEntry), typeof(View), typeof(View), defaultBindingMode: BindingMode.OneTime, defaultValue: null);

        public View NextEntry
        {
            get => (View)GetValue(NextEntryProperty);
            set => SetValue(NextEntryProperty, value);
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);

            bindable.Completed += Entry_Completed;
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            if (NextEntry != null && AssociatedObject.ReturnType == ReturnType.Next)
            {
                NextEntry.Focus();
            }
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.Completed -= Entry_Completed;

            base.OnDetachingFrom(bindable);
        }
    }
}
