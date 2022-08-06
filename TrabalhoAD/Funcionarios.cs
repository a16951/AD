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
    public partial class Funcionarios : UserControl
    {
        public Funcionarios()
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
                NpgsqlDataAdapter da = new NpgsqlDataAdapter("SELECT r.cod_pessoa, r.nome_pessoa, r.nome_especialidade,r.cod_tipomaterial, t.nome_tipomaterial,r.cod_ala, a.nome_alahospitalar, a.cod_hospital, h.nome_hospital  FROM recursoshumanos r inner join alahospitalar a ON r.cod_ala = a.cod_ala inner join hospital h ON h.cod_hospital = a.cod_hospital inner join tipomaterial t ON r.cod_tipomaterial = t.cod_tipomaterial", con);

                DataTable dtb1 = new DataTable();
                da.Fill(dtb1);
                dgvfuncionarios.DataSource = dtb1;
                dgvfuncionarios.Columns[0].HeaderText = "ID Funcionário";
                dgvfuncionarios.Columns[1].HeaderText = "Nome Funcionário";
                dgvfuncionarios.Columns[2].HeaderText = "Especialidade";
                dgvfuncionarios.Columns[3].HeaderText = "ID Tipo Material";
                dgvfuncionarios.Columns[4].HeaderText = "Tipo Material";
                dgvfuncionarios.Columns[5].HeaderText = "ID Ala Hospitalar";
                dgvfuncionarios.Columns[6].HeaderText = "Ala Hospitalar";
                dgvfuncionarios.Columns[7].HeaderText = "ID Hospital";
                dgvfuncionarios.Columns[8].HeaderText = "Hospital";
                this.dgvfuncionarios.Sort(this.dgvfuncionarios.Columns["cod_pessoa"], ListSortDirection.Ascending);
            }
        }

        private void textconcelhohospital_TextChanged(object sender, EventArgs e)
        {

        }

        private void btguardar_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btElimin_Click(object sender, EventArgs e)
        {
            NpgsqlConnection con = new NpgsqlConnection(connstring);
            con.ConnectionString = "Host=localhost;Username=postgres;Password=123456789joao;Database=snsDB";
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                String query = " Delete from recursoshumanos WHERE cod_pessoa=" + textBox3.Text + "";
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

        private void btguardar_Click_1(object sender, EventArgs e)
        {
            NpgsqlConnection con = new NpgsqlConnection(connstring);
            con.ConnectionString = "Host=localhost;Username=postgres;Password=123456789joao;Database=snsDB";
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                String query = "INSERT into recursoshumanos(nome_pessoa, nome_especialidade, cod_ala, cod_tipomaterial)values('" + textBox1.Text + "','" + textBox2.Text + "','" + txtidalahos.Text + "','" + txtidtipmat.Text + "'); ";
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

        private void btEditar_Click(object sender, EventArgs e)
        {
            NpgsqlConnection con = new NpgsqlConnection(connstring);
            con.ConnectionString = "Host=localhost;Username=postgres;Password=123456789joao;Database=snsDB";
            con.Open();

            if (con.State == System.Data.ConnectionState.Open)
            {
                String query1 = "UPDATE recursoshumanos SET nome_pessoa = '" + textBox1.Text + "' WHERE recursoshumanos.cod_pessoa = '" + textBox3.Text + "'; ";
                NpgsqlCommand sql1 = new NpgsqlCommand(query1, con);
                String query2 = "UPDATE recursoshumanos SET nome_especialidade = '" + textBox2.Text + "' WHERE recursoshumanos.cod_pessoa = '" + textBox3.Text + "';";
                NpgsqlCommand sql2 = new NpgsqlCommand(query2, con);
                string query3 = "UPDATE recursoshumanos SET cod_tipomaterial ='" + txtidtipmat.Text +"' WHERE recursoshumanos.cod_pessoa = '" + textBox3.Text + "';";
                NpgsqlCommand sql3 = new NpgsqlCommand(query3, con);
                string query4 = "UPDATE recursoshumanos SET cod_ala = '" + txtidalahos.Text + "'  WHERE recursoshumanos.cod_pessoa = '" + textBox3.Text + "';";
                NpgsqlCommand sql4 = new NpgsqlCommand(query4, con);

                try
                {
                    sql1.ExecuteNonQuery();
                    sql2.ExecuteNonQuery();
                    sql3.ExecuteNonQuery();
                    sql4.ExecuteNonQuery();
                    



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

        private void Funcionarios_Load(object sender, EventArgs e)
        {
            GridView();
        }

        private void dgvfuncionarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dgvfuncionarios.Sort(this.dgvfuncionarios.Columns["cod_pessoa"], ListSortDirection.Ascending);
        }

       

      
        private void dgvfuncionarios_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                
                DataGridViewRow row = this.dgvfuncionarios.Rows[e.RowIndex];
                textBox3.Text = row.Cells[0].Value.ToString();
                txtnomfunc.Text = row.Cells[1].Value.ToString();
                txtespecialidade.Text = row.Cells[2].Value.ToString();
                txtmaterial.Text = row.Cells[4].Value.ToString();
                txtalahos.Text = row.Cells[6].Value.ToString();
                txthospitall.Text = row.Cells[8].Value.ToString();
                textBox1.Text = row.Cells[1].Value.ToString();
                textBox2.Text = row.Cells[2].Value.ToString();
                txtidtipmat.Text = row.Cells[3].Value.ToString();
                txtidalahos.Text = row.Cells[5].Value.ToString();
               
                   
            }
        }
    
    }
}
