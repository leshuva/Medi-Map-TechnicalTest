namespace TechnicalTest;

public interface IErrorLogger
{
    /// <summary>
    /// Adds any errors to the ErrorLog table.
    /// </summary>
    /// <param name="errorMessage">The error message to store</param>
    void LogError(string errorMessage);
}