namespace kr.bbon.Xamarin.Forms.Converters
{
    public class ListViewTappedItem
    {
        public object Group { get; set; }

        public object Item { get; set; }

        public int ItemIndex { get; set; }

        public static ListViewTappedItem Empty
        {
            get => new ListViewTappedItem
            {
                Group = null,
                Item = null,
                ItemIndex = -1,
            };
        }
    }
}
