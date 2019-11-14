﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Test.Menus
{
    public static class addWallets      // by Patrik
    {
        public static void Run()
        {
            Console.Clear();
            Console.Write("\n\t\t\t\t\t\tWelcome "); /**/ Snowbird.Write(Snowbird.user.Username, ConsoleColor.Blue);
            Console.WriteLine("Create new wallet\n");
            string query;
            // Type
            int type = 0;
            Console.Write("  ("); /**/ Snowbird.Write("1", ConsoleColor.Yellow); /**/ Console.Write(") "); Snowbird.WriteLine("Wallet", ConsoleColor.Red, ConsoleColor.Black);
            Console.Write("  ("); /**/ Snowbird.Write("2", ConsoleColor.Yellow); /**/ Console.Write(") "); Snowbird.WriteLine("Account", ConsoleColor.Red, ConsoleColor.Black);
            switch (Snowbird.KeyPressed())
            {
                case "1":
                    type = 0;
                    break;
                case "2":
                    type = 1;
                    break;
                default:
                    break;
            }

            float amount = 0;
            string currency = "huf";

            // Amount
            Console.Write("Amount: ");
            amount = Convert.ToSingle(Snowbird.GetNumbers());

            // Currency
            Console.WriteLine("Currency:");
            Console.Write("  ("); /**/ Snowbird.Write("1", ConsoleColor.Yellow); /**/ Console.Write(") "); Snowbird.WriteLine("HUF", ConsoleColor.Green, ConsoleColor.Gray);
            Console.Write("  ("); /**/ Snowbird.Write("2", ConsoleColor.Yellow); /**/ Console.Write(") "); Snowbird.WriteLine("EUR", ConsoleColor.Green, ConsoleColor.Gray);
            Console.Write("  ("); /**/ Snowbird.Write("3", ConsoleColor.Yellow); /**/ Console.Write(") "); Snowbird.WriteLine("USD", ConsoleColor.Green, ConsoleColor.Gray);
            switch (Snowbird.KeyPressed())
            {
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

            // Date
            DateTime myDateTime = DateTime.Now;
            string dateTime = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");

            Random r = new Random();
            if(type == 1)
            {
                string account_name;
                int account_number;
                Console.Write("Account Name: ");
                account_name = Console.ReadLine();

                // ezt nemtudom hogy kellene, megirom így egyenlőre
                account_number= r.Next(1, 1000);

                query = "INSERT INTO `wallets` (`id`, `user_id`, `type`, `amount`, `currency`, `account_name`, `account_number`, `description`, `created_at`) VALUES (NULL, '" + Login.GetUserId(Snowbird.user.Username) + "', '" + type + "', '" + amount + "', '" + currency + "', '" + account_name + "', '" + account_number + "', NULL, '" + dateTime + "');";
                Snowbird.db.NonQuery(query);
            }

            query = "INSERT INTO `wallets` (`id`, `user_id`, `type`, `amount`, `currency`, `account_name`, `account_number`, `description`, `created_at`) VALUES (NULL, '" + Login.GetUserId(Snowbird.user.Username) + "', '0', '" + amount + "', '" + currency + "', NULL, NULL, NULL, '" + dateTime + "');";
            Snowbird.db.NonQuery(query);



        }

    }
}
