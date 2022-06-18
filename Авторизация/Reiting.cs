using MaterialSkin;
using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Авторизация
{
    public partial class Reiting : MaterialForm
    {
        public MySqlConnection conn = DBUtils.DBConnection();
        public Reiting()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue400, Primary.Blue800, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("insert into reiting(film, reiting, comment) values('" + film.Text + "','" + value.Text + "','" + comment.Text + "')", conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Отзыв отправлен!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                conn.Close();

                Client client = new Client();
                client.Show();
                Close();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
        }
    }
}
