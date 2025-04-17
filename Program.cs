// Chandler Wixom, lab 11 ATM machine, 4/16/2025


// loadBankCustomers() saveBankCustomers() validateUser() checkBalance() withdrawMoney() depositMoney() quickWithdraw(int Amount) displayTranactions()

//Loading Users



Dictionary<string, List<string>> users = LoadBankCustomers();

Console.WriteLine(users["jallen"][2]);


//Loads Users from text file
 Dictionary<string, List<string>> LoadBankCustomers()
{
    List<string> tempReadLines = new List<string>(File.ReadLines("../../../bank.txt"));

    
    Dictionary <string, List<string>> Users = new Dictionary<string, List<string>>();

    foreach (string line in tempReadLines)
    {
        string[] temp = line.Split(",");
        Users.Add(temp[0], new List<string>(temp));
    }

    return Users;
}