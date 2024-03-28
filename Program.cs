
using Asistentie;

//Cantidad de minutos mínimos que se debe haber conectado un alumno para considerarlo presente.
const int minDurationParaAsistencia = 60;

var asistencias = new List<Asistentie.Asistencia>();
var participantes = new List<Participante>();
var erroresLecturaParticipants = new List<string>();

//Leo el archivo de Alumnos
var alumnos = File.ReadAllLines("Alumnos.csv");
for (int i = 1; i < alumnos.Count(); i++)
{
    string nombre = alumnos[i].Replace(","," ");
    nombre = StringService.Normalizar(nombre);
    var asistencia = new Asistencia(new Alumno(nombre));
    asistencias.Add(asistencia);
}

var parti = File.ReadAllLines("participants.csv");
for (int i = 1; i < parti.Count(); i++)
{
    try
    {

        var nombreParticipante = parti[i].Split(",")[0];
        nombreParticipante = StringService.Normalizar(nombreParticipante);
        var durationParticipante = int.Parse(parti[i].Split(",")[2]);
        var participante = new Participante(nombreParticipante,durationParticipante);
        participantes.Add(participante);
    }
    catch
    {
        erroresLecturaParticipants.Add(parti[i]);
    }
}

Console.WriteLine($"{participantes.Count()} participantes registrados (Cant de registros en el reporte de Zoom)");

//Elimino los participantes que tienen un duración menor a minDurationParaAsistencia
var participantesAeliminar = new List<Participante>();
foreach(Participante p in participantes)
{
    if(p.Duration < minDurationParaAsistencia)
    {
        participantesAeliminar.Add(p);
    }
}

foreach(Participante p in participantesAeliminar)
{
    participantes.Remove(p);
}

Console.WriteLine($"{participantes.Count()} participantes presentes (registros que cumplen con Duration > {minDurationParaAsistencia})");

Console.WriteLine($"{erroresLecturaParticipants.Count()} errores de lectura");
foreach(string s in erroresLecturaParticipants)
{
    Console.WriteLine(s);
}

var participantesMatcheados = new List<Participante>();
var asistenciasMatcheadas = new List<Asistencia>();
//Matcheo
foreach(Asistencia a in asistencias)
{
    foreach(Participante p in participantes)
    {
        string nombreAlumno = a.Alumno.Nombre.ToLower().Trim();
        string nombreParticipante = p.Nombre.ToLower().Trim();

        bool esMatch = false;
        if (nombreAlumno == nombreParticipante)
        {
            esMatch = true;
        }

        if (nombreAlumno.IndexOf(nombreParticipante) != -1)
        {
            esMatch = true;
        }

        if (nombreParticipante.IndexOf(nombreAlumno) != -1)
        {
            esMatch = true;
        }

        //Valido si separando el nombre del participante por un espacio, los dos primeros
        // string están contenido en el nombre del alumno, por separado.
        var nombresPar = nombreParticipante.Split(" ");
        if (nombresPar.Length > 1)
        {

            if (nombreAlumno.IndexOf(nombresPar[0]) != -1 && nombreAlumno.IndexOf(nombresPar[1]) != -1)
            {
                esMatch = true;
            }
        }

        if (esMatch)
        {
            a.Estado = "P";
            participantesMatcheados.Add(p);
            asistenciasMatcheadas.Add(a);
        }
    }
}

Console.WriteLine($"{participantesMatcheados.Count()} participantes matcheados");

/*
foreach (Asistencia asistencia in asistencias)
{
    Console.WriteLine($"{asistencia.Alumno.Nombre} {asistencia.Estado}");
}
*/

//Elimino los participantes matcheados
foreach (Participante p in participantesMatcheados)
{
    participantes.Remove(p);
}

Console.WriteLine("");

//Muestro los participantes no matcheados
Console.WriteLine($"{participantes.Count()} participantes NO matcheados (registros que existen en el reporte de Zoom pero no se pudieron matchear con un registro de Alumnos)");
Console.WriteLine("");
Console.WriteLine("Nombre (tiempo conectado)");

foreach (Participante participante in participantes)
{
    Console.WriteLine($"{participante.Nombre} ({participante.Duration})");
}

//Escribo el csv de asistencias
StreamWriter outputFile = new StreamWriter("Asistencias.csv",false);
foreach(Asistencia a in asistencias)
{
    string linea = $"{a.Alumno.Nombre},{a.Estado}";
    outputFile.WriteLine(linea);
    
}
outputFile.Close();