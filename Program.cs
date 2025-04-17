// Chandler Wixom, lab 11 ATM machine, 4/16/2025


// loadBankCustomers() saveBankCustomers() validateUser() checkBalance() withdrawMoney() depositMoney() quickWithdraw(int Amount) displayTranactions()


using System.ComponentModel.Design;
using System.Reflection;
using System.Reflection.Metadata;
using System.Transactions;

Dictionary<string, List<string>> users = LoadBankCustomers();
Console.Clear();

(bool access, string name) = validateUser(users);

if (access)
{

    List<string> userinfo = Menu(name, users[name]);
    Saving(users, userinfo);



}
else
{
    Console.WriteLine("Error please try again later -To many failed attempts");
}


// saving from user

static void Saving(Dictionary<string, List<string>> users, List<string> userinfo)
{
    List <string> finalList = new List<string>();
   users[userinfo[0]] = userinfo;
    foreach (string key in users.Keys)
    {
      string temp = String.Join(',', users[key]);
        finalList.Add(temp);
    }
    File.WriteAllLines("../../../bank.txt", finalList);
}


//Loads Users from text file
Dictionary<string, List<string>> LoadBankCustomers()
{
    List<string> tempReadLines = new List<string>(File.ReadLines("../../../bank.txt"));


    Dictionary<string, List<string>> Users = new Dictionary<string, List<string>>();

    foreach (string line in tempReadLines)
    {
        List<string> trim = new List<string>();
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
(bool, string) validateUser(Dictionary<string, List<string>> users)
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

                Int32.TryParse(Console.ReadLine(), out int pintry);

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
    (int balance, string info) transactions = (Convert.ToInt32(userInfo[2]), "");
    Queue<string> history = new Queue<string>();
    char input;
    WriteMenu();
    do
    {




        input = Console.ReadKey(true).KeyChar;

        switch (input)
        {
            case '1':
                WriteMenu();
                Console.WriteLine($"Balance is {transactions.balance}");
                break;

            case '2':
                WriteMenu();
                transactions = Withdrawl(transactions.balance, -1);
                history.Enqueue(transactions.info);
                break;

            case '3':
                WriteMenu();
                transactions = Deposit(transactions.balance);
                history.Enqueue(transactions.info);
                break;

            case '4':
                WriteMenu();
                DisplayTransactions(history);
                history.Enqueue(transactions.info);
                break;

            case '5':
                WriteMenu();
                transactions = Withdrawl(transactions.balance, 40);
                history.Enqueue(transactions.info);
                break;

            case '6':
                WriteMenu();
                transactions = Withdrawl(transactions.balance, 100);
                history.Enqueue(transactions.info);
                break;

        }

        if (history.Count > 5)
        {
            history.Dequeue();
        }



    } while (input != '7');

    int finalBal = 0;
    finalBal = transactions.balance;
    List<string> newUserInfo = new List<string>(); // iduno a better way to do this AAAAAAAAAAA !?!?!?
    newUserInfo.Add(userInfo[0]);
    newUserInfo.Add(userInfo[1]);
    newUserInfo.Add(finalBal.ToString());

   
    // temp update user stuff and return updated
    return newUserInfo;
}

// display transactions in proper order

static void DisplayTransactions(Queue<string> strings)
{
    List<string> history = new List<string>(strings);

    history.Reverse();
    Console.WriteLine("Transaction History");
    foreach (string line in history)
    {
        Console.WriteLine(line);
    }
}



// withdrawl 

static (int, string) Withdrawl(int current, int request)
{

    if (request == -1)
    {
        Console.WriteLine($"How much would you like to withdraw || Current Balance {current}");
        bool passed = false;
        do
        {
            passed = Int32.TryParse(Console.ReadLine(), out int ask);
            if (passed)
            {
                request = ask;
            }
        }
        while (!passed);

    }
    ;
    if (request < 1)
    {

        Console.WriteLine("error you can't withdraw a negative amount");
        return (current, $"{DateTime.Now} Transaction Error");
    }




    if (request > current)
    {

        Console.WriteLine($"error you can't withdraw more ({request}) then you have ({current})");
        return (current, $"{DateTime.Now} Transaction Error");
    }
    else
    {

        int old = current;
        current = current - request;
        Console.WriteLine($"You withdrew {request} || New Balance {current}");
        return (current, $"{DateTime.Now} || {old} - {request} || Current {current}");
    }

}


// Deposit 

static (int, string) Deposit(int current)
{

    int request = 0;
    Console.WriteLine($"How much would you like to deposit || Current Balance {current}");
    bool passed = false;
    do
    {
        passed = Int32.TryParse(Console.ReadLine(), out int ask);
        if (passed)
        {
            request = ask;
        }
    }
    while (!passed);

    ;
    if (request < 1)
    {

        Console.WriteLine("error you can't deposit a negative amount");
        return (current, $"{DateTime.Now} Transaction Error");
    }



    int old = current;
    current = current + request;
    Console.WriteLine($"You deposited {request} || New Balance {current}");
    return (current, $"{DateTime.Now} || {old} + {request} || Current {current}");


}

void WriteMenu()
{
    Console.Clear();
    Console.WriteLine($"Hello {name}");
    Console.WriteLine(@"
1. Check Balance
2. Withdraw
3. Deposit
4. Display last 5 transactions
5. Quick Withdraw $40
6. Quick Withdraw $100
7. End current session");
}