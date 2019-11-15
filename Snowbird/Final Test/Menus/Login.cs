﻿using System;
using System.Text.RegularExpressions;
using System.Security;
using System.Collections.Generic;
using System.Globalization;

namespace Final_Test.Menus {
    public static class Login {

        public static void Run() {

            while(true) {
                string identifier = "", password = "", type = "1";

                Console.Clear();
                Console.Clear();
                Console.Write("\n\t\t\t\t\t\tWelcome to "); /**/ Snowbird.Write("Snowbird Wallet", ConsoleColor.Black, ConsoleColor.White);
                Console.WriteLine("!"); /**/ Snowbird.Write("\n\n\tUsername/email: ", ConsoleColor.Blue);
                identifier = Console.ReadLine();

                if (identifier.Length >= 6) {

                    string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
                    if (Regex.IsMatch(identifier, pattern)) type = "0";

                    Snowbird.Write("\n\tPassword:       ", ConsoleColor.DarkRed);
                    password = Snowbird.GetHashedPass();

                    if (type == "0") {
                        string s = "FROM users WHERE email='" + identifier + "' AND password='" + password + "';";
                        if (Snowbird.db.Count("SELECT COUNT(*) " + s) == 1) {
                            
                            Console.WriteLine("Welcome {0}! ({1})", Snowbird.db.Select("SELECT username " + s, 1, new string[1]{"username"})[0][0], Snowbird.db.Select("SELECT id " + s, 1, new string[1] { "id" })[0][0]);
                            break;
                        }
                    } else {
                        string s = "FROM users WHERE username='" + identifier + "' AND password='" + password + "';";
                        if (Snowbird.db.Count("SELECT COUNT(*) " + s) == 1) {

                            Snowbird.user = new User(identifier, GetUserId(identifier));

                            string getWalletCount = "SELECT COUNT(*) FROM wallets WHERE user_id='" + Snowbird.user.UserId + "';";

                            if( Snowbird.db.Count(getWalletCount) == 0 )
                                AddWallet(Snowbird.user.UserId, 0);
                            
                            Snowbird.user.WalletCount = Snowbird.db.Count(getWalletCount);   
                            Snowbird.user.Wallets = GetWallets(Snowbird.user.UserId);

                            Snowbird.user.TransactionCount = Snowbird.db.Count("SELECT COUNT(*) FROM transactions t RIGHT JOIN wallets w ON t.wallet_id = w.id WHERE w.user_id='" + Snowbird.user.UserId + "';");
                            Snowbird.user.Transactions = GetTransactions(Snowbird.user.UserId);

                            Snowbird.Login = false;

                            break;

                        }
                    }
                }
            }

        }

        /// <summary>
        /// Get the User ID for a given Username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>9 digit ID</returns>
        public static string GetUserId(string username) {
            return Snowbird.db.Select("SELECT id FROM users WHERE username='" + username + "';", 1, new string[1] { "id" })[0][0];
        }

        /// <summary>
        /// Grabs all the information for all of the User's wallets
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>A nested string type List of all the wallet datas</returns>
        public static List<string>[] GetWallets(string userId) {
            return Snowbird.db.Select("SELECT * FROM wallets WHERE user_id='" + userId + "' ORDER BY type;", 9, new string[9] { "id", "user_id", "type", "amount", "currency", "account_name", "account_number", "description", "created_at" });
        }
        /// <summary>
        /// Grabs all the information for all of the User's transactions
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>A nested string type List of all the transactions datas</returns>
        public static List<string>[] GetTransactions(string userId) {
            return Snowbird.db.Select("SELECT * FROM transactions t RIGHT JOIN wallets w ON t.wallet_id=w.id WHERE user_id='" + userId + "' ORDER BY t.id;", 8, new string[8] { "id", "wallet_id", "type", "amount", "fromWalletId", "toWalletId", "description", "created_at" });
        }

        /// <summary>
        /// Creates a Wallet for the user
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="type">Wallet type</param>
        public static void AddWallet(string userId, int type) {

            if (type == 0) {
                Console.Clear();
                Console.WriteLine("Create your first wallets\n");
                double amount = 0.0;
                string currency = "huf";

                // Amount
                Console.Write("Amount: ");
                amount = double.Parse(Snowbird.GetNumbers(), CultureInfo.InvariantCulture.NumberFormat);
                Snowbird.WriteLine(amount + "", ConsoleColor.Blue);

                Console.WriteLine("Currency:");
                Console.Write("  ("); /**/ Snowbird.Write("1", ConsoleColor.Yellow); /**/ Console.Write(") "); Snowbird.WriteLine("HUF", ConsoleColor.Green, ConsoleColor.Gray);
                Console.Write("  ("); /**/ Snowbird.Write("2", ConsoleColor.Yellow); /**/ Console.Write(") "); Snowbird.WriteLine("EUR", ConsoleColor.Green, ConsoleColor.Gray);
                Console.Write("  ("); /**/ Snowbird.Write("3", ConsoleColor.Yellow); /**/ Console.Write(") "); Snowbird.WriteLine("USD", ConsoleColor.Green, ConsoleColor.Gray);
                switch (Snowbird.KeyPressed()) {
                    case "1":
                        currency = "huf";
                        break;
                    case "2":
                        currency = "eur";
                        break;
                    case "3":
                        currency = "usd";
                        break;
                    default: break;
                }

                DateTime myDateTime = DateTime.Now;
                string dateTime = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");

                string query = $"INSERT INTO `wallets` (`id`, `user_id`, `type`, `amount`, `currency`, `account_name`, `account_number`, `description`, `created_at`) " +
                               $"VALUES (NULL, @u, @t, , '" + currency + "', NULL, NULL, NULL, '" + dateTime + "');";
                //               VALUES (NULL, '" + userId + "', '0', '" + amount + "', '" + currency + "', NULL, NULL, NULL, '" + dateTime + "');";
                Snowbird.db.NonQuery(query);
            }

        }

    }
}