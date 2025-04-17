// Chandler Wixom, lab 11 ATM machine, 4/16/2025


// loadBankCustomers() saveBankCustomers() validateUser() checkBalance() withdrawMoney() depositMoney() quickWithdraw(int Amount) displayTranactions()


using System.ComponentModel.Design;
using System.Reflection;
using System.Transactions;

Dictionary<string, List<string>> users = LoadBankCustomers();
Console.Clear();

(bool access, string name) = validateUser(users);

if (access)
{
    Menu(name,users[name]);

}
else
{
    Console.WriteLine("Error please try again later -To many failed attempts");
}




//Loads Users from text file
 Dictionary<string, List<string>> LoadBankCustomers()
{
    List<string> tempReadLines = new List<string>(File.ReadLines("../../../bank.txt"));

    
    Dictionary <string, List<string>> Users = new Dictionary<string, List<string>>();

    foreach (string line in tempReadLines)
    {
        List<string> trim = new List<string> ();
        string[] temp = line.Split(",");
        foreach (string word in temp)
        {
        trim.Add(word.Trim());
        }
        
        Users.Add(trim[0], new List<string>(trim));
    }

    return Users;
}

// login system
(bool,string) validateUser(Dictionary<string, List<string>> users)
{
    do
    {
        Console.Clear();
    Console.Write("What is your Username :");
    string? nametry = Console.ReadLine();

    if (users.ContainsKey(nametry))
    {
        int trys = 0;
        while (trys < 3)
        {
        Console.Write("What is your Pin :");
        
        Int32.TryParse(Console.ReadLine() ,out int pintry);

        int pin = Convert.ToInt32(users[nametry][1]);
        if (pintry == pin)
        {
            return (true, nametry);
        }
        else 
        {
            trys++;
            Console.WriteLine("Incorrect Pin");
        }
        }
        
        return (false, "BLOCKED");
    }
    Console.WriteLine("Hmmm?");
    Console.ReadKey(true);

    } while (true);
}


// displays and runs menu
List<string> Menu(string name, List<string> userInfo)
{
    int balance = Convert.ToInt32(userInfo[2]);


    Console.WriteLine($"Hello {name}");
    Console.WriteLine(@"
1. Check Balance
2. Withdraw
3. Deposit
4. Display last 5 transactions
5. Quick Withdraw $40
6. Quick Withdraw $100
7. End current session");
char input;

do
{
    input = Console.ReadKey(true).KeyChar;
    
    switch (input)
    {
        case '1' : Console.WriteLine($"Balance is {balance}");
        break;

        case '2' : balance = Withdrawl(balance, -1);
        break;

        case '3' : ;
        break;

        case '4' : ;
        break;

        case '5' : balance = Withdrawl(balance, 40);
        break;

        case '6' : ;
        break;

    }

   




} while (input != '7');


// temp update user stuff and return updated
    return userInfo;
}

// withdrawl 

int Withdrawl(int current, int request)
{
    if (request == -1)
        {
            Console.WriteLine("How much would you like to withdraw");
            bool passed = false;
            do
            {
            passed = Int32.TryParse(Console.ReadLine() , out int ask);
            if (passed)
            {
            request = ask;
            }
            }
            while (!passed);
            
        };
    if (request <1)
    {
        Console.WriteLine("error you can't withdraw a negative amount");
        return current;
    }




    if (request > current)
    {
        Console.WriteLine($"error you can't withdraw more ({request}) then you have ({current})");
        return current;
    }
    else
    {
        current = current - request;
        Console.WriteLine($"You withdrew {request}");
        return current;
    }

}