using FOP_Final_Project__BSIT_101___106_.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FOP_Final_Project__BSIT_101___106_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<User> usersList = new List<User>();

            DisplayInitialUsers(usersList);
            
            var choicesMessage = "Please select a user to login:" +
                                "\n1 Admin" +
                                "\n2 Warehouse" +
                                "\n3 User" +
                                "\n0 Exit";
         
            var running = true;
            while (running)
            {
                Console.WriteLine(choicesMessage);
                Console.Write("\nEnter user role: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                User loggedIn;
                switch (choice)
                {
                    case 1:
                        loggedIn = Login(1, usersList);
                        if (loggedIn != null)
                            AdminOptions(usersList);
                        break;
                    case 2:
                        loggedIn = Login(2, usersList);
                        //if (loggedIn != null)
                            //WarehouseOptions(usersList);            placeholder lang mga yan for warehouse pati user methofs
                        break;
                    case 3:
                        loggedIn = Login(3, usersList);
                        //if (loggedIn != null)
                            //UserOptions(usersList);
                        break;
                    case 0:
                        running = false;
                        break;
                }
            }
        }

        public static void DisplayInitialUsers(List<User> usersList)
        {
            usersList.Add(new User("Labi_Admin", "123", "Admin", true));
            usersList.Add(new User("Bumatay_Admin", "123", "Admin", true));
            usersList.Add(new User("Deocampo_Warehouse", "123", "Warehouse", true));  //true = active, false = inactive
            usersList.Add(new User("Malayo_Warehouse", "123", "Warehouse", false));
            usersList.Add(new User("Medrano_User", "123", "User", false)); 
        }

        public static string DisplayRoleName(int role)
        {
            string name = "";
            switch (role)
            {
                case 1:
                    name = "Admin";
                    break;
                case 2:
                    name = "Warehouse";
                    break;
                case 3:
                    name = "User";
                    break;
            }
            return name;
        }
        public static User Login (int role, List<User> usersList) 
        {
            for (int attempts = 1; attempts <= 3; attempts++)
            {
                Console.Write("\nPlease enter your username: ");
                string username = Console.ReadLine();
                Console.Write("Please enter your password: ");
                string password = Console.ReadLine();

                User found = null;
                foreach (var user in usersList)
                {
                    if (user.Username == username && user.Password == password && user.Role == DisplayRoleName(role))
                    {
                        found = user;
                        break;
                    }
                }
                if (found != null)
                {
                    if (!found.IsActive)
                    {
                        Console.WriteLine("\nYour account is inactive. Please contact the administrator."); //test natin after magkaron na ng inactive users        
                        return null;
                    }
                    Console.WriteLine($"\nLogin successful! Welcome, {found.Username}. \n");
                    return found;
                }
                else
                {
                    Console.WriteLine($"\nInvalid username or password. Please try again. \nAttempts left: {3 - attempts}/3.");
                }
            }
            Console.WriteLine("Maximum login attempts exceeded. Exiting the program.");
            Environment.Exit(0);
            return null;
        }

        public static void AdminOptions(List<User> usersList)
        {
            var options = "Please select an option:" +
                          "\n1. Update Password " +
                          "\n2. De-activate User" +
                          "\n3. Add Users" + //partially done
                          "\n4. Display All Users" + //partially done
                          "\n5. Display Inactive Users" +
                          "\n6. Display Active Users" +
                          "\n7. Display Products" +
                          "\n8. Display Orders" +
                          "\n9. Logout"; //done

            int option;
            do
            {
                Console.WriteLine(options);
                option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        //UpdatePassword(); 
                        break;
                        //De-activate User
                    case 2:
                        break;
                    case 3: AddUsers(usersList); //incomplete sa validation
                        break;
                    case 4:
                        DisplayAllUsers(usersList); //complete pero check parin validations 
                        break;
                    case 5:
                        //DisplayInactiveUsers();
                        break;
                    case 6:
                        //DisplayActiveUsers();
                        break;
                    case 7:
                        //DisplayProducts();
                        break;
                    case 8:
                        //DisplayOrders();
                        break;
                    case 9:
                        Console.WriteLine("Logged Out. \n");
                        break;
                }
            } while (option != 9);
        }
        
        public static void UpdatePassword()
        {
        //Not done
        }
        public static void DisplayAllUsers (List<User> usersList) 
        {
            if (usersList.Count == 0)
            {
                Console.WriteLine("\nNo users found"); 
            }
            if (usersList.Count > 0)
            {
                Console.WriteLine("\nList of Users: \n");
                foreach (var user in usersList)
                {
                    Console.WriteLine($"Username: {user.Username}");
                    Console.WriteLine($"Role: {user.Role}");
                    Console.WriteLine($"Status: { (user.IsActive ? "Active" : "Inactive") }");

                    Console.WriteLine();
                    }
                }
            }

        public static void AddUsers(List<User> usersList) //may kulang pa ex: "user already exists"
        {
            Console.Write("Please enter your username: ");
            string username = Console.ReadLine();

            Console.Write("Please enter your password: ");
            string password = Console.ReadLine();
            usersList.Add(new User(username, password, "User", true));

            Console.WriteLine("\nUser added successfully!");

            Console.WriteLine(); 
        }

        public static void ActiveUsers() 
        {
            //not
            //

        }
    }
}

