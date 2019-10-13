namespace kr.bbon.Xamarin.Forms.Converters
{
    public class ListViewVisibilityItem
    {
        public object Item { get; set; }

        public int ItemIndex { get; set; }

        public static ListViewVisibilityItem Empty
        {
            get => new ListViewVisibilityItem
            {
                Item = null,
                ItemIndex = -1,
            };
        }
    }
}
