using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using WebApplication1.Models;
using WebApplication1.Repositories.Interfaces;
using OracleCommand = Oracle.ManagedDataAccess.Client.OracleCommand;
using OracleConnection = Oracle.ManagedDataAccess.Client.OracleConnection;
using OracleDataAdapter = Oracle.ManagedDataAccess.Client.OracleDataAdapter;
using OracleParameter = Oracle.ManagedDataAccess.Client.OracleParameter;

namespace WebApplication1.Repositories.Concrete
{
    public class UserRepostiory : IUserRepository
    {
        private const string _connectionString = "Data Source=localhost:1521/xe; User ID=DietApp; Password=Diet";
        public IEnumerable<USER> Users
        {
            get
            {
                using (var context = new DietDbContext())
                {
                    return context.USERS.ToList();
                }
            } 
            set { }
        }

        public IEnumerable<USER_DATA> UsersData
        {
            get
            {
                using (var context = new DietDbContext())
                {
                    return context.USER_DATA.ToList();
                }
            }
            private set { }
        }

        public IEnumerable<USERINFO> UserInfo
        {
            get
            {
                using (var context = new DietDbContext())
                {
                    return context.USERINFOes.ToList();
                }
            }
            private set { }
        }

        public IEnumerable<USERS_LOGS> UsersLogs
        {
            get
            {
                using (var context = new DietDbContext())
                {
                    return context.USERS_LOGS.ToList();
                }
            }
            private set { }

        }

        public void CreateUserData(USER_DATA data)
        {
            using (var context = new DietDbContext())
            {
                context.CREATEUSERDATA(data.HEIGHT, data.WEIGHT, data.USER_ID);
                context.SaveChanges();
            }
        }

        public void DeletUserData(int id)
        {
            using (var context = new DietDbContext())
            {
                var itemToRemove = context.USER_DATA.FirstOrDefault(x => x.USER_DATA_ID == id);
                if(itemToRemove !=null)
                    context.USER_DATA.Remove(itemToRemove);
                context.SaveChanges();
            }
        }

        public int GetUserIdByName(string name)
        {
            int id = -1;
            using (var context = new DietDbContext())
            {
                var result = context.USERS.Where(x => x.USER_NAME == name).FirstOrDefault();
                if (result != null) id = result.USER_ID;
            }
            return id;
        }

        public void CreateUserLog(int userId, string deviceInfo)
        {
            using (var ctx = new DietDbContext())
            {
                ctx.ADDUSERLOG(userId, deviceInfo);
                ctx.SaveChanges();
            }
        }


        public void AddUserToDb(USER user)
        {
            var userNameParam = new OracleParameter("USERNAME",OracleDbType.Varchar2, user.USER_NAME, ParameterDirection.Input);
            var userPassParam = new OracleParameter("PASSWORD", OracleDbType.Varchar2, user.USER_PASSWORD, ParameterDirection.Input);
            var userGenderParam = new OracleParameter("USERGENDER", OracleDbType.Varchar2, user.GENDER, ParameterDirection.Input);
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
               OracleCommand cmd = new OracleCommand("CREATEUSER", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(userNameParam);
                cmd.Parameters.Add(userPassParam);
                cmd.Parameters.Add(userGenderParam);

                connection.Open();
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                cmd.ExecuteNonQuery();

                //  context.SaveChanges();
            }
        }

        public void SetNewPassword(int userId, string password)
        {
            using (var ctx = new DietDbContext())
            {
                ctx.CHANGEPASSWORD(password, userId);
                ctx.SaveChanges();
            }
        }

        public int LoginUsingPassword(string username, string password)
        {
            var result = 0;
            var userNameParam = new OracleParameter("USERNAME", OracleDbType.Varchar2, username, ParameterDirection.Input);
            var userPassParam = new OracleParameter("PASSWORD", OracleDbType.Varchar2, password, ParameterDirection.Input);
            var resultPassParam = new OracleParameter("result_tab", OracleDbType.Int32, ParameterDirection.ReturnValue);
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                OracleCommand cmd = new OracleCommand("LOGINFUNC", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.BindByName = true;

                cmd.Parameters.Add(resultPassParam);
                cmd.Parameters.Add(userNameParam);
                cmd.Parameters.Add(userPassParam);


                connection.Open();
                cmd.ExecuteNonQuery();

                var res = cmd.Parameters["result_tab"].Value.ToString();
                result = Convert.ToInt32(res);
                connection.Close();
                //  context.SaveChanges();
            }

            return result;
        }

        public bool ValidateData(USER user)
        {
            using (var context = new DietDbContext())
            {
                //CALL DB PROCEDURE
            }
            return false;
        }
    }
}