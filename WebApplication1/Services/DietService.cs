using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Antlr.Runtime.Misc;
using WebApplication1.Models;
using WebApplication1.Repositories.Concrete;
using WebApplication1.Repositories.Interfaces;

namespace WebApplication1.Services
{
    public class DietService
    {
        private DietRepository _dietRepository;
        private IProductRepository _productRepository;
        private UserRepostiory _userRepository;

        public DietService()
        {
            _productRepository = new ProductRepository();
            _dietRepository = new DietRepository();
            _userRepository = new UserRepostiory();
        }

        public int AddDiet(DateTime date)
        {
            int id = _dietRepository.CreateNewDiet(date);
            return id;
        }

        public List<DIET> GetAllUsersDiets(string name)
        {
            var id = _userRepository.GetUserIdByName(name);
            var dietIds = _dietRepository.USER_DIETS.Where(x => x.USER_ID == id).Select(t => t.DIET_ID).ToList();

            var result = _dietRepository.DIETS.Where(x => dietIds.Contains(x.DIET_ID)).ToList();
            return result;
        }

        public void AddProductToDiet(ProductDietViewModel vm)
        {
            _dietRepository.AddProductToDiet(vm.DietId, vm.ProductId, vm.Quantity);
        }

        public void RegisterDietForUser(DietViewModel vm, string name)
        {
            int userId = _userRepository.GetUserIdByName(name);
            var alreadyInBase  = _dietRepository.USER_DIETS.FirstOrDefault(x => x.USER_ID == userId && x.DIET_ID == vm.DietId);
            if (alreadyInBase != null)
                return;


            var entry = new USER_DIETS()
            {
                ENTRY_DATE = vm.Date,
                DIET_ID = vm.DietId,
                USER_ID = userId
            };

            _dietRepository.AddUserDietsEntry(entry);
        }

        public void RegisterDietForUser(ShareDietViewModel vm)
        {
            int userId = _userRepository.GetUserIdByName(vm.Name);
            var alreadyInBase = _dietRepository.USER_DIETS.FirstOrDefault(x => x.USER_ID == userId && x.DIET_ID == vm.DietId);

            if (alreadyInBase != null)
                return;


            var diet = _dietRepository.DIETS.FirstOrDefault(x => x.DIET_ID == vm.DietId);

            var entry = new USER_DIETS()
            {
                ENTRY_DATE = diet.ENTRY_DATE,
                DIET_ID = vm.DietId,
                USER_ID = userId
            };

            _dietRepository.AddUserDietsEntry(entry);
        }

        public Dictionary<PRODUCT,short> GetChosenProducts(int dietId)
        {
           // var result = new List<PRODUCT>();

            var query = _dietRepository.DIETS_PRODUCTS.Where(x => x.DIET_ID == dietId)
                .ToDictionary(p => p.PRODUCT_ID, p => p.QUANTITY);

            var ret = _productRepository.Products.ToDictionary(x => x.PRODUCT_ID, x => x);

            var result = query.ToDictionary(x => ret[x.Key], x => x.Value);


           // result.AddRange(ret);
            return result;
        }

        public void UpdateDate(int dietId, DateTime date)
        {
           _dietRepository.UpdateDate(dietId,date);
        }

        public void DeleteDiet(int dietId)
        {
            _dietRepository.DeleteAllDietProductsEntries(dietId);
            _dietRepository.DeleteDiet(dietId);

        }

    }
}