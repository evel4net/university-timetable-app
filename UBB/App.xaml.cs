namespace UBB
{
    public partial class App : Application
    {
        public static string AppServerUrl = "..."; // url to the server with the timetable data html file

        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}
