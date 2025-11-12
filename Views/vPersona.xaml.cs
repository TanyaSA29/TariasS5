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
    private void btnActualizar_Clicked(object sender, EventArgs e)
{
    lblMensaje.Text = "";
    var text = txtNombre.Text?.Trim();

    if (string.IsNullOrEmpty(text))
    {
        lblMensaje.Text = "Escribe 'nombreViejo;nombreNuevo' para actualizar.";
        return;
    }

  
    string[] parts = text.Split(';', 2);

    if (parts.Length != 2 || string.IsNullOrWhiteSpace(parts[0]) || string.IsNullOrWhiteSpace(parts[1]))
    {
        lblMensaje.Text = "Formato inválido. Usa: nombreViejo;nombreNuevo (por ejemplo: Juan;Juanito)";
        return;
    }

    string viejo = parts[0].Trim();
    string nuevo = parts[1].Trim();

    App.personRepository.UpdatePersonByName(viejo, nuevo);
    lblMensaje.Text = App.personRepository.mensaje;
    ListarPersonas.ItemsSource = App.personRepository.GetAllPeople();
}

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        lblMensaje.Text = "";
        var text = txtNombre.Text?.Trim();

        if (string.IsNullOrEmpty(text))
        {
            lblMensaje.Text = "Escribe el nombre a eliminar (o 'eliminar:nombre').";
            return;
        }
            // Cambiar nombre con ; para mejor uso xd
        if (text.StartsWith("eliminar:", StringComparison.OrdinalIgnoreCase) ||
            text.StartsWith("borrar:", StringComparison.OrdinalIgnoreCase))
        {
            int idx = text.IndexOf(':');
            if (idx >= 0 && idx + 1 < text.Length)
                text = text.Substring(idx + 1).Trim();
        }

        App.personRepository.DeletePersonByName(text);
        lblMensaje.Text = App.personRepository.mensaje;
        ListarPersonas.ItemsSource = App.personRepository.GetAllPeople();
    }
}