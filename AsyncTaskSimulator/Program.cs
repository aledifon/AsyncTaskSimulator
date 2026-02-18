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

                var key = Console.ReadKey(intercept: true).Key;

                Task monitCancelTask;

                // Reset the Cancellation Token Source to allow use it again
                CancellationManager.ResetCancellationToken();

                switch (key)
                {                    
                    // Asynchronous Tasks Execution
                    case ConsoleKey.A:
                        // Call the Async method Monitoring to allow Cancel the future tasks.                        
                        monitCancelTask = CancellationManager.MonitoringLoopAsync();

                        // Call All tasks asynchronously and wait till all of them have finished                        
                        await ExecuteTasksAsync(taskModels);

                        // Reset the TaskRunning Flag
                        CancellationManager.CancelMonitoringEnabled = false;

                        // Assure Cancel Monitoring has finished before staring the next iteration.
                        await monitCancelTask;

                        break;
                    // Synchronous Tasks Execution
                    case ConsoleKey.S:
                        // Call the Async method Monitoring to allow Cancel the future tasks.
                        monitCancelTask = CancellationManager.MonitoringLoopAsync();

                        // Call all taks in a certain order (1st 1 and 2 and when they are finished then call the 3rd one)
                        await ExecuteTasksInOrder(taskModels);                        

                        // Reset the TaskRunning Flag
                        CancellationManager.CancelMonitoringEnabled = false;

                        // Assure Cancel Monitoring has finished before staring the next iteration.
                        await monitCancelTask;

                        break;
                    // Quit the Console Application
                    case ConsoleKey.Q:
                        return;                    
                    default:
                        Console.WriteLine("Please tap again one of the valid keys \n");
                        break;
                }                
            }                        
        }

        #region Messages
        static void ShowInitMessages()
        {
            Console.WriteLine("Please press one of the following options \n");

            Console.WriteLine("'A' to execute the tasks asynchronously.\n");           
            Console.WriteLine("'S' to execute the tasks synchronously or concurrently.\n");
            Console.WriteLine("'Q' to quit the application.\n");
            Console.WriteLine("Optionally presss 'X' during the Tasks execution to cancel it.\n");
        }
        #endregion

        #region Tasks Methods
        static List<TaskModel<string>> InitTasks()
        {
            List<TaskModel<string>> taskModelsList = new List<TaskModel<string>>
            {
                new TaskModel<string>("Boil Water", 3000, BoilWaterTask.BoilWaterAsync),
                new TaskModel<string>("Heat Water", 3000, HeatMilkTask.HeatMilkAsync),
                new TaskModel<string>("Heat Coffee", 5000, HeatCoffeeTask.HeatCoffeeAsync)
            };

            return taskModelsList;
        }
        static async Task ExecuteTasksAsync(List<TaskModel<string>> taskModels)
        {
            await Task.WhenAll(taskModels.Select(tm => tm.ExecuteTask()));

            // Show Results
            foreach (var taskModel in taskModels)
            {
                Console.WriteLine("\n\n\n");

                if (taskModel.Result != null)
                    Console.WriteLine($"The Result of the Task {taskModel.Name} is {taskModel.Result}.\n");
                else if (CancellationManager.CancellToken.IsCancellationRequested)
                    Console.WriteLine($"The Task {taskModel.Name} was cancelled.\n");
                else
                    Console.WriteLine($"The Task {taskModel.Name} finished with no result.\n");
            }
                
        }        
        static async Task ExecuteTasksInOrder(List<TaskModel<string>> taskModels)
        {            
            await taskModels[0].ExecuteTask();
            await taskModels[1].ExecuteTask();
            await taskModels[2].ExecuteTask();            

            Console.WriteLine("\n\n\n");

            for(int i = 0; i < taskModels.Count; i++)
            {
                if (taskModels[i].Result != null)
                    Console.WriteLine($"The Result of the Task {taskModels[i].Name} is {taskModels[i].Result}.\n");
                else if (CancellationManager.CancellToken.IsCancellationRequested)
                    Console.WriteLine($"The Task {taskModels[i].Name} was cancelled.\n");
                else
                    Console.WriteLine($"The Task {taskModels[i].Name} was cancelled.\n");
            }
        }
        #endregion        
    }
}
