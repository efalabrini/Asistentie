namespace Asistentie;
public class Participante
{
    public string Nombre { get; set; }
    public int Duration { get; set; }

    public Participante(string nombre, int duration)
    {
        Nombre = nombre;
        Duration = duration;
    }
}