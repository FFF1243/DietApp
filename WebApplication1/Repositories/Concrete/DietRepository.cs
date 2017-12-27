using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using WebApplication1.Models;

namespace WebApplication1.Repositories.Concrete
{
    public class DietRepository
    {
        private const string _connectionString = "Data Source=localhost:1521/xe; User ID=DietApp; Password=Diet";
        public IEnumerable<DIET> DIETS
        {
            get
            {
                using (var ctx = new DietDBContext())
                {
                    return ctx.DIETS.ToList().ToList();
                }
            }
            private set
            {
                
            }
        }

        public IEnumerable<DIET_PRODUCTS> DIETS_PRODUCTS
        {
            get
            {
                using (var ctx = new DietDBContext())
                {
                    return ctx.DIET_PRODUCTS.ToList();
                }
            }
            private set
            {

            }
        }
        public IEnumerable<USER_DIETS> USER_DIETS
        {
            get
            {
                using (var ctx = new DietDBContext())
                {
                    return ctx.USER_DIETS.ToList();
                }
            }
            private set
            {

            }
        }

        public void AddUserDietsEntry(USER_DIETS entry)
        {
            using (var ctx = new DietDBContext())
            {
                ctx.USER_DIETS.Add(entry);
                ctx.SaveChanges();
            }
        }

        /*public void AddDiet(DIET diet)
        {
            using (var ctx = new DietDBContext())
            {
                ctx.DIETS.Add(diet);
                ctx.SaveChanges();
            }
        }*/

        public void AddProductToDiet(int dietId, int productId, int quantity)
        {
            using (var ctx = new DietDBContext())
            {
                ctx.ADDPRODUCTODIET(productId, dietId, quantity);
                ctx.SaveChanges();
            }
        }

        public int CreateNewDiet(DateTime date)
        {
            var result = 0;
           
            var dateParam = new OracleParameter("DATE_PARAM", OracleDbType.Date, date, ParameterDirection.Input);
            var resultPassParam = new OracleParameter("returnID", OracleDbType.Int32, ParameterDirection.ReturnValue);
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                OracleCommand cmd = new OracleCommand("ADDDIET", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.BindByName = true;

                cmd.Parameters.Add(resultPassParam);
                cmd.Parameters.Add(dateParam);


                connection.Open();
                cmd.ExecuteNonQuery();

                var res = cmd.Parameters["returnID"].Value.ToString();
                result = Convert.ToInt32(res);
                connection.Close();
            }

            return result;
        }

        public void UpdateDate(int dietId, DateTime date)
        {
            using (var ctx = new DietDBContext())
            {
                var item = ctx.DIETS.Where(x => x.DIET_ID == dietId).FirstOrDefault();
                item.ENTRY_DATE = date;
                ctx.SaveChanges();
            }
         
        }

        public void DeleteAllDietProductsEntries(int dietId)
        {
            using (var ctx = new DietDBContext())
            {
                var items = ctx.DIET_PRODUCTS.Where(x => x.DIET_ID == dietId).ToList();
                ctx.DIET_PRODUCTS.RemoveRange(items);
                ctx.SaveChanges();
            }
        }

        public void DeleteDiet(int dietId)
        {
            using (var ctx = new DietDBContext())
            {
                var item = ctx.DIETS.FirstOrDefault(x => x.DIET_ID == dietId);
                if (item != null)
                {
                    var userDiets = item.USER_DIETS;
                    ctx.USER_DIETS.RemoveRange(userDiets);
                    ctx.DIETS.Remove(item);
                    ctx.SaveChanges();
                }          
            }
        }

        public void EditDiet(int dietId)
        {
            
        }

    }
}