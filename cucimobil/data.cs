using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ujikom
{
    internal class data
    {
        public static string role;
        MySqlConnection conn = new MySqlConnection("server=localhost;port=3306;username=root;password=;database=cucimobil_db");
        private Form activateForm;
        public static string username = "";
        public static string id_user = "";

        public void openChildForm(Form childForm, Panel panel, object btnSender)
        {
            if (activateForm != null)
            {
                activateForm.Close();
            }

            activateForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panel.Controls.Add(childForm);
            panel.Tag = childForm;

            childForm.BringToFront();
            childForm.Show();

        }


        public void command(String query)
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        public void showData(string query, DataGridView dg)
        {
            try
            {
                conn.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                sda.Fill(dt);
                dg.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void getDatarole(ComboBox cb)
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select distinct role from users", conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                cb.Items.Clear(); // Clear existing items before adding new ones

                while (reader.Read())
                {
                    string data = reader["role"].ToString();

                    // Check if the role is not already present in the ComboBox before adding it
                    if (!cb.Items.Contains(data))
                    {
                        cb.Items.Add(data);
                    }
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        public void getDatapaket(ComboBox cb)
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select distinct nama_produk from products", conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                cb.Items.Clear(); // Clear existing items before adding new ones

                while (reader.Read())
                {
                    string data = reader["nama_produk"].ToString();

                    // Check if the role is not already present in the ComboBox before adding it
                    if (!cb.Items.Contains(data))
                    {
                        cb.Items.Add(data);
                    }
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void disable(KeyPressEventArgs e, object sender)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}