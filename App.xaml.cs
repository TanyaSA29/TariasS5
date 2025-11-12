
namespace TariasS5
{
    public partial class App : Application
    {
        public static Utiles.PersonRepo personRepository { get; set; }

        public App(Utiles.PersonRepo _personRepo)
        {

            InitializeComponent();
            personRepository= _personRepo;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new Views.vPersona());// ejecutar la nueva vista
        }
    }
}