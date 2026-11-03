public class DashBoardMenuCard
{
	public string Title { get; set; }
	public string URL { get; set; }
	public string Icon { get; set; }

	public DashBoardMenuCard(string title, string url, string icon)
	{
		Title = title;
		URL = url;
		Icon = icon;
	}
}
