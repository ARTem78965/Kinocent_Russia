using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using MySql.Data.MySqlClient;

namespace Авторизация
{
    public partial class Feedback : MaterialForm
    {
        MySqlConnection conn = DBUtils.DBConnection();
        public Feedback()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue900, Primary.Blue800, Primary.LightBlue900, Accent.Blue700, TextShade.WHITE);
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (film.Text != "" && reiting.Text != "" && reiting.Text != "")
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("insert into reiting(film, reiting, comment) values('" + film.Text + "', '" + reiting.Text + "', '" + comment.Text + "');", conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Отзыв отправлен!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                conn.Close();
                Client client = new Client();
                client.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Не заполнены поля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                conn.Close();
            }
        }
    }
}
