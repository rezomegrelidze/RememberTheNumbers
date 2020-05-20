using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RememberTheNumbers
{
    public partial class Form1 : Form
    {
        private Label[] numberLabels;
        private Random rand;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rand = new Random();
            AssignNumberLabels();
            RandomizeLabels();
        }

        private void RandomizeLabels()
        {
            foreach (var numberLabel in numberLabels)
            {
                numberLabel.Text = rand.Next(100).ToString();
            }
        }

        private void AssignNumberLabels()
        {
            numberLabels = this.Controls.Cast<Control>()
                .Where(c => c is Label && !(c is LinkLabel))
                .Cast<Label>().ToArray();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            foreach (var numberLabel in numberLabels)
            {
                numberLabel.Visible = false;
            }
        }

        private void finishButton_Click(object sender, EventArgs e)
        {
            // check if textbox values are same with label values
            var textBoxValues = this.Controls.Cast<Control>()
                .Where(c => c is TextBox).Cast<TextBox>()
                .Select(textBox => textBox.Text == "" ? 0 : int.Parse(textBox.Text));

            var labelValues = numberLabels.Select(label => int.Parse(label.Text));

            if (labelValues.SequenceEqual(textBoxValues))
            {
                MessageBox.Show("you are a winner!!");
                highScoresListBox.Items.Add(5);
            }
            else
            {
                MessageBox.Show("try again man");

                var labelsHashSet = labelValues.ToHashSet();
                var textBoxHashSet = textBoxValues.ToHashSet();

                labelsHashSet.IntersectWith(textBoxHashSet);

                highScoresListBox.Items.Add(labelsHashSet.Count);
            }

            NewGame();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void NewGame()
        {
            RandomizeLabels();
            ClearTextBoxes();
            numberLabels.ToList().ForEach(l => l.Visible = true); // make all labels visible again
        }

        private void ClearTextBoxes()
        {
            var textBoxes = this.Controls.Cast<Control>()
                .Where(c => c is TextBox).Cast<TextBox>();

            textBoxes.ToList().ForEach(textBox => textBox.Clear());
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
