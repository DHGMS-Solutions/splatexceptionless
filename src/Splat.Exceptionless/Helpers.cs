namespace Splat.Exceptionless
{
    public static class Helpers
    {
        public static void UseExceptionless()
        {
            var funcLogManager = new FuncLogManager(type => new ExceptionlessSplatLogger(type));

            Locator.CurrentMutable.RegisterConstant(funcLogManager, typeof(ILogManager));
        }
    }
}
