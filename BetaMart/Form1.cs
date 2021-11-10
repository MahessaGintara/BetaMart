using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;


namespace BetaMart
{
    public partial class Form1 : Form
    {
        OleDbConnection oleDbConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Mahessa Gintara\Documents\VisualStudioDatabase\BetaMart.accdb");
        int id_edit_item;

        int mouseX, mouseY;
        bool mouseM;
        
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        ///     Panel 1
        /// </summary>
        /// <param name="Magiro"></param>
        /// <param name="Magiro"></param>
        /// 

        private void home_visible_changed(object sender, EventArgs e)
        {
            if (panel1.Visible)
            {
                try
                {
                    oleDbConnection.Open();
                    string query = "SELECT * FROM Item";
                    OleDbDataAdapter adapter = new OleDbDataAdapter(query, oleDbConnection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    oleDbConnection.Close();
                }
            }
        }

        private void tombol_tambah_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int id_edit = Convert.ToInt32(Math.Round(numericUpDown5.Value, 0));
            try
            {
                oleDbConnection.Open();
                string query = "SELECT * FROM Item WHERE (ID_Barang = " + id_edit + ")";
                OleDbCommand command = new OleDbCommand(query, oleDbConnection);
                using(OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        panel1.Visible = false;
                        panel3.Visible = true;

                        id_edit_item = int.Parse(String.Format("{0}", reader["ID_barang"]));
                        textBox3.Text = String.Format("{0}", reader["nama_barang"]);
                        numericUpDown4.Value = int.Parse(String.Format("{0}", reader["harga_barang"]));
                        numericUpDown3.Value = int.Parse(String.Format("{0}", reader["jumlah_barang"]));
                    }
                    else
                    {
                        MessageBox.Show("Data Tidak Ditemukan", "Info");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                oleDbConnection.Close();
            }
        }
        /// <summary>
        ///     Panel 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        //tombol tambah
        private void button1_Click(object sender, EventArgs e)
        {
            string nama_barang = textBox2.Text;
            int harga_barang = Convert.ToInt32(Math.Round(numericUpDown1.Value, 0));
            int jumlah_barang = Convert.ToInt32(Math.Round(numericUpDown2.Value, 0));
            try
            {
                oleDbConnection.Open();
                string query = "INSERT INTO Item (nama_barang, harga_barang, jumlah_barang) " +
                    "VALUES ('" + nama_barang + "'," + harga_barang + "," + jumlah_barang + ")";
                OleDbCommand command = new OleDbCommand(query, oleDbConnection);
                command.ExecuteNonQuery();

                MessageBox.Show("Barang Berhasil Ditambahkan");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error" ,MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                oleDbConnection.Close();
                panel2.Visible = false;
                panel1.Visible = true;
            }
        }
        //tombol batal
        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
        }

        /// <summary>
        ///     Panel 3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel1.Visible = true;
        }

        private void button_edit_Click(object sender, EventArgs e)
        {
            string nama_barang = textBox3.Text;
            int harga_barang = Convert.ToInt32(Math.Round(numericUpDown4.Value, 0));
            int jumlah_barang = Convert.ToInt32(Math.Round(numericUpDown3.Value, 0));
            try
            {
                oleDbConnection.Open();
                string query = "UPDATE Item SET nama_barang = '" + nama_barang + "', harga_barang=" + harga_barang + ", jumlah_barang = " + jumlah_barang + " WHERE ID_Barang = " + id_edit_item + "";
                OleDbCommand command = new OleDbCommand(query, oleDbConnection);
                command.ExecuteNonQuery();

                MessageBox.Show("Barang Berhasil Diubah");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                oleDbConnection.Close();

                panel3.Visible = false;
                panel1.Visible = true;
            }
        }

        private void delete_button_Click(object sender, EventArgs e)
        {
            int id_delete = Convert.ToInt32(Math.Round(numericUpDown6.Value, 0));

            DialogResult dialogResult = MessageBox.Show("Anda yakin ingin menghapus id " + id_delete + "", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if(dialogResult == DialogResult.Yes)
            {
                try
                {
                    oleDbConnection.Open();
                    string query = "DELETE FROM item WHERE id_barang =" + id_delete;
                    OleDbCommand command = new OleDbCommand(query, oleDbConnection);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Barang Berhasil Dihapus");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    oleDbConnection.Close();
                    panel1.Visible = false;
                    panel1.Visible = true;
                }
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel4_MouseUp(object sender, MouseEventArgs e)
        {
            mouseM = false;
        }

        private void panel4_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseM)
            {
                SetDesktopLocation(MousePosition.X - mouseX, MousePosition.Y - mouseY);
            }
        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;
            mouseM = true;
        }
    }
}
