namespace CakeShop
{
    internal class HealthCheckOptions
    {
        public System.Func<object, bool> Predicate { get; set; }
        public object ResponseWriter { get; set; }
    }
}