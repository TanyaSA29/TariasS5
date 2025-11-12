using TariasS5.Models;

namespace TariasS5.Views;

public partial class vPersona : ContentPage
{
    public vPersona()
    {
        InitializeComponent();
        txtNombre.TextChanged += (s, e) => lblMensaje.Text = "";
    }

    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
        lblMensaje.Text = "";
        App.personRepository.AddNewPerson(txtNombre.Text);
        lblMensaje.Text = App.personRepository.mensaje;

        ListarPersonas.ItemsSource = App.personRepository.GetAllPeople();


    }
    private void btnListar_Clicked(object sender, EventArgs e)
    {
        lblMensaje.Text = "";
        List<Persona> person = App.personRepository.GetAllPeople();
        ListarPersonas.ItemsSource = person;
        lblMensaje.Text = App.personRepository.mensaje;

    }
    
}