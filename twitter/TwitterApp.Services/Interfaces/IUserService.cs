using System;
using System.Collections.Generic;
using System.Text;
using TwitterApp.Models.User;

namespace TwitterApp.Services.Interfaces
{
    public interface IUserService
    {
        void AddEntity(RegisterModel user);

        void DeleteEntity(int id);

        List<RegisterModel> GetAllEntities();

        RegisterModel GetEntityById(int id);

        void UpdateEntity(RegisterModel user);

        RegisterModel GetEntityByExternalId(string id);
    }
}
