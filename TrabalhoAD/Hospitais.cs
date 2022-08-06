using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using System.Data.SqlClient;

namespace TrabalhoAD
{
    public partial class Hospitais : UserControl
    {
        public Hospitais()
        {
            InitializeComponent();
        }

        string connstring = String.Format("Server=127.0.0.1;Port=50180;" + "User Id=postgres;Password=123456789joao;Database=snsDB;");
        String teste = String.Format("Server=127.0.0.1;Port=50180;" + "User Id=postgres;Password=123456789joao;Database=snsDB;");

        private void Hospitais_Load(object sender, EventArgs e)
        {
            GridView();

        }

        public void GridView()
        {
            NpgsqlConnection con = new NpgsqlConnection(teste);
            con.ConnectionString = "Host=localhost;Username=postgres;Password=123456789joao;Database=snsDB";
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                NpgsqlDataAdapter da = new NpgsqlDataAdapter("Select * from hospital", con);
                DataTable dtb1 = new DataTable();
                da.Fill(dtb1);
                dataGridViewhospital.DataSource = dtb1;
                this.dataGridViewhospital.Sort(this.dataGridViewhospital.Columns["cod_hospital"], ListSortDirection.Ascending);
                dataGridViewhospital.Columns[0].HeaderText = "ID Hospital";
                dataGridViewhospital.Columns[1].HeaderText = "Nome Hospital";
                dataGridViewhospital.Columns[2].HeaderText = "Localidade";
                dataGridViewhospital.Columns[3].HeaderText = "Concelho";
                dataGridViewhospital.Columns[4].HeaderText = "Código Postal";
               

            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btguardar_Click(object sender, EventArgs e)
        {
            NpgsqlConnection con = new NpgsqlConnection(connstring);
            con.ConnectionString = "Host=localhost;Username=postgres;Password=123456789joao;Database=snsDB";
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                String query = " INSERT INTO hospital (nome_hospital,localidade,concelho,cod_postal) VALUES ('" + textnomhospital.Text + "','" + textlocalidadehospital.Text + "','" + textconcelhohospital.Text + "','" + textcodpostalhospital.Text + "')";
                NpgsqlCommand sql = new NpgsqlCommand(query, con);
                try
                {
                    NpgsqlDataReader dr = sql.ExecuteReader();
                    MessageBox.Show("Adicionado com sucesso!");
                    GridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro!");
                }
            }
            con.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewhospital_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dataGridViewhospital.Sort(this.dataGridViewhospital.Columns["cod_hospital"], ListSortDirection.Ascending);
        }

        private void dataGridViewhospital_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridViewhospital.Rows[e.RowIndex];
                txtidhospital.Text = row.Cells[0].Value.ToString();
                textnomhospital.Text = row.Cells[1].Value.ToString();
                textlocalidadehospital.Text = row.Cells[2].Value.ToString();
                textconcelhohospital.Text = row.Cells[3].Value.ToString();
                textcodpostalhospital.Text = row.Cells[4].Value.ToString();
            }
        }

        private void btEditar_Click(object sender, EventArgs e)
        {
            NpgsqlConnection con = new NpgsqlConnection(connstring);
            con.ConnectionString = "Host=localhost;Username=postgres;Password=123456789joao;Database=snsDB";
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                String query = "Update hospital Set nome_hospital = '" + textnomhospital.Text + "', localidade = '" + textlocalidadehospital.Text + "', concelho = '" + textconcelhohospital.Text + "'  ,cod_postal =  '" + textcodpostalhospital.Text + "' where cod_hospital = '" + txtidhospital.Text + "';";
                NpgsqlCommand sql = new NpgsqlCommand(query, con);
                try
                {
                    NpgsqlDataReader dr = sql.ExecuteReader();
                    MessageBox.Show("Alterado com sucesso!");
                    GridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro!");
                }
            }
            con.Close();

           
        }

        private void btElimin_Click(object sender, EventArgs e)
        {
            NpgsqlConnection con = new NpgsqlConnection(connstring);
            con.ConnectionString = "Host=localhost;Username=postgres;Password=123456789joao;Database=snsDB";
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                String query = " Delete from hospital WHERE cod_hospital=" + txtidhospital.Text + "";
                NpgsqlCommand sql = new NpgsqlCommand(query, con);
                try
                {
                    NpgsqlDataReader dr = sql.ExecuteReader();
                    MessageBox.Show("Eliminado com sucesso!");
                    GridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro!");
                }
            }
            con.Close();


        }
    }
    
       
}
