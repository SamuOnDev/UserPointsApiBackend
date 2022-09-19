using UserPointsApiBackend.DataAccess;
using UserPointsApiBackend.Models.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace UserPointsApiBackend.Services
{
    public class UserPointServices : IUserPointServices
    {
        private readonly UserPointsDBContext _context;

        public UserPointServices(UserPointsDBContext context)
        {
            _context = context;
        }

        private UserPoint? UserSearchQuery(int id)
        {

            var userFound = (from user in _context.UserPoints
                             where id == user.Id
                             select user).FirstOrDefault();

            if (userFound == null)
            {
                return null;
            }

            var userWithPoints = new UserPoint()
            {
                Id = userFound.Id,
                TotalPoints = userFound.TotalPoints,
                Points = userFound.Points,
                Rank = userFound.Rank
            };

            return userWithPoints;
        }

        public UserPoint AddPointsToUser(int id, int quantityOfProducts)
        {

            var searchResult = UserSearchQuery(id);

            if (searchResult != null)
            {
                switch (searchResult.Rank)
                {
                    case (Rank)0:
                        searchResult.TotalPoints = searchResult.TotalPoints + (quantityOfProducts * 0.2f);
                        break;
                    case (Rank)1:
                        searchResult.TotalPoints = searchResult.TotalPoints + (quantityOfProducts * 0.4f);
                        break;
                    case (Rank)2:
                        searchResult.TotalPoints = searchResult.TotalPoints + (quantityOfProducts * 0.6f);
                        break;
                }
            }
            else
            {
                throw new Exception("No se ha encontrado al usuario");
            }

            return searchResult;
        }
    }
}

// TODO: Servicio de webhook para recoger el evento de cobro y agregar los puntos a los usuarios
