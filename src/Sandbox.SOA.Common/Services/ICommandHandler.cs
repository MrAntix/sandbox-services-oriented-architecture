namespace Sandbox.SOA.Common.Services
{
    public interface ICommandHandler
    {
        void Handle<TIn>(TIn model);
        TOut Handle<TIn, TOut>(TIn model);
    }
}