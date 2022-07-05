using System;
using System.Collections.Generic;

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
        private bool _isWork = true;
        private List<Player> _players = new List<Player>();
        private bool _state;
        private int _result;
        private string _userInput;

        public void Work()
        {
            while (_isWork)
            {
                Console.WriteLine("1 - Добавить игрока \n2 - Сменить статус бана \n3 - Удалить игрока\n4 - Вывести игроков\n5 - выход\nВыберите вариант:");
                string userInput = Console.ReadLine();
                Console.Clear();

                switch (userInput)
                {
                    case "1":
                        AddPlayer();
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
                        GetMessage("Данные не корректны");
                        break;
                }
            }
        }

        private void AddPlayer()
        {
            string name;
            Console.WriteLine("Введите имя игрока:");
            name = Console.ReadLine();
            Console.WriteLine("Введите уровень игрока:");
            _state = CheckState();

            if (_state)
            {
                _players.Add(new Player(name, _result));
                GetMessage("Игрок добавлен!");
            }
            else
            {
                GetMessage("Введите корректные данные.");
            }
        }

        private void SetPlayerBanStatus()
        {
            if (_players.Count > 0)
            {
                ShowDetails();
                Console.WriteLine("Чтобы изменить статус бана персонажа, напишите его порядковый номер:");
                _state = CheckState();

                if (_state && (_result - 1) < _players.Count)
                {
                    if (_players[_result - 1].IsBanned == false)
                    {
                        _players[_result - 1].Ban();
                    }
                    else
                    {
                        _players[_result - 1].UnBan();
                    }
                    GetMessage("Успешно!");
                }
                else
                {
                    GetMessage("Данные не корректны.");
                }
            }
            else
            {
                GetMessage("Игроков нет.");
            }
        }

        private void RemovePlayer()
        {
            if (_players.Count > 0)
            {
                ShowDetails();
                Console.WriteLine("Чтобы удалить игрока, напишите его порядковый номер:");
                _state = CheckState();

                if (_state && (_result - 1) < _players.Count)
                {
                    _players.RemoveAt(_result - 1);
                    GetMessage("Успех!");
                }
                else
                {
                    GetMessage("Данные не корректны.");
                }
            }
            else
            {
                GetMessage("Игроков нет.");
            }
        }

        private void ShowDetails()
        {
            Console.WriteLine("База даннных игроков:");

            for (int i = 0; i < _players.Count; i++)
            {
                Console.Write(i + 1 + ".");
                _players[i].ShowInfo();
            }
        }

        private bool CheckState()
        {
            _userInput = Console.ReadLine();
            _state = int.TryParse(_userInput, out _result);
            return _state;
        }

        private void GetMessage(string text = "")
        {
            Console.WriteLine(text);
            Console.WriteLine("Для продолжения нажмите любую клавишу: ");
            Console.ReadKey();
            Console.Clear();
        }
    }

}
class Player
{
    private string _name;
    private int _level;
    private string _stateOfBan;

    public bool IsBanned { get; private set; }

    public Player(string name, int level)
    {
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

        Console.WriteLine($"Имя - {_name}. Уровень - {_level}. Статус бана - {_stateOfBan}. ");
    }
}
