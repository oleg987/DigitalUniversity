namespace Common.Commands;

public interface ICommand
{
    public Task Execute(CancellationToken cancellationToken = default);
}