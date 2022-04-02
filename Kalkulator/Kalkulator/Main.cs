using System;
using System.Linq;
using System.Windows.Forms;

namespace Kalkulator
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        #region Global Variables

        private double lastValue = 0;
        private string mainValueString = "0";
        private short currentOperation = 0;
        private bool newInput = true;
        private bool calc = false;

        #endregion

        #region Functions
        private void Calculate()
        {
            try
            {
                double currentValue = Convert.ToDouble(mainValueString);
                mainValueString = String.Empty;

                switch (currentOperation)
                {
                    case 1:         // +
                        currentValue = lastValue + currentValue;
                        break;
                    case 2:         // -
                        currentValue = lastValue - currentValue;
                        break;
                    case 3:         // *
                        currentValue = lastValue * currentValue;
                        break;
                    case 4:         // /
                        if (currentValue != 0)
                            currentValue = lastValue / currentValue;
                        else
                            mainValueString = "Nemoguće dijeliti sa 0";
                        break;
                    case 5:         // square
                        currentValue *= currentValue;
                        break;
                    case 6:         // sqrt
                        currentValue = Math.Sqrt(currentValue);
                        break;
                    case 7:         // nth power
                        if (!calc)
                            calc = true;
                        else
                            currentValue = Math.Pow(lastValue, currentValue);
                        break;
                    case 8:         // nth root
                        if (!calc)
                            calc = true;
                        else
                            currentValue = Math.Pow(currentValue, 1 / lastValue);
                        break;
                    case 9:         // log10
                        if (currentValue <= 0)
                            mainValueString = "Logaritam od negativnih brojeva i nule nije definisan";
                        else
                            currentValue = Math.Log10(currentValue);
                        break;
                    case 10:        // ln
                        if (currentValue <= 0)
                            mainValueString = "Logaritam od negativnih brojeva i nule nije definisan";
                        else
                            currentValue = Math.Log(currentValue);
                        break;
                    case 11:        // x!
                        if (currentValue < 0)
                            mainValueString = "Faktorijel od negativnih brojeva nije definisan";
                        else if (currentValue != (int)currentValue)
                            mainValueString = "Faktorijel od decimalnih brojeva nije definisan";
                        else
                        {
                            int temp = (int)currentValue;
                            currentValue = 1;

                            for (int i = 2; i <= temp; i++)
                                currentValue *= i;
                        }
                        break;
                    case 12:        // 1/x
                        if (currentValue != 0)
                            currentValue = 1 / currentValue;
                        else
                            mainValueString = "Nemoguće dijeliti sa 0";
                        break;
                    case 13:        // sin
                        if (rb_degree.Checked)
                            currentValue = currentValue * Math.PI / 180d;

                        currentValue = Math.Sin(currentValue);
                        break;
                    case 14:        // cos
                        if (rb_degree.Checked)
                            currentValue = currentValue * Math.PI / 180d;

                        currentValue = Math.Cos(currentValue);
                        break;
                    case 15:        // tg
                        if (rb_degree.Checked)
                            currentValue = currentValue * Math.PI / 180d;

                        currentValue = Math.Tan(currentValue);
                        break;
                    case 16:        // ctg
                        if (rb_degree.Checked)
                            currentValue = currentValue * Math.PI / 180d;

                        currentValue = 1 / Math.Tan(currentValue);
                        break;
                    case 17:        // arcsin
                        if (currentValue < -1 || currentValue > 1)
                            mainValueString = "Ulaz mora biti između -1 i 1";
                        else
                        {
                            currentValue = Math.Asin(currentValue);

                            if (rb_degree.Checked)
                                currentValue = currentValue * 180d / Math.PI;
                        }
                        break;
                    case 18:        // arccos
                        if (currentValue < -1 || currentValue > 1)
                            mainValueString = "Ulaz mora biti između -1 i 1";
                        else
                        {
                            currentValue = Math.Acos(currentValue);

                            if (rb_degree.Checked)
                                currentValue = currentValue * 180d / Math.PI;
                        }
                        break;
                    case 19:        // arctg
                        currentValue = Math.Atan(currentValue);

                        if (rb_degree.Checked)
                            currentValue = currentValue * 180d / Math.PI;
                        break;
                    case 20:        // arcctg
                        currentValue = Math.Atan(1 / currentValue);

                        if (rb_degree.Checked)
                            currentValue = currentValue * 180d / Math.PI;
                        break;
                    case 21:
                        currentValue = lastValue % currentValue;
                        break;
                    case 22:
                        currentValue /= 100;
                        break;
                }

                currentValue = Math.Round(currentValue, 8);

                if (currentOperation != 7 && currentOperation != 8)
                {
                    calc = false;
                    currentOperation = 0;
                }

                if (currentValue == (int)currentValue && mainValueString == String.Empty)
                    mainValueString = ((int)currentValue).ToString();
                else if (mainValueString == String.Empty)
                    mainValueString = currentValue.ToString();

                newInput = true;
                lastValue = currentValue;
                lb_display.Text = mainValueString;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Events

        #region Button Events

        #region Numbers
        private void btn_0_Click(object sender, EventArgs e)
        {
            try
            {
                if (newInput)
                {
                    mainValueString = "0";
                    newInput = false;
                }
                else if (mainValueString != "0")
                    mainValueString += "0";

                lb_display.Text = mainValueString;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddNumber(object sender, EventArgs e)
        {
            try
            {
                if (mainValueString != "0" && !newInput)
                    mainValueString += ((Button)sender).Text;
                else
                {
                    mainValueString = ((Button)sender).Text;
                    newInput = false;
                }

                lb_display.Text = mainValueString;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void BasicOperationEvent(object sender, EventArgs e)
        {
            try
            {
                Calculate();

                switch (((Button)sender).Name)
                {
                    case "btn_plus":
                        currentOperation = 1;
                        break;
                    case "btn_minus":
                        currentOperation = 2;
                        break;
                    case "btn_multiply":
                        currentOperation = 3;
                        break;
                    case "btn_divide":
                        currentOperation = 4;
                        break;
                    case "btn_mod":
                        currentOperation = 21;
                        break;
                    case "btn_percent":
                        currentOperation = 22;
                        break;
                }

                lastValue = Convert.ToDouble(mainValueString);

                newInput = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SimpleOperationEvent(object sender, EventArgs e)
        {
            try
            {
                switch (((Button)sender).Name)
                {
                    case "btn_square":
                        currentOperation = 5;
                        break;
                    case "btn_sqrt":
                        currentOperation = 6;
                        break;
                    case "btn_log":
                        currentOperation = 9;
                        break;
                    case "btn_ln":
                        currentOperation = 10;
                        break;
                    case "btn_factorial":
                        currentOperation = 11;
                        break;
                    case "btn_recipriocal":
                        currentOperation = 12;
                        break;
                    case "btn_sin":
                        currentOperation = 13;
                        break;
                    case "btn_cos":
                        currentOperation = 14;
                        break;
                    case "btn_tg":
                        currentOperation = 15;
                        break;
                    case "btn_ctg":
                        currentOperation = 16;
                        break;
                    case "btn_arcsin":
                        currentOperation = 17;
                        break;
                    case "btn_arccos":
                        currentOperation = 18;
                        break;
                    case "btn_arctg":
                        currentOperation = 19;
                        break;
                    case "btn_arcctg":
                        currentOperation = 20;
                        break;
                }

                Calculate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Delete/Clear
        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (mainValueString.Length > 1)
                    mainValueString = mainValueString.Substring(0, mainValueString.Length - 1);

                lb_display.Text = mainValueString;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            try
            {
                mainValueString = "0";
                lastValue = 0;
                calc = false;

                lb_display.Text = mainValueString;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Non-Operation Buttons
        private void btn_equals_Click(object sender, EventArgs e)
        {
            try
            {
                Calculate();

                currentOperation = 0;
                calc = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_plusMinus_Click(object sender, EventArgs e)
        {
            try
            {
                if (mainValueString.Length > 0 && mainValueString[0] == '-')
                    mainValueString = mainValueString.Substring(1);
                else
                    mainValueString = "-" + mainValueString;

                lb_display.Text = mainValueString;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_decimal_Click(object sender, EventArgs e)
        {
            try
            {
                if ((1.2d).ToString().Contains(',') && !mainValueString.Contains(','))
                    mainValueString += ",";
                else if ((1.2d).ToString().Contains('.') && !mainValueString.Contains('.'))
                    mainValueString += ".";

                newInput = false;

                lb_display.Text = mainValueString;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_pi_Click(object sender, EventArgs e)
        {
            try
            {
                mainValueString = Math.PI.ToString();

                lb_display.Text = mainValueString;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_e_Click(object sender, EventArgs e)
        {
            try
            {
                mainValueString = Math.E.ToString();

                lb_display.Text = mainValueString;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Nth Power & Root
        private void btn_nthPower_Click(object sender, EventArgs e)     // 7
        {
            try
            {
                currentOperation = 7;

                if (calc)
                    Calculate();
                else
                {
                    calc = true;
                    lastValue = Convert.ToDouble(mainValueString);
                }

                newInput = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_nthRoot_Click(object sender, EventArgs e)      // 8
        {
            try
            {
                currentOperation = 8;

                if (calc)
                    Calculate();
                else
                {
                    calc = true;
                    lastValue = Convert.ToDouble(mainValueString);
                }

                newInput = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #endregion

        #endregion

        private void Main_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    Calculate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetFocus(object sender, MouseEventArgs e)
        {
            try
            {
                this.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}