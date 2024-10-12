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
    public partial class Form2 : Form
    {
        //database connection 
        string con_string = @"Data Source=DESKTOP-I3HMPT7;Initial Catalog=Grifindore_DB;Integrated Security=True";
        public Form2()
        {
            InitializeComponent();
            //screen senter 
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button3_MouseClick(object sender, MouseEventArgs e)
        {
            //Variable declaration
            float overtime, nopay, monthlysal, overallattendance, cycledaterange,
            absentdays, basepay, allowance, otrate, othrs, grosspay, taxrate, tax;

            overallattendance = float.Parse(textBox10.Text);
            cycledaterange = float.Parse(textBox1.Text);
            absentdays = float.Parse(textBox8.Text);
            monthlysal = float.Parse(textBox3.Text);
            //NOPAY CALCULATION 
            if (overallattendance < cycledaterange)
            {
                nopay = (monthlysal / cycledaterange) * absentdays;
                textBox17.Text = nopay.ToString();
            }

            //BASE PAY CALCULATION
            allowance = float.Parse(textBox5.Text);
            otrate = float.Parse(textBox4.Text);
            othrs = float.Parse(textBox11.Text);
            overtime = othrs * otrate;
            basepay = monthlysal + allowance + overtime;
            textBox14.Text = basepay.ToString();
            textBox13.Text = overtime.ToString();
            //GROSS PAY CALCULATION
            taxrate = float.Parse(textBox15.Text);
            nopay = float.Parse(textBox17.Text);
            tax = basepay * (taxrate / 100);
            grosspay = basepay - (nopay + tax);
            textBox16.Text = grosspay.ToString();

        }

        private void button4_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(con_string);
                con.Open();
                SqlCommand mycmd = new SqlCommand("insert into Salary_tb(EID,BasePay,GovTax,GossPay) values(@EID,@BasePay,@GovTax,@GossPay)", con);

                mycmd.Parameters.AddWithValue("@EID", textBox6.Text);
                mycmd.Parameters.AddWithValue("@BasePay", textBox14.Text);
                mycmd.Parameters.AddWithValue("@GovTax", textBox15.Text);
                mycmd.Parameters.AddWithValue("@GossPay", textBox16.Text);

                mycmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Inserted");
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);

            }
        }

        private void button2_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(con_string))
                {
                    con.Open();
                    using (SqlCommand mycmd = new SqlCommand("SELECT name,bsal,otrate,allowance,Govtax FROM employee WHERE eid = @eid", con))
                    {
                        mycmd.Parameters.AddWithValue("@eid", textBox6.Text); 

                        using (SqlDataReader reader = mycmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                textBox7.Text = reader["name"].ToString();
                                textBox3.Text = reader["bsal"].ToString();
                                textBox4.Text = reader["otrate"].ToString();
                                textBox5.Text = reader["allowance"].ToString();
                                textBox15.Text = reader["Govtax"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Employee not found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void button6_MouseClick(object sender, MouseEventArgs e)
        {
            From_main frm = new From_main();
            frm.Show();
            this.Hide();
        }

        private void button7_MouseClick(object sender, MouseEventArgs e)
        {
            //exit button 
            var result = MessageBox.Show("Terminate Application ?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(con_string))
                {
                    con.Open();
                    using (SqlCommand mycmd = new SqlCommand("SELECT leaves,Cycle_range,Start_dt,end_dt FROM leaves WHERE year = @year", con))
                    {
                        mycmd.Parameters.AddWithValue("@year", textBox20.Text);

                        using (SqlDataReader reader = mycmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                textBox2.Text = reader["leaves"].ToString();
                                textBox1.Text = reader["Cycle_range"].ToString();
                                textBox18.Text = reader["Start_dt"].ToString();
                                textBox19.Text = reader["end_dt"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Year Data not found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

    }
}
