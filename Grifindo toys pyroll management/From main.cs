using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grifindo_toys_pyroll_management
{
    public partial class From_main : Form
    {
        public From_main()
        {
            InitializeComponent();
            //screen center 
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //to the employee 
            Employee_from frm = new Employee_from();
            frm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //to the leaves 
            Lesves_form frm = new Lesves_form();
            frm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //to the salary form 
            Form2 frm = new Form2();
            frm.Show();
            this.Hide();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // to the month salary 
            Month_salary_from frm = new Month_salary_from();
            frm.Show();
            this.Hide();
        }

        private void From_main_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Exit Button 
            var result = MessageBox.Show("Terminate Application ?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button6_MouseClick(object sender, MouseEventArgs e)
        {
            salary_from frm = new salary_from();
            frm.Show();
            this.Hide();
        }
    }
}
