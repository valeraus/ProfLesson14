using System.Windows;
using System;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Task2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private bool isConnected;
        public MainWindow()
        {
            InitializeComponent();
            // Ініціалізація таймера
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += Timer_Tick;

            // Початковий стан підключення
            isConnected = false;
        }
        private void OnConnectButtonClick(object sender, RoutedEventArgs e)
        {
            textBox.Text = "Підключено до бази даних";

            // Імітація затримки для підключення
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));

            timer.Start();
        }

        private void OnDisconnectButtonClick(object sender, RoutedEventArgs e)
        {
            // Затримка для імітації відключення від БД
            textBox.Text = "Відключено від бази даних";
            timer.Stop();
        }

        private void OnTimerTick(object state)
        {
            if (isConnected)
            {
                // Оновлення TextBox з повідомленням "Дані отримані"
                UpdateTextBox("Дані отримані");
            }
        }

        private void UpdateTextBox(string message)
        {
            // Оновлення TextBox з використанням диспетчера потоку для доступу до UI-елементів
            Dispatcher.Invoke(() => textBox.Text = message);
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            textBox.Text = "Дані отримані. Час: " + DateTime.Now.ToString();
        }
    }
}
