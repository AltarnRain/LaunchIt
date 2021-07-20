using GameOptimizer.DependencyInjection;
using StrongInject;

namespace GameOptimizer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using var container = new DIContainer();
            container.Run(x => x.Start());
        }
    }
}
