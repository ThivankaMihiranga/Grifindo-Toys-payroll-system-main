using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Grifindo_toys_pyroll_management
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-I3HMPT7;Initial Catalog=Grifindore_DB;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
            //center screen 
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private int incorrectAttempts = 0;
        private void button1_Click_1(object sender, EventArgs e)
        {
            con.Open();
            String LoginQuery = "Select username, password From user_db Where username='" + txtusr.Text + "' And password='" + txtpaw.Text + "' ";
            SqlCommand cmd = new SqlCommand(LoginQuery, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                button1.BackColor = Color.Green;
                MessageBox.Show("Login Success !", "Login Success !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                From_main frm = new From_main();
                frm.Show();
            }
            else
            {
                  incorrectAttempts++;

                  if (incorrectAttempts >= 5) //Exit 
                 {
                     MessageBox.Show("Too many incorrect attempts. The application will now exit.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     Application.Exit();
                 }
                 else
                 {
                   button1.BackColor = Color.Red;
                   MessageBox.Show("Incorrect Password. Please try again. Attempts left: {5 - incorrectAttempts}", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 }
                // MessageBox.Show("Invalid Login! Try Again!", "Invalid Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            con.Close();

            //login Button 
            //if (txtusr.Text == "Admin" && txtpaw.Text == "123")
            //{
            // Correct credentials
            // button1.BackColor = Color.Green;
            //  From_main frm = new From_main();
            //  frm.Show();
            // this.Hide();
            // }
            // else
            //{
            // Incorrect credentials
            //  incorrectAttempts++;

            //  if (incorrectAttempts >= 5) //Exit 
            // {
            //     MessageBox.Show("Too many incorrect attempts. The application will now exit.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //     Application.Exit();
            // }
            // else
            // {
            //   button1.BackColor = Color.Red;
            //   MessageBox.Show("Incorrect Password. Please try again. Attempts left: {5 - incorrectAttempts}", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            // }
        }

        private void button5_MouseClick(object sender, MouseEventArgs e)
        {
            var result = MessageBox.Show("Terminate Application ?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }

    
    
}
