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
    public partial class Products : Form
    {
        string id;
        data f = new data();
        public Products()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void ButtonK_Click(object sender, EventArgs e)
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

        private void Products_Load(object sender, EventArgs e)
        {

        }

        private void ButtonA_Click(object sender, EventArgs e)
        {
            this.Hide(); // Menyembunyikan form ini
            new kelolapengguna().Show(); // Menampilkan form login
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
