namespace MVVM_Tutorial.Commands;

using System.Threading.Tasks;

internal abstract class AsyncCommandBase : CommandBase
{
    private bool isExecuting;

    private bool IsExecuting
    {
        get => isExecuting;
        set { isExecuting = value; OnCanExecuteChanged(); }
    }

    public override async void Execute(object? parameter)
    {
        IsExecuting = true;
        try
        {
            await ExecuteAsync(parameter);
        }
        finally
        {
            IsExecuting = false;
        }
    }
    public override bool CanExecute(object? parameter)
    {
        return base.CanExecute(parameter) && !IsExecuting;
    }

    public abstract Task ExecuteAsync(object? parameter);
}
