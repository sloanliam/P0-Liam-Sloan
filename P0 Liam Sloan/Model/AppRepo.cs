using Model.Entities;
using System.Linq;
using System.Collections.Generic;
using System;
using Serilog;
using Serilog.Sinks.File;
using Serilog.Sinks.SystemConsole;

namespace Model
{
    public class AppRepo
    {

        /// <Summary>
        /// This is the powerhouse class for the backend. AppRepo handles communications with the database, and is 
        /// 'fed' data from AppController (in turn collection data/parameters from the main menu selections).
        /// </Summary>

        private revtrainingdbContext _context;

        public AppRepo(revtrainingdbContext context){
            _context = context;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        // This command returns all users. Only available as an admin.
        public List<User> ViewAllUsers(){
            return _context.Users.Select(
                user => new Model.User(user.Username)
            ).ToList();
        }

        // An important thing to note is that this method checks the username, and then the password afterwards.
        public bool LogIn(string username, string password){
            try {
                var user = _context.Users.Single(user => user.Username.Equals(username));
                if(user.Password.Equals(password)){
                    Console.WriteLine("Log in successful");
                    return true;
                }else{
                    Console.WriteLine("Login was not successful, try again.");
                    return false;
                }
            } catch(System.InvalidOperationException e){
                Log.Error(e, "There was an error logging in.");
                Console.WriteLine("Login was not successful, try again.");
                return false;
            }
        }

        // This method will produce a review from the user. 
        // Perhaps this method is a little confusing to look at, but I found that it works well.
        public void WriteReview(string restaurant, int zipcode, string currentUser){
            var availableRestaurants = _context.Restaurants.Where(res => res.Name.Contains(restaurant) && res.Zipcode == zipcode).ToList();
            if(availableRestaurants.Count < 1){
                Console.WriteLine("Could not be found.");
                Console.WriteLine("Add Restaurant?");

                string input = Console.ReadLine();

                if(input.Equals("yes")){
                    _context.Add(new Entities.Restaurant{
                        Name=restaurant,
                        Zipcode=zipcode
                    });
                    _context.SaveChanges();
                    WriteReview(restaurant, zipcode, currentUser);
                }else{
                    Console.WriteLine("Returning to menu...");
                }
            }
            foreach(var rest in availableRestaurants){

                try {

                    var currentUserId = _context.Users.Single(user => user.Username.Equals(currentUser));

                    int userId = currentUserId.Id;

                    Console.WriteLine("Enter your review:");
                    string review = Console.ReadLine();

                    Console.WriteLine("Enter your star rating (5 stars maximum): ");
                    string preStar = Console.ReadLine();

                    int starResult = int.Parse(preStar);

                    if(starResult > 5 || starResult < 1){
                        Console.WriteLine("Rating must be a number between 1 and 5.");
                        WriteReview(restaurant, zipcode, currentUser);
                    }
                    
                    if(starResult <= 5){
                        _context.Reviews.Add(
                            new Entities.Review {
                                Review1 = review,
                                RestaurantId = rest.Id,
                                UserId = userId,
                                Stars = starResult
                            }
                        );
                        _context.SaveChanges();
                        Console.WriteLine("Review submitted.");
                    }
                } catch(System.FormatException e){
                    Console.WriteLine("Not a valid response.");
                }
            }
            Console.Clear();
        }

        public void ListReviews(string restaurant, int zipcode){

            Console.Clear();

            var availableRestaurants = _context.Restaurants.Where(rest => rest.Name.Equals(restaurant) && rest.Zipcode == zipcode);
            int restId = 0;
            foreach(var rest in availableRestaurants){
                restId = rest.Id;
            }

            float avgRating = 0;
            int count = 0;

            var matchedReviews = _context.Reviews.Where(mReviews => mReviews.RestaurantId == restId);
            foreach(var review in matchedReviews){
                Console.WriteLine("################");
                Console.WriteLine($"A user said:");
                Console.WriteLine($"{review.Review1}");
                Console.WriteLine($"Star rating: {review.Stars}");
                count += 1;
                avgRating += (float)review.Stars;
            }

            if(avgRating != 0){
                Console.WriteLine("*****************************");
                Console.WriteLine("Average Star Rating: " + avgRating / count);
                Console.WriteLine("Total Reviews: " + count);
                Console.WriteLine("*****************************");
            } else {
                Console.WriteLine("Could not find any reviews for that restaurant.");
            }
        }

        // this method will check to see if the username is already taken. 
        public void CreateAccount(string name, string username, string password){
            var userList = _context.Users.Where(users => users.Username.Equals(username)).ToList();
            if(userList.Count > 0){
                Console.WriteLine($"I'm sorry but that username isn't available.");
            } else {
                Console.WriteLine("Creating account...");
                _context.Users.Add(new Entities.User{
                    Name = name,
                    Username = username,
                    Password = password
                });
                _context.SaveChanges();
                Console.WriteLine("Account Created.");
            }
        }

        public string FindUser(int? userId){
            try {
                var userList = _context.Users.Single(users => users.Id == userId);
                return (string)userList.Username;
            } catch(System.InvalidOperationException e){
                Log.Error(e, "Error Finding user.");
                return "This user cannot be found.";
            }
        }

        public User GetUserInformation(string username){
            try{
                var user = _context.Users.Single(foundUser => foundUser.Username.Equals(username));
                User selectedUser = new User(user.Id, user.Name, user.Username, user.Password, user.IsAdmin);
                return selectedUser;
            } catch(System.InvalidOperationException e){
                Console.WriteLine("Could not be found.");
                return new User();
            }
        }

        public string RecommendRestaurant(int zipcode){
            var reviews = _context.Reviews.Where(review => review.Stars > 4).ToList();
            var restaurants = _context.Restaurants.Where(restaurant => restaurant.Zipcode == zipcode).ToList();

            foreach(var rev in reviews){
                foreach(var rest in restaurants){
                    if(rev.RestaurantId == rest.Id){
                        return $"{rest.Name} has high reviews.";
                    }
                }
            }
            return "There aren't any highly reviews restaurants in your area.";
        }

        public bool GetAdminStatus(string username){
            try {
                var usernames = _context.Users.Single(user => user.Username.Equals(username));
                if(usernames.IsAdmin == "yes"){
                    return true;
                } else {
                    return false;
                } 
            } catch (System.InvalidOperationException e){
                return false;
            }
        }

        public void BanUser(string username){
            var users = _context.Users.Single(user => user.Username.Equals(username));
            int banUserId = users.Id;

            var reviews = _context.Reviews.Where(review => review.UserId == banUserId).ToList();

            foreach(var bannedReview in reviews){
                if(bannedReview.UserId == users.Id){
                    _context.Remove(bannedReview);
                }
            }
            _context.SaveChanges();
        }
    }
}