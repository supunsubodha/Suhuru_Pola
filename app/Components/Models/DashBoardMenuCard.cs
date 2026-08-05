public class DashBoardMenuCard
{
	public string Title { get; set; }
	public string Role { get; set; } //farmers,buyers,all
	public string URL { get; set; }
	public string Icon { get; set; }

	public DashBoardMenuCard(string title, string role, string url, string icon)
	{
		Title = title;
		Role = role;
		URL = url;
		Icon = icon;
	}
}
