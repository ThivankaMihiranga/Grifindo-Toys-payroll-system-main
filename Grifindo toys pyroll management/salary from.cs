using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//add sql databse 
using System.Data.Sql;
using System.Data.SqlClient;



namespace Grifindo_toys_pyroll_management
{


    public partial class salary_from : Form
    {
        //database connection 
        string con_string = @"Data Source=DESKTOP-I3HMPT7;Initial Catalog=Grifindore_DB;Integrated Security=True";

        public salary_from()
        {
            InitializeComponent();
            //screen senter 
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //insert button 
            try
            {
                int noDays = (int)(dpend.Value - dpstart.Value).TotalDays;
                SqlConnection con = new SqlConnection(con_string);
                con.Open();
                SqlCommand mycmd = new SqlCommand
                ("insert into salCycle(year,month,salcycleStart,salcyCleend,noDays,taxRate) values(@year,@month,@salcycleStart,@salCycleend,@noDays,@taxRate)", con);
                mycmd.Parameters.AddWithValue("@year", textBox1.Text);
                mycmd.Parameters.AddWithValue("@month", textBox2.Text);
                mycmd.Parameters.AddWithValue("@salcycleStart",dpstart.Value.Date);
                mycmd.Parameters.AddWithValue("@salCycleend", dpend.Value.Date);
                mycmd.Parameters.AddWithValue("@noDays", noDays);
                mycmd.Parameters.AddWithValue("@taxRate", textBox3.Text);

                mycmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Inserted");
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //delete button 
            try
            {
                SqlConnection con = new SqlConnection(con_string);
                con.Open();
                SqlCommand mycmd = new SqlCommand("delete from salCycle where year=@year ", con);
                mycmd.Parameters.AddWithValue("@year", textBox1.Text);
                mycmd.Parameters.AddWithValue("@month", textBox2.Text);


                mycmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Delete");
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //show salcucle details 
            try
            {
                SqlConnection con = new SqlConnection(con_string);
                con.Open();
                SqlCommand mycmd = new SqlCommand("select*from salCycle", con);
                SqlDataAdapter ad = new SqlDataAdapter(mycmd);
                DataTable dt = new DataTable();
                ad.Fill(dt);

                dataGridView1.DataSource = dt;
                con.Close();

            }
            catch (Exception ee)
            {
                MessageBox.Show("Error");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Update button 
            try
            {
                int noDays = (int)(dpend.Value - dpstart.Value).TotalDays;
                SqlConnection con = new SqlConnection(con_string);
                con.Open();
                SqlCommand mycmd = new SqlCommand
                ("update salCycle set  year=@year, month=@month ,salcycleStart=@salcycleStart ,salCycleend=@salCycleend ,noDays=@noDays,taxRate=@taxRate where year=@year", con);
                mycmd.Parameters.AddWithValue("@year", textBox1.Text);
                mycmd.Parameters.AddWithValue("@month", textBox2.Text);
                mycmd.Parameters.AddWithValue("@salcycleStart", dpstart.Value.Date);
                mycmd.Parameters.AddWithValue("@salCycleend", dpend.Value.Date);
                mycmd.Parameters.AddWithValue("@noDays", noDays);
                mycmd.Parameters.AddWithValue("@taxRate", textBox3.Text);

                mycmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Inserted");
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);

            }
        }

        private void salary_from_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            //exit button 
            var result = MessageBox.Show("Terminate Application ?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // show main form 
            From_main frm = new From_main();
            frm.Show();
            this.Hide();
        }
    }
}
