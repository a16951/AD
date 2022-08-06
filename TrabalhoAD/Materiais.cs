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
    public partial class Materiais : UserControl
    {
        public Materiais()
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
                NpgsqlDataAdapter da = new NpgsqlDataAdapter("Select r.cod_material, r.cod_tipomaterial, t.nome_tipomaterial, h.nome_hospital, r.cod_ala, a.nome_alahospitalar ,r.cod_estado, e.nome_estado, e.data_entrega  from recursosmateriais r inner join estado e ON r.cod_estado = e.cod_estado inner join alahospitalar a ON r.cod_ala = a.cod_ala inner join tipomaterial t ON r.cod_tipomaterial = t.cod_tipomaterial inner join hospital h ON a.cod_hospital = h.cod_hospital", con);

                DataTable dtb1 = new DataTable();
                da.Fill(dtb1);
                dgvmateriais.DataSource = dtb1;
                this.dgvmateriais.Sort(this.dgvmateriais.Columns["cod_material"], ListSortDirection.Ascending);
                dgvmateriais.Columns[0].HeaderText = "ID Material";
                dgvmateriais.Columns[1].HeaderText = "ID Tipo Material";
                dgvmateriais.Columns[2].HeaderText = "Tipo Material";
                dgvmateriais.Columns[3].HeaderText = "Hospital";
                dgvmateriais.Columns[4].HeaderText = "ID Ala Hospitalar";
                dgvmateriais.Columns[5].HeaderText = "Ala Hospitalar";
                dgvmateriais.Columns[6].HeaderText = "ID Estado Material";
                dgvmateriais.Columns[7].HeaderText = "Estado Material";
                dgvmateriais.Columns[8].HeaderText = "Data de Entrega";

            }
        }


           private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

       


        private void btEditar_Click(object sender, EventArgs e)
        {
            NpgsqlConnection con = new NpgsqlConnection(connstring);
            con.ConnectionString = "Host=localhost;Username=postgres;Password=123456789joao;Database=snsDB";
            con.Open();

            if (con.State == System.Data.ConnectionState.Open)
            {
                String query1 = "UPDATE recursosmateriais m SET cod_tipomaterial = '" + textBox3.Text + "' WHERE m.cod_material = '" + txtidmat.Text + "'; ";
                NpgsqlCommand sql1 = new NpgsqlCommand(query1, con);
                string query2 = "UPDATE recursosmateriais m SET cod_ala='" + textBox2.Text + "' WHERE m.cod_material = '" + txtidmat.Text + "';";
                NpgsqlCommand sql2 = new NpgsqlCommand(query2, con);
                string query3 = "UPDATE recursosmateriais m SET cod_estado='" + textBox1.Text + "' WHERE m.cod_material= '" + txtidmat.Text + "';";
                NpgsqlCommand sql3 = new NpgsqlCommand(query3, con);

                if (textBox5.TextLength != 0)
                {
                    string query4 = "UPDATE estado m SET data_entrega = NULL WHERE m.cod_estado= '" + textBox1.Text + "';";
                    NpgsqlCommand sql4 = new NpgsqlCommand(query4, con);
                    sql4.ExecuteNonQuery();
                }
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

        private void btElimin_Click(object sender, EventArgs e)
        {
            NpgsqlConnection con = new NpgsqlConnection(connstring);
            con.ConnectionString = "Host=localhost;Username=postgres;Password=123456789joao;Database=snsDB";
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                String query = " Delete from recursosmateriais WHERE cod_material=" + txtidmat.Text + "";
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

        private void btguardar_Click(object sender, EventArgs e)
        {

            
            NpgsqlConnection con = new NpgsqlConnection(connstring);
            con.ConnectionString = "Host=localhost;Username=postgres;Password=123456789joao;Database=snsDB";
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                String query = "INSERT INTO recursosmateriais (cod_tipomaterial,cod_ala,cod_estado)values("+textBox3.Text+","+textBox2.Text+","+textBox1.Text+"); ";
                NpgsqlCommand sql = new NpgsqlCommand(query, con);

                if (textBox5.TextLength!= 0)
                {
                    DateTime dt = DateTime.Parse(textBox5.Text);
                    String query1 = "INSERT INTO estado (nome_estado, data_entrega)values('Em reparação'," + dt + "); ";
                    NpgsqlCommand sql1 = new NpgsqlCommand(query1, con);
                    NpgsqlDataReader dr1 = sql1.ExecuteReader();
                }
               
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

        private void Materiais_Load_1(object sender, EventArgs e)
        {
            GridView();
        }

        private void dgvmateriais_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dgvmateriais.Sort(this.dgvmateriais.Columns["cod_material"], ListSortDirection.Ascending);
        }

        private void dgvmateriais_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvmateriais.Rows[e.RowIndex];
                txtidmat.Text = row.Cells[0].Value.ToString();
                txttipomat.Text = row.Cells[2].Value.ToString();
                txthospital.Text = row.Cells[3].Value.ToString();
                txtalahospital.Text = row.Cells[5].Value.ToString();
                txtestado.Text = row.Cells[7].Value.ToString();
                textBox4.Text = row.Cells[8].Value.ToString();
                textBox3.Text = row.Cells[1].Value.ToString();
                textBox2.Text = row.Cells[4].Value.ToString();
                textBox1.Text = row.Cells[6].Value.ToString();
                textBox5.Text = row.Cells[8].Value.ToString();
            }
        }

        private void txtalahospital_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    
}
