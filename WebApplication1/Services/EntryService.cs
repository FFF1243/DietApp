using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using WebApplication1.Models;
using WebApplication1.Repositories.Concrete;

namespace WebApplication1.Services
{
    public class EntryService
    {
        private UserRepostiory _repostiory;

        public EntryService()
        {
            _repostiory = new UserRepostiory();
        }

        public List<USER_DATA> GetUserEntries(string name)
        {
            var id = ((UserRepostiory)_repostiory).GetUserIdByName(name);
            var result = _repostiory.UsersData.Where(x => x.USER_ID == id).ToList();
            return result;
        }

        public EntriesIndexViewModel GetEntriesVM(string name)
        {
            var id = ((UserRepostiory)_repostiory).GetUserIdByName(name);
            var entries = _repostiory.UsersData.Where(x => x.USER_ID == id).ToList();

            var userInfo = _repostiory.UserInfo.Where(x => x.USER_NAME.Equals(name)).FirstOrDefault();

            var vm = new EntriesIndexViewModel();
            if (userInfo != null)
            {
                vm = new EntriesIndexViewModel()
                {
                    UserData = entries,
                    RecentHeight = Convert.ToString(userInfo.RECENT_HEIGHT),
                    RecentWeight = Convert.ToString(userInfo.RECENT_WEIGHT, CultureInfo.InvariantCulture)
                };


            }
            return vm;
        }
        public void CreateUserEntry(string name, UserDataViewModel vm)
        {
            var id = ((UserRepostiory)_repostiory).GetUserIdByName(name);
            var data = new USER_DATA()
            {
                HEIGHT = vm.Height,
                WEIGHT = vm.Weight,
                ENTRY_DATE = DateTime.Now,
                USER_ID = id
            };
            _repostiory.CreateUserData(data);
        }

        public void DeleteUserData(int id)
        {
            _repostiory.DeletUserData(id);
        }

        public Chart GetChartForUser(string name)
        {
            var list = GetUserEntries(name);

            var xAxes = list.OrderBy(x=>x.ENTRY_DATE).Select(x => x.ENTRY_DATE).ToList();
            var yAxes = list.OrderBy(x => x.ENTRY_DATE).Select(x => x.WEIGHT).ToList();
            var myChart = new Chart(width: 600, height: 400,theme: ChartTheme.Green)
                .AddTitle("Progres")
                .AddSeries(
                    name: "Waga",
                    chartType: "Line",
                    xValue: xAxes,
                    yValues: yAxes);
  


            return myChart;
        }

        
    }
}