namespace UBB.Domain
{
	public class Block
	{
		public string Day { get; set; }
		public string Subject { get; set; }
		public string Style { get; set; }
		public string Location { get; set; }
		public string StartHour { get; set; }
		public string EndHour { get; set; }
		public string Week { get; set; }

		public FormattedString GetText()
		{
			FormattedString formattedText = new FormattedString();
			formattedText.Spans.Add(new Span { Text = Subject, FontAttributes = FontAttributes.Bold });
			formattedText.Spans.Add(new Span { Text = "\nroom " + Location + "\n" + StartHour + ":00 - " + EndHour + ":00" });

			return formattedText;
		}

		public Color GetColor()
		{
			if (this.Style == "Course") return Color.FromArgb("#ff9999");
			else if (this.Style == "Seminar") return Color.FromArgb("#ccffcc");
			else return Color.FromArgb("#ffffc1");
		}
	}
}