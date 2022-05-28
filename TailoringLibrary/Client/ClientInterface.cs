using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TailoringLibrary.Client
{
    public class ClientInterface
    {
        public static string FIO()
        {
            Console.WriteLine("\nВведите вашу фамилию: ");
            string surname = Console.ReadLine();
            Console.WriteLine("\nВведите ваше имя: ");
            string name = Console.ReadLine();
            Console.WriteLine("\nВведите ваше отчество: ");
            string otchestvo = Console.ReadLine();
            string fio = "{" + surname + "," + name + "," + otchestvo + "}";

            return fio;
        }
        public static string MenuOfOrder()
        {
            string orderMenu;
            Console.WriteLine("Хотите закончить? 1 - да, 2 - нет");
            string exit = Console.ReadLine();
            if (exit == "1")
            {
                orderMenu = "<End>";
                return orderMenu;
            }
            else
            {
                Console.WriteLine("\n\n................................................................................\n");
                Console.WriteLine("Выберите изделие и напишите его код (код - изделие):\n");
                Console.WriteLine("\t301 - пальто");
                Console.WriteLine("\t302 - кардиган");
                Console.WriteLine("\t303 - жакет");
                string izdelie = Console.ReadLine();
                if (izdelie != "301" && izdelie != "302" && izdelie != "303")
                {
                    Console.WriteLine("Неверный код");
                    MenuOfOrder();
                }

                Console.WriteLine("\nВыберите материал и напишите его код (код - материал): \n");
                Console.WriteLine("\t101 - кашемир");
                Console.WriteLine("\t102 - буклет");
                Console.WriteLine("\t103 - твид");
                string material = Console.ReadLine();
                if (material != "101" && material != "102" && material != "103")
                {
                    Console.WriteLine("Неверный код");
                    MenuOfOrder();
                }

                Console.WriteLine("\nВыберите вид материала подкладки и напишите его код (код - материал подкладки): \n");
                Console.WriteLine("\t201 - шелк натуральный");
                Console.WriteLine("\t202 - вискоза");
                Console.WriteLine("\t203 - полиэстер");
                string podkladka = Console.ReadLine();
                if (podkladka != "201" && podkladka != "202" && podkladka != "203")
                {
                    Console.WriteLine("Неверный код");
                    MenuOfOrder();
                }

                double sum = Summa(izdelie, material, podkladka);
                string fio = FIO();

                orderMenu = fio + ",{" + izdelie + "," + material + "," + podkladka + "},{" + sum + "},{" + DateTime.Now.Day + "." + DateTime.Now.Month
                + "." + DateTime.Now.Year + "}";

                return orderMenu;
            } 
        }
        public static double Summa(string izdelie, string material, string podkladka)
        {
            double summa = 0;
            double metreMaterial = 0;
            double metrePodkladka = 0;
            double summaAll = 0;
            switch (izdelie)
            {
                case "301":
                    {
                        summa = summa + 1500;
                        metreMaterial = metreMaterial + 2.5;
                        metrePodkladka = metrePodkladka + 2;
                        break;
                    }
                case "302":
                    {
                        summa = summa + 1000;
                        metreMaterial = metreMaterial + 2;
                        metrePodkladka = metrePodkladka + 1.5;
                        break;
                    }
                case "303":
                    {
                        summa = summa + 850;
                        metreMaterial = metreMaterial + 1.5;
                        metrePodkladka = metrePodkladka + 1.5;
                        break;
                    }
            }
            switch (material)
            {
                case "101":
                    {
                        summaAll = summa + metreMaterial * 600;
                        break;
                    }
                case "102":
                    {
                        summaAll = summa + metreMaterial * 350;
                        break;
                    }
                case "103":
                    {
                        summaAll = summa + metreMaterial * 450;
                        break;
                    }
            }
            switch (podkladka)
            {
                case "201":
                    {
                        summaAll = summaAll + metrePodkladka * 350;
                        break;
                    }
                case "202":
                    {
                        summaAll = summaAll + metrePodkladka * 250;
                        break;
                    }
                case "203":
                    {
                        summaAll = summaAll + metrePodkladka * 200;
                        break;
                    }
            }
            return summaAll;
        }
    }
}
