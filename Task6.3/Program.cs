using System;
using System.Collections.Generic;
using System.Linq;

namespace Task6._3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataBase dataBase = new DataBase();
            dataBase.Work();
        }
    }

    class DataBase
    {
        private List<Player> _players = new List<Player>();

        public void Work()
        {
            int uniqueNumber = 1;
            bool _isWork = true;

            while (_isWork)
            {
                Console.WriteLine("1 - Добавить игрока \n2 - Сменить статус бана \n3 - Удалить игрока\n4 - Вывести игроков\n5 - выход\nВыберите вариант:");
                string userInput = Console.ReadLine();
                Console.Clear();

                switch (userInput)
                {
                    case "1":
                        AddPlayer();
                        uniqueNumber++;
                        break;
                    case "2":
                        SetPlayerBanStatus();
                        break;
                    case "3":
                        RemovePlayer();
                        break;
                    case "4":
                        ShowDetails();
                        GetMessage();
                        break;
                    case "5":
                        _isWork = false;
                        break;
                    default:
                        WriteError();
                        break;
                }
            }
        }

        private void AddPlayer()
        {
            int result = 0;

            Console.WriteLine("Введите имя игрока:");
            string name = Console.ReadLine();
            Console.WriteLine("Введите уровень игрока:");

            if (CheckState(ref result))
            {
                _players.Add(new Player(name, result));
                GetMessage("Игрок добавлен!");
            }
            else
            {
                WriteError();
            }
        }

        private void SetPlayerBanStatus()
        {
            ShowDetails();
            Console.WriteLine("Чтобы изменить статус бана персонажа, напишите его уникальный номер:");
            string uniqueNumber = Console.ReadLine();

            if (TryGetPlayer(out Player player, uniqueNumber))
            {
                player.Ban();
            }
            else
            {
                player.UnBan();
            }
            GetMessage("Успешно!");
        }

        private void RemovePlayer()
        {
            ShowDetails();
            Console.WriteLine("Чтобы удалить игрока, напишите его уникальный номер:");
            string uniqueNumber = Console.ReadLine();

            if (TryGetPlayer(out Player player, uniqueNumber))
            {
                _players.Remove(player);
                GetMessage("Успех!");
            }
            else
            {
                WriteError();
            }
        }

        private bool TryGetPlayer(out Player player, string uniqueNumber)
        {
            player = null;

            int.TryParse(uniqueNumber, out int number);

            for (int i = 0; i < _players.Count; i++)
            {
                if (_players.ElementAt(i).UniqueNumber == number)
                {
                    player = _players[i];
                    return true;
                }
            }

            return false;
        }

        private void ShowDetails()
        {
            Console.WriteLine("База даннных игроков:");

            for (int i = 0; i < _players.Count; i++)
            {
                _players[i].ShowInfo();
            }
        }

        private bool CheckState(ref int result)
        {
            string userInput = Console.ReadLine();
            bool state = int.TryParse(userInput, out result);
            return state;
        }

        private void GetMessage(string text = "")
        {
            Console.WriteLine(text);
            Console.WriteLine("Для продолжения нажмите любую клавишу: ");
            Console.ReadKey();
            Console.Clear();
        }

        private void WriteError()
        {
            GetMessage("Введите корректные данные!");
        }
    }

}
class Player
{
    private static int _uniqueNumber;
    private string _name;
    private int _level;
    private string _stateOfBan;

    public bool IsBanned { get; private set; }

    public int UniqueNumber { get; set; }

    public Player(string name, int level)
    {

        UniqueNumber = ++_uniqueNumber;
        _name = name;
        _level = level;
        IsBanned = false;
    }

    public void Ban()
    {
        IsBanned = true;
    }
    public void UnBan()
    {
        IsBanned = false;
    }

    public void ShowInfo()
    {
        if (IsBanned)
        {
            _stateOfBan = "забанен";
        }
        else
        {
            _stateOfBan = "не забанен";
        }

        Console.WriteLine($"{UniqueNumber}.Имя - {_name}. Уровень - {_level}. Статус бана - {_stateOfBan}. ");
    }
}
