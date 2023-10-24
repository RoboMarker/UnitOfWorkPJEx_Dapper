﻿using UnitOfWorkPJEx_DapperRepository.Models.Data;
using UnitOfWorkPJEx_DapperRepository.Interface;
using UnitOfWorkPJEx_DapperService.Interface;

namespace UnitOfWorkPJEx_DapperService.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork_Dapper _unitOfWork;
        public UserService(IUnitOfWork_Dapper unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> GetById(int UserId)
        {
            if (UserId > 0)
            {
                var User =  _unitOfWork.Users.GetById(UserId);
                if (User != null)
                {
                    return User;
                }
            }
            return null;
        }

        public async Task<IEnumerable<User>> GetUserAll()
        {
            var userslist =  _unitOfWork.Users.GetAll();
            return userslist;
        }

        public async Task<bool> CreateUser(User user)
        {
            if(user!=null)
            {
                 _unitOfWork.Users.Add(user);
                 _unitOfWork.Commit();

            }
            return false;
        }
        public async Task<bool> UpdateUser(User updateUser)
        {
            if (updateUser != null)
            {
                var user =  _unitOfWork.Users.GetById(updateUser.UserId);
                if (user != null)
                {
                    user.UserName = updateUser.UserName;
                    user.Age=updateUser.Age;
                    user.Sex = updateUser.Sex;
                    user.CityId = updateUser.CityId;
                    user.CountryId=updateUser.CountryId;
                    _unitOfWork.Users.Update(user);
                }
                _unitOfWork.Commit();
            }
            return false;
        }

        public async Task<bool> DeleteUser(int UserId)
        {
            if (UserId > 0)
            {
                var user =  _unitOfWork.Users.GetById(UserId);
                if (user != null)
                {
                    _unitOfWork.Users.Delete(user);
                    _unitOfWork.Commit();

                }
            }
            return false;
        }
    }
}
