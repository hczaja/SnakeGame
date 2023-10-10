namespace Snakeventures.Exceptions;

internal class UnableToLoadComponentException : Exception
{
    public UnableToLoadComponentException(string componentName) : base($"Unable to load {componentName} exception.") { }
}
