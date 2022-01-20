using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bank
{
	public enum AppState
	{
		Connexion = 101,
		MainMenu = 102,
		// main menu options :
		DisplayBalance = 1,
		WithDraw = 2,
		Credit = 3,
		ChangePin = 4,
		Home = 5,
		Quit = 6,
	}

	public class MyAccount
	{
		public decimal m_balance;
		public string m_pin;

		public MyAccount(decimal b, string p)
		{
			m_balance = b;
			m_pin = p;
		}
	}


	internal class Program
	{
		#region Fields

		private static Dictionary<string, MyAccount> _bankData = new Dictionary<string, MyAccount>()
		{
			{ "0001", new MyAccount(100,"987")},
			{ "123", new MyAccount(99999,"123")},
		};

		private static string _str;
		private static string _currentCard;
		private static MyAccount _currentAccount;

		// Regex à tester					---- CONTINUER avec exclusion de tout ce qui n'est pas un nombre
		private static Regex _rgxAccount = new Regex(@"([0-9])");

		// Regex à tester					---- CONTINUER avec exclusion de tout ce qui n'est pas un nombre
		private static Regex _rgxPin = new Regex(@"([0-9])");

		// état de l'application
		private static AppState _appState = AppState.Home;

		// App is running
		private static bool _isRunning = true;

		#endregion



		#region Methods

		static void Main(string[] args)
		{
			while (_isRunning)
			{
				switch (_appState)
				{
					case AppState.Home:
						DisplayHeader("Bienvenue dans le DAB CléManuAle");
						_appState = AppState.Connexion;
						break;
					case AppState.Connexion:
						Connexion();
						break;
					case AppState.MainMenu:
						DisplayHeader("Menu principal");
						MainMenu();
						break;
					case AppState.DisplayBalance:
						DisplayHeader("Solde");
						DisplayBalance();
						break;
					case AppState.WithDraw:
						DisplayHeader("Retrait");
						WithDraw();
						break;
					case AppState.Credit:
						DisplayHeader("Depôt");
						Credit();
						break;
					case AppState.ChangePin:
						DisplayHeader("Changer le PIN");
						ChangePin();
						break;
					case AppState.Quit:
						DisplayHeader("Quitter");
						QuitApp();
						break;
					default:
						break;
				}
			}
		}

		private static void DisplayHeader(string title)
		{
			Console.Clear();
			Console.WriteLine("------------------");
			Console.WriteLine(title);
			Console.WriteLine("------------------");
		}

		private static void ControlBackMenu()
		{
			if (Console.ReadKey().Key == ConsoleKey.Enter)
			{
				_appState = AppState.MainMenu;
			}
		}

		private static void Connexion()
		{
			int step = 0;

			// Test du numéro de carte
			while (step == 0)
			{
				Console.WriteLine("Entrer un numéro de carte valide. Entrer \"Annuler\" pour annuler.");
				_str = Console.ReadLine();
				if (_rgxAccount.IsMatch(_str) && _bankData.ContainsKey(_str))
				{
					step = 1;
					break;
				}
				else if (_str == "Annuler")
				{
					_appState = AppState.Home;
					return;
				}
			}

			_currentCard = _str;

			while (step == 1)
			{
				Console.WriteLine("Entrer un PIN valide. Entrer \"Annuler\" pour annuler.");
				_str = Console.ReadLine();
				if (_rgxPin.IsMatch(_str) && _str == _bankData[_currentCard].m_pin)
				{
					step = 2;
					break;
				}
				else if (_str == "Annuler")
				{
					_appState = AppState.Home;
					return;
				}
			}

			Console.WriteLine("Compte trouvé et PIN correspondant.");
			_currentAccount = _bankData[_currentCard];
			_appState = AppState.MainMenu;
		}

		private static void MainMenu()
		{
			Console.WriteLine(@"Main menu :
	1 : afficher solde
	2 : retrait
	3 : dépôt
	4 : changer PIN
	5 : déconnecter
	6 : quitter
			");

			int result = 0;
			_appState = AppState.Quit;
			bool isChoiceOk = false;
			while (!isChoiceOk)
			{
				Console.WriteLine("Choisissez une option. ");
				_str = Console.ReadLine();
				
				if (int.TryParse(_str, out result) && result >= 1 && result == (int)_appState)
				{
					isChoiceOk = true;
				}
			}

			Console.WriteLine($"Menu choisi {result}");

			_appState = (AppState)Enum.Parse(typeof(AppState), _str);

		}

		private static void DisplayBalance()
		{
			Console.WriteLine($"Solde = {_currentAccount.m_balance}. Appuyer sur Entrée pour revenir au menu.");
			ControlBackMenu();
		}

		private static void WithDraw()
		{
			if (_currentAccount.m_balance == 0)
			{
				Console.WriteLine("Retrait impossible. Votre compte est à sec.");
				_appState = AppState.MainMenu;
				return;
			}

			Console.WriteLine($"Vous avez {_currentAccount.m_balance}. Combien voulez-vous retirer ?");

			decimal result;

			do
			{
				Console.WriteLine("Entrer un montant valide et inférieur au solde.");
				_str = Console.ReadLine();
			} while (!decimal.TryParse(_str, out result) || result > _currentAccount.m_balance || result <= 0);

			_currentAccount.m_balance -= result;

			Console.WriteLine($"Vous avez retiré {result}. " +
				$"Il reste {_currentAccount.m_balance} sur le compte." +
				$" Appuyer sur Entrée pour revenir au menu.");
			ControlBackMenu();
		}

		private static void Credit()
		{
			Console.WriteLine($"Vous avez {_currentAccount.m_balance}. Combien voulez-vous déposer ?");

			decimal result;

			do
			{
				Console.WriteLine("Entrer un montant valide.");
				_str = Console.ReadLine();
			} while (!decimal.TryParse(_str, out result) || result <= 0);

			_currentAccount.m_balance += result;

			Console.WriteLine($"Vous avez crédité {result}. Votre compte est de {_currentAccount.m_balance}." +
				$" Appuyer sur Entrée pour revenir au menu.");
			ControlBackMenu();
		}

		private static void ChangePin()
		{
			Console.WriteLine($"Votre PIN est {_currentAccount.m_pin}");
			Console.WriteLine("Le numéro est uniquement composé de 3 chiffres.");

			do
			{
				Console.WriteLine("Entrez un nouveau PIN valide.");
				_str = Console.ReadLine();
			} while (!_rgxPin.IsMatch(_str));

			_currentAccount.m_pin = _str;

			Console.WriteLine("Votre PIN a été modifié. Appuyer sur Entrée pour revenir au menu.");
			ControlBackMenu();
		}

		private static void QuitApp()
		{
			Console.WriteLine("Merci d'avoir utilisé cette borne.");
			Environment.Exit(0);
		}

		#endregion
	}
}
