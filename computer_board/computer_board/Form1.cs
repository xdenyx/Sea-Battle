using System;
using System.IO.MemoryMappedFiles;
using System.Threading;
using System.Windows.Forms;

namespace computer_board
{
    public partial class Form1 : Form
    {
        private Button[,] computer_btns = new Button[10, 10];
        private MemoryMappedFile mmf;
        private MemoryMappedFile commandMmf;
        private Mutex mutex;
        private bool gameStarted = false;
        private bool shipsPlaced1 = false;
        private bool shipsPlaced2 = false;
        public Form1()
        {
            InitializeComponent(); // инициализация компонентов формы
            InitializeComputer_panel(); // инициализация поля компьютера
            mmf = MemoryMappedFile.OpenExisting("SeaBattleMMF");
            commandMmf = MemoryMappedFile.OpenExisting("SeaBattleCommandMMF");
            mutex = Mutex.OpenExisting("SeaBattleMutex");

            Thread thread = new Thread(Check_commands);
            thread.Start();
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            Resize_panel(ComputerPanel, computer_btns);
        }
        private void Resize_panel(Panel panel, Button[,] buttons)
        {
            int panelSize = Math.Min(panel.Width, panel.Height);
            int buttonSize = panelSize / 10;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    buttons[i, j].Size = new System.Drawing.Size(buttonSize, buttonSize);
                    buttons[i, j].Location = new System.Drawing.Point(i * buttonSize, j * buttonSize);
                }
            }
        }
        private void InitializeComputer_panel()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(InitializeComputer_panel));
                return;
            }
            ComputerPanel.Controls.Clear();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    computer_btns[i, j] = new Button();
                    computer_btns[i, j].Size = new System.Drawing.Size(30, 30);
                    computer_btns[i, j].Location = new System.Drawing.Point(i * 30, j * 30);
                    computer_btns[i, j].Tag = "water";
                    ComputerPanel.Controls.Add(computer_btns[i, j]);
                }
            }
            PlaceShip_random();
        }
        private void Check_commands()
        {
            while (true)
            {
                mutex.WaitOne();
                using (var accessor = commandMmf.CreateViewAccessor())
                {
                    int command = accessor.ReadInt32(0);
                    if (command == 1 && !gameStarted) // команда начать игру
                    {
                        gameStarted = true;
                        shipsPlaced1 = accessor.ReadBoolean(4); // чтение состояния первого поля
                        shipsPlaced2 = accessor.ReadBoolean(5); // чтение состояния второго поля
                        Thread thread = new(Check_playerMove);
                        thread.Start();
                    }
                    else if (command == 2) // команда перезапуска игры
                    {
                        gameStarted = false;
                        InitializeComputer_panel();
                    }
                    accessor.Write(0, 0); // сброс команды
                }
                mutex.ReleaseMutex();
                Thread.Sleep(100); // проверка команд каждые 100 миллисекунд
            }
        }
        private void Check_playerMove()
        {
            while (gameStarted)
            {
                int x, y;
                mutex.WaitOne();
                using (var accessor = mmf.CreateViewAccessor())
                {
                    x = accessor.ReadInt32(0);
                    y = accessor.ReadInt32(4);
                    accessor.Write(8, 0); 
                    if (x == -1 && y == -1)
                    {
                        mutex.ReleaseMutex();
                        continue;
                    }
                }
                mutex.ReleaseMutex();
                bool hit = computer_btns[x, y].Tag.ToString() == "ship";
                computer_btns[x, y].BackColor = hit ? System.Drawing.Color.Red : System.Drawing.Color.Blue;
                mutex.WaitOne();
                using (var accessor = mmf.CreateViewAccessor())
                {
                    accessor.Write(8, hit ? 1 : 0);
                }
                mutex.ReleaseMutex();
                Check_gameOver();
                Thread.Sleep(0); 
                computer_move();
            }
        }
        // метод для выполнения хода компьютера
        private void computer_move()
        {
            Random rnd = new Random();
            int x1, y1, x2, y2;
            bool repeatAttack1, repeatAttack2;

            // ход по первому полю, если оно заполнено
            if (shipsPlaced1)
            {
                do
                {
                    x1 = rnd.Next(10);
                    y1 = rnd.Next(10);

                    mutex.WaitOne();
                    using (var accessor = mmf.CreateViewAccessor())
                    {
                        accessor.Write(12, x1);
                        accessor.Write(16, y1);
                        accessor.Write(20, 0); // сброс статуса попадания первого поля
                        accessor.Write(40, 0); // сброс статуса повторной атаки первого поля
                    }
                    mutex.ReleaseMutex();

                    Thread.Sleep(0); // симуляция задержки

                    mutex.WaitOne();
                    using (var accessor = mmf.CreateViewAccessor())
                    {
                        repeatAttack1 = accessor.ReadInt32(40) == 1;
                    }
                    mutex.ReleaseMutex();
                } while (repeatAttack1);
            }

            // ход по второму полю, если оно заполнено
            if (shipsPlaced2)
            {
                do
                {
                    x2 = rnd.Next(10);
                    y2 = rnd.Next(10);
                    mutex.WaitOne();
                    using (var accessor = mmf.CreateViewAccessor())
                    {
                        accessor.Write(28, x2);
                        accessor.Write(32, y2);
                        accessor.Write(36, 0); // сброс статуса попадания второго поля
                        accessor.Write(44, 0); // сброс статуса повторной атаки второго поля
                    }
                    mutex.ReleaseMutex();
                    Thread.Sleep(0); // симуляция задержки
                    mutex.WaitOne();
                    using (var accessor = mmf.CreateViewAccessor())
                    {
                        repeatAttack2 = accessor.ReadInt32(44) == 1;
                    }
                    mutex.ReleaseMutex();
                } while (repeatAttack2);
            }
            Check_gameOver();
        }
        // метод для проверки, все ли корабли уничтожены
        private bool Check_ShipsDestroyed()
        {
            foreach (var button in computer_btns)
            {
                if (button.Tag.ToString() == "ship" && button.BackColor != System.Drawing.Color.Red)
                {
                    return false;
                }
            }
            return true;
        }
        private void Check_gameOver()
        {
            if (Check_ShipsDestroyed())
            {
                using (var accessor = mmf.CreateViewAccessor())
                {
                    accessor.Write(24, 1); // передаем сигнал о том, что игрок выиграл
                }
                gameStarted = false;
            }
            else
            {
                mutex.WaitOne();
                using (var accessor = mmf.CreateViewAccessor())
                {
                    accessor.Write(24, 0); // игра не закончена
                }
                mutex.ReleaseMutex();
            }
        }
        // метод для случайного размещения кораблей на сетке компьютера
        private void PlaceShip_random()
        {
            int[] ships = { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };
            Random rnd = new Random();

            foreach (var ship in ships)
            {
                bool placed = false;
                while (!placed)
                {
                    int x = rnd.Next(10);
                    int y = rnd.Next(10);
                    bool horizontal = rnd.Next(2) == 0;
                    if (CanPlaceShip(x, y, ship, horizontal))
                    {
                        PlaceShip(x, y, ship, horizontal);
                        placed = true;
                    }
                }
            }
        }
        // метод для проверки возможности размещения корабля
        private bool CanPlaceShip(int x, int y, int length, bool horizontal)
        {
            for (int i = 0; i < length; i++)
            {
                int dx = horizontal ? i : 0;
                int dy = horizontal ? 0 : i;
                int nx = x + dx;
                int ny = y + dy;
                if (nx < 0 || nx >= 10 || ny < 0 || ny >= 10 || computer_btns[nx, ny].Tag != "water")
                {
                    return false;
                }
            }
            for (int i = -1; i <= length; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int dx = horizontal ? i : j;
                    int dy = horizontal ? j : i;
                    int nx = x + dx;
                    int ny = y + dy;
                    if (nx >= 0 && nx < 10 && ny >= 0 && ny < 10 && computer_btns[nx, ny].Tag != "water")
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        // метод для размещения корабля на сетке
        private void PlaceShip(int x, int y, int length, bool horizontal)
        {
            for (int i = 0; i < length; i++)
            {
                int dx = horizontal ? i : 0;
                int dy = horizontal ? 0 : i;
                computer_btns[x + dx, y + dy].Tag = "ship";
            }
        }
    }
}
