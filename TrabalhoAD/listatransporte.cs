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
    public partial class listatransporte : UserControl
    {
        public listatransporte()
        {
            InitializeComponent();
        }

        string connstring = String.Format("Server=127.0.0.1;Port=50180;" + "User Id=postgres;Password=123456789joao;Database=snsDB;");
        String teste = String.Format("Server=127.0.0.1;Port=50180;" + "User Id=postgres;Password=123456789joao;Database=snsDB;");

      

        public void GridView()
        {
            NpgsqlConnection con = new NpgsqlConnection(teste);
            con.ConnectionString = "Host=localhost;Username=postgres;Password=123456789joao;Database=snsDB";
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                NpgsqlDataAdapter da = new NpgsqlDataAdapter("Select cod_transporte,cod_material, t.ida,ala_ida.nome_alahospitalar nome_ida,h1.nome_hospital, t.origem, ala_origem.nome_alahospitalar nome_origem, h2.nome_hospital FROM transporte t inner join alahospitalar ala_ida on ala_ida.cod_ala = t.ida inner join alahospitalar ala_origem on ala_origem.cod_ala = t.origem inner join hospital h1 on ala_ida.cod_hospital = h1.cod_hospital inner join hospital h2 on ala_origem.cod_hospital = h2.cod_hospital", con);
                DataTable dtb1 = new DataTable();
                da.Fill(dtb1);
                dgvtransporte.DataSource = dtb1;
                
                dgvtransporte.Columns[0].HeaderText = "ID Transporte";
                dgvtransporte.Columns[1].HeaderText = "ID Material";
                dgvtransporte.Columns[2].HeaderText = "ID Ida (Ala)";
                dgvtransporte.Columns[3].HeaderText = "Local Ida (Ala)";
                dgvtransporte.Columns[4].HeaderText = "Local Ida (hospital)";
                dgvtransporte.Columns[5].HeaderText = "ID Origem (Ala)";
                dgvtransporte.Columns[6].HeaderText = "Local Origem (Ala)";
                dgvtransporte.Columns[7].HeaderText = "Local Origem (Hospital)";
                this.dgvtransporte.Sort(this.dgvtransporte.Columns["cod_transporte"], ListSortDirection.Ascending);


            }
        }

        private void dgvfuncionarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

      

        private void btguardar_Click(object sender, EventArgs e)
        {
            NpgsqlConnection con = new NpgsqlConnection(connstring);
            con.ConnectionString = "Host=localhost;Username=postgres;Password=123456789joao;Database=snsDB";
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                String query = " INSERT INTO transporte (cod_material, ida, origem) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + txtasdasdad.Text + "')";
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

        private void listatransporte_Load(object sender, EventArgs e)
        {
            GridView();
        }

        private void btElimin_Click(object sender, EventArgs e)
        {
            NpgsqlConnection con = new NpgsqlConnection(connstring);
            con.ConnectionString = "Host=localhost;Username=postgres;Password=123456789joao;Database=snsDB";
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                String query = " Delete from transporte WHERE cod_transporte=" + txtidtranspo.Text + "";
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

        private void btEditar_Click(object sender, EventArgs e)
        {
            NpgsqlConnection con = new NpgsqlConnection(connstring);
            con.ConnectionString = "Host=localhost;Username=postgres;Password=123456789joao;Database=snsDB";
            con.Open();

            if (con.State == System.Data.ConnectionState.Open)
            {
            String query1 = "UPDATE transporte SET cod_material = '" + textBox1.Text + "' WHERE transporte.cod_transporte = '" + txtidtranspo.Text + "';";
            NpgsqlCommand sql1 = new NpgsqlCommand(query1, con);
            string query2 = "UPDATE transporte SET ida ='" + textBox2.Text + "' WHERE transporte.cod_transporte = '" + txtidtranspo.Text + "';";
            NpgsqlCommand sql2 = new NpgsqlCommand(query2, con);
            string query3 = "UPDATE transporte SET origem ='" + txtasdasdad.Text + "' WHERE transporte.cod_transporte = '" + txtidtranspo.Text + "';";
            NpgsqlCommand sql3 = new NpgsqlCommand(query3, con);

                try
            {
                sql1.ExecuteNonQuery();
                sql2.ExecuteNonQuery();
                sql3.ExecuteNonQuery();
               
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

        private void dgvtransporte_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dgvtransporte.Sort(this.dgvtransporte.Columns["cod_transporte"], ListSortDirection.Ascending);
        }

        private void dgvtransporte_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvtransporte.Rows[e.RowIndex];
                txtidtranspo.Text = row.Cells[0].Value.ToString();
                txtidmaterial.Text = row.Cells[1].Value.ToString();
                txtidaala.Text = row.Cells[3].Value.ToString();
                txtidahospital.Text = row.Cells[4].Value.ToString();
                txtorigemala.Text = row.Cells[6].Value.ToString();
                txthospitall.Text = row.Cells[7].Value.ToString();
                textBox1.Text = row.Cells[1].Value.ToString();
                textBox2.Text = row.Cells[2].Value.ToString();
                txtasdasdad.Text = row.Cells[5].Value.ToString();
            }
        }
    }
}
