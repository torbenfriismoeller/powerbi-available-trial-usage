using System;

namespace PowerbiAvailableTrialUsage
{
    class Program
    {
        static void Main(string[] args)
        {
            var pbi = new Powerbi();

            var result = pbi.TryGetAvailableFeaturesAsync();
            result.Wait();

            Console.WriteLine(result.Result);
            Console.ReadLine();
        }
    }
}
