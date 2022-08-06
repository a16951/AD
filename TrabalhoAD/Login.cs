using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using Npgsql;



namespace TrabalhoAD
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

        }

        string connstring = String.Format("Server=127.0.0.1;Port=50180;" + "User Id=postgres;Password=123456789joao;Database=snsDB;");
        String teste = String.Format("Server=127.0.0.1;Port=50180;" + "User Id=postgres;Password=123456789joao;Database=snsDB;");



        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wnsg, int wparam, int lparam);

        private void txtuser_Enter(object sender, EventArgs e)
        {
            if (txtuser.Text == "USER")
            {
                txtuser.Text = "";
                txtuser.ForeColor = Color.LightGray;
            }
        }

        private void txtuser_Leave(object sender, EventArgs e)
        {
            if (txtuser.Text == "")
            {
                txtuser.Text = "USER";
                txtuser.ForeColor = Color.DimGray;
            }
        }

        private void txtpass_Enter(object sender, EventArgs e)
        {
            if (txtpass.Text == "PASSWORD")
            {
                txtpass.Text = "";
                txtpass.ForeColor = Color.LightGray;
                txtpass.UseSystemPasswordChar = true;
            }
        }

        private void txtpass_Leave(object sender, EventArgs e)
        {
            if (txtpass.Text == "")
            {
                txtpass.Text = "PASSWORD";
                txtpass.ForeColor = Color.DimGray;
                txtpass.UseSystemPasswordChar = false;
            }
        }

        private void btfechar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void txtuser_TextChanged(object sender, EventArgs e)
        {

        }

        private void lineShape1_Click(object sender, EventArgs e)
        {

        }

        private void btlogin_Click(object sender, EventArgs e)
        {

            NpgsqlConnection con = new NpgsqlConnection(connstring);
            con.ConnectionString = "Host=localhost;Username=postgres;Password=123456789joao;Database=snsDB";
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                String query = "select username,password from admin WHERE username= '" + txtuser.Text + "' and password='" + txtpass.Text + "'";
                NpgsqlCommand sql = new NpgsqlCommand(query, con);
                try
                {
                    NpgsqlDataReader dr = sql.ExecuteReader();
                    dr.Read();
                   
                    if (dr.HasRows)
                    {
                        
                        this.Hide();

                        BemVindo f = new BemVindo(this);
                        f.ShowDialog();

                        Base yo = new Base();
                        yo.Show();
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                        MessageBox.Show("Login Inválido");
                    }
                    dr.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Login Inválido");
                }
            }
            con.Close();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
