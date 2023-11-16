using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calcul
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public interface IUserInterface
    {
        string GetUserInput(string prompt);
        void ShowMessage(string message);
    }

    public partial class MainWindow : Window, IUserInterface
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            string sideAInput = txtSideA.Text;
            string sideBInput = txtSideB.Text;
            string sideCInput = txtSideC.Text;

            Triangle triangle = TriangleCalculator.CalculateTriangle(sideAInput, sideBInput, sideCInput);

            // Отображение типа треугольника
            txtTriangleType.Text = triangle.Type.ToString();

            // Отображение координат вершин
            txtVertexA.Text = $" ({triangle.Vertices[0].Item1}, {triangle.Vertices[0].Item2})";
            txtVertexB.Text = $" ({triangle.Vertices[1].Item1}, {triangle.Vertices[1].Item2})";
            txtVertexC.Text = $" ({triangle.Vertices[2].Item1}, {triangle.Vertices[2].Item2})";

            IUserInterface userInterface = this; // Получение ссылки на интерфейс пользователя

            string userInput = userInterface.GetUserInput("Данные введены "); // Пользовательский ввод
            userInterface.ShowMessage("Готово"); // Отображение сообщения
        }

        public string GetUserInput(string prompt)
        {
            string userInput = null;

            Application.Current.Dispatcher.Invoke(() =>
            {
                userInput = MessageBox.Show(prompt, "User Input", MessageBoxButton.OKCancel).ToString();
            });

            return userInput;
        }

        public void ShowMessage(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MessageBox.Show(message, "Message");
            });
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string recipient = txtRecipient.Text;
            string subject = txtSubject.Text;
            string body = txtBody.Text;

            IMailSender mailSender = new MockMailSender();
            string output = $"Отправка электронной почты:\nПолучатель: {recipient}\nТема: {subject}\nТело: {body}";

            txtOutput.Text = output; // Display the output in the TextBox
            mailSender.SendEmail(recipient, subject, body);
        }
    }
}

