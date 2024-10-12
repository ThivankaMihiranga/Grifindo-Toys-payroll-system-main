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
    public partial class Month_salary_from : Form
    {
        //databse connection 
        string con_string = @"Data Source=DESKTOP-I3HMPT7;Initial Catalog=Grifindore_DB;Integrated Security=True";


        //variables 
        double TotalNumberOfDays = 0;
        double OTRates = 0;
        double OTHours = 0;
        double OTAmount = 0;
        double BasicSalary = 0;
        double NoOfAbsentDays = 0;
        double NoPayValue = 0;
        double Allowance = 0;
        double GovermentTaxRate = 0;

        double TotalAllowance = 0;
        double TaxAmount = 0;
        double TotalDeduction = 0;
        double NetPay = 0;

        public void calculate()
        {
            try
            {//salary deatials
                SqlConnection con = new SqlConnection(con_string);
                con.Open();
                SqlCommand mycmd = new SqlCommand("select noDays,taxRate from salCycle where year=@year and month=@month", con);
                mycmd.Parameters.AddWithValue("@year", comboBox1.SelectedItem.ToString());
                mycmd.Parameters.AddWithValue("@month", comboBox2.SelectedItem.ToString());
                SqlDataReader r = mycmd.ExecuteReader();

                while (r.Read())
                {
                    TotalNumberOfDays = double.Parse(r["noDays"].ToString());
                    GovermentTaxRate = double.Parse(r["taxRate"].ToString());

                }
                con.Close();
                //employee deatials

                con.Open();
                mycmd = new SqlCommand("select bsal,otrate,allowance from employee where eid=@eid", con);
                mycmd.Parameters.AddWithValue("@eid", comboBox3.SelectedItem.ToString());
                r = mycmd.ExecuteReader();

                while (r.Read())
                {
                    BasicSalary = double.Parse(r["bsal"].ToString());
                    Allowance = double.Parse(r["allowance"].ToString());
                    OTRates = double.Parse(r["otrate"].ToString());

                }
                con.Close();

                //calculations
                OTHours = double.Parse(textBox1.Text);
                OTAmount = Math.Round(OTRates * OTHours, 2);

                NoOfAbsentDays = double.Parse(textBox3.Text);
                NoPayValue = Math.Round((BasicSalary / TotalNumberOfDays) * NoOfAbsentDays, 2);

                TotalAllowance = BasicSalary + Allowance + OTAmount;

                TaxAmount = Math.Round(TotalAllowance * GovermentTaxRate / 100, 2);
                TotalDeduction = NoPayValue + TaxAmount;

                NetPay = TotalAllowance - TotalDeduction;

                netsal.Text = NetPay.ToString();
                MessageBox.Show("Net salary:" + NetPay.ToString());
                
                

            }
            catch (Exception ee)
            {
                MessageBox.Show("Error");
            }
        }



        public Month_salary_from()
        {
            InitializeComponent();
            //screen center 
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                calculate();
                // Validate input values
                if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null || comboBox3.SelectedItem == null)
                {
                    MessageBox.Show("Please select all required fields.");
                    return;
                }

                if (!int.TryParse(textBox1.Text, out int otHrs))
                {
                    MessageBox.Show("Invalid overtime hours value.");
                    return;
                }

                if (!int.TryParse(textBox3.Text, out int abDays))
                {
                    MessageBox.Show("Invalid absence days value.");
                    return;
                }

                if (!decimal.TryParse(netsal.Text, out decimal netSal))
                {
                    MessageBox.Show("Invalid net salary value.");
                    return;
                }               

                // Insert the calculated values into the database
                using (SqlConnection con = new SqlConnection(con_string))
                {
                    con.Open();
                    string query = "INSERT INTO salary (year, month, eid, OThrs, abDays, netsal) VALUES (@year, @month, @eid, @OThrs, @abDays, @netsal)";
                    using (SqlCommand mycmd = new SqlCommand(query, con))
                    {
                        mycmd.Parameters.AddWithValue("@year", comboBox1.SelectedItem.ToString());
                        mycmd.Parameters.AddWithValue("@month", comboBox2.SelectedItem.ToString());
                        mycmd.Parameters.AddWithValue("@eid", comboBox3.SelectedItem.ToString());
                        mycmd.Parameters.AddWithValue("@OThrs", otHrs);
                        mycmd.Parameters.AddWithValue("@abDays", abDays);
                        mycmd.Parameters.AddWithValue("@netsal", netSal);

                        mycmd.ExecuteNonQuery();
                        MessageBox.Show("Record inserted successfully.");
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            calculate();
            try
            {
                SqlConnection con = new SqlConnection(con_string);
                con.Open();
                SqlCommand mycmd = new SqlCommand
                ("update salary set OThrs=@OThrs,abDays=@abDays,netsal=@netsal where year=@year and month=@month and eid=@eid", con);
                mycmd.Parameters.AddWithValue("@year", comboBox1.SelectedItem.ToString());
                mycmd.Parameters.AddWithValue("@month", comboBox2.SelectedItem.ToString());
                mycmd.Parameters.AddWithValue("@eid", comboBox3.SelectedItem.ToString());
                mycmd.Parameters.AddWithValue("@OThrs", textBox1.Text);
                mycmd.Parameters.AddWithValue("@abDays", textBox3.Text);
                mycmd.Parameters.AddWithValue("@netsal", label2.Text);

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
            // delete button 
            try
            {
                SqlConnection con = new SqlConnection(con_string);
                con.Open();
                SqlCommand mycmd = new SqlCommand("delete from salary where year=@year and month=@month and eid=@eid", con);
                mycmd.Parameters.AddWithValue("@year", comboBox1.SelectedItem.ToString());
                mycmd.Parameters.AddWithValue("@month", comboBox2.SelectedItem.ToString());
                mycmd.Parameters.AddWithValue("@eid", comboBox3.SelectedItem.ToString());

                mycmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Deleted");
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // show salary details 
            try
            {
                SqlConnection con = new SqlConnection(con_string);
                con.Open();
                SqlCommand mycmd = new SqlCommand("select*from salary", con);
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

        private void Month_salary_from_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(con_string);
                con.Open();

                SqlCommand mycmd = new SqlCommand("SELECT year FROM salCycle ", con);
                SqlDataReader r = mycmd.ExecuteReader();

                while (r.Read())
                {
                    comboBox1.Items.Add(r["year"].ToString());

                }

                con.Close();

                con.Open();

                mycmd = new SqlCommand("SELECT month FROM salCycle ", con);

                r = mycmd.ExecuteReader();

                while (r.Read())
                {
                    comboBox2.Items.Add(r["month"].ToString());
                }

                con.Close();

                con.Open();

                mycmd = new SqlCommand("SELECT eid FROM employee ", con);

                r = mycmd.ExecuteReader();

                while (r.Read())
                {
                    comboBox3.Items.Add(r["eid"].ToString());
                }

                con.Close();

            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // exit button and massege show 
            var result = MessageBox.Show("Terminate Application ?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
