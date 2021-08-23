using Model;
using System.Collections.Generic;

namespace Controller
{
    public class AppController
    {

        /// <summary>
        /// This is the routing section of the app. This class controls communications between
        /// The view layer and the data layer
        /// </summary>
        
        private AppRepo _appRepo;

        public AppController(AppRepo appRepo){
            _appRepo = appRepo;
        }

        public List<User> ViewAllUsers(){
            return _appRepo.ViewAllUsers();
        }

        public bool LogIn(string username, string password){
            if(_appRepo.LogIn(username, password)){
                return true;
            } else {
                return false;
            }
        }

        public void WriteReview(string restaurant, int zipcode, string currentUser){
            _appRepo.WriteReview(restaurant, zipcode, currentUser);
        }

        public void ListReviews(string restaurant, int zipcode){
            _appRepo.ListReviews(restaurant, zipcode);
        }

        public void CreateAccount(string name, string username, string password){
            _appRepo.CreateAccount(name, username, password);
        }

        public string FindUser(int? userId){
            return _appRepo.FindUser(userId);
        }

        public string RecommendRestaurant(int zipcode){
           return _appRepo.RecommendRestaurant(zipcode);
        }

        public bool GetAdminStatus(string username){
            return _appRepo.GetAdminStatus(username);
        }

        public void BanUser(string username){
            _appRepo.BanUser(username);
        }

        public User SelectUser(string username){
            return _appRepo.GetUserInformation(username);
        }
    }
}