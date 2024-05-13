namespace AuthService.Commands;

public interface ICommand
{
    public Task Execute(CancellationToken cancellationToken = default);
}