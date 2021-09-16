using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //declaramos las variables a utilizar en el programa
        double _credito = 0, _debito = 0, auxiliar = 0;

        //evento click del boton agregar
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //evaluamos si los campos estan vacios, de no estarlo entonces se procedera a ejecutar el codigo dentro de la condicion
            if (txt_cuenta.Text != string.Empty && txt_ncuenta.Text != string.Empty && txt_monto.Text != string.Empty && cmb_tipo.Text != string.Empty)
            {
                //evaluamos si es a debito o a credito que se agregara el valor
                if (cmb_tipo.Text.ToLower() == "debito")
                    dgbDatos.Rows.Add(txt_cuenta.Text, txt_ncuenta.Text, txt_monto.Text, "-");
                else
                    dgbDatos.Rows.Add(txt_cuenta.Text, txt_ncuenta.Text, "-", txt_monto.Text);

                Limpiar(); //limpiamos los textbox con la funcion limpiar creada mas abajo
                calculo(dgbDatos); //ejecutamos la funcion que hara el calulo y mostrara en los textbox correspondientes el saldo en debito y credito

                //codigo para simular la partida doble de la contabilidad, si los montos son diferentes, la partida doble no se cumple
                if (txt_Debito.Text != txt_Credito.Text)
                {
                    msg_label.Text = "No cumple con la partida doble";
                    msg_label.ForeColor = Color.Red;
                }
                else
                {
                    msg_label.Text = "Cumple con la partida doble";
                    msg_label.ForeColor = Color.Blue;
                }
            }
            else
            {
                //si falta algun dato en los textbox entonces arrojara este error en pantalla
                MessageBox.Show("Debe completar todos los campos");
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmb_tipo.SelectedIndex = 0; //inicia el combo box en la posicion 0 
        }

        //funcion para limpiar los componentes textbox
        void Limpiar(){
            txt_cuenta.Clear();
            txt_ncuenta.Clear();
            txt_monto.Clear();
            cmb_tipo.SelectedIndex = 0;
        }

        //funcion para calcular los montos en los textbox de debito y credito
        void calculo(DataGridView dgdatos)
        {
            foreach (DataGridViewRow row in dgdatos.Rows)
            {
                if (row.Cells[2].Value.ToString() != "-")
                {
                    auxiliar = Convert.ToDouble(row.Cells[2].Value);
                    _debito += auxiliar;
                }
                if (row.Cells[3].Value.ToString() != "-")
                {
                    auxiliar = Convert.ToDouble(row.Cells[3].Value);
                    _credito += auxiliar;
                }        


            }
            
            txt_Credito.Text = _credito.ToString("#,###.##");
            txt_Debito.Text = _debito.ToString("#,###.##");
            auxiliar = 0;
            _debito = 0;
            _credito = 0;
        }
    }
}
