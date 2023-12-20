using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace OtoSpace
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btngiris_Click(object sender, EventArgs e)
        {
            string connectionString="";
            string plaka = txt1.Text;
            DateTime girisSaati = DateTime.Now;

            if (!string.IsNullOrEmpty(plaka))
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string query = "";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("", plaka);
                        command.Parameters.AddWithValue("", girisSaati);


                        try
                        {
                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Plaka ve giriş saati başarıyla kaydedildi.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Hata oluştu: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir plaka girin.");
            }
        }

        private void btnucret_Click(object sender, EventArgs e)
        {
            string connectionString = "";

            string plaka = txt1.Text;
            DateTime cikissaati = DateTime.Now;
            decimal ucret = 0;

            if (!string.IsNullOrEmpty(plaka))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("", plaka);

                        try
                        {
                            connection.Open();
                            object result = command.ExecuteScalar();

                            if (result != null)
                            {
                                DateTime girisSaati = Convert.ToDateTime(result);
                                TimeSpan parkSuresi = cikissaati - girisSaati;

                                
                                ucret = Convert.ToDecimal(Math.Ceiling(parkSuresi.TotalHours)) * 10;

                                //MessageBox oluşturulacak
                            }
                            else
                            {
                                MessageBox.Show("Giriş yapılmış bir kayıt bulunamadı.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Hata oluştu: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir plaka girin.");
            }
        }

        private void btnkayıt_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;";

            string adSoyad = txtad.Text;
            string tcNo = txttc.Text;
            string telefon = txttel.Text;
            string adres = txtadres.Text;
            string arabaMarkasi = txtmarka.Text;
            string model = txtmodel.Text;
            string renk = txtrenk.Text;
            string plaka = txtplaka.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO ArabaBilgileri (AdSoyad, TcNo, Telefon, Adres, ArabaMarkasi, Model, Renk, Plaka) " +
                               "VALUES (@AdSoyad, @TcNo, @Telefon, @Adres, @ArabaMarkasi, @Model, @Renk, @Plaka)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AdSoyad", adSoyad);
                    command.Parameters.AddWithValue("@TcNo", tcNo);
                    command.Parameters.AddWithValue("@Telefon", telefon);
                    command.Parameters.AddWithValue("@Adres", adres);
                    command.Parameters.AddWithValue("@ArabaMarkasi", arabaMarkasi);
                    command.Parameters.AddWithValue("@Model", model);
                    command.Parameters.AddWithValue("@Renk", renk);
                    command.Parameters.AddWithValue("@Plaka", plaka);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Kayıt başarıyla eklendi.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata oluştu: " + ex.Message);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // VIP üye fotoğrafı eklemek için kod yazılacak
        }
    }
    }
}