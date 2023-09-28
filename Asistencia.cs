namespace Asistentie;
public class Asistencia
{
    public Alumno Alumno { get; set; }

    //P = Presente, "" = Ausente
    public string Estado { get; set; } 

    public Asistencia(Alumno alumno)
    {
        Alumno = alumno;
        Estado = "";
    }
}