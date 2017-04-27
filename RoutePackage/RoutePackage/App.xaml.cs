using RoutePackage.Models.Database;
using RoutePackage.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RoutePackage
{
    public partial class App : Application
    {
        public static double ScreenHeight;
        public static double ScreenWidth;

        public DBContext Contexto { get; set; }

        public App()
        {
            Contexto = new DBContext();

            InitializeComponent();

            MainPage = new TabbedPage
            {
                Children =
                {
                    new NavigationPage(new MapPage(Contexto))
                    {
                        Title = "Buscar",
                        Icon = Device.OnPlatform("tab_feed.png",null,null)
                    },
                    new NavigationPage(new AddNewPlace(Contexto))
                    {
                        Title = "Nuevos lugares",
                        Icon = Device.OnPlatform("tab_about.png",null,null)
                    },
                    new NavigationPage(new ItemsPage())
                    {
                        Title = "Anteriores",
                        Icon = Device.OnPlatform("tab_about.png",null,null)
                    }

                }
            };
        }

        protected override void OnStart()
        {
            Contexto.Configurar<SearchPlaceDB>();
        }

    }
}
