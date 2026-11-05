public class DashBoardMenuCard
{
	public string Title { get; set; }
	public string URL { get; set; }
	public string Icon { get; set; }
	public string DisplayValue { get; set; }

	public DashBoardMenuCard(string title, string displayValue, string url, string icon)
	{
		Title = title;
		DisplayValue = displayValue;
		URL = url;
		Icon = icon;
	}
}
