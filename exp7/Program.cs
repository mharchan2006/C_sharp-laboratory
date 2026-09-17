using System;
using System.Drawing;
using System.Windows.Forms;

namespace Exp7
{
    public class MyForm : Form
    {
        Button button;

        public MyForm()
        {
            button = new Button();

            button.Text = "Click Me";
            button.Location = new Point(100, 80);

            button.Click += Button_Click;

            Controls.Add(button);

            Text = "My First Form";
            Width = 300;
            Height = 200;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello World!");
        }
    }

    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new MyForm());
        }
    }
}
