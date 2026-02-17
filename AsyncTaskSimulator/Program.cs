namespace AsyncTaskSimulator
{
    internal class Program
    {
        /*
        Proyecto sugerido: “Task Manager / Simulador de Tareas Asíncronas”

        OBJETIVO: crear una app de consola que simule varias tareas que se ejecutan de manera concurrente y que puedas monitorizar, cancelar o esperar sus resultados.

        FUNCIONALIDADES BÁSICAS
        1. Crear varias “tareas” simuladas
            - Cada tarea puede ser “cocer agua”, “calentar leche”, “preparar café”, etc.
            - Cada tarea puede durar un tiempo distinto (simulado con Task.Delay o Thread.Sleep).

        2. Ejecutar tareas en paralelo
            - Usar Task.Run o Parallel.For/Parallel.ForEach.
            - Mostrar qué hilo ejecuta cada tarea (como en tus ejemplos anteriores).

        3. Usar async/await
            - Cada tarea puede ser un método async Task<T> que devuelve algún resultado (por ejemplo, “agua caliente” o “café listo”).
            - Combinar varias tareas con Task.WhenAll o Task.WhenAny.

        4. Cancelar tareas
            - Implementar cancelación con CancellationTokenSource (como en tu ejemplo de CancellationTask).
            - Mostrar qué tareas fueron canceladas.

        5. Resultados finales
            - Una vez terminadas todas las tareas, mostrar el resultado de cada una.
            - Si alguna fue cancelada, indicarlo.
         */

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
