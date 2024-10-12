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
    public partial class Employee_from : Form
    {
        // Database connection
        private string con_string = @"Data Source=DESKTOP-I3HMPT7;Initial Catalog=Grifindore_DB;Integrated Security=True";

        public Employee_from()
        {
            InitializeComponent();
            // Center screen
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Insert Button
            try
            {
                if (int.TryParse(textBox6.Text, out int allowance) &&
                    int.TryParse(textBox8.Text, out int contact) &&
                    int.TryParse(textBox9.Text, out int govtax))
                {
                    using (SqlConnection con = new SqlConnection(con_string))
                    {
                        con.Open();
                        using (SqlCommand mycmd = new SqlCommand(
                            "INSERT INTO employee (eid, name, address, dob, bsal, allowance, otrate, gender, nic, Contact, Govtax) " +
                            "VALUES (@eid, @name, @address, @dob, @bsal, @allowance, @otrate, @gender, @nic, @contact, @govtax)", con))
                        {
                            mycmd.Parameters.AddWithValue("@eid", textBox1.Text);
                            mycmd.Parameters.AddWithValue("@name", textBox2.Text);
                            mycmd.Parameters.AddWithValue("@address", textBox3.Text);
                            mycmd.Parameters.AddWithValue("@dob", dateTimePicker1.Value.Date);
                            mycmd.Parameters.AddWithValue("@bsal", decimal.Parse(textBox4.Text)); // Assuming bsal is a decimal
                            mycmd.Parameters.AddWithValue("@otrate", decimal.Parse(textBox5.Text)); // Assuming otrate is a decimal
                            mycmd.Parameters.AddWithValue("@allowance", allowance);
                            mycmd.Parameters.AddWithValue("@nic", textBox7.Text);
                            mycmd.Parameters.AddWithValue("@contact", contact);
                            mycmd.Parameters.AddWithValue("@gender", comboBox1.Text);
                            mycmd.Parameters.AddWithValue("@govtax", govtax);

                            mycmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Inserted successfully.");
                }
                else
                {
                    MessageBox.Show("Please enter valid numeric values for Allowance, Contact, and Govtax.");
                }
            }
            catch (FormatException fe)
            {
                MessageBox.Show($"Input format error: {fe.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting data: {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //update button 
            try
            {
                SqlConnection con = new SqlConnection(con_string);
                con.Open();
                SqlCommand mycmd = new SqlCommand
                ("update employee set eid=@eid, name=@name, address=@address ,dob=@dob ,bsal=@bsal ,otrate=@otrate ,allowance=@allowance ,gender=@gender ,nic=@nic ,Contact=@contact ,Govtax=@govtax where eid=@eid", con);
                mycmd.Parameters.AddWithValue("@eid", textBox1.Text);
                mycmd.Parameters.AddWithValue("@name", textBox2.Text);
                mycmd.Parameters.AddWithValue("@address", textBox3.Text);
                mycmd.Parameters.AddWithValue("@dob", dateTimePicker1.Value.Date);
                mycmd.Parameters.AddWithValue("@bsal", textBox4.Text);
                mycmd.Parameters.AddWithValue("@otrate", textBox5.Text);
                mycmd.Parameters.AddWithValue("@allowance", textBox6.Text);
                mycmd.Parameters.AddWithValue("@nic", textBox7.Text);
                mycmd.Parameters.AddWithValue("@contact", textBox8.Text);
                mycmd.Parameters.AddWithValue("@gender", comboBox1.Text);
                mycmd.Parameters.AddWithValue("@govtax", textBox9.Text);

                mycmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Update");
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
                SqlCommand mycmd = new SqlCommand("delete from employee where eid=@eid;", con);
                mycmd.Parameters.AddWithValue("@eid", textBox1.Text);
                mycmd.Parameters.AddWithValue("@name", textBox2.Text);
                mycmd.Parameters.AddWithValue("@address", textBox3.Text);
                mycmd.Parameters.AddWithValue("@dob", dateTimePicker1.Value.Date);
                mycmd.Parameters.AddWithValue("@bsal", textBox4.Text);
                mycmd.Parameters.AddWithValue("@allowance", textBox5.Text);
                mycmd.Parameters.AddWithValue("@nic", textBox7.Text);
                mycmd.Parameters.AddWithValue("@contact", textBox8.Text);
                mycmd.Parameters.AddWithValue("@gender", comboBox1.Text);
                mycmd.Parameters.AddWithValue("@govtax", textBox9.Text);

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
            //serch button 
            try
            {
                SqlConnection con = new SqlConnection(con_string);
                con.Open();
                SqlCommand mycmd = new SqlCommand("select*from Employee", con);
                SqlDataAdapter ad = new SqlDataAdapter(mycmd);
                DataTable dt = new DataTable();
                ad.Fill(dt);

                dataGridView1.DataSource = dt;
                con.Close();

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void Employee_from_Load(object sender, EventArgs e)
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
            //Back button 
            From_main frm = new From_main();
            frm.Show();
            this.Hide();
        }

        private void button7_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox1.Text == "")
                MessageBox.Show("please fill");
        }

        private void button8_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox2.Text == "")
                MessageBox.Show("gfwqdetyu");
        }

        private void button9_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }
    }
}
