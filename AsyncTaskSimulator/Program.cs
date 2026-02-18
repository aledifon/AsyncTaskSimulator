using System.Threading.Tasks;

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

        static async Task Main(string[] args)
        {
            // Create tasks
            List<TaskModel<string>> taskModels = InitTasks();

            while (true)
            {
                ShowInitMessages();

                var keyInfo = Console.ReadKey(intercept: true);

                Task monitCancelTask;

                switch (keyInfo.Key)
                {
                    
                    case ConsoleKey.A:
                        // Call the Monitoring method to allow Cancel the future tasks.                        
                        monitCancelTask = Task.Run(() => CancellationManager.MonitoringLoop());

                        // Call All tasks asynchronously and wait till all of them have finished                        
                        await ExecuteTasksAsync(taskModels);

                        // Reset the TaskRunning Flag
                        CancellationManager.AreTasksRunning = false;

                        break;                    
                    case ConsoleKey.S:
                        // Call the Monitoring method to allow Cancel the future tasks.
                        monitCancelTask = Task.Run(() => CancellationManager.MonitoringLoop());

                        // Call all taks in a certain order (1st 1 and 2 and when they are finished then call the 3rd one)
                        Task ordererTasks = Task.Run(() => ExecuteTasksInOrder(taskModels));
                        ordererTasks.Wait();

                        // Reset the TaskRunning Flag
                        CancellationManager.AreTasksRunning = false;

                        break;                    
                    default:
                        Console.WriteLine("Please tap again one of the valid keys \n");
                        break;
                }                                
            }                        
        }

        #region
        static void ShowInitMessages()
        {
            Console.WriteLine("Please press one of the following options \n");

            Console.WriteLine("'A' to execute the tasks asynchronously \n");           
            Console.WriteLine("'S' to execute the tasks synchronously or concurrently \n");
        }
        #endregion

        #region Tasks Methods
        static List<TaskModel<string>> InitTasks()
        {
            List<TaskModel<string>> taskModelsList = new List<TaskModel<string>>
            {
                new TaskModel<string>("Boil Water", 1000, BoilWaterTask.BoilWaterAsync),
                new TaskModel<string>("Heat Water", 1000, HeatMilkTask.HeatMilkAsync),
                new TaskModel<string>("Heat Coffee", 1000, HeatCoffeeTask.HeatCoffeeAsync)
            };

            return taskModelsList;
        }
        static async Task ExecuteTasksAsync(List<TaskModel<string>> taskModels)
        {
            await Task.WhenAll(taskModels.Select(tm => tm.ExecuteTask()));
        }        
        static void ExecuteTasksInOrder(List<TaskModel<string>> taskModels)
        {
            var task1 = Task.Run(() => taskModels[0].ExecuteTask());                        
            var task2 = Task.Run(() => taskModels[1].ExecuteTask());

            Task.WaitAll(task1,task2);

            var task3 = Task.Run(() => taskModels[2].ExecuteTask());

            task3.Wait();
        }
        
        #endregion
    }
}
