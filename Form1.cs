using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MICRUDPRUEBA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int id;

        SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=MICRUDPRUEBA_DB;User ID=sa;Password=123456");
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetEmpList();
        }

        void GetEmpList()
        {
            SqlCommand c = new SqlCommand("EXEC SP_ListarUsuarios", conn);
            SqlDataAdapter sd = new SqlDataAdapter(c);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dtgv_Tabla.DataSource = dt;
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            string sexo;
            string nombre = txt_nombre.Text;
            string apellido = txt_apellido.Text;
            int edad = Convert.ToInt32(txtn_edad.Value);
            if (rdb_M.Checked == true) { sexo = "Masculino"; } else { sexo = "Femenino"; }
            DateTime fechaNa = Convert.ToDateTime(dateTimePicker1.Value);
            conn.Open();
            SqlCommand c = new SqlCommand("EXEC SP_InsertarUsuario " + "'"+ nombre +"','"+ apellido + "','" + edad + "','" + sexo +"','"+fechaNa+"'", conn);
            c.ExecuteNonQuery();
            conn.Close();
            GetEmpList();
        }

        private void dtgv_Tabla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgv_Tabla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgv_Tabla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            String var = Convert.ToString(dtgv_Tabla.SelectedRows[0].Cells[0].Value);
            if (String.IsNullOrEmpty(var))
            {
                MessageBox.Show("Porfavor seleccione un usuario");
            }
            else
            {
                try
                {
                    //Toma los datos en orden de izquierda a derecha
                    id = Convert.ToInt32(dtgv_Tabla.SelectedRows[0].Cells[0].Value);
                    txt_nombre.Text = Convert.ToString(dtgv_Tabla.SelectedRows[0].Cells[1].Value);
                    txt_apellido.Text = Convert.ToString(dtgv_Tabla.SelectedRows[0].Cells[2].Value);
                    txtn_edad.Value = Convert.ToInt32(dtgv_Tabla.SelectedRows[0].Cells[3].Value);

                    string sexo = Convert.ToString(dtgv_Tabla.SelectedRows[0].Cells[4].Value);
                    if (sexo.Equals("Masculino")) { rdb_M.Checked = true; rdb_F.Checked = false; } else { rdb_M.Checked = false; rdb_F.Checked = true; }

                    dateTimePicker1.Value = Convert.ToDateTime(dtgv_Tabla.SelectedRows[0].Cells[5].Value);
                }
                catch (Exception)
                {

                    throw;
                }
            } 
        }

        private void btn_editar_Click(object sender, EventArgs e)
        {
            id = Convert.ToInt32(dtgv_Tabla.SelectedRows[0].Cells[0].Value);
            string sexo;
            string nombre = txt_nombre.Text;
            string apellido = txt_apellido.Text;
            int edad = Convert.ToInt32(txtn_edad.Value);
            if (rdb_M.Checked == true) { sexo = "Masculino"; } else { sexo = "Femenino"; }
            DateTime fechaNa = Convert.ToDateTime(dateTimePicker1.Value);
            conn.Open();
            SqlCommand c = new SqlCommand("EXEC SP_ActualizarUsuario " + id +",'" + nombre + "','" + apellido + "','" + edad + "','" + sexo + "','" + fechaNa + "'", conn);
            c.ExecuteNonQuery();
            conn.Close();
            GetEmpList();
        }

        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            id = Convert.ToInt32(dtgv_Tabla.SelectedRows[0].Cells[0].Value);
            conn.Open();
            SqlCommand c = new SqlCommand("EXEC SP_EliminarUsuario " + id, conn);
            c.ExecuteNonQuery();
            conn.Close();
            GetEmpList();
        }
    }
}
