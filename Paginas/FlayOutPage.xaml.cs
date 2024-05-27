namespace Lerdo_MX_PQM.Paginas;

public partial class FlayOutPage : FlyoutPage
{
	public FlayOutPage()
	{
		InitializeComponent();
        flayoutPage.collectionView.SelectionChanged += CollectionView_SelectionChanged;
    }
    public void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var item = e.CurrentSelection.FirstOrDefault() as FlyoutPageItem;
        if (item != null)
        {
            Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
            IsPresented = false;
        }
    }
}
