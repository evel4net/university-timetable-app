using UBB.Domain;

namespace UBB
{
	public partial class MainPage : ContentPage
	{
		private UBB.Repository.Repository blocksRepository;
		private string currentDay;
		private string currentWeek;

		public MainPage()
		{
			InitializeComponent();

			this.blocksRepository = new UBB.Repository.Repository();
			this.blocksRepository.LoadXMLData();

			currentWeek = "1";
		}

		private void OnDayButtonClick(object sender, EventArgs e)
		{
			
			Button clickedDayButton = (Button)sender;
			currentDay = clickedDayButton.CommandParameter.ToString();

			this.ShowBlocksList(currentDay, currentWeek);
		}

		private void OnSwitchToggle(object sender, EventArgs e)
		{
			Switch weekSwitch = (Switch)sender;

			if (weekSwitch.IsToggled == false) currentWeek = "1";
			else currentWeek = "2";

			WeekLabel.Text = $"Week {currentWeek}";

			if (currentDay != null)	this.ShowBlocksList(currentDay, currentWeek);
		}

		private void ShowBlocksList(string day, string week)
		{
			BlocksLayout.Clear();

			List<UBB.Domain.Block> blocksList = blocksRepository.GetDayBlocksList(currentDay, currentWeek);

			foreach (Block block in blocksList) {
				Label blockLabel = new Label();
				blockLabel.FormattedText = block.GetText();
				blockLabel.BackgroundColor = block.GetColor();
				blockLabel.Padding = 10;
				blockLabel.Margin = 10;

				BlocksLayout.Children.Add(blockLabel);
			}
		}

		private void OnReloadButtonClick(object sender, EventArgs e)
		{
			blocksRepository.ReloadXMLData();

			BlocksLayout.Clear();
			currentDay = null;
		}
	}
}
