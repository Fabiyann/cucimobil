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
    public partial class FormLogin : Form
    {
        // Deklarasi koneksi ke database MySQL
        MySqlConnection conn = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=cucimobil_db");

        // Membuat objek dari class data
        data f = new data();

        // Fungsi untuk melakukan proses login
        void login()
        {
            try
            {
                // Membuka koneksi ke database
                conn.Open();

                // Membuat adapter data MySQL untuk menjalankan query login
                MySqlDataAdapter sda = new MySqlDataAdapter("SELECT * From users WHERE username='" + username.Text + "'AND password='" + password.Text + "'", conn);

                // Membuat objek DataTable untuk menampung hasil query
                DataTable dt = new DataTable();

                // Mengisi DataTable dengan hasil query
                sda.Fill(dt);

                // Memeriksa apakah terdapat hasil dari query (pengguna ditemukan)
                if (dt.Rows.Count > 0)
                {
                    // Jika ada, lakukan iterasi untuk setiap baris data
                    foreach (DataRow dr in dt.Rows)
                    {
                        // Mendapatkan role dan id_user dari data pengguna yang login
                        data.role = dr["role"].ToString();
                        data.id_user = dr["id"].ToString();

                        // Menyimpan log aktivitas login ke database
                        f.command("insert into log (id_user, activity, created_at) VALUES ('" + data.id_user + "', 'Login', NOW())");

                        // Menampilkan pesan sukses login
                        MessageBox.Show("login sukses !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Menavigasikan pengguna ke form yang sesuai berdasarkan peran (role)
                        if (data.role == "admin")
                        {
                            this.Hide();
                            new kelolapengguna().Show();
                        }
                        else if (data.role == "kasir")
                        {
                            this.Hide();
                            new transaksi().Show();
                        }
                        else if (data.role == "owner")
                        {
                            this.Hide();
                            new log_activity().Show();
                        }
                    }
                }
                else
                {
                    // Jika tidak ada hasil dari query, tampilkan pesan akun salah
                    MessageBox.Show("Akun Yang Anda Masukan Salah", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            // Menangani exception jika terjadi kesalahan dalam proses login
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            // Menutup koneksi ke database setelah selesai menggunakan
            finally
            {
                conn.Close();
            }
        }
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

        private void ButtonL_Click(object sender, EventArgs e)
        {
            // Memeriksa apakah bidang nama pengguna dan sandi telah diisi
            if (username.Text == "" || password.Text == "")
            {
                // Menampilkan pesan peringatan jika ada bidang yang kosong
                MessageBox.Show("Semua kolom harus di isi!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Memanggil fungsi login jika semua bidang telah diisi
                login();
            }
        }
    }
}
