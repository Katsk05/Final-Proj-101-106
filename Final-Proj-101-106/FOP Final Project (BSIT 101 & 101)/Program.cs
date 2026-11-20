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
                        //WarehouseOptions(usersList);            placeholder lang mga yan for warehouse/user methodss
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
        public static User Login(int role, List<User> usersList)
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
                          "\n1. Update Password " + //done but check for bugs
                          "\n2. De-activate User" + //done but check for bugs
                          "\n3. Add Users" + //incomplete sa validation (user already exists)
                          "\n4. Display All Users" + //done but check for bugs
                          "\n5. Display Inactive Users" + // have bug
                          "\n6. Display Active Users" + //done check bugs
                          "\n7. Display Products" + //wait fro warehouse and user role
                          "\n8. Display Orders" + //wait for user role
                          "\n9. Logout"; //done

            int option;
            do
            {
                Console.Write($"{options} \n\nEnter your choice: ");
                option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        UpdatePassword(usersList);
                        break;                  
                    case 2:
                        DeactivateUser(usersList); //test & check
                        break;
                    case 3:
                        AddUsers(usersList); //test
                        break;
                    case 4:
                        DisplayAllUsers(usersList); //complete pero check parin validations 
                        break;
                    case 5:
                        ShowInactiveUsers(usersList);
                        break;
                    case 6:
                        ShowActiveUsers(usersList);
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

        public static void UpdatePassword(List<User> usersList)
        {
            Console.Write("\nEnter the username of the account you want to update: ");
            string username = Console.ReadLine();

            User target = null;
            foreach (var user in usersList)
            {
                if (user.Username == username)
                {
                    target = user;
                    break;
                }
            }
            if (target == null)
            {
                Console.WriteLine("User not found.");

            }

            Console.Write("Enter the new password: ");
            string newPassword = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                Console.WriteLine("\nPassword cannot be empty.\n");
                return;
            }
            if (newPassword == target.Password)
            {
                Console.WriteLine("\nNew password cannot be the same as the old password.");
                return;
            }
            Console.Write("Confirm the new password: ");
            string confirmPassword = Console.ReadLine();

            if (newPassword != confirmPassword)
            {
                Console.WriteLine("\nPasswords do not match. Password not updated. \n");
                return;
            }
            target.Password = newPassword;
            Console.WriteLine($"\nPassword for {target.Username} updated successfully. \n");

        }
        public static void DisplayAllUsers(List<User> usersList)
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
                    Console.WriteLine($"Status: {(user.IsActive ? "Active" : "Inactive")}");

                    Console.WriteLine();
                }
            }
        }

        public static void AddUsers(List<User> usersList) //may kulang pa ex: "user already exists"
        {

            Console.Write("\nPlease enter your username: ");
            string username = Console.ReadLine();

            foreach (var user in usersList)
            {
                if (user.Username == username)
                {
                    Console.WriteLine("Account already exists. Please create a different account.\n");
                    return;
                }
            }
            Console.Write("Please enter your password: ");
            string password = Console.ReadLine();
            usersList.Add(new User(username, password, "User", true));

            Console.WriteLine("Please select a role for the new user: " +
                              "\n1. Admin" +
                              "\n2. Warehouse" +
                              "\n3. User");
            int role;
            Console.Write("Enter the selected role: ");

            if (!int.TryParse(Console.ReadLine(), out role) || role < 1 || role > 3)
            {
                Console.WriteLine("\nInvalid account type. User not added.\n");
                return;
            }

            string roleName = DisplayRoleName(role);
            usersList.Add(new User(username, password, roleName, true));

            Console.WriteLine($"\n{roleName} account added successfully!\n");

            Console.WriteLine();
        }
        public static void DeactivateUser(List<User> usersList)
        {
            Console.Write("\nEnter the username of the account you want to deactivate: ");
            string username = Console.ReadLine();
            User target = null;
            foreach (var user in usersList)
            {
                if (user.Username == username)
                {
                    target = user;
                    break;
                }
            }
            if (target == null)
            {
                Console.WriteLine("User not found.");
                return;
            }
            if (!target.IsActive)
            {
                Console.WriteLine("User is already inactive.");
                return;
            }
            if (target.Role == "Admin")
            {
                Console.WriteLine("Cannot deactivate an Admin user.");
                return;
            }
            target.IsActive = false;
            Console.WriteLine($"User {target.Username} has been deactivated successfully.");
        }
        public static void ShowActiveUsers(List<User> usersList)
        {
            Console.WriteLine("\nList of Active Users");

            int count = 0;
            foreach (var user in usersList)
            {
                if (user.IsActive)
                {
                    count++;
                    Console.WriteLine($"Username: {user.Username}");
                    Console.WriteLine($"Role: {user.Role}");
                    Console.WriteLine($"Status: {(user.IsActive ? "Active" : "Inactive")}");
                    Console.WriteLine();
                }
                if (count == 0)
                {
                    Console.WriteLine("No active users found.");
                }
            }
        }
        public static void ShowInactiveUsers(List<User> usersList)
        {
            Console.WriteLine("\nList of Inactive Users");

            int count = 0;
            foreach (var user in usersList.Where(u => !u.IsActive))
            {
                count++;
                Console.WriteLine($"Username: {user.Username}");
                Console.WriteLine($"Role: {user.Role}");
                Console.WriteLine($"Status: {(user.IsActive ? "Active" : "Inactive")}");
                Console.WriteLine();
            }
            if (count == 0)
            {
                Console.WriteLine("No inactive users found.");
            }
        }
    }
}
