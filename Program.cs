// Chandler Wixom, lab 11 ATM machine, 4/16/2025


// loadBankCustomers() saveBankCustomers() validateUser() checkBalance() withdrawMoney() depositMoney() quickWithdraw(int Amount) displayTranactions()


Dictionary<string, List<string>> users = LoadBankCustomers();
Console.Clear();

(bool access, string name) = validateUser(users);

if (access)
{
Console.WriteLine(access);
Console.WriteLine(name);
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

    } while (true);
}