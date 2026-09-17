using System;
using System.Windows.Forms;

namespace WindowsFormsControls
{
    public class Form1 : Form
    {
        Label nameLabel;
        TextBox nameTextBox;
        ComboBox courseComboBox;
        RadioButton maleRadio;
        RadioButton femaleRadio;
        CheckBox termsCheckBox;
        Button submitButton;
        ListBox resultListBox;

        public Form1()
        {
            // Form
            Text = "Windows Forms Controls";
            Width = 500;
            Height = 450;

            // Label
            nameLabel = new Label();
            nameLabel.Text = "Name:";
            nameLabel.Left = 30;
            nameLabel.Top = 30;
            nameLabel.Width = 100;
            Controls.Add(nameLabel);

            // TextBox
            nameTextBox = new TextBox();
            nameTextBox.Left = 130;
            nameTextBox.Top = 30;
            nameTextBox.Width = 200;
            Controls.Add(nameTextBox);

            // ComboBox
            courseComboBox = new ComboBox();
            courseComboBox.Left = 130;
            courseComboBox.Top = 70;
            courseComboBox.Width = 200;
            courseComboBox.Items.Add("B.Tech IT");
            courseComboBox.Items.Add("B.E CSE");
            courseComboBox.Items.Add("B.E ECE");
            Controls.Add(courseComboBox);

            // RadioButtons
            maleRadio = new RadioButton();
            maleRadio.Text = "Male";
            maleRadio.Left = 130;
            maleRadio.Top = 110;
            Controls.Add(maleRadio);

            femaleRadio = new RadioButton();
            femaleRadio.Text = "Female";
            femaleRadio.Left = 200;
            femaleRadio.Top = 110;
            Controls.Add(femaleRadio);

            // CheckBox
            termsCheckBox = new CheckBox();
            termsCheckBox.Text = "I agree";
            termsCheckBox.Left = 130;
            termsCheckBox.Top = 150;
            Controls.Add(termsCheckBox);

            // Button
            submitButton = new Button();
            submitButton.Text = "Submit";
            submitButton.Left = 130;
            submitButton.Top = 190;
            submitButton.Click += SubmitButton_Click;
            Controls.Add(submitButton);

            // ListBox
            resultListBox = new ListBox();
            resultListBox.Left = 30;
            resultListBox.Top = 240;
            resultListBox.Width = 350;
            resultListBox.Height = 100;
            Controls.Add(resultListBox);
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            resultListBox.Items.Clear();

            resultListBox.Items.Add("Name: " + nameTextBox.Text);
            resultListBox.Items.Add("Course: " + courseComboBox.Text);

            if (maleRadio.Checked)
                resultListBox.Items.Add("Gender: Male");
            else if (femaleRadio.Checked)
                resultListBox.Items.Add("Gender: Female");

            resultListBox.Items.Add("Agreed: " + termsCheckBox.Checked);
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
