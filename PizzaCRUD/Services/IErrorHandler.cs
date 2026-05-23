namespace PizzaCRUD.Services
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}