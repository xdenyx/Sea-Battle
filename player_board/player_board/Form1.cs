using System;
using System.IO.MemoryMappedFiles;
using System.Threading;
using System.Windows.Forms;

namespace player_board
{
    public partial class player_form : Form
    {
        // поля для хранения кнопок
        private Button[,] player1_btns = new Button[10, 10];
        private Button[,] attack_btns = new Button[10, 10];
        private Button[,] player2_btns = new Button[10, 10];
        private MemoryMappedFile mmf; // файл для передачи координат
        private MemoryMappedFile commandMmf; // файл для передачи значения кнопок (старт, перезагрузка)
        private Mutex mutex; // мьютекс для доступа к данным
        private bool gameStarted = false;
        private bool shipsPlaced1 = false; // корабли пока не размещены
        private bool shipsPlaced2 = false;
        private int[] ships = { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 }; // размеры кораблей
        private int currentShipLength;
        private Button[,] currentGrid;
        private bool horizontal = true; // выбор размещения корабля по умолчанию горизонтальный
        public player_form()
        {
            InitializeComponent();
            InitializePlayer_panel1(); // поле 1 игрока
            InitializePlayer_panel2(); // поле 2 игрока
            InitializeAttack_panel(); // поле для атак
            
            mmf = MemoryMappedFile.CreateOrOpen("SeaBattleMMF", 1000);
            commandMmf = MemoryMappedFile.CreateOrOpen("SeaBattleCommandMMF", 1000);
            mutex = new Mutex(false, "SeaBattleMutex");
            start_btn.Enabled = false; // кнопка начала отключена
            ship_selector.SelectedIndex = 0;
            currentShipLength = ships[0];
            currentGrid = player1_btns;
        }
        private void player_form_Resize(object sender, EventArgs e)
        {
            Resize_panel(player1_panel, player1_btns); // изменение размеров кнопок на поле игрока
            Resize_panel(player2_panel, player2_btns); // изменение размеров кнопок на поле атакуемого компьютером
            Resize_panel(attack_panel, attack_btns); // изменение размеров кнопок на поле атак
        }
        // метод для изменения размеров кнопок на панели
        private void Resize_panel(Panel panel, Button[,] buttons)
        {
            int panelSize = Math.Min(panel.Width, panel.Height); // вычисление минимального размера панели
            int buttonSize = panelSize / 10; // вычисление размера кнопок в зависимости от размера панели
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    buttons[i, j].Size = new System.Drawing.Size(buttonSize, buttonSize); // установка размера кнопок
                    buttons[i, j].Location = new System.Drawing.Point(i * buttonSize, j * buttonSize); // установка позиции кнопок
                }
            }
        }
        // метод для инициализации сетки 1
        private void InitializePlayer_panel1()
        {
            player1_panel.Controls.Clear();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    player1_btns[i, j] = new Button();
                    player1_btns[i, j].Size = new System.Drawing.Size(30, 30);
                    player1_btns[i, j].Location = new System.Drawing.Point(i * 30, j * 30);
                    player1_btns[i, j].Click += Player1_btn_Click;
                    player1_panel.Controls.Add(player1_btns[i, j]);
                }
            }
        }
        // метод для инициализации сетки 2
        private void InitializePlayer_panel2()
        {
            player2_panel.Controls.Clear();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    player2_btns[i, j] = new Button();
                    player2_btns[i, j].Size = new System.Drawing.Size(30, 30);
                    player2_btns[i, j].Location = new System.Drawing.Point(i * 30, j * 30);
                    player2_btns[i, j].Click += Player2_btn_Click;
                    player2_panel.Controls.Add(player2_btns[i, j]);
                }
            }
        }
        // метод для инициализации сетки атак
        private void InitializeAttack_panel()
        {
            attack_panel.Controls.Clear();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    attack_btns[i, j] = new Button();
                    attack_btns[i, j].Size = new System.Drawing.Size(30, 30);
                    attack_btns[i, j].Location = new System.Drawing.Point(i * 30, j * 30);
                    attack_btns[i, j].Click += atack_btn_Click;
                    attack_btns[i, j].Tag = "unvisited"; // Начальное состояние
                    attack_panel.Controls.Add(attack_btns[i, j]);
                }
            }
        }
        // метод для размещения корабля на сетке
        private void PlaceShip(Button[,] grid, int x, int y, int length, bool horizontal)
        {
            for (int i = 0; i < length; i++)
            {
                int dx = horizontal ? i : 0;
                int dy = horizontal ? 0 : i;
                grid[x + dx, y + dy].Tag = "ship"; // установка тега "ship" для кнопок, соответствующих кораблю
                grid[x + dx, y + dy].BackColor = System.Drawing.Color.Gray; // изменение цвета кнопок для отображения корабля
            }
        }

        // обработчик нажатия на кнопку сетки игрока
        private void Player1_btn_Click(object sender, EventArgs e)
        {
            if (currentShipLength <= 0) return; // проверка, есть ли еще корабли для размещения

            Button btn = sender as Button;
            int x = btn.Location.X / 30;
            int y = btn.Location.Y / 30;

            if (CanPlaceShip(currentGrid, x, y, currentShipLength, horizontal))
            {
                PlaceShip(currentGrid, x, y, currentShipLength, horizontal); // размещение корабля
                int index = ship_selector.SelectedIndex + 1;
                if (index < ship_selector.Items.Count)
                {
                    ship_selector.SelectedIndex = index;
                    currentShipLength = ships[index];
                }
                else
                {
                    if (currentGrid == player1_btns)
                    {
                        shipsPlaced1 = true;
                        currentGrid = player2_btns;
                        ship_selector.SelectedIndex = 0;
                        currentShipLength = ships[0];
                        status_label.Text = "Разместите корабли на другом поле или начните игру.";
                        DisableGrid(player1_btns);
                    }
                    else
                    {
                        shipsPlaced2 = true;
                        currentGrid = null;
                        status_label.Text = "Все корабли размещены. Можете начать игру";
                        DisableGrid(player2_btns);
                    }
                    
                    // если хотя бы одно поле заполнено, активируем кнопку старта
                    if (shipsPlaced1 || shipsPlaced2)
                    {
                        start_btn.Enabled = true;
                    }
                }
            }
            else
            {
                status_label.Text = "Невозможно разместить корабль здесь.";
            }
        }
        // обработчик нажатия на кнопку сетки атакуемого компьютером
        private void Player2_btn_Click(object sender, EventArgs e)
        {
            currentGrid = player2_btns; // устанавливаем текущую сетку на вторую
            Player1_btn_Click(sender, e);
        }
        // обработчик нажатия на кнопку атаки
        private void atack_btn_Click(object sender, EventArgs e)
        {
            if (!gameStarted)
            {
                status_label.Text = "Сначала начните игру!";
                return;
            }

            Button btn = sender as Button;
            if (btn.Tag.ToString() != "unvisited")
            {
                status_label.Text = "Вы уже посещали эту клетку";
                return;
            }

            int x = btn.Location.X / 30;
            int y = btn.Location.Y / 30;

            // сообщаем компьютеру о ходе
            mutex.WaitOne();
            using (var accessor = mmf.CreateViewAccessor())
            {
                accessor.Write(0, x);  // запись координаты X
                accessor.Write(4, y);  // запись координаты Y
                accessor.Write(8, 0);  // сброс статуса попадания
            }
            mutex.ReleaseMutex();
            btn.Tag = "visited";  // нажата кнопка
            btn.BackColor = System.Drawing.Color.Gray;
            // ждем хода компьютера
            Check_computerMove();
        }

        // обработчик нажатия на кнопку старта
        private void start_btn_Click(object sender, EventArgs e)
        {
            gameStarted = true;
            status_label.Text = "Игра началась. Ваш ход!";

            // Делаем неактивным незаполненное поле
            if (!shipsPlaced1)
            {
                DisableGrid(player1_btns);
            }

            if (!shipsPlaced2)
            {
                DisableGrid(player2_btns);
            }

            // уведомляем компьютер о начале игры
            mutex.WaitOne();
            using (var accessor = commandMmf.CreateViewAccessor())
            {
                accessor.Write(0, 1); // запись команды начала игры в позицию 0
                accessor.Write(4, shipsPlaced1); // передаем состояние заполнения полей
                accessor.Write(5, shipsPlaced2);
            }
            mutex.ReleaseMutex();
            mutex.WaitOne();
            using (var accessor = mmf.CreateViewAccessor())
            {
                accessor.Write(0, -1);
                accessor.Write(4, -1);
            }
            mutex.ReleaseMutex();
        }

        // обработчик нажатия на кнопку перезапуска
        private void Restart_btn_Click(object sender, EventArgs e)
        {
            RestartGame();
        }
        private void AutoPlace1_btn_Click(object sender, EventArgs e)
        {
            PlaceShip_random(player1_btns);
            shipsPlaced1 = true;
            start_btn.Enabled = true;
            status_label.Text = "Карабли на первом поле размещены";
            DisableGrid(player1_btns);
        }
        private void AutoPlace2_btn_Click(object sender, EventArgs e)
        {
            PlaceShip_random(player2_btns);
            shipsPlaced2 = true;
            start_btn.Enabled = true;
            status_label.Text = "Карабли на втором поле размещены";
            DisableGrid(player2_btns);
        }
        private void Orientation_btn_Click(object sender, EventArgs e)
        {
            horizontal = !horizontal;
            orientation_btn.Text = horizontal ? "Горизонтально" : "Вертикально";
        }

        // метод для отключения поля
        private void DisableGrid(Button[,] grid)
        {
            foreach (var button in grid)
            {
                button.Enabled = false;
            }
        }
        // метод для проверки хода компьютера
        private void Check_computerMove()
        {
            int x1, y1, x2, y2;
            bool repeatAttack1, repeatAttack2;
            // Ход по первому полю, если оно заполнено
            if (shipsPlaced1)
            {
                do
                {
                    mutex.WaitOne();
                    using (var accessor = mmf.CreateViewAccessor())
                    {
                        x1 = accessor.ReadInt32(12);
                        y1 = accessor.ReadInt32(16);
                        accessor.Write(20, 0);  // сброс статуса попадания первого поля
                        accessor.Write(40, 0);  // сброс статуса повторной атаки первого поля
                    }
                    mutex.ReleaseMutex();

                    bool hit1 = player1_btns[x1, y1].Tag != null && player1_btns[x1, y1].Tag.ToString() == "ship";
                    bool alreadyAttacked1 = player1_btns[x1, y1].BackColor == System.Drawing.Color.Red || player1_btns[x1, y1].BackColor == System.Drawing.Color.Blue;

                    player1_btns[x1, y1].BackColor = hit1 ? System.Drawing.Color.Red : System.Drawing.Color.Blue;

                    repeatAttack1 = alreadyAttacked1;

                    mutex.WaitOne();
                    using (var accessor = mmf.CreateViewAccessor())
                    {
                        accessor.Write(40, repeatAttack1 ? 1 : 0); // статус повторной атаки первого поля
                        accessor.Write(20, hit1 ? 1 : 0); // статус попадания в первое поле
                    }
                    mutex.ReleaseMutex();
                    Check_gameOver();
                } while (repeatAttack1);
            }
            // Ход по второму полю, если оно заполнено
            if (shipsPlaced2)
            {
                do
                {
                    mutex.WaitOne();
                    using (var accessor = mmf.CreateViewAccessor())
                    {
                        x2 = accessor.ReadInt32(28);
                        y2 = accessor.ReadInt32(32);
                        accessor.Write(36, 0);  // сброс статуса попадания второго поля
                        accessor.Write(44, 0);  // сброс статуса повторной атаки второго поля
                    }
                    mutex.ReleaseMutex();

                    bool hit2 = player2_btns[x2, y2].Tag != null && player2_btns[x2, y2].Tag.ToString() == "ship";
                    bool alreadyAttacked2 = player2_btns[x2, y2].BackColor == System.Drawing.Color.Red || player2_btns[x2, y2].BackColor == System.Drawing.Color.Blue;

                    player2_btns[x2, y2].BackColor = hit2 ? System.Drawing.Color.Red : System.Drawing.Color.Blue;

                    repeatAttack2 = alreadyAttacked2;

                    mutex.WaitOne();
                    using (var accessor = mmf.CreateViewAccessor())
                    {
                        accessor.Write(44, repeatAttack2 ? 1 : 0); // статус повторной атаки второго поля
                        accessor.Write(36, hit2 ? 1 : 0); // статус попадания во второе поле
                    }
                    mutex.ReleaseMutex();
                    Check_gameOver();
                } while (repeatAttack2);

            }
            status_label.Text = "Ваш ход";
        }
        // метод для проверки возможности размещения корабля
        private bool CanPlaceShip(Button[,] grid, int x, int y, int length, bool horizontal)
        {
            for (int i = 0; i < length; i++)
            {
                int dx = horizontal ? i : 0;
                int dy = horizontal ? 0 : i;
                int nx = x + dx;
                int ny = y + dy;
                if (nx < 0 || nx >= 10 || ny < 0 || ny >= 10 || grid[nx, ny].Tag != null)
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
                    if (nx >= 0 && nx < 10 && ny >= 0 && ny < 10 && grid[nx, ny].Tag != null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // метод для случайного размещения кораблей на сетке
        private void PlaceShip_random(Button[,] grid)
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
                    if (CanPlaceShip(grid, x, y, ship, horizontal))
                    {
                        PlaceShip(grid, x, y, ship, horizontal);
                        placed = true;
                    }
                }
            }
        }

        
        private bool Check_ShipsDestroyed(Button[,] grid)
        {
            foreach (var button in grid)
            {
                if (button.Tag != null && button.Tag.ToString() == "ship" && button.BackColor != System.Drawing.Color.Red)
                {
                    return false;
                }
            }
            return true;
        }
        private void Check_gameOver()
        {
            using (var accessor = mmf.CreateViewAccessor())
            {
                if (accessor.ReadInt32(24) == 1) // Проверка, если компьютер сигнализировал о проигрыше
                {
                    MessageBox.Show("Вы выиграли! Игра будет перезапущена.");
                    RestartGame();
                }
            }
            bool player1Lost = shipsPlaced1 && Check_ShipsDestroyed(player1_btns);
            bool player2Lost = shipsPlaced2 && Check_ShipsDestroyed(player2_btns);

            if (player1Lost)
            {
                string message = "Компьютер выиграл 1 поле! Игра будет перезапущена.";
                gameStarted = false;
                MessageBox.Show(message);
                RestartGame();
            }
            if (player2Lost)
            {
                string message = "Компьютер выиграл 2 поле! Игра будет перезапущена.";
                gameStarted = false;
                MessageBox.Show(message);
                RestartGame();
            }
            if (player1Lost && player2Lost)
            {
                string message = "Компьютер выиграл оба поля! Игра будет перезапущена.";
                gameStarted = false;
                MessageBox.Show(message);
                RestartGame();
            }
        }
        // метод для перезапуска игры
        private void RestartGame()
        {
            gameStarted = false;
            shipsPlaced1 = false;
            shipsPlaced2 = false;
            start_btn.Enabled = false;
            InitializePlayer_panel1();
            InitializePlayer_panel2(); 
            InitializeAttack_panel();
            ship_selector.SelectedIndex = 0;
            currentShipLength = ships[0];
            currentGrid = player1_btns;
            status_label.Text = "Игра сброшена. Разместите свои корабли и нажмите 'Начать игру'.";
            // уведомляем компьютер о перезапуске игры
            mutex.WaitOne();
            using (var accessor = commandMmf.CreateViewAccessor())
            {
                accessor.Write(0, 2); // запись команды перезапуска игры в позицию 0
            }
            mutex.ReleaseMutex();
            mutex.WaitOne();
            using (var accessor = mmf.CreateViewAccessor())
            {
                for (int i = 0; i < 1000; i++)
                {
                    accessor.Write(i, (byte)0);
                }
            }
            mutex.ReleaseMutex();
        }

    }
}
