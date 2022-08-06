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

namespace TrabalhoAD
{
    public partial class Base : Form
    {
        public Base()
        {
            InitializeComponent();
            panel1.Height = button1.Height;
            hospitais1.BringToFront();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wnsg, int wparam, int lparam);


        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void btFechar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Height = button1.Height;
            panel1.Top = button1.Top;
            hospitais1.BringToFront();
        }

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void BarraTitulo_MouseEnter(object sender, EventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Height = button2.Height;
            panel1.Top = button2.Top;
            materiais1.BringToFront();
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Height = button3.Height;
            panel1.Top = button3.Top;
            funcionarios1.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Height = button4.Height;
            panel1.Top = button4.Top;
            listatransporte1.BringToFront();
        }

     

       

        private void Logout_Click(object sender, EventArgs e)
        {
            
            this.Hide();
            Login cc = new Login();
            cc.Show();

        }

        private void adicionar1_Load(object sender, EventArgs e)
        {

        }
    }
}
