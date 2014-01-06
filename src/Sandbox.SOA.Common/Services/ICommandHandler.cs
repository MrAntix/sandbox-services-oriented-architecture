namespace Sandbox.SOA.Common.Services
{
    public interface ICommandHandler
    {
        void Handle<T>(T model);
        TOut Handle<TIn, TOut>(TIn model);
    }
}