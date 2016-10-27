using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodAutomat
{
    class Program
    {
        public static string some_string;
        public static int money;
        static void Main(string[] args)
        {
            Stash ob = new Stash();// object of Stash

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You have 150 rubles. Enter the coin of 1, 2, 5, 10 rubles." + //instructions
                              "To select from the \n menu write \"start\":");
            Console.WriteLine("'cake' - 50 rubles. In stock {0} thing. 'cookies' - 10 rubles. In stock {1} things." +
                              " 'wafers' - 30 rubles. In stock {2} things. ", ob.Cake.Number, ob.Cookies.Number, ob.Wafers.Number);
            Console.ForegroundColor = ConsoleColor.White;
            while (true)
            {
                some_string = Console.ReadLine();
                some_string = some_string.ToLower();// error of  uppercase removed
                if (some_string == "start") // if 'start' then you must choose some food 
                    break;

                try { money = Convert.ToInt32(some_string); } // format exeption
                catch
                {
                    Console.WriteLine("Invalid input format. Enter the number");
                    continue;
                }
                if ((money == 1 | money == 2 | money == 5 | money == 10) & (ob.Cash + money <= 150)) // if you give automat coins of right nominal then go
                {
                    ob.Get_money(money); // automat get money
                    Console.WriteLine("Cash " + ob.Cash);
                }
                else
                    Console.WriteLine("Invalid denomination of the coin, or you have not enough money");
            }

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Select some food from the menu. Enter a name which written in single quotes " +
                                  "\n'cake' - 50 rubles. In stock {0} thing. 'cookies' - 10 rubles. In stock {1} things." +
                                  "\n 'wafers' - 30 rubles." +
                                  " In stock {2} things. ", ob.Cake.Number, ob.Cookies.Number, ob.Wafers.Number);
                Console.WriteLine("Enter the name, or you can take short change." +
                                  " 'short_change' is the command for short change. Cash is {0}", ob.Cash);
                Console.ForegroundColor = ConsoleColor.White;
                some_string = Console.ReadLine();
                some_string = some_string.ToLower();

                foreach (var meal in ob.meals) // foreach in list of meals
                {
                    if (some_string == meal.Name)
                        if (ob.Cash >= meal.Price)
                        {
                            if (meal.Number != 0)
                            {
                                ob.instruction(some_string);
                                ob.Give_money(meal.Price);// minus money which you pay
                                meal.Number--;
                            }
                            else
                                ob.not_enough_number(some_string); // if not enough number of thing
                        }
                        else
                            ob.not_enough_money(); // if not enough money
                }

                if (some_string == "short_change")
                {
                    Console.WriteLine("Your short change: {0} 10-rubles coin, {1} - 5 rubles coin, {2} - 2 rubles coin, {3}" +
                        "- 1 rubles coin. In total {4}", ob.Cash / 10, ob.Cash % 10 / 5, ob.Cash % 10 % 5 / 2, ob.Cash % 10 % 5 % 2, ob.Cash);
                    Console.ReadKey();
                    break;

                }
                if (some_string != "short_change" & some_string != ob.Cake.Name & some_string != ob.Cookies.Name & some_string != ob.Wafers.Name)
                    Console.WriteLine("The name which you entered does not exist.");
            }
        }
    }
}




class Meal
{

    public Meal(string Name, int Price, int Number) //constructor for meal
    {
        this.name = Name;
        this.price = Price;
        this.number = Number;
    }
    private int price;
    public int Price //incapculate price
    {
        get { return this.price; }

    }
    private int number;

    public int Number
    {
        get { return number; }
        set
        {
            if (value >= 0)
                number = value;
            else
                throw new Exception(String.Format("Unfortunately {0} ended. Choose something else, or take short change", Name));

        }
    }
    private string name;
    public string Name
    {
        get { return this.name; }
    }
}



class Stash : instruction_of_stash
{
    public Meal Cake = new Meal("cake", 50, 4);
    public Meal Cookies = new Meal("cookies", 10, 3);
    public Meal Wafers = new Meal("wafers", 30, 10);

    public List<Meal> meals = new List<Meal>(); // new list of meal
    private int cash;

    public Stash()
    {
        meals.Add(Cake); // add obj to list of meal
        meals.Add(Cookies);
        meals.Add(Wafers);
    }

    public int Cash //incapsulate cash
    {
        get { return cash; }
        set
        {
            if (value >= cash)
                cash -= value;
            else
                throw new Exception("Cash on short change is not enough");
        }
    }

    public void Get_money(int get_money) // method for get money
    {
        cash += get_money;

    }

    public void Give_money(int give_money) //method for give money from cash on some thing which user choose
    {
        cash -= give_money;
    }
}

class instruction_of_stash // instruction of automat
{
    public void instruction(string some_string)
    {
        Console.WriteLine("Here are your {0}.You can buy something else or take short_change", some_string);
    }
    public void not_enough_money()
    {
        Console.WriteLine("In the automat is not enough money to buy");
    }
    public void not_enough_number(string some_string)
    {
        Console.WriteLine("Unfortunately {0} ended. Choose something else, or take 'short_change'", some_string);
    }
}
