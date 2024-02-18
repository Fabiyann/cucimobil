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
    public partial class kelolapengguna : Form
    {
        public kelolapengguna()
        {
            InitializeComponent();
            button1.Enabled = true;
        }
        data f = new data(); 
        string id = "";
        private void kelolapengguna_Load(object sender, EventArgs e)
        {
            f.showData("select * from users", dataGridView1); 
            f.getDatarole(cbrole);
            button5.Click += button5_Click;
        }
       
       


        // Fungsi untuk mengosongkan nilai dari textbox dan menampilkan data pengguna dari database
        void clear()
        {
            txtus.Text = string.Empty;
            txtnama.Text = string.Empty;
            txtkatasandi.Text = string.Empty;
            cbrole.Text = string.Empty;
            button1.Enabled = true; // Mengaktifkan kembali tombol 'buat'
            f.showData("select * from users", dataGridView1); // Menampilkan data pengguna di DataGridView
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

        private void button6_Click(object sender, EventArgs e)
        {
            f.command("insert into log (id_user, activity, created_at) VALUES ('" + data.id_user + "', 'Admin Menghapus Pengguna', NOW())");
            // Memeriksa apakah kolom teks kosong atau tidak
            if (txtus.Text == string.Empty || txtnama.Text == string.Empty || txtkatasandi.Text == string.Empty || cbrole.Text == string.Empty)
            {
                MessageBox.Show("Semua Kolom Harus Di Isi!");
            }
            else
            {
                // Mencatat aktivitas admin menghapus pengguna
               

                // Query untuk menghapus pengguna yang dipilih dari database
                f.command("delete from users where username = '" + txtus.Text + "'");

                clear(); // Mengosongkan nilai textbox dan menampilkan data pengguna terbaru
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            f.command("insert into log (id_user, activity, created_at) VALUES ('" + data.id_user + "', 'Admin Mengedit Pengguna', NOW())");
            if (txtus.Text == string.Empty || txtkatasandi.Text == string.Empty || txtnama.Text == string.Empty || cbrole.Text == string.Empty)
            {

            }
            else
            {
                // Query untuk mengubah data pengguna yang dipilih di database
                f.command("update users SET username = '" + txtus.Text + "', password = '" + txtkatasandi.Text + "', nama = '" + txtnama.Text + "', role = '" + cbrole.Text + "', updated_ad = NOW() WHERE id = '" + id + "'");
                clear(); // Mengosongkan nilai textbox dan menampilkan data pengguna terbaru
            }
        }
            
            
        

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           

         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Memeriksa apakah kolom teks kosong atau tidak
            if (txtus.Text == string.Empty || txtkatasandi.Text == string.Empty || txtnama.Text == string.Empty || cbrole.Text == string.Empty)
            {
                MessageBox.Show("Semua Kolom Harus Di Isi!");
            }
            else
            {
                // Mencatat aktivitas admin menambahkan pengguna
                f.command("insert into log (id_user, activity, created_at) VALUES ('" + data.id_user + "', 'Admin Menambahkan Pengguna', NOW())");

                // Query untuk menambahkan pengguna baru ke database
                f.command( "insert INTO users ( username, password, nama, role, created_at) VALUES ( '" + txtus.Text + "', '" + txtkatasandi.Text + "', '" + txtnama.Text + "', '" + cbrole.Text + "', NOW())");
               // Eksekusi query

                clear(); // Mengosongkan nilai textbox dan menampilkan data pengguna terbaru
            }
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {  
            DataGridViewRow dr = this.dataGridView1.Rows[e.RowIndex];
            txtus.Text = dr.Cells[1].Value.ToString();
            txtnama.Text = dr.Cells[3].Value.ToString();
            txtkatasandi.Text = dr.Cells[2].Value.ToString();
            cbrole.Text = dr.Cells[4].Value.ToString();
            id = dr.Cells[0].Value.ToString();
        }

        private void ButtonP_Click(object sender, EventArgs e)
        {
            this.Hide(); // Menyembunyikan form ini
            new Products().Show(); // Menampilkan form login
        }
    }
}
    