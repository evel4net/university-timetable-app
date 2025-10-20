using System.Xml.Serialization;

namespace UBB.Repository
{
	public class Repository
	{
		public List<UBB.Domain.Block> blocksList { get; private set; }

		public Repository()
		{
			this.blocksList = new List<UBB.Domain.Block>();
		}

		public async Task CreateXMLFile()
		{
			// read html with the timetable data from server url address

			HttpClient httpClient = new HttpClient();
			HttpResponseMessage response = await httpClient.GetAsync(UBB.App.AppServerUrl + "/timetable.html");

			if (response.StatusCode == System.Net.HttpStatusCode.OK) {
				var text = await response.Content.ReadAsStringAsync();
				var dataText = text.Substring(53, text.Length - 74);

				var fileText = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<ArrayOfBlock xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n" + dataText + "\r\n</ArrayOfBlock>";

				using (var sw = new StreamWriter(System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "Timetable.xml"), false)) {
					sw.Write(fileText);
				}
			}
		}

		public async void LoadXMLData()
		{
			// check if file to read exists -- if not, create it

			if (System.IO.Path.Exists(System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "Timetable.xml")) == false) await this.CreateXMLFile();

			// read data from file

			using (FileStream fs = System.IO.File.OpenRead(System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "Timetable.xml"))) {
				XmlSerializer serializer = new XmlSerializer(this.blocksList.GetType());
				this.blocksList = (List<UBB.Domain.Block>)serializer.Deserialize(fs);
			}
		}

		public async void ReloadXMLData()
		{
			await this.CreateXMLFile();
			this.LoadXMLData();
		}

		public List<UBB.Domain.Block> GetDayBlocksList(string day, string week)
		{
			List<UBB.Domain.Block> dayBlocksList = new List<UBB.Domain.Block>();

			foreach (UBB.Domain.Block block in blocksList) {
				if (block.Day == day && (block.Week == week || block.Week == "null")) dayBlocksList.Add(block);
			}

			dayBlocksList.OrderBy(b=>b.StartHour);

			return dayBlocksList;
		}
	}
}