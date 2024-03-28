namespace Asistentie;

public static class StringService
{
    public static string Normalizar(String texto)
    {
        texto = texto.Replace("á","a");
        texto = texto.Replace("é","e");
        texto = texto.Replace("í","i");
        texto = texto.Replace("ó","o");
        texto = texto.Replace("ú","u");


        foreach (char s in texto)
        {
            if (!"abcdefghijklmnñopqrstuvwxyz123456789".Contains(s.ToString().ToLower()))
            {
                texto = texto.Replace(s.ToString(), " ");
            }
        }


        return texto;

    }
}