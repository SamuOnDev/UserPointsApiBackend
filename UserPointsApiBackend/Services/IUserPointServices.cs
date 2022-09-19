using UserPointsApiBackend.Models.DataModels;

namespace UserPointsApiBackend.Services
{
    public interface IUserPointServices
    {
        UserPoint AddPointsToUser(int id, int quantityOfProducts);
    }
}
