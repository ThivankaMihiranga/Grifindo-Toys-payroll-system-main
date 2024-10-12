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
    public partial class Lesves_form : Form
    {
        //database connection 
        string con_string = @"Data Source=DESKTOP-I3HMPT7;Initial Catalog=Grifindore_DB;Integrated Security=True";

        public Lesves_form()
        {
            InitializeComponent();
            // screen center 
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //insert button 
            try
            {
                SqlConnection con = new SqlConnection(con_string);
                con.Open();
                SqlCommand mycmd = new SqlCommand("insert into leaves(year,leaves,cycle_range,start_dt,end_dt) values(@year,@leaves,@cycle,@salcycleStart,@salCycleend)", con);

                mycmd.Parameters.AddWithValue("@year", textBox1.Text);
                mycmd.Parameters.AddWithValue("@leaves", textBox3.Text);
                mycmd.Parameters.AddWithValue("@cycle", textBox2.Text);
                mycmd.Parameters.AddWithValue("@salcycleStart", dateTimePicker1.Value.Date);
                mycmd.Parameters.AddWithValue("@salCycleend", dateTimePicker2.Value.Date);

                mycmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Inserted");
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //update button 
            try
            {
                SqlConnection con = new SqlConnection(con_string);
                con.Open();
                SqlCommand mycmd = new SqlCommand("update leaves set  year=@year,leaves=@leaves,cycle_range=@cycle,start_dt=@salcycleStart,end_dt=@salCycleend  where year=@year", con);
                mycmd.Parameters.AddWithValue("@year", textBox1.Text);
                mycmd.Parameters.AddWithValue("@leaves", textBox3.Text);
                mycmd.Parameters.AddWithValue("@cycle", textBox2.Text);
                mycmd.Parameters.AddWithValue("@salcycleStart", dateTimePicker1.Value.Date);
                mycmd.Parameters.AddWithValue("@salCycleend", dateTimePicker2.Value.Date);


                mycmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Updated");
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
                SqlCommand mycmd = new SqlCommand("delete from leaves where year=@year;", con);
                mycmd.Parameters.AddWithValue("@year", textBox1.Text);  
                
                mycmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Delete");
            }
            catch (Exception ee)
            {
                MessageBox.Show("Error");

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //data show button 
            try
            {
                SqlConnection con = new SqlConnection(con_string);
                con.Open();
                SqlCommand mycmd = new SqlCommand("select*from leaves", con);
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

        private void Lesves_form_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            //exit button 
            // exit massege show button 
            var result = MessageBox.Show("Are you shour do you wantto Exit", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //to the form main 
            From_main frm = new From_main();
            frm.Show();
            this.Hide();
        }
    }
}
