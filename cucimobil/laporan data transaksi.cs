using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ujikom;

namespace cucimobil
{
    public partial class laporan_data_transaksi : Form
    {
        string id;
        data f = new data();
        public laporan_data_transaksi()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // Mencatat aktivitas admin mengedit pengguna
                f.command("insert into log (id_user, activity, created_at) VALUES ('" + data.id_user + "', 'Logout', NOW())");

                new Dictionary<string, object>
                    {
                {"@id_user", data.id_user},
                {"@activity", "Admin melakukan aktivitas tertentu"} // Ganti dengan aktivitas yang sesuai
                    };

                MessageBox.Show("Log aktivitas berhasil dicatat.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat mencatat log aktivitas: " + ex.Message);
            }
        }
    }
}
