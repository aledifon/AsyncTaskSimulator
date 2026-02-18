using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncTaskSimulator
{
    internal class TaskModel<T>
    {
        public string Name { get; }
        public int Duration { get; }
        public T? Result { get; private set; }            

        private readonly Func<int, Task<T>> _operation;

        public TaskModel(string name, int duration, Func<int, Task<T>> operation)
        {
            this.Name = name;
            this.Duration = duration;
            _operation = operation;
        }

        public Task GetTask() => ExecuteTask();

        public async Task ExecuteTask()
        {
            var myThread = Thread.CurrentThread.ManagedThreadId;

            Console.WriteLine($"Executing the task on the thread: {myThread}");

            Result = await _operation(Duration);

            Console.WriteLine($"The task on the thread {myThread} has finished");
        }
    }
}
