using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UAParser;
using WebApplication1.Models;
using WebApplication1.Repositories.Concrete;

namespace WebApplication1.Services
{
    public class UserService
    {
        private UserRepostiory _userRepostiory;

        public UserService()
        {
            _userRepostiory = new UserRepostiory();
        }
        public void AddUsersLog(string userName, string deviceInfo)
        {
            int userId = _userRepostiory.GetUserIdByName(userName);
            _userRepostiory.CreateUserLog(userId, deviceInfo);
        }

        public string getDeviceInfoFromUserAgent(string userAgent)
        {
            var uaParser = Parser.GetDefault();
            ClientInfo c = uaParser.Parse(userAgent);
            string res = c.Device.Brand.ToString()+ " " + c.OS.ToString()+ " " + c.UA.ToString();
            return res;
        }

        public void SetNewPassword(string userName, string newPassword)
        {
            int userId = _userRepostiory.GetUserIdByName(userName);
            _userRepostiory.SetNewPassword(userId, newPassword);
        }

        public List<USERS_LOGS> GetUsersLogs(int userId)
        {
          return  _userRepostiory.UsersLogs.Where(x => x.USER_ID == userId).OrderByDescending(x=> x.LOG_DATE).Take(5).ToList();
        }
    }
}