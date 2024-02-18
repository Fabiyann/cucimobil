using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ujikom;

namespace cucimobil
{
    public partial class log_activity : Form
    {
        string id;
        data f = new data();
        public log_activity()
        {
            InitializeComponent();
        }

        void filtegdata()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=cucimobil_db");
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    using (DataTable dt = new DataTable("log"))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("SELECT l.id, l.id_user, u.nama, u.role, l.activity, l.created_at " + "FROM log l " + "JOIN users u ON l.id_user = u.id WHERE DATE (l.created_at) >= DATE (@fromdate) AND DATE (l.created_at) < DATE (@todate + INTERVAL 1 DAY)", conn))

                        {
                            cmd.Parameters.AddWithValue("@fromdate", dt1.Value.Date);
                            cmd.Parameters.AddWithValue("@todate", dt2.Value.Date);
                            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(cmd);
                            mySqlDataAdapter.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            // Memunculkan dialog konfirmasi sebelum keluar
            DialogResult result = MessageBox.Show("Apakah Anda yakin ingin keluar?", "Konfirmasi Keluar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Mencatat aktivitas logout
                f.command("insert into log (id_user, activity, created_at) VALUES ('" + data.id_user + "', 'Logout', NOW())");

                this.Hide(); // Menyembunyikan form ini
                new FormLogin().Show(); // Menampilkan form login
            }
        }

        private void log_activity_Load(object sender, EventArgs e)
        {
            string query = "SELECT l.id, l.id_user, u.nama, u.role, l.activity, l.created_at " + "FROM log l " + "JOIN users u ON l.id_user = u.id";

            f.showData(query, dataGridView1);
        }

        private void filter_Click(object sender, EventArgs e)
        {
            filtegdata();
        }
    }
}

