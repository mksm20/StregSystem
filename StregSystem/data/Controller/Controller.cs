using StregSystem.data.models;
using StregSystem.data.views;
using System;
using System.Collections.Generic;

namespace StregSystem.data.Controller
{
    public class Controller
    {
        public Controller(IStregsystem stregSystem, IStregsystemCLI ui)
        {
            _stregsystem = stregSystem;
            _ui = ui;
            _ui.CommandParse += OnCommandParse;
            _stregsystem.LowBalance += OnLowBalance;
            initiateController();
        }


        private Dictionary<string, Delegate> adminCommands = new Dictionary<string, Delegate>();
        private IStregsystem _stregsystem;
        private IStregsystemCLI _ui;
        private delegate void mydel(Type val);

        private void initiateController()
        {
            adminCommands.Add(":quit", new Action(_ui.Close));
            //adminCommands.Add(":activate", new Action<int>(_stregsystem.ActivateProduct));
            //adminCommands.Add(":deactivate", new Action<int>(_stregsystem.DeactivateProduct));
            //adminCommands.Add(":crediton", new Action<int>(_stregsystem.CreditOnOff));
            //adminCommands.Add(":creditoff", new Action<int>(_stregsystem.CreditOnOff));
            //adminCommands.Add(":addcredits", new Func<string, double,Transaction>(_stregsystem.AddCreditsToAccount));
        }
        public void OnLowBalance(object source, UserArgs e)
        {
            _ui.DisplayLowCredit($"{e.user.UserName} your balance is {e.user.Balance}, please consider refilling");
        }
        public void OnCommandParse(object source, CommandArgs e)
        {
            CommandParser(e.Command);
        }
        public void CommandParser(string command)
        {
            string[] commandArr = command.Split(" ");
            if (commandArr[0].StartsWith(":"))
            {
                try
                {
                    foreach(var item in adminCommands)
                    {
                        if(item.Key == commandArr[0])
                        {
                            item.Value.DynamicInvoke();
                        }
                    }
                    
                    switch (commandArr[0])
                    {
                        //case ":quit":
                        //case ":q":
                        //    adminCommands. (commandArr[0]);
                        //    return;
                        case ":activate":
                            _stregsystem.ActivateProduct(int.Parse(commandArr[1]));
                            return;
                        case ":deactivate":
                            _stregsystem.DeactivateProduct(int.Parse(commandArr[1]));
                            return;
                        case ":crediton":
                        case ":credioff":
                            _stregsystem.CreditOnOff(int.Parse(commandArr[1]));
                            return;
                        case ":addcredits":
                            _stregsystem.AddCreditsToAccount(commandArr[1], double.Parse(commandArr[2]));
                            return;
                        case ":addproduct":
                            List<string> product = new List<string>();
                            for(int i = 1; i < commandArr.Length; i++)
                            {
                                product.Add(commandArr[i]);
                            }
                            _stregsystem.createNewProduct(product);
                            return;
                        case ":negativebalance":
                            List<User> negativeUsers = _stregsystem.GetUsers(u => u.Balance < 0);
                            _ui.DisplayUsers(negativeUsers);
                            return;
                        default:
                            _ui.DisplayAdminCommandNotFoundMessage($"Are you sure you entered the correct command? {commandArr[0]}");
                            return;
                    }
                }catch(IndexOutOfRangeException e)
                {
                    _ui.DisplayAdminCommandNotFoundMessage($"You have to enter a second command after {commandArr[0]}");
                    return;
                }catch(FormatException e)
                {
                    _ui.DisplayAdminCommandNotFoundMessage($"You seem to have entered the wrong parameters {e.Message}");
                }
            }else if(commandArr[0] == "transactions")
            {
                try
                {
                    int s;
                    List<string> temp = _stregsystem.GetTransactionForUser(commandArr[1], 10);
                    _ui.DisplayUserBuysProduct(temp);
                    return;
                }
                catch (IndexOutOfRangeException)
                {
                    _ui.DisplayGeneralError("Username was incorrect");
                }
                catch (System.FormatException)
                {
                    _ui.DisplayGeneralError("Incorrect formatting");
                }        
            }else if(commandArr[0] == "")
            {
                _ui.PrintProductList();
            }
            else
            {
                
                try
                {
                    User user = _stregsystem.GetUserByUsername(commandArr[0]);
                    if (commandArr.Length == 1)
                    {
                        _ui.DisplayUserInfo(user);
                    }
                    for (int i = 1; i < commandArr.Length; i++)
                    {

                       try
                       {
                            Product temp = _stregsystem.GetProductByID(int.Parse(commandArr[i]));
                            try
                            {
                                _stregsystem.BuyProduct(user, temp);
                                _ui.DisplayUserBuysProduct(user.buyTransactions[^1]);
                            }
                            catch (InsufficientCreditsException)
                            {
                                _ui.DisplayInsufficientCash(user, temp);
                                return;
                            }

                       }
                       catch (IndexOutOfRangeException) 
                       {
                           _ui.DisplayProductNotFound(commandArr[i]);
                            return;
                       }
                       catch (System.FormatException)
                       {
                            _ui.DisplayGeneralError("Your formatting was incorrect");
                            return;
                       }
                       catch (OverflowException)
                       {
                           _ui.DisplayGeneralError("Overflow Error");
                       }
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    _ui.DisplayUserNotFound(commandArr[0]);
                    return;
                }
            }
        }

    }
}
