# Programa para asignar las asistencias en base a un reporte de uso de Zoom.

# Funcionamiento del programa
A partir de un archivo "Alumnos.csv" y un archivo "participants.csv", genera un archivo de salida "Asistencias.csv"

- Alumnos.csv es un archivo con las columnas "Nombre" y "Apellido"

- participants.csv es un archivo con las columas "Name","User Email","Total Duration", "Guest"
Es el archivo que emite el reporte de Zoom.

- Asistencias.csv es el archivo de salida, que contiene en la primer columna, el nombre completo del alumno y en la segunda columna una "P" indicando que estuvo presente.

# Forma de uso
Dada una planilla de alumos para completar una columna con una P en caso de que el alumno esté presente, se debe generar a partir de esa planilla, el archivo "Alumnos.csv".

Luego de una meeting en Zoom, se debe descargar el reporte de uso, que genera el archivo participants.csv.

Los archivos "Alumnos.csv" y "participants.csv" debe estar en la misma carpeta que el ejecutable.

En esa misma carpeta, el programa escribe el archivo "Asistencias.csv".

Finalmente se debe copiar la segunda columna del archivo "Asistencias.csv" en la planilla de alumnos a completar.

## Importante
- El orden de los registros del archivo Alumnos.csv, tiene que ser el mismo orden que la planilla de alumnos a completar.
- El archivo "Alumnos.csv" y el archivo "participants.csv" deben estar codificados con el mismo charset. Recomendable trabajar siempre con UTF-8.