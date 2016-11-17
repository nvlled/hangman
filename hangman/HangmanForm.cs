using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace hangman
{
    public partial class HangmanForm : Form
    {
        string puzzleText;
        string[] puzzleWords;
        string[] imageNames;
        int imgIndex = -1;
        ISet<char> charset;
        int answered = 0;

        public HangmanForm()
        {
            InitializeComponent();
            initButtons();
            imageNames = Directory.EnumerateFiles("./images").ToArray();

            var timer = new Timer();
            timer.Interval = 33;
            timer.Tick += paintLoop;
            timer.Start();

            startNewGame();
        }
        void startNewGame()
        {
            var entry = PuzzleData.Random();
            txtCategory.Text = entry.Item1;
            setPuzzleText(entry.Item2);
            imgIndex = -1;
            answered = 0;
            btnNext.Visible = false;
            setEnabledButtons(true);
        }

        void paintLoop(object sender, EventArgs e)
        {
            var g = panelDrawing.CreateGraphics();
            if (imgIndex < 0)
                g.Clear(Color.White);
            if (imgIndex >= 0 && imgIndex <= imageNames.Length)
            {
                var filename = imageNames[imgIndex];
                var img = Image.FromFile(filename);
                g.DrawImage(img, new Rectangle(0, 0, panelDrawing.Width, panelDrawing.Height));
            }
        }

        void tryLetter(object sender, EventArgs e)
        {
            var btn = (Button) sender;
            var trych = btn.Text[0];

            btn.Enabled = false;

            bool correct = false;
            for (var i = 0; i < puzzleWords.Length; i++)
            {
                var word = puzzleWords[i];
                for (var j = 0; j < word.Length; j++)
                {
                    var ch = word[j];
                    if (trych == ch)
                    {
                        panelText.Controls[i].Controls[j].Text = ch + "";
                        correct = true;
                    }
                }
            }
            if (!correct)
            {
                imgIndex++;
                if (imgIndex >= imageNames.Length - 1)
                {
                    setEnabledButtons(false);
                    showAnswer();
                    MessageBox.Show("gameover");
                }
            }
            else
            {
                answered++;
            }

            if (isCompleted()) {
                btnNext.Visible = true;
                setEnabledButtons(false);
            }
        }

        void showAnswer()
        {
            setPuzzleText(puzzleText, true);
        }

        void setEnabledButtons(bool val)
        {
            foreach (var btn in panelButtons.Controls)
            {
                ((Button)btn).Enabled = val;
            }
        }

        bool isCompleted()
        {
            return answered == charset.Count;
        }

        public void initButtons()
        {
            var alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            panelButtons.Controls.Clear();
            foreach (var c in alpha)
            {
                var btn = new Button();
                btn.Size = new Size(30, 33);
                btn.Text = c+"";
                btn.Click += tryLetter;
                panelButtons.Controls.Add(btn);
            }
        }

        public void setPuzzleText(string text, bool showAnswer = false)
        {
            puzzleText = text.ToUpper();
            panelText.Controls.Clear();
            puzzleWords = puzzleText.Split(' ').ToArray();
            charset = new SortedSet<char>();
            foreach (var word in puzzleWords) 
            {
                var wordPanel = new FlowLayoutPanel();
                wordPanel.Height = 20;
                wordPanel.Width = (word.Length+1) * 22;
                wordPanel.Margin = new Padding(3, 3, 3, 3);
                foreach (var c in word)
                {
                    var txtbox = new TextBox();
                    txtbox.Size = new Size(20, 22);
                    txtbox.Margin = new Padding(1);
                    txtbox.ReadOnly = true;
                    wordPanel.Controls.Add(txtbox);
                    charset.Add(c);
                    if (showAnswer)
                        txtbox.Text = c+"";
                }
                panelText.Controls.Add(wordPanel);
            }
        }

        private void newGameToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            startNewGame();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            startNewGame();
        }
    }
}
