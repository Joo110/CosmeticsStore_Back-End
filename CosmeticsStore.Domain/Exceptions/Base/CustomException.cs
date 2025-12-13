namespace CosmeticsStore.Domain.Exceptions.Base
{
    public abstract class CustomException : Exception
    {
        public virtual string Title { get; }

        protected CustomException(string message)
            : base(message)
        {
            Title = "Custom Exception";
        }

        protected CustomException(string message, string title)
            : base(message)
        {
            Title = title;
        }
    }
}
