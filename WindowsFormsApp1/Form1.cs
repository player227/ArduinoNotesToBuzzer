using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        ErrorProvider ErrorProvider = new ErrorProvider();
        private int PictureBoxIndex = 0;
        private bool edit_mode = false;
        private bool what_size = false; // 0 for note select 1 for size select
        private bool lock_size = false;
        private string[] tempos = new string[] { "Prestissimo","Presto", "Allegro", "Allegretto", "Moderato", "Andante", "Adiago", "Grave", "Lento" };
        private int[] bpm = new int[] { 204, 184, 144, 120, 144, 92, 71, 63, 50 };
        private double BeatsPerMin;
        private double temp1; //for trying to parse string to double
        private double temp2; //for trying to parse string to double
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Focus();
            this.MaximizeBox = false;
            this.KeyDown += Keydown;
            this.KeyDown += Left_Right;
            //this.KeyDown += EditMode;
            comboBox1.Items.Add("Tempo:");
            comboBox1.Items.Add("BPM:");
            comboBox2.Items.AddRange(tempos);
            comboBox1.SelectedIndex = 0;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;

        }
        private void PictureBoxIndexChanged() //vertical red line
        {
            if (PictureBoxIndex == 0) pictureBox2.BackColor = Color.IndianRed; else pictureBox2.BackColor = Color.Transparent;
            if (PictureBoxIndex == 1) pictureBox3.BackColor = Color.IndianRed; else pictureBox3.BackColor = Color.Transparent;
            if (PictureBoxIndex == 2) pictureBox4.BackColor = Color.IndianRed; else pictureBox4.BackColor = Color.Transparent;
            if (PictureBoxIndex == 3) pictureBox5.BackColor = Color.IndianRed; else pictureBox5.BackColor = Color.Transparent;
        }

        /*private void EditMode(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.W)
            {
                if (checkBox1.Checked) checkBox1.Checked = false;
                else checkBox1.Checked = true;
            }
        }*/
        private void Left_Right(object sender, KeyEventArgs e) //move vertical red line
        {
            if (!edit_mode)
            {
                if (e.KeyCode == Keys.E)
                {
                    PictureBoxIndex++; if (PictureBoxIndex > 3) PictureBoxIndex = 0; PictureBoxIndexChanged();
                }
                if (e.KeyCode == Keys.Q)
                {
                    PictureBoxIndex--; if (PictureBoxIndex < 0) PictureBoxIndex = 3; PictureBoxIndexChanged();
                }
            }
        }

        private void Keydown(object sender, KeyEventArgs e) 
        {
            if (!edit_mode)
            {
                if (!what_size) NotePress(sender, e); // check if we are waiting for a type of note or note size 
                else GetSize(sender, e);
            }
        }
        private void NotePress(object sender, KeyEventArgs e) // coresponding keys and notes
        {
            if (e.KeyCode == Keys.D0 || e.KeyCode == Keys.NumPad0)
            {
                richTextBox1.Text += ("Pause "); pictureBox7.BackColor = Color.IndianRed; what_size = true;
            }
            else if (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1)
            {
                richTextBox1.Text += ("C" + (PictureBoxIndex + 2).ToString() + " ");
                if (lock_size) richTextBox1.Text += textBox1.Text + " ";
                else { pictureBox7.BackColor = Color.IndianRed; what_size = true; }
            }
            else if (e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2)
            {
                richTextBox1.Text += ("D" + (PictureBoxIndex + 2).ToString() + " ");
                if (lock_size) richTextBox1.Text += textBox1.Text + " ";
                else { pictureBox7.BackColor = Color.IndianRed; what_size = true; }
            }
            else if (e.KeyCode == Keys.D3 || e.KeyCode == Keys.NumPad3)
            {
                richTextBox1.Text += ("E" + (PictureBoxIndex + 2).ToString() + " ");
                if (lock_size) richTextBox1.Text += textBox1.Text + " ";
                else { pictureBox7.BackColor = Color.IndianRed; what_size = true; }
            }
            else if (e.KeyCode == Keys.D4 || e.KeyCode == Keys.NumPad4)
            {
                richTextBox1.Text += ("F" + (PictureBoxIndex + 2).ToString() + " ");
                if (lock_size) richTextBox1.Text += textBox1.Text + " ";
                else { pictureBox7.BackColor = Color.IndianRed; what_size = true; }
            }
            else if (e.KeyCode == Keys.D5 || e.KeyCode == Keys.NumPad5)
            {
                richTextBox1.Text += ("G" + (PictureBoxIndex + 2).ToString() + " ");
                if (lock_size) richTextBox1.Text += textBox1.Text + " ";
                else { pictureBox7.BackColor = Color.IndianRed; what_size = true; }
            }
            else if (e.KeyCode == Keys.D6 || e.KeyCode == Keys.NumPad6)
            {
                richTextBox1.Text += ("A" + (PictureBoxIndex + 2).ToString() + " ");
                if (lock_size) richTextBox1.Text += textBox1.Text + " ";
                else { pictureBox7.BackColor = Color.IndianRed; what_size = true; }
            }
            else if (e.KeyCode == Keys.D7 || e.KeyCode == Keys.NumPad7)
            {
                richTextBox1.Text += ("B" + (PictureBoxIndex + 2).ToString() + " ");
                if (lock_size) richTextBox1.Text += textBox1.Text + " ";
                else { pictureBox7.BackColor = Color.IndianRed; what_size = true; }
            }
        }

        private void GetSize(object sender, KeyEventArgs e) // coresponding keys and sizes
        {
            if (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1)
            {
                richTextBox1.Text += "1 "; pictureBox7.BackColor = Color.White; what_size = false;
            }
            else if (e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2)
            {
                richTextBox1.Text += "0.5 "; pictureBox7.BackColor = Color.White; what_size = false;
            }
            else if (e.KeyCode == Keys.D3 || e.KeyCode == Keys.NumPad3)
            {
                richTextBox1.Text += "0.25 "; pictureBox7.BackColor = Color.White; what_size = false;
            }
            else if (e.KeyCode == Keys.D4 || e.KeyCode == Keys.NumPad4)
            {
                richTextBox1.Text += "0.125 "; pictureBox7.BackColor = Color.White; what_size = false;
            }
            else if (e.KeyCode == Keys.D5 || e.KeyCode == Keys.NumPad5)
            {
                richTextBox1.Text += "0.0675 "; pictureBox7.BackColor = Color.White; what_size = false;
            }
            else if (e.KeyCode == Keys.D6 || e.KeyCode == Keys.NumPad6)
            {
                richTextBox1.Text += "___ "; pictureBox7.BackColor = Color.White; what_size = false;
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) edit_mode = true;
            else { edit_mode = false; checkBox1.Focus(); }
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            try { 
                double.Parse( textBox1.Text, System.Globalization.CultureInfo.InvariantCulture);
                checkBox2.Checked = true;
                lock_size = true;
            }
            catch {
                ErrorProvider.SetError(textBox1, "Numbers only!");
                checkBox2.Checked = false;
                lock_size = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                label15.Visible = false;
                label14.Visible = false;
                textBox2.Visible = false;
                textBox3.Visible = false;

                comboBox2.Visible = true;
                textBox4.Visible = true;
                label16.Visible = true;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                comboBox2.Visible = false;
                textBox4.Visible = false;
                label16.Visible = false;

                label15.Visible = true;
                label14.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox4.Text = bpm[comboBox2.SelectedIndex].ToString();
        }

        private string note(string inputNote) //get freq of note
        {
            //switch case all notes + pause
            switch (inputNote)
            {
                case "C2": return "tone(buzzer,65);";
                case "D2": return "tone(buzzer,73);";
                case "E2": return "tone(buzzer,82);";
                case "F2": return "tone(buzzer,87);";
                case "G2": return "tone(buzzer,97);";
                case "A2": return "tone(buzzer,110);";
                case "B2": return "tone(buzzer,123);";
                case "C3": return "tone(buzzer,131);";
                case "D3": return "tone(buzzer,147);";
                case "E3": return "tone(buzzer,165);";
                case "F3": return "tone(buzzer,175);";
                case "G3": return "tone(buzzer,196);";
                case "A3": return "tone(buzzer,220);";
                case "B3": return "tone(buzzer,247);";
                case "C4": return "tone(buzzer,262);";
                case "D4": return "tone(buzzer,294);";
                case "E4": return "tone(buzzer,330);";
                case "F4": return "tone(buzzer,350);";
                case "G4": return "tone(buzzer,392);";
                case "A4": return "tone(buzzer,440);";
                case "B4": return "tone(buzzer,494);";
                case "C5": return "tone(buzzer,524);";
                case "D5": return "tone(buzzer,587);";
                case "E5": return "tone(buzzer,659);";
                case "F5": return "tone(buzzer,698);";
                case "G5": return "tone(buzzer,784);";
                case "A5": return "tone(buzzer,880);";
                case "B5": return "tone(buzzer,987);";
                case "Pause": return "noTone(buzzer)";
                default:
                    return string.Empty;
            }
        }

        private string Duration(double size,double BPM) //get druation of note in 10^-3 seconds
        {
            //formula size*60/(whole_note's_bps)= duration
            return "delay("+((size*60.0/BPM)*1000).ToString()+");\n";
        }
        private void BuildCode(string input) // build code
        {
            
            try
            {
                bool a = true;//true -> type of note : false -> duration of note 
                string[] parts = richTextBox1.Text.Split(null); //  split text of rich textbox by 
                //generic beginning 
                /*                                   
                const int buzzer = A0;
                void setup()
                {
                    // put your setup code here, to run once:
                    pinMode(buzzer, OUTPUT);
                }
                */
                richTextBox2.Text = "const int buzzer = A0;\nvoid setup()\n{\npinMode(buzzer,OUTPUT);\n}\n\n";
                richTextBox2.Text += "void loop(){\n";
                //converted notes
                //tone(buzzer,<FREQ>);delay(<DURATION>);
                //noTone(buzzer); delay(5);
                foreach (string i in parts)
                {
                    if (a)
                    {
                        richTextBox2.Text += note(i);
                    }
                    else
                    {
                        richTextBox2.Text += Duration(Double.Parse(i), BeatsPerMin);
                    }

                    if (a) a = false;
                    else { a = true; richTextBox2.Text += "noTone(buzzer); delay(5);\n"; }

                }

                //end
                richTextBox2.Text += "\n\ndelay(5000);\n\n}";

            }
            catch
            {
                MessageBox.Show("ERROR\nCheck notes!\n"+"(Enter all custom durations and check if every note has a duration pair");
            }
        }
        private bool ErrorCheck()
        {
            if (comboBox1.SelectedIndex == 0)
            {
                Double.TryParse(textBox4.Text, out temp1);
                if (textBox4.Text == string.Empty)
                { ErrorProvider.SetError(this.textBox4, "Bpm not set!"); return true; }
                else if (temp1 == 0)
                { ErrorProvider.SetError(this.textBox4, "Numbers only!"); return true; }
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                Double.TryParse(textBox2.Text, out temp1);
                Double.TryParse(textBox3.Text, out temp2);
                if (textBox2.Text == string.Empty)
                { ErrorProvider.SetError(this.textBox2, "Size of note not set!"); return true; }
                else if (temp1 == 0)
                { ErrorProvider.SetError(this.textBox2, "Numbers only!"); return true; }
                if (textBox3.Text == string.Empty)
                { ErrorProvider.SetError(this.textBox3, "Bpm not set"); return true; }
                else if (temp2 == 0)
                { ErrorProvider.SetError(this.textBox3, "Numbers only!"); return true; }
            }
            if (richTextBox1.Text == string.Empty)
            { ErrorProvider.SetError(this.richTextBox1, "There must be notes!"); return true; }

            return false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ErrorProvider.Clear();
            string input = richTextBox1.Text;
            if (!ErrorCheck())//build code if there are no errors
            {
                if (comboBox1.SelectedIndex == 0)
                    BeatsPerMin = Double.Parse(textBox4.Text)*4; //BPM FOR WHOLE NOTE
                else
                    BeatsPerMin = Double.Parse(textBox3.Text)*(1/Double.Parse(textBox2.Text));//BPM FOR WHOLE NOTE
                BuildCode(input);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            ErrorProvider.Clear();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ErrorProvider.Clear();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            ErrorProvider.Clear();
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            ErrorProvider.Clear();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ErrorProvider.Clear();
        }



        //Focus problems failsafes(can't use KeyDown when object has focus instead of form itself):    //ADD EDIT FUNCTION
        private void checkBox1_GotFocus(object sender, EventArgs e)
        {
            this.Focus();
        }
        private void checkBox2_GotFocus(object sender, EventArgs e)
        {
            this.Focus();
        }
        private void richTextBox1_GotFocus(object sender, EventArgs e)
        {
            if (!edit_mode) this.Focus();
        }
        private void RichTextBox2_GotFocus(object sender, EventArgs e)
        {
            if (!edit_mode) this.Focus();
        }
        private void Button1_GotFocus(object sender, EventArgs e)
        {
            this.Focus();
        }
        private void textbox1_GotFocus(object sender, EventArgs e)
        {
            if (!edit_mode) this.Focus();
        }
        private void comboBox1_GotFocus(object sender, EventArgs e)
        {
            if (!edit_mode) this.Focus();
        }
        private void comboBox2_GotFocus(object sender, EventArgs e)
        {
            if (!edit_mode) this.Focus();
        }
        private void textbox4_GotFocus(object sender, EventArgs e)
        {
            if (!edit_mode) this.Focus();
        }
        private void textbox2_GotFocus(object sender, EventArgs e)
        {
            if (!edit_mode) this.Focus();
        }
        private void textbox3_GotFocus(object sender, EventArgs e)
        {
            if (!edit_mode) this.Focus();
        }
        private void button2_GotFocus(object sender, EventArgs e)
        {
            this.Focus();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("................CONTROLS................\n" +
                            "-Q,E - move red vertical scale left/right\n" +
                            "-numbers 0-7 - designated note/pause(0)\n" +
                            "-after note is selected horizontal red line "+
                            "will light up to indicate note/pause lenght selection\n"+
                            "-You can lock size of the note\n" +
                            "-Set tempo by BPM, with help of tempo or\n" +
                            "note=BPM\n" +
                            "-To edit stuff enable EDIT MODE by\n" +
                            "clicking proper checkbox");
        }
    }
}
