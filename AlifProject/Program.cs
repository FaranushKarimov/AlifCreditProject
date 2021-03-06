namespace AlifProject
{
    using System;
    using System.Data;
    using System.Collections.Generic;
    class Program
    {
        static void Main(string[] args)
        {
            string adminLogin = "admin";
            string adminPassword = "password";
            string[] familyArray = new string[] { "Single", "Married", "Divorced", "Widowed" };
            int clientID = 0;


            //! Dictionary example 
            var dictionary = new Dictionary<string, int>();

            int choice;
        //bool working = true; 

        startCode:
            System.Console.Write(@"Welcome to AlifCreditHistory
                Are you client or admin? 
                1. User
                2. Admin
                Please type reference number: ");

            int userInput;
            if (int.TryParse(Console.ReadLine(), out userInput))
            {
                if (userInput == 1)
                {
                    goto userLogin;
                }
                else if (userInput == 2)
                {
                    goto adminLogin;
                }
                else
                {
                    goto startCode;
                }
            }



        userLogin:
            System.Console.WriteLine("\t\t\t\tUser Panel");
            System.Console.Write(@"
                1. Register
                2. Login
                Please type referece number: ");
            int clientInput;
            if (int.TryParse(Console.ReadLine(), out clientInput))
            {
                if (clientInput == 1)
                {
                    goto userRegister;
                }
                else if (clientInput == 2)
                {
                    goto clientLogin;
                }
                else
                {
                    ConsoleShow.Red("\t\t\t\tIncorrect choice. Try Again");
                    goto userLogin;
                }
            }

        clientLogin:
            System.Console.WriteLine("\t\t\t\tPlease type your login and password");
            System.Console.Write("\t\t\t\tLogin: ");
            string tempClientLogin = Console.ReadLine();
            System.Console.Write("\t\t\t\tPassword: ");
            string tempClientPassword = Console.ReadLine();
            Logins clientLogin = new Logins(tempClientLogin, tempClientPassword);
            clientID = clientLogin.Login();
            if (clientID == -1 || clientID == -2)
            {
                goto clientLogin;
            }
            else
            {
                goto clientFunctions;
            }

        clientFunctions:
            System.Console.Write(@"
                What you can do:
                1. Credit application filling 
                2. Applications State View
                3. See graph of payment by Application ID
                4. Exit
                Please type reference number: ");
            int clientFunctionsChoice;
            if (int.TryParse(Console.ReadLine(), out clientFunctionsChoice))
            {
                if (clientFunctionsChoice == 1)
                {
                    goto creditApplicationFilling;
                }
                else if (clientFunctionsChoice == 2)
                {
                    goto clientApplicationView;
                }
                else if (clientFunctionsChoice == 3)
                {
                    goto graphPayment;
                }
                else if (clientFunctionsChoice == 4)
                {
                    goto startCode;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("\t\t\t\tError try again");
                    Console.ForegroundColor = ConsoleColor.White;
                    goto clientFunctions;
                }
            }

        graphPayment:
            System.Console.Write("Type Application ID: ");
            int id = int.Parse(Console.ReadLine());
            Payment payment = new Payment(clientID, id);
            payment.showPaymentGraphByClientID();
            goto clientFunctions;


        creditApplicationFilling:
            System.Console.WriteLine("\t\t\t\tThere a few questions to fill out: ");
        creditAmountChecking:
            System.Console.Write("\t\t\t\tAmount of credit: ");
            double creditAmount;
            if (!double.TryParse(Console.ReadLine(), out creditAmount))
            {
                goto creditAmountChecking;
            }
        creditTermChecking:
            System.Console.Write("\t\t\t\tTerm (1 Month, 2 Months.....)\nPlease enter number of months:");
            int creditTerm;
            if (!int.TryParse(Console.ReadLine(), out creditTerm))
            {
                goto creditTermChecking;
            }
        creditGoalChecking:
            System.Console.WriteLine("\t\t\t\tChoose goal for getting credit:\n\t\t\t\t1.Home Equipment\n\t\t\t\t2.Fix\n\t\t\t\t3.Phones\n\t\t\t\t4.Other");
            System.Console.Write("\t\t\t\tPlease type reference number: ");
            int clientGoalChoice; string clientGoal;
            if (!int.TryParse(Console.ReadLine(), out clientGoalChoice))
            {
                System.Console.WriteLine("\t\t\t\tError: Try Again");
                goto creditGoalChecking;
            }
            else if (clientGoalChoice < 1 || clientGoalChoice > 4)
            {
                System.Console.WriteLine("\t\t\t\tError: Try Again");
                goto creditGoalChecking;
            }
            else
            {
                switch (clientGoalChoice)
                {
                    case 1: clientGoal = "Home"; break;
                    case 3: clientGoal = "Fix"; break;
                    case 4: clientGoal = "Others"; break;
                    default: clientGoal = "Phone"; break;
                }
            }
        creditSalaryChecking:
            System.Console.Write("\t\t\t\tYour Salary:");
            double creditSalary;
            if (!double.TryParse(Console.ReadLine(), out creditSalary))
            {
                goto creditSalaryChecking;
            }
            System.Console.WriteLine(creditSalary);

            Applications application = new Applications(clientID, creditAmount, creditTerm, clientGoal, creditSalary);
            application.RegisterForCredit();
            goto clientFunctions;

        clientApplicationView:
            Applications applications = new Applications(clientID);
            applications.creditStateView();
            goto clientFunctions;

        userRegister:
            System.Console.WriteLine("\t\t\t\tPlease fill following information carefully!!!");
            System.Console.Write("\t\t\t\tEnter your Firstname: ");
            string tempFirstname = Console.ReadLine();
            System.Console.Write("\t\t\t\tEnter your Lastname: ");
            string tempSecondname = Console.ReadLine();
            System.Console.Write("\t\t\t\tEnter your Middlename: ");
            string tempMiddlename = Console.ReadLine();

        loginPasswordChecking:
            System.Console.Write("\t\t\t\tEnter your new login: (Use phone numbers) ");
            string login = Console.ReadLine();
            System.Console.Write("\t\t\t\tEnter your new password: ");
            string password = Console.ReadLine();
            Logins logins = new Logins(login, password);
            var result = logins.Register();
            if (result == -1)
            {
                goto loginPasswordChecking;
            }
            else if (result == -2)
            {
                goto userLogin;
            }

        //! if time available input birthyear)))) Note for future 
        ageIntEntering:
            System.Console.Write("\t\t\t\tEnter your birthday in format(dd/MM/yyyy): ");
            string birthday = Console.ReadLine();
            DateTime birthdayDate;
            int tempAge;
            if (DateTime.TryParse(birthday, out birthdayDate))
            {
                tempAge = GetAge(birthdayDate);
            }
            else
            {
                System.Console.WriteLine("\t\t\t\tError: try again");
                goto ageIntEntering;
            }

        //! Check whether matches or not
        tempGenderChecking:
            System.Console.Write("\t\t\t\tEnter your Gender: M/F");
            string tempGender = Console.ReadLine();
            if (tempGender == "M")
            {
                tempGender = "Male";
            }
            else if (tempGender == "F")
            {
                tempGender = "Female";
            }
            else
            {
                System.Console.WriteLine("\t\t\t\tError try again ... ");
                goto tempGenderChecking;
            }

        //! Citizenship Checking
        tempCitizenshipChecking:
            System.Console.Write("\t\t\t\tEnter your citizenship: Tajikistan/Other");
            string tempCitizenship = Console.ReadLine();
            if (tempCitizenship.ToLower() == "tajikistan")
            {
                tempCitizenship = "Tajikistan";
            }
            else if (tempCitizenship.ToLower() == "other")
            {
                tempCitizenship = "Other";
            }
            else
            {
                System.Console.WriteLine("Error try again ... ");
                goto tempCitizenshipChecking;
            }
        //! FamilyState Checking
        tempFamilyChecking:
            bool isInFamilyArray = false;
            System.Console.Write("\t\t\t\tEnter your family state....\n\t\t\t\t1. Married\n\t\t\t\t2. Divorced\n\t\t\t\t3. Single\n\t\t\t\t4. Widowed\n\t\t\t\t4Type family state: ");
            string tempFamily = Console.ReadLine();
            foreach (var item in familyArray)
            {
                if (item.ToLower() == tempFamily.ToLower())
                {
                    isInFamilyArray = true;
                    tempFamily = item;
                    break;
                }
            }
            if (!isInFamilyArray)
            {
                goto tempFamilyChecking;
            }
            System.Console.Write("Your City: ");
            string tempCity = Console.ReadLine();
            System.Console.Write("Your District (ex: Sino): ");
            string tempDistrict = Console.ReadLine();
            System.Console.Write("Your Street(ex: Shevchenko 23): ");
            string tempStreet = Console.ReadLine();
            System.Console.Write("Your apartment/house number: ");
            string tempHouse = Console.ReadLine();
        ClientAdding:
            Client client = new Client(tempFirstname, tempSecondname, tempMiddlename, tempGender, tempAge, tempCitizenship, tempFamily, tempCity, tempDistrict, tempStreet, tempHouse);
            bool isAdded = client.AddClient();
            if (!isAdded)
            {
                goto ClientAdding;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("\t\t\t\tCongratulations..... You have done it\n Now you may Login");
            Console.ForegroundColor = ConsoleColor.White;
            goto userLogin;


        /// Admin Login
        adminLogin:
            System.Console.WriteLine(@"                     Admin Panel:");
            System.Console.Write("Admin Login: ");
            string tempAdminLogin = Console.ReadLine();
            System.Console.Write("Admin Password: ");
            string tempAdminPassword = Console.ReadLine();
            Admins admins = new Admins(tempAdminLogin, tempAdminPassword);
            bool isExist = admins.Login();
            if ((tempAdminLogin == adminLogin && tempAdminPassword == adminPassword)
                        || isExist == true)
            {
                goto adminDashboard;
            }
            else
            {
                System.Console.WriteLine("Error: not found ");
                goto adminLogin;
            }

        /// Second Stage
        adminDashboard:
            System.Console.Write(@"Admin Dashboard:
                1. Info about Client by ID
                2. Show All Clients with ID
                3. Register Admin
                4. Exit
                Type reference number: ");
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                if (choice == 1 || choice == 2 || choice == 3 || choice == 4)
                {
                    goto adminFuntions;
                }
                else
                {
                    System.Console.WriteLine("Incorrect choice");
                    goto adminDashboard;
                }
            }
            else
            {
                System.Console.WriteLine("Incorrect choice");
                goto adminDashboard;
            }

        adminFuntions:
            Client client1 = new Client();
            switch (choice)
            {
                case 1:
                    {
                        //! Choices Like CreditHistory, PaymentHistory, Client Info
                        System.Console.Write("Enter client ID: ");
                        int userClientId = int.Parse(Console.ReadLine());
                        client1 = new Client(userClientId);
                        client1.showByClientId(userClientId);
                        goto adminDashboard;

                    }
                    break;
                case 2:
                    {
                        //! Choices Like Client Info
                        System.Console.WriteLine();
                        client1.showAllClients();
                        goto adminDashboard;

                    }
                    break;
                case 3:
                    {
                        System.Console.WriteLine("Enter new Login and Password for new Admin");
                        System.Console.Write("Login: ");
                        tempAdminLogin = Console.ReadLine();
                        System.Console.Write("Password: ");
                        tempAdminPassword = Console.ReadLine();
                        Admins admins1 = new Admins(tempAdminLogin, tempAdminPassword);
                        bool isAdminRegistered = admins1.Register();
                        if (isAdminRegistered)
                        {
                            System.Console.WriteLine("Admin Registered");
                            goto adminDashboard;
                        }
                        else
                        {
                            System.Console.WriteLine("Error while Registering");
                            goto adminFuntions;
                        }

                    }
                    break;
                case 4:
                    {
                        goto startCode;
                    }
                    break;
            }


        }
        public static int GetAge(DateTime birthDate)
        {
            DateTime n = DateTime.Now; // To avoid a race condition around midnight
            int age = n.Year - birthDate.Year;

            if (n.Month < birthDate.Month || (n.Month == birthDate.Month && n.Day < birthDate.Day))
                age--;

            return age;
        }
    }
    }

