using Controller;
using Model;
using System.Collections.Generic;
using System;

namespace View
{
    public class MainMenu
    {

        /// <summary>
        /// This is the main menu, which is started at Program.cs. 
        /// </summary>
        
        private AppController _appController;

        public MainMenu(AppController appController){
            _appController = appController;
        }

        private bool loggedIn = false;
        private string currentUser = "none";
        private bool isAdmin = false;
        private int userId;

        public void Start(){
            bool running = true;

            do {
                
                if(currentUser.Equals("none")){
                    Console.WriteLine("You are not logged in.");
                } else {
                    Console.WriteLine($"Welcome {currentUser}");
                }

                Console.WriteLine("Please make a selection.");
                Console.WriteLine("[0] Exit.");
                Console.WriteLine("[1] Log In.");
                Console.WriteLine("[2] Review a Restaurant.");
                Console.WriteLine("[3] See reviews.");
                Console.WriteLine("[4] Create Account");
                Console.WriteLine("[5] Recommend me a restaurant");

                if(GetAdminStatus(currentUser)){
                    Console.WriteLine("##### - Admin Menu - #####");
                    Console.WriteLine("[6] View All Users.");
                    Console.WriteLine("[7] Remove a user's reviews.");
                    Console.WriteLine("[8] Get info about a user.");
                }

                switch(Console.ReadLine()){
                    case "0":
                        Console.WriteLine("Exiting...");
                        running = false;
                    break;

                    case "1":
                        Console.WriteLine("Please enter your Username:");
                        string username = Console.ReadLine();
                        Console.WriteLine("Please Enter your password:");
                        string password = Console.ReadLine();
                        LogIn(username, password);
                    break;

                    case "2":
                        try {
                            if(loggedIn){
                                Console.Clear();
                                Console.WriteLine("Enter the name of the Restaurant.");
                                string r = Console.ReadLine();
                                Console.WriteLine("Enter the zipcode.");
                                string z = Console.ReadLine();

                                int zip = int.Parse(z);

                                WriteReview(r, zip, currentUser);
                            } else {
                                Console.Clear();
                                Console.WriteLine("You must login first.");
                            }
                        } catch(System.FormatException e){
                            Console.WriteLine("Not a valid response.");
                        }
                    break;

                    case "3":
                        Console.WriteLine("Please enter the restaurant:");
                        string restaurant = Console.ReadLine();
                        Console.WriteLine("Please Enter the zipcode:");
                        string zipcode = Console.ReadLine();

                        int zipResult = int.Parse(zipcode);

                        ListReviews(restaurant, zipResult);
                    break;

                    case "4":
                        Console.WriteLine("Enter your name:");
                        string newName = Console.ReadLine();
                        Console.WriteLine("Enter a username:");
                        string newUsername = Console.ReadLine();
                        Console.WriteLine("Enter a Password:");
                        string newPassword = Console.ReadLine();

                        CreateAccount(newName, newUsername, newPassword);
                    break;

                    case "5":
                        Console.WriteLine("Please enter your zipcode.");
                        string preZip = Console.ReadLine();

                        Console.Clear();

                        int zipResult1 = int.Parse(preZip);

                        Console.WriteLine(RecommendRestaurant(zipResult1));
                    break;

                    case "6":
                        if(GetAdminStatus(currentUser)){
                            ViewAllUsers();
                        }
                    break;

                    case "7":
                        if(GetAdminStatus(currentUser)){
                            string bannedUser = Console.ReadLine();
                            BanUser(bannedUser);

                            Console.WriteLine("Successfully Removed all of user's reviews.");
                        }
                    break;

                    case "8":
                        Console.WriteLine("Please enter the username: ");
                        string selectedUser = Console.ReadLine();

                        SelectUser(selectedUser);
                    break;

                    default:
                        Console.WriteLine("Not a valid response.");
                    break;
                }

            } while(running);
        }

        public void ViewAllUsers(){
            List<User> users = _appController.ViewAllUsers();
            foreach(var user in users){
                Console.WriteLine(user.Name);
            }
        }

        public void LogIn(string username, string password){
            Console.Clear();
            if(_appController.LogIn(username, password)){
                loggedIn = true;
                currentUser = username;
            } else {
                Console.WriteLine("You are not logged in");
            }
            Console.Clear();
        }

        public void WriteReview(string restaurant, int zipcode, string currentUser){
            Console.Clear();
            _appController.WriteReview(restaurant, zipcode, currentUser);
        }

        public void ListReviews(string restaurant, int zipcode){
            Console.Clear();
            _appController.ListReviews(restaurant, zipcode);
        }

        public void CreateAccount(string name, string username, string password){
            _appController.CreateAccount(name, username, password);
        }

        public string FindUser(int? userId){
            return _appController.FindUser(userId);
        }

        public string RecommendRestaurant(int zipcode){
            return _appController.RecommendRestaurant(zipcode);
        }

        public bool GetAdminStatus(string username){
            return _appController.GetAdminStatus(username);
        }

        public void BanUser(string username){
            _appController.BanUser(username);
        }

        public void SelectUser(string username){
            Console.WriteLine("Name: " + _appController.SelectUser(username).Name);
            Console.WriteLine("Username: " + _appController.SelectUser(username).Username);
            Console.WriteLine("Password: " + _appController.SelectUser(username).Password);
            Console.WriteLine("Is Admin: " + _appController.SelectUser(username).IsAdmin);
        }
    }
}