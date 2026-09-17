using System;
class Program
{
    static void Main(string[] args)
    {
        int num1, num2;
        Console.WriteLine("----- Largest of Two Numbers -----");
        Console.Write("Enter First Number: ");
        num1 = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter Second Number: ");
        num2 = Convert.ToInt32(Console.ReadLine());
        if (num1 > num2)
        {
            Console.WriteLine(num1 + " is the Largest Number.");
        }
        else if (num2 > num1)
        {
            Console.WriteLine(num2 + " is the Largest Number.");
        }
        else
        {
            Console.WriteLine("Both numbers are Equal.");
        }
        Console.WriteLine("Program Executed Successfully.");
    }
}

