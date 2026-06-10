namespace ETicaret.Application.Shared.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SeedOrderAttribute : Attribute
    {
        public int Order { get; }
        public SeedOrderAttribute(int order)
        {
            Order = order;
        }
    }
}
