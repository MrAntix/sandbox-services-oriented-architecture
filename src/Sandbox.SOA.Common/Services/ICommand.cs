namespace Sandbox.SOA.Common.Services
{
    public interface ICommand<in TIn, out TOut> : ICommand
    {
        TOut Execute(TIn model);
    }

    public interface ICommand<in TIn> : ICommand
    {
        void Execute(TIn model);
    }

    public interface ICommand
    {
    }
}