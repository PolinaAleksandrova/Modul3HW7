using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Modul3HW7
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            var someTasks = new List<Task<int>>()
            {
                Task.Run(() => CountNumbersOfFibonacci(2, token)),
                Task.Run(() => CountNumbersOfFibonacci(8, token)),
                Task.Run(() => CountNumbersOfFibonacci(10, token))
            };

            var res = Task.WhenAll(someTasks);

            foreach (var result in res.GetAwaiter().GetResult())
            {
                Console.WriteLine(result);
            }

            var timer = 10000;
            cancellationTokenSource.CancelAfter(timer);

            try
            {
                var task = Task.Run(() => CountNumbersOfFibonacciRecurs(50, token));

                var numResult = task.GetAwaiter().GetResult();

                Console.WriteLine($"Number of Fibonacci {numResult}");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public static int CountNumbersOfFibonacci(int ind, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            if (ind == 0 || ind == 1)
            {
                return ind;
            }

            return CountNumbersOfFibonacci(ind - 1, token)
                + CountNumbersOfFibonacci(ind - 2, token);
        }

        public static async Task<int> CountNumbersOfFibonacciRecurs(int index, CancellationToken token)
        {
            return await Task.Run(() => CountNumbersOfFibonacci(index, token));
        }
    }
}