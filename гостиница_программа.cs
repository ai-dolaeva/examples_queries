using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Npgsql;
using System.Text.RegularExpressions;
using System.Data.Common;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string nameHotel1 = "", hotelid, sql, bookid;
        bool click = false, clickfeed = false;
        private DataSet dstemp = new DataSet();
        private DataTable dttemp = new DataTable();
        private DataSet dshotel = new DataSet();
        private DataTable dthotel = new DataTable();
        private DataSet dsfeed = new DataSet();
        private DataTable dtfeed = new DataTable();
        static String connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=1;Database=hotels;";
        NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);

        private void Select(string str, DataGridView dataGridView, DataSet ds, DataTable dt)
        {
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(str, npgSqlConnection);
            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];
            dataGridView.DataSource = dt;
            if (dataGridView.Columns.Contains("id_hotel"))
                dataGridView.Columns["id_hotel"].Visible = false;
            if (dataGridView.Columns.Contains("id_room"))
                dataGridView.Columns["id_room"].Visible = false;
            if (dataGridView.Columns.Contains("Отзыв"))
                dataGridView.Columns["Отзыв"].Width = 200;
            if (dataGridView.Columns.Contains("Услуги"))
                dataGridView.Columns["Услуги"].Width = 300;
        }
        private bool Operation(string str)
        {
            NpgsqlCommand cmd = new NpgsqlCommand(str, npgSqlConnection);
            if (cmd.ExecuteScalar() == null)
            {
                labelMessage.Text = "Операция выполнена";
                return true;
            }
            else return false;
        }
        private void Interf()
        {
            textBoxNamehotel.Text = "по названию";
            textBoxMinPrice.Text = "0";
            textBoxMaxPrice.Text = "10000";
            textBoxNamehotel.ForeColor = SystemColors.GrayText;
            textBoxMinPrice.ForeColor = SystemColors.GrayText;
            textBoxMaxPrice.ForeColor = SystemColors.GrayText;
            textPassword.Visible = false;
            textLogin.Visible = false;
            textBoxNamehotel.Visible = false;
            textBoxMinPrice.Visible = false;
            textBoxMaxPrice.Visible = false;
            button_Feedback.Visible = false;
            button_Book.Visible = false;
            button_Add.Visible = false;
            button_Update.Visible = false;
            button_Delete.Visible = false;
            button_Find.Visible = false;
            button_ShowFeedback.Visible = false;
            button_return.Visible = false;
            DataGridMain.Visible = false;
            DataGridFeedback.Visible = false;
            Name_basa_main.Visible = false;
            labelMessage.Visible = false;
            labalFeedback.Visible = false;
            buttonDelFeed.Visible = false;
            buttonShowBook.Visible = false;
            checkedListBox1.Visible = false;
            dateTimePicker1.Visible = false;
            dateTimePicker2.Visible = false;
            textBoxEm.Visible = false;
            textBoxFN.Visible = false;
            textBoxGr.Visible = false;
            textBoxPay.Visible = false;
            textBoxPh.Visible = false;
            textBoxPN.Visible = false;
            textBoxSN.Visible = false;
            label1.Visible = false;
            textBoxfeed.Visible = false;
            checkBoxPay.Visible = false;
            buttonreturn1.Visible = false;
            button_Enter.Visible = true;
            comboBox1.Visible = true;
        }
        public Form1()
        {
            InitializeComponent();
            Interf();
        }

        private void textlogin_Click(object sender, EventArgs e)
        {
            textLogin.Text = null;
            textLogin.ForeColor = SystemColors.WindowText;
        }
        private void textPassword_Click(object sender, EventArgs e)
        {
            textPassword.Text = null;
            textPassword.PasswordChar = '*';
        }
        private void textBoxNamehotel_Click(object sender, EventArgs e)
        {
            textBoxNamehotel.Text = null;
            textBoxNamehotel.ForeColor = SystemColors.WindowText;
        }
        private void textBoxMinPrice_Click(object sender, EventArgs e)
        {
            textBoxMinPrice.Text = null;
            textBoxMinPrice.ForeColor = SystemColors.WindowText;
        }
        private void textBoxMaxPrice_Click(object sender, EventArgs e)
        {
            textBoxMaxPrice.Text = null;
            textBoxMaxPrice.ForeColor = SystemColors.WindowText;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem.ToString() == "Администратор сайта")
            {
                textPassword.Visible = true;
                textLogin.Visible = false;
            }

            if (comboBox1.SelectedItem.ToString() == "Гостиница")
            {
                textPassword.Visible = true;
                textLogin.Visible = true;
            }

            if (comboBox1.SelectedItem.ToString() == "Клиент")
            {
                textPassword.Visible = false;
                textLogin.Visible = false;
            }
        }
        private void button_Enter_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedItem.ToString() == "Администратор сайта")
                {
                    string pass = "1";
                    //MessageBox.Show(textBox2.Text, "Ошибка", MessageBoxButtons.OK);
                    if (textPassword.Text.Equals(pass))
                    {
                        comboBox1.Visible = false;
                        button_Enter.Visible = false;
                        textPassword.Visible = false;
                        labelMessage.Visible = false;
                        buttonDelFeed.Visible = true;
                        DataGridFeedback.Visible = true;
                        labalFeedback.Visible = true;
                        labalFeedback.Text = "Отзывы";
                        DataGridMain.Visible = true;
                        Name_basa_main.Visible = true;
                        button_Add.Visible = true;
                        button_Update.Visible = true;
                        button_Delete.Visible = true;
                        button_Delete.Text = "удалить";
                        button_ShowFeedback.Visible = true;
                        dateTimePicker1.Visible = true;
                        textBoxGr.Visible = true;
                        textBoxGr.Text = "шагов назад";
                        button_return.Visible = true;
                        buttonreturn1.Visible = true;
                        sql = String.Format("SELECT hotel.id_hotel, hotel.name_hotel as Имя, hotel.adress as Адрес, hotel.phone_hotel as Телефон," +
                         " hotel.email_hotel as \"Email\", hotel.headname as Директор, hotel.category as Категория, hotel.type_hotel as Тип, " +
                         "Услуги, Логин, Пароль from hotel LEFT JOIN(select hotel.id_hotel as idtbserv, STRING_AGG(service_hotel.services_hotel, ', ') as Услуги  from hotel," +
                            "service_hotel, service_rendered_hotel where service_rendered_hotel.hotel = hotel.id_hotel and service_rendered_hotel.service_hotel = service_hotel.id_service_hotel" +
                            " GROUP BY hotel.id_hotel) as tbserv ON hotel.id_hotel = idtbserv LEFT JOIN(select hotel.id_hotel as idtblog, log_in.login_hotel as Логин, log_in.password_hotel as Пароль" +
                            " from hotel, log_in where log_in.hotel = hotel.id_hotel) as tblog ON hotel.id_hotel = idtblog");
                        Select(sql, DataGridMain, dshotel, dthotel);
                    }
                    else
                    {
                        textPassword.Text = "пароль";
                        textPassword.PasswordChar = '\0';
                        labelMessage.Visible = true;
                        labelMessage.Text = "Пароль неверен. Попробуйте еще раз";
                    }
                }

                if (comboBox1.SelectedItem.ToString() == "Гостиница")
                {
                    textPassword.Visible = true;
                    textLogin.Visible = true;
                    bool access = false;
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter("select log_in.hotel, log_in.login_hotel, log_in.password_hotel, hotel.name_hotel from log_in, hotel where log_in.hotel=hotel.id_hotel", npgSqlConnection);
                    dstemp.Reset();
                    da.Fill(dstemp);
                    dttemp = dstemp.Tables[0];
                    for (int i = 0; i < dttemp.Rows.Count; i++)
                    {
                        if ((textPassword.Text.Equals(dttemp.Rows[i].ItemArray.ElementAt(2).ToString())) && (textLogin.Text.Equals(dttemp.Rows[i].ItemArray.ElementAt(1).ToString())))
                        {
                            access = true;
                            hotelid = dttemp.Rows[i].ItemArray.ElementAt(0).ToString();
                            Name_basa_main.Text += " гостиницы \"" + dttemp.Rows[i].ItemArray.ElementAt(3).ToString() + "\"";
                        }
                    }
                    if (access)
                    {
                        DataGridFeedback.Visible = true;
                        labalFeedback.Visible = true;
                        DataGridMain.Visible = true;
                        Name_basa_main.Visible = true;
                        button_Add.Visible = true;
                        button_Update.Visible = true;
                        button_Delete.Visible = true;
                        button_ShowFeedback.Visible = true;
                        comboBox1.Visible = false;
                        button_Enter.Visible = false;
                        labelMessage.Visible = false;
                        textPassword.Visible = false;
                        textLogin.Visible = false;
                        button_ShowFeedback.Visible = true;
                        buttonShowBook.Visible = true;
                        checkedListBox1.Visible = true;
                        dateTimePicker1.MinDate = DateTime.Now;
                        dateTimePicker2.MinDate = DateTime.Now;
                        dateTimePicker1.MaxDate = DateTime.Now.AddMonths(3);
                        dateTimePicker2.MaxDate = DateTime.Now.AddMonths(3);
                        labalFeedback.Text = "Бронирование";
                        sql = String.Format("SELECT id_room, Цена, Бронь, Расположение, Тип, Услуги from(SELECT room.hotel, room.id_room, room.price as Цена, " +
                            "room.status as Бронь, room.location as Расположение, room.level as Тип from room where room.hotel = {0}) as rm LEFT JOIN " +
                            "(select room.id_room as idtbserv, STRING_AGG(service_room.services_room, ', ') as Услуги  from service_rendered_room, " +
                            "service_room, room  where service_rendered_room.service_room = service_room.id_service_room  and service_rendered_room.room = room.id_room " +
                            "GROUP BY room.id_room) as tbserv ON  rm.id_room = idtbserv", hotelid);
                        Select(sql, DataGridMain, dshotel, dthotel);
                    }
                    else
                    {
                        labelMessage.Visible = true;
                        textLogin.Text = "логин";
                        textPassword.Text = "пароль";
                        textPassword.PasswordChar = '\0';
                        labelMessage.Text = "Такой пользователь не найден. Попробуйте еще раз";
                    }
                }

                if (comboBox1.SelectedItem.ToString() == "Клиент")
                {
                    comboBox1.Visible = false;
                    button_Enter.Visible = false;
                    textBoxNamehotel.Visible = true;
                    textBoxMinPrice.Visible = true;
                    textBoxMaxPrice.Visible = true;
                    button_Find.Visible = true;
                    DataGridMain.Visible = true;
                    Name_basa_main.Visible = true;
                    dateTimePicker1.Visible = true;
                    dateTimePicker2.Visible = true;
                    dateTimePicker1.MinDate = DateTime.Now;
                    dateTimePicker2.MinDate = DateTime.Now;
                    dateTimePicker1.MaxDate = DateTime.Now.AddMonths(3);
                    dateTimePicker2.MaxDate = DateTime.Now.AddMonths(3);
                    buttonShowBook.Visible = true;
                    buttonShowBook.Text = "выбрать номер";
                    button_Delete.Visible = true;
                    button_Delete.Text = "отменить бронь";
                    button_ShowFeedback.Visible = true;
                    DataGridFeedback.Visible = true;
                    labelMessage.Visible = true;
                    labalFeedback.Visible = true;
                    textBoxEm.Visible = true;
                    textBoxFN.Visible = true;
                    textBoxGr.Visible = true;
                    textBoxPh.Visible = true;
                    textBoxPN.Visible = true;
                    textBoxSN.Visible = true;
                    label1.Visible = true;
                    checkBoxPay.Visible = true;
                    string sql = String.Format("select hotel.id_hotel, hotel.name_hotel AS Имя, hotel.adress AS Адрес, hotel.category AS Категория, hotel.type_hotel AS Тип, " +
                        "STRING_AGG(DISTINCT service_hotel.services_hotel, ', ') AS Услуги from hotel, service_hotel, service_rendered_hotel, room " +
                        "where service_rendered_hotel.hotel = hotel.id_hotel and room.hotel = hotel.id_hotel and service_rendered_hotel.service_hotel =" +
                        " service_hotel.id_service_hotel GROUP BY hotel.id_hotel, hotel.name_hotel, hotel.adress, hotel.category, hotel.type_hotel ");
                    Select(sql, DataGridMain, dshotel, dthotel);
                }
            }
            catch
            {
                MessageBox.Show("Выберите пользователя из списка", "Сообщение", MessageBoxButtons.OK);
            }
        }
        private void dataGridMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            click = true;
            nameHotel1 = DataGridMain[0, DataGridMain.CurrentCell.RowIndex].Value.ToString();
        }
        private void DataGridFeedback_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            clickfeed = true;
        }
        private void button_ShowFeedback_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Администратор сайта")
            {
                try
                {
                    labelMessage.Visible = false;
                    sql = String.Format("SELECT text_feedback as Отзыв, person as от, date_feedback as дата from feedback," +
                        " hotel where feedback.hotel = hotel.id_hotel and feedback.hotel = {0}", DataGridMain[0, DataGridMain.CurrentCell.RowIndex].Value.ToString());
                    Select(sql, DataGridFeedback, dsfeed, dtfeed);
                }
                catch
                {
                    labelMessage.Visible = true;
                    labelMessage.Text = "Выберите гостиницу";
                }
            }
            if (comboBox1.SelectedItem.ToString() == "Гостиница")
            {
                labalFeedback.Text = "Отзывы";
                sql = String.Format("SELECT DISTINCT text_feedback as Отзыв, person as от, date_feedback as дата from feedback where feedback.hotel = {0}", hotelid);
                Select(sql, DataGridFeedback, dsfeed, dtfeed);
            }
            if (comboBox1.SelectedItem.ToString() == "Клиент")
            {
                try
                {
                    labalFeedback.Text = "Отзывы";
                    sql = String.Format("SELECT DISTINCT text_feedback as Отзыв, person as от, date_feedback as дата from feedback where feedback.hotel = {0}", nameHotel1);
                    Select(sql, DataGridFeedback, dsfeed, dtfeed);
                    textBoxfeed.Visible = true;
                    button_Feedback.Visible = true;
                    button_Book.Visible = false;
                    labelMessage.Text = "Поля со * обязательны дял заполнения";
                }
                catch
                {
                    labelMessage.Visible = true;
                    labelMessage.Text = "Выберите гостиницу";
                }
            }
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Гостиница") //добавить номер
            {
                try
                {
                    bool stat = false;
                    if ((DataGridMain[2, DataGridMain.CurrentCell.RowIndex].Value.ToString() == "") || (DataGridMain[2, DataGridMain.CurrentCell.RowIndex].Value.ToString() == stat.ToString()))
                        stat = false;
                    else stat = true;
                    sql = String.Format("Insert into room (price, status,location,level, hotel) " +
                       "VALUES ('{0}','{1}','{2}','{3}','{4}') RETURNING id_room",
                      DataGridMain[1, DataGridMain.CurrentCell.RowIndex].Value.ToString(), stat.ToString(),
                      DataGridMain[3, DataGridMain.CurrentCell.RowIndex].Value.ToString(), DataGridMain[4, DataGridMain.CurrentCell.RowIndex].Value.ToString(),
                      hotelid);
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, npgSqlConnection);
                    dstemp.Reset();
                    da.Fill(dstemp);
                    dttemp = dstemp.Tables[0];
                    string idhotel = dttemp.Rows[0].ItemArray.ElementAt(0).ToString();

                    string[] mass = Regex.Split(DataGridMain[5, DataGridMain.CurrentCell.RowIndex].Value.ToString(), ", ");
                    for (int i = 0; i < mass.Length; i++)
                    {
                        sql = String.Format("select id_service_room from  service_room where '{0}' in (services_room)", mass[i]);
                        da = new NpgsqlDataAdapter(sql, npgSqlConnection);
                        dstemp.Reset();
                        da.Fill(dstemp);
                        dttemp = dstemp.Tables[0];

                        if (dttemp.Rows.Count > 0)
                        {
                            string idser = dttemp.Rows[0].ItemArray.ElementAt(0).ToString();
                            sql = String.Format("INSERT INTO service_rendered_room VALUES({0}, {1})", idser, idhotel);
                            Operation(sql);
                        }
                        else
                        {
                            sql = String.Format("INSERT INTO service_room (services_room) VALUES('{0}') RETURNING id_service_room", mass[i]);
                            da = new NpgsqlDataAdapter(sql, npgSqlConnection);
                            dstemp.Reset();
                            da.Fill(dstemp);
                            dttemp = dstemp.Tables[0];
                            sql = String.Format("INSERT INTO service_rendered_room VALUES({0}, {1})", dttemp.Rows[0].ItemArray.ElementAt(0).ToString(), idhotel);
                            Operation(sql);
                        }
                        if (stat)
                        {
                            sql = String.Format("INSERT INTO booking(arrivaldate, datedeparture, room) VALUES('{0}', '{1}', {2})", dateTimePicker1.Value.ToString(), dateTimePicker2.Value.ToString(), idhotel);
                            Operation(sql);
                        }
                        button_Enter_Click(sender, e);
                    }
                }
                catch { }
            }
            if (comboBox1.SelectedItem.ToString() == "Администратор сайта") //добавить гостиницу
            {
                try
                {
                    sql = String.Format("Insert into hotel (name_hotel, adress, phone_hotel,email_hotel, headname, category, type_hotel) " +
                        "VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}') RETURNING id_hotel",
                       DataGridMain[1, DataGridMain.CurrentCell.RowIndex].Value.ToString(), DataGridMain[2, DataGridMain.CurrentCell.RowIndex].Value.ToString(),
                       DataGridMain[3, DataGridMain.CurrentCell.RowIndex].Value.ToString(), DataGridMain[4, DataGridMain.CurrentCell.RowIndex].Value.ToString(),
                       DataGridMain[5, DataGridMain.CurrentCell.RowIndex].Value.ToString(), DataGridMain[6, DataGridMain.CurrentCell.RowIndex].Value.ToString(),
                       DataGridMain[7, DataGridMain.CurrentCell.RowIndex].Value.ToString());
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, npgSqlConnection);
                    dstemp.Reset();
                    da.Fill(dstemp);
                    dttemp = dstemp.Tables[0];
                    string idhotel = dttemp.Rows[0].ItemArray.ElementAt(0).ToString();

                    string[] mass = Regex.Split(DataGridMain[8, DataGridMain.CurrentCell.RowIndex].Value.ToString(), ", ");
                    for (int i = 0; i < mass.Length; i++)
                    {
                        sql = String.Format("select id_service_hotel from  service_hotel where '{0}' in (services_hotel)", mass[i]);
                        da = new NpgsqlDataAdapter(sql, npgSqlConnection);
                        dstemp.Reset();
                        da.Fill(dstemp);
                        dttemp = dstemp.Tables[0];

                        if (dttemp.Rows.Count > 0)
                        {
                            string idser = dttemp.Rows[0].ItemArray.ElementAt(0).ToString();
                            sql = String.Format("INSERT INTO service_rendered_hotel VALUES({0}, {1})", idser, idhotel);
                            Operation(sql);
                        }
                        else
                        {
                            sql = String.Format("INSERT INTO service_hotel (services_hotel) VALUES('{0}') RETURNING id_service_hotel", mass[i]);
                            da = new NpgsqlDataAdapter(sql, npgSqlConnection);
                            dstemp.Reset();
                            da.Fill(dstemp);
                            dttemp = dstemp.Tables[0];
                            sql = String.Format("INSERT INTO service_rendered_hotel VALUES({0}, {1})", dttemp.Rows[0].ItemArray.ElementAt(0).ToString(), idhotel);
                            Operation(sql);
                        }
                    }

                    sql = String.Format("INSERT INTO log_in VALUES({0}, '{1}', '{2}') ", idhotel, DataGridMain[9, DataGridMain.CurrentCell.RowIndex].Value.ToString(), DataGridMain[10, DataGridMain.CurrentCell.RowIndex].Value.ToString());
                    Operation(sql);

                    button_Enter_Click(sender, e);
                }
                catch { }
            }
        }

        private void button_Update_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Гостиница") //изменить номер
            {
                try
                {
                    sql = String.Format("Update room set price='{0}', status ='{1}', location='{2}',level='{3}' where id_room={4}",
                      DataGridMain[1, DataGridMain.CurrentCell.RowIndex].Value.ToString(), DataGridMain[2, DataGridMain.CurrentCell.RowIndex].Value.ToString(),
                      DataGridMain[3, DataGridMain.CurrentCell.RowIndex].Value.ToString(), DataGridMain[4, DataGridMain.CurrentCell.RowIndex].Value.ToString(),
                      DataGridMain[0, DataGridMain.CurrentCell.RowIndex].Value.ToString());
                    Operation(sql);

                    sql = String.Format("select id_service_room, services_room from service_room, room, service_rendered_room where " +
                        "service_rendered_room.room = room.id_room and service_rendered_room.service_room = service_room.id_service_room" +
                        " and service_rendered_room.room= {0}", DataGridMain[0, DataGridMain.CurrentCell.RowIndex].Value.ToString());

                    string[] mass = Regex.Split(DataGridMain[5, DataGridMain.CurrentCell.RowIndex].Value.ToString(), ", ");
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, npgSqlConnection);
                    dstemp.Reset();
                    da.Fill(dstemp);
                    dttemp = dstemp.Tables[0];
                    bool have = false;
                    int ind = 0;
                    if (dttemp.Rows.Count > 0)
                    {
                        for (int i = 0; i < dttemp.Rows.Count; i++)
                        {
                            have = false;
                            for (int j = 0; j < mass.Length; j++)
                            {
                                if (dttemp.Rows[i].ItemArray.ElementAt(1).ToString() == mass[j])
                                {
                                    have = true;
                                    ind = i;
                                }
                            }
                            if (!have)
                            {
                                sql = String.Format("delete from service_rendered_room where service_room={0} and room={1}", dttemp.Rows[ind].ItemArray.ElementAt(0).ToString(), DataGridMain[0, DataGridMain.CurrentCell.RowIndex].Value.ToString());
                                Operation(sql);
                            }
                        }
                    }
                    for (int i = 0; i < mass.Length; i++)
                    {
                        sql = String.Format("select id_service_room from service_room where '{0}' in (services_room)", mass[i]);
                        da = new NpgsqlDataAdapter(sql, npgSqlConnection);
                        dstemp.Reset();
                        da.Fill(dstemp);
                        dttemp = dstemp.Tables[0];
                        if (dttemp.Rows.Count > 0)
                        {
                        }
                        else
                        {
                            sql = String.Format("INSERT INTO service_room (services_room) VALUES('{0}') RETURNING id_service_room", mass[i]);
                            da = new NpgsqlDataAdapter(sql, npgSqlConnection);
                            dstemp.Reset();
                            da.Fill(dstemp);
                            dttemp = dstemp.Tables[0];
                            sql = String.Format("INSERT INTO service_rendered_room VALUES({0}, {1})", dttemp.Rows[0].ItemArray.ElementAt(0).ToString(), DataGridMain[0, DataGridMain.CurrentCell.RowIndex].Value.ToString());
                            Operation(sql);
                        }
                    }

                }
                catch { }
                button_Enter_Click(sender, e);
            }
            if (comboBox1.SelectedItem.ToString() == "Администратор сайта") //изменить гостиницу
            {
                try
                {
                    sql = String.Format("Update hotel set name_hotel='{0}', adress ='{1}', phone_hotel='{2}',email_hotel='{3}', headname='{4}', " +
                        "category='{5}', type_hotel='{6}' where id_hotel={7}",
                      DataGridMain[1, DataGridMain.CurrentCell.RowIndex].Value.ToString(), DataGridMain[2, DataGridMain.CurrentCell.RowIndex].Value.ToString(),
                      DataGridMain[3, DataGridMain.CurrentCell.RowIndex].Value.ToString(), DataGridMain[4, DataGridMain.CurrentCell.RowIndex].Value.ToString(),
                      DataGridMain[5, DataGridMain.CurrentCell.RowIndex].Value.ToString(), DataGridMain[6, DataGridMain.CurrentCell.RowIndex].Value.ToString(),
                      DataGridMain[7, DataGridMain.CurrentCell.RowIndex].Value.ToString(), DataGridMain[0, DataGridMain.CurrentCell.RowIndex].Value.ToString());
                    Operation(sql);

                    sql = String.Format("select id_service_hotel, services_hotel from service_hotel, hotel, service_rendered_hotel where " +
                        "service_rendered_hotel.hotel = hotel.id_hotel and service_rendered_hotel.service_hotel = service_hotel.id_service_hotel" +
                        " and service_rendered_hotel.hotel = {0}", DataGridMain[0, DataGridMain.CurrentCell.RowIndex].Value.ToString());

                    string[] mass = Regex.Split(DataGridMain[8, DataGridMain.CurrentCell.RowIndex].Value.ToString(), ", ");
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, npgSqlConnection);
                    dstemp.Reset();
                    da.Fill(dstemp);
                    dttemp = dstemp.Tables[0];
                    bool have = false;
                    int ind = 0;
                    if (dttemp.Rows.Count > 0)
                    {
                        for (int i = 0; i < dttemp.Rows.Count; i++)
                        {
                            have = false;
                            for (int j = 0; j < mass.Length; j++)
                            {
                                if (dttemp.Rows[i].ItemArray.ElementAt(1).ToString() == mass[j])
                                {
                                    have = true;
                                    ind = i;
                                }
                            }
                            if (!have)
                            {
                                sql = String.Format("delete from service_rendered_hotel where service_hotel={0} and hotel={1}", dttemp.Rows[ind].ItemArray.ElementAt(0).ToString(), DataGridMain[0, DataGridMain.CurrentCell.RowIndex].Value.ToString());
                                Operation(sql);
                            }
                        }
                    }
                    for (int i = 0; i < mass.Length; i++)
                    {
                        sql = String.Format("select id_service_hotel from service_hotel where '{0}' in (services_hotel)", mass[i]);
                        da = new NpgsqlDataAdapter(sql, npgSqlConnection);
                        dstemp.Reset();
                        da.Fill(dstemp);
                        dttemp = dstemp.Tables[0];
                        if (dttemp.Rows.Count > 0)
                        {
                        }
                        else
                        {
                            sql = String.Format("INSERT INTO service_hotel (services_hotel) VALUES('{0}') RETURNING id_service_hotel", mass[i]);
                            da = new NpgsqlDataAdapter(sql, npgSqlConnection);
                            dstemp.Reset();
                            da.Fill(dstemp);
                            dttemp = dstemp.Tables[0];
                            sql = String.Format("INSERT INTO service_rendered_hotel VALUES({0}, {1})", dttemp.Rows[0].ItemArray.ElementAt(0).ToString(), DataGridMain[0, DataGridMain.CurrentCell.RowIndex].Value.ToString());
                            Operation(sql);
                        }

                    }

                    sql = String.Format("select * from log_in  where '{0}' in (hotel)", DataGridMain[0, DataGridMain.CurrentCell.RowIndex].Value.ToString());
                    da = new NpgsqlDataAdapter(sql, npgSqlConnection);
                    dstemp.Reset();
                    da.Fill(dstemp);
                    dttemp = dstemp.Tables[0];
                    if (dttemp.Rows.Count > 0)
                    {
                        sql = String.Format("Update log_in set login_hotel ='{0}', password_hotel='{1}' where hotel={2}", DataGridMain[9, DataGridMain.CurrentCell.RowIndex].Value.ToString(), DataGridMain[10, DataGridMain.CurrentCell.RowIndex].Value.ToString(), DataGridMain[0, DataGridMain.CurrentCell.RowIndex].Value.ToString());
                        Operation(sql);
                    }
                    else
                    {
                        sql = String.Format("INSERT INTO log_in VALUES({0}, '{1}', '{2}') ", DataGridMain[0, DataGridMain.CurrentCell.RowIndex].Value.ToString(), DataGridMain[9, DataGridMain.CurrentCell.RowIndex].Value.ToString(), DataGridMain[10, DataGridMain.CurrentCell.RowIndex].Value.ToString());
                        Operation(sql);
                    }

                    button_Enter_Click(sender, e);
                }
                catch
                { }
            }
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Гостиница") // удалить номер
            {
                try
                {
                    sql = String.Format("select service_rendered_room.room, service_rendered_room.service_room from room, service_rendered_room, " +
                        "service_room where room.id_room = {0} and service_rendered_room.service_room = service_room.id_service_room and " +
                        "service_rendered_room.room = room.id_room", DataGridMain[0, DataGridMain.CurrentCell.RowIndex].Value.ToString());
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, npgSqlConnection);
                    dstemp.Reset();
                    da.Fill(dstemp);
                    dttemp = dstemp.Tables[0];
                    for (int i = 0; i < dttemp.Rows.Count; i++)
                    {
                        sql = String.Format("Delete from service_rendered_room where room={0} and service_room={1}", dttemp.Rows[i].ItemArray.ElementAt(0).ToString(), dttemp.Rows[i].ItemArray.ElementAt(1).ToString());
                        Operation(sql);
                    }
                    sql = String.Format("select id_booking, id_payment from room, booking, payment where room.id_room = {0} and booking.room = room.id_room " +
                        "and payment.booking = booking.id_booking", DataGridMain[0, DataGridMain.CurrentCell.RowIndex].Value.ToString());
                    da = new NpgsqlDataAdapter(sql, npgSqlConnection);
                    dstemp.Reset();
                    da.Fill(dstemp);
                    dttemp = dstemp.Tables[0];
                    for (int i = 0; i < dttemp.Rows.Count; i++)
                    {
                        sql = String.Format("Delete from payment where id_payment={0}", dttemp.Rows[i].ItemArray.ElementAt(1).ToString());
                        Operation(sql);
                    }
                    sql = String.Format(" select id_booking, id_customer from room, booking, customer where room.id_room = {0} and " +
                        "booking.room = room.id_room and booking.customer = customer.id_customer", DataGridMain[0, DataGridMain.CurrentCell.RowIndex].Value.ToString());
                    da = new NpgsqlDataAdapter(sql, npgSqlConnection);
                    dstemp.Reset();
                    da.Fill(dstemp);
                    dttemp = dstemp.Tables[0];
                    for (int i = 0; i < dttemp.Rows.Count; i++)
                    {
                        sql = String.Format("Delete from booking where id_booking={0}", dttemp.Rows[i].ItemArray.ElementAt(0).ToString());
                        Operation(sql);
                        sql = String.Format("Delete from customer where id_customer={0}", dttemp.Rows[i].ItemArray.ElementAt(1).ToString());
                        Operation(sql);
                    }
                    sql = String.Format(" select id_booking from room, booking where room.id_room = {0} and " +
                        "booking.room = room.id_room", DataGridMain[0, DataGridMain.CurrentCell.RowIndex].Value.ToString());
                    da = new NpgsqlDataAdapter(sql, npgSqlConnection);
                    dstemp.Reset();
                    da.Fill(dstemp);
                    dttemp = dstemp.Tables[0];
                    for (int i = 0; i < dttemp.Rows.Count; i++)
                    {
                        sql = String.Format("Delete from booking where id_booking={0}", dttemp.Rows[i].ItemArray.ElementAt(0).ToString());
                        Operation(sql);
                    }
                    sql = String.Format("Delete from room where id_room={0}", DataGridMain[0, DataGridMain.CurrentCell.RowIndex].Value.ToString());
                    Operation(sql);
                    labelMessage.Visible = false;
                    button_Enter_Click(sender, e);
                }
                catch
                {
                    labelMessage.Visible = true;
                    labelMessage.Text = "Выберите номер";
                }
            }
            if (comboBox1.SelectedItem.ToString() == "Клиент") //убрать бронь
            {
                try
                {
                    sql = String.Format("select room, customer from booking where id_booking={0}", bookid);
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, npgSqlConnection);
                    dstemp.Reset();
                    da.Fill(dstemp);
                    dttemp = dstemp.Tables[0];
                    sql = String.Format("Update room set status=false where id_room={0}", dttemp.Rows[0].ItemArray.ElementAt(0).ToString());
                    Operation(sql);
                    sql = String.Format("Delete from payment where booking={0}", bookid);
                    Operation(sql);
                    sql = String.Format("delete from booking where id_booking={0}", bookid);
                    Operation(sql);
                    sql = String.Format("delete from customer where id_customer={0}", dttemp.Rows[0].ItemArray.ElementAt(1).ToString());
                    Operation(sql);
                    buttonShowBook_Click(sender, e);
                }
                catch { }
            }
            if (comboBox1.SelectedItem.ToString() == "Администратор сайта") //удалить гостиницу
            {
                try
                {
                    sql = String.Format("select service_rendered_hotel.hotel, service_rendered_hotel.service_hotel from hotel, service_rendered_hotel," +
                        " service_hotel where hotel.id_hotel = {0} and service_rendered_hotel.service_hotel = service_hotel.id_service_hotel and " +
                        "service_rendered_hotel.hotel = hotel.id_hotel", DataGridMain[0, DataGridMain.CurrentCell.RowIndex].Value.ToString());
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, npgSqlConnection);
                    dstemp.Reset();
                    da.Fill(dstemp);
                    dttemp = dstemp.Tables[0];
                    for (int i = 0; i < dttemp.Rows.Count; i++)
                    {
                        sql = String.Format("Delete from service_rendered_hotel where hotel={0} and service_hotel={1}", dttemp.Rows[i].ItemArray.ElementAt(0).ToString(), dttemp.Rows[i].ItemArray.ElementAt(1).ToString());
                        Operation(sql);
                    }
                    sql = String.Format("Delete from log_in where hotel={0}", DataGridMain[0, DataGridMain.CurrentCell.RowIndex].Value.ToString());
                    Operation(sql);
                    sql = String.Format("Delete from feedback where hotel={0}", DataGridMain[0, DataGridMain.CurrentCell.RowIndex].Value.ToString());
                    Operation(sql);

                    sql = String.Format("select service_rendered_room.room, service_rendered_room.service_room from room, service_rendered_room, " +
                        "service_room where room.hotel = {0} and service_rendered_room.service_room = service_room.id_service_room and " +
                        "service_rendered_room.room = room.id_room", DataGridMain[0, DataGridMain.CurrentCell.RowIndex].Value.ToString());
                    da = new NpgsqlDataAdapter(sql, npgSqlConnection);
                    dstemp.Reset();
                    da.Fill(dstemp);
                    dttemp = dstemp.Tables[0];
                    for (int i = 0; i < dttemp.Rows.Count; i++)
                    {
                        sql = String.Format("Delete from service_rendered_room where room={0} and service_room={1}", dttemp.Rows[i].ItemArray.ElementAt(0).ToString(), dttemp.Rows[i].ItemArray.ElementAt(1).ToString());
                        Operation(sql);
                    }
                    sql = String.Format("select id_booking, id_payment from room, booking, payment where room.hotel = {0} and booking.room = room.id_room " +
                        "and payment.booking = booking.id_booking", DataGridMain[0, DataGridMain.CurrentCell.RowIndex].Value.ToString());
                    da = new NpgsqlDataAdapter(sql, npgSqlConnection);
                    dstemp.Reset();
                    da.Fill(dstemp);
                    dttemp = dstemp.Tables[0];
                    for (int i = 0; i < dttemp.Rows.Count; i++)
                    {
                        sql = String.Format("Delete from payment where id_payment={0}", dttemp.Rows[i].ItemArray.ElementAt(1).ToString());
                        Operation(sql);
                    }
                    sql = String.Format(" select id_booking, id_customer from room, booking, customer where room.hotel = {0} and " +
                        "booking.room = room.id_room and booking.customer = customer.id_customer", DataGridMain[0, DataGridMain.CurrentCell.RowIndex].Value.ToString());
                    da = new NpgsqlDataAdapter(sql, npgSqlConnection);
                    dstemp.Reset();
                    da.Fill(dstemp);
                    dttemp = dstemp.Tables[0];
                    for (int i = 0; i < dttemp.Rows.Count; i++)
                    {
                        sql = String.Format("Delete from booking where id_booking={0}", dttemp.Rows[i].ItemArray.ElementAt(0).ToString());
                        Operation(sql);
                        sql = String.Format("Delete from customer where id_customer={0}", dttemp.Rows[i].ItemArray.ElementAt(1).ToString());
                        Operation(sql);
                    }
                    sql = String.Format(" select id_booking from room, booking where room.id_room = {0} and " +
                       "booking.room = room.id_room", DataGridMain[0, DataGridMain.CurrentCell.RowIndex].Value.ToString());
                    da = new NpgsqlDataAdapter(sql, npgSqlConnection);
                    dstemp.Reset();
                    da.Fill(dstemp);
                    dttemp = dstemp.Tables[0];
                    for (int i = 0; i < dttemp.Rows.Count; i++)
                    {
                        sql = String.Format("Delete from booking where id_booking={0}", dttemp.Rows[i].ItemArray.ElementAt(0).ToString());
                        Operation(sql);
                    }
                    sql = String.Format("Delete from hotel where id_hotel={0}", DataGridMain[0, DataGridMain.CurrentCell.RowIndex].Value.ToString());
                    Operation(sql);
                    button_Enter_Click(sender, e);
                    labelMessage.Visible = false;
                }
                catch
                {
                    labelMessage.Visible = true;
                    labelMessage.Text = "Выберите гостиницу";
                }
            }
        }

        private void button_Book_Click(object sender, EventArgs e)
        {
            try
            {
                sql = String.Format("Update room set status=true where id_room={0}", DataGridFeedback[0, DataGridFeedback.CurrentCell.RowIndex].Value.ToString());
                Operation(sql);
                sql = String.Format("INSERT INTO customer (firstname, lastname, patronymic, phone_customer, email_customer, citizenship) " +
                    "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}') RETURNING id_customer", textBoxFN.Text, textBoxSN.Text, textBoxPN.Text, textBoxPh.Text, textBoxEm.Text, textBoxGr.Text);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, npgSqlConnection);
                dstemp.Reset();
                da.Fill(dstemp);
                dttemp = dstemp.Tables[0];
                sql = String.Format("INSERT INTO booking (arrivaldate, datedeparture, customer, room) " +
                    "VALUES ('{0}', '{1}', {2}, {3})  RETURNING id_booking", dateTimePicker1.Value.ToString(), dateTimePicker2.Value.ToString(), dttemp.Rows[0].ItemArray.ElementAt(0).ToString(), DataGridFeedback[0, DataGridFeedback.CurrentCell.RowIndex].Value.ToString());
                da = new NpgsqlDataAdapter(sql, npgSqlConnection);
                dstemp.Reset();
                da.Fill(dstemp);
                dttemp = dstemp.Tables[0];
                bookid = dttemp.Rows[0].ItemArray.ElementAt(0).ToString();
                labelMessage.Text = "Поля со * обязательны для заполнения";
                buttonShowBook_Click(sender, e);
            }
            catch
            {
                labelMessage.Text = "Выберите номер и заполните обязательные поля";
            }
        }

        private void button_Feedback_Click(object sender, EventArgs e)
        {
            try
            {
                sql = String.Format("INSERT INTO feedback (hotel, text_feedback, person, date_feedback) VALUES ({0}, '{1}', '{2}', '{3}')", nameHotel1, textBoxfeed.Text, textBoxFN.Text, DateTime.Now);
                Operation(sql);
                button_ShowFeedback_Click(sender, e);
            }
            catch { }
        }
        private void Form_Load(object sender, EventArgs e)
        {
            npgSqlConnection.Open();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            npgSqlConnection.Close();
        }
        private void buttonDelFeed_Click(object sender, EventArgs e)
        {
            try
            {
                labelMessage.Visible = false;
                sql = String.Format("Delete from feedback where feedback.text_feedback = '{0}' and feedback.hotel={1}", DataGridFeedback[0, DataGridFeedback.CurrentCell.RowIndex].Value.ToString(), DataGridMain[0, DataGridMain.CurrentCell.RowIndex].Value.ToString());
                Operation(sql);
                button_ShowFeedback_Click(sender, e);
            }
            catch
            {
                labelMessage.Visible = true;
                labelMessage.Text = "Выберите отзыв";
            }
        }

        private void buttonShowBook_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Гостиница")
            {
                try
                {
                    labalFeedback.Text = "Бронирования";
                    DataGridFeedback.Visible = true;
                    labalFeedback.Visible = true;
                    labelMessage.Visible = false;
                    sql = String.Format("select customer.firstname as Имя, customer.lastname as Фамилия, customer.patronymic as Отчество," +
                    "customer.phone_customer as Телефон, customer.email_customer as \"Email\", customer.citizenship as Гражданство, booking.arrivaldate as \"Дата приезда\", booking.datedeparture as \"Дата отъезда\" from customer," +
                    "booking, room where  booking.room = room.id_room and  booking.customer = customer.id_customer and room.id_room={0}", nameHotel1);
                    Select(sql, DataGridFeedback, dsfeed, dtfeed);
                }
                catch
                {
                    labelMessage.Visible = true;
                    labelMessage.Text = "Выберите номер";
                }
            }
            if (comboBox1.SelectedItem.ToString() == "Клиент")
            {
                try
                {
                    sql = String.Format("SELECT id_room, Цена, Бронь, Расположение, Тип, Услуги from(SELECT room.hotel, room.id_room, room.price as Цена, " +
                        "room.status as Бронь, room.location as Расположение, room.level as Тип from room where room.hotel = {0} and room.status=false) as rm LEFT JOIN " +
                        "(select room.id_room as idtbserv, STRING_AGG(service_room.services_room, ', ') as Услуги  from service_rendered_room, " +
                        "service_room, room  where service_rendered_room.service_room = service_room.id_service_room  and service_rendered_room.room = room.id_room " +
                        "GROUP BY room.id_room) as tbserv ON  rm.id_room = idtbserv", nameHotel1);
                    Select(sql, DataGridFeedback, dsfeed, dtfeed);
                    labalFeedback.Text = "Номера";
                    textBoxfeed.Visible = false;
                    button_Feedback.Visible = false;
                    button_Book.Visible = true;
                    labelMessage.Text = "Поля со * обязательны дял заполнения";
                }
                catch
                {
                    labelMessage.Visible = true;
                    labelMessage.Text = "Выберите гостиницу";
                }
            }
        }

        private void checkBoxPay_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPay.Checked == true)
            {
                textBoxPay.Visible = true;
                try
                {
                    sql = String.Format("select price from room where id_room={0}", DataGridFeedback[0, DataGridFeedback.CurrentCell.RowIndex].Value.ToString());
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, npgSqlConnection);
                    dstemp.Reset();
                    da.Fill(dstemp);
                    dttemp = dstemp.Tables[0];
                    int pr = Convert.ToInt32(dttemp.Rows[0].ItemArray.ElementAt(0)) * ((int)(dateTimePicker2.Value - dateTimePicker1.Value).TotalDays + 1);

                    sql = String.Format("INSERT INTO payment (paymentmethod,bill,date_payment,booking) VALUES ({0}, '{1}', '{2}', '{3}')", textBoxPay.Text, pr, DateTime.Now, bookid);
                    MessageBox.Show("Cчет - " + pr.ToString() + " за " + ((int)(dateTimePicker2.Value - dateTimePicker1.Value).TotalDays + 1).ToString() + " дней. Время оплаты: " + DateTime.Now.ToString(), "Чек", MessageBoxButtons.OK);
                    Operation(sql);
                }
                catch { }
            }
            else
            {
                textBoxPay.Visible = false;
            }
        }

        private void button_return_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Администратор сайта")
            {
                int back = 0, forward = 0;
                DateTime date2 = dateTimePicker1.Value;
                //   ToLongTimeString();
                sql = String.Format("SELECT Id, tim, tablename, operation, idtb, name,  adress, phone,  email, headname, category, type, refer, services " +
                    " FROM temper WHERE tim>'{0}' ORDER BY id DESC", date2);
                try
                {
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, npgSqlConnection);
                    dstemp.Reset();
                    da.Fill(dstemp);
                    dttemp = dstemp.Tables[0];
                    back = Convert.ToInt32(dttemp.Rows[0].ItemArray.ElementAt(0));
                    forward = dttemp.Rows.Count;
                }
                catch
                {
                    MessageBox.Show("Нет действий", "Ошибка", MessageBoxButtons.OK);
                }
                for (int i = 0; i < forward; i++)
                {
                    try
                    {
                        sql = String.Format("SELECT Id, tim, tablename, operation, idtb, name,  adress, phone,  email, headname, category, type, refer, services " +
                    " FROM temper WHERE Id='{0}'", back);
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, npgSqlConnection);
                        dstemp.Reset();
                        da.Fill(dstemp);
                        dttemp = dstemp.Tables[0];

                        if (dttemp.Rows[0].ItemArray.ElementAt(3).ToString() == "INSERT")
                        {
                            if (dttemp.Rows[0].ItemArray.ElementAt(2).ToString() == "service_hotel")
                            {
                                sql = String.Format("DELETE FROM service_hotel Where id_service_hotel='{0}'", dttemp.Rows[0].ItemArray.ElementAt(4).ToString());
                                NpgsqlCommand cmd = new NpgsqlCommand(sql, npgSqlConnection);
                            }
                            if (dttemp.Rows[0].ItemArray.ElementAt(2).ToString() == "hotel")
                            {
                                sql = String.Format("DELETE FROM hotel Where id_hotel='{0}'", dttemp.Rows[0].ItemArray.ElementAt(4).ToString());
                                NpgsqlCommand cmd = new NpgsqlCommand(sql, npgSqlConnection);
                            }
                            if (dttemp.Rows[0].ItemArray.ElementAt(2).ToString() == "service_rendered_hotel")
                            {
                                sql = String.Format("DELETE FROM service_rendered_hotel Where hotel='{0}' and service_hotel='{1}'", dttemp.Rows[0].ItemArray.ElementAt(12).ToString(), dttemp.Rows[0].ItemArray.ElementAt(4).ToString());
                                NpgsqlCommand cmd = new NpgsqlCommand(sql, npgSqlConnection);
                            }
                        }
                        if (dttemp.Rows[0].ItemArray.ElementAt(3).ToString() == "UPDATE")
                        {
                            if (dttemp.Rows[0].ItemArray.ElementAt(2).ToString() == "service_hotel")
                            {
                                sql = String.Format("Update service_hotel set id_service_hotel='{0}',services_hotel='{1}'", dttemp.Rows[0].ItemArray.ElementAt(4).ToString(), dttemp.Rows[0].ItemArray.ElementAt(13).ToString());
                                NpgsqlCommand cmd = new NpgsqlCommand(sql, npgSqlConnection);
                            }
                            if (dttemp.Rows[0].ItemArray.ElementAt(2).ToString() == "hotel")
                            {
                                sql = String.Format("Update hotel set id_hotel ='{0}', name_hotel='{1}', adress='{2}', phone_hotel='{3}', email='{4}', " +
                                    "headname='{5}',  category='{6}', type='{7}'", dttemp.Rows[0].ItemArray.ElementAt(4).ToString(), dttemp.Rows[0].ItemArray.ElementAt(5).ToString()
                                    , dttemp.Rows[0].ItemArray.ElementAt(6).ToString(), dttemp.Rows[0].ItemArray.ElementAt(7).ToString(), dttemp.Rows[0].ItemArray.ElementAt(8).ToString()
                                    , dttemp.Rows[0].ItemArray.ElementAt(9).ToString(), dttemp.Rows[0].ItemArray.ElementAt(10).ToString(), dttemp.Rows[0].ItemArray.ElementAt(11).ToString());
                                NpgsqlCommand cmd = new NpgsqlCommand(sql, npgSqlConnection);
                            }
                            if (dttemp.Rows[0].ItemArray.ElementAt(2).ToString() == "service_rendered_hotel")
                            {
                                sql = String.Format("Update service_rendered_hotel set service_hotel='{0}' and hotel='{1}'", dttemp.Rows[0].ItemArray.ElementAt(12).ToString(), dttemp.Rows[0].ItemArray.ElementAt(4).ToString());
                                NpgsqlCommand cmd = new NpgsqlCommand(sql, npgSqlConnection);
                            }
                        }
                        if (dttemp.Rows[0].ItemArray.ElementAt(3).ToString() == "DELETE")
                        {
                            if (dttemp.Rows[0].ItemArray.ElementAt(2).ToString() == "service_hotel")
                            {
                                sql = String.Format("Insert into service_hotel Values ('{0}','{1}')", dttemp.Rows[0].ItemArray.ElementAt(4).ToString(), dttemp.Rows[0].ItemArray.ElementAt(13).ToString());
                                NpgsqlCommand cmd = new NpgsqlCommand(sql, npgSqlConnection);
                            }
                            if (dttemp.Rows[0].ItemArray.ElementAt(2).ToString() == "hotel")
                            {
                                sql = String.Format("Insert into hotel(id_hotel, name_hotel, adress, phone_hotel , email_hotel,headname, category,type_hotel) Values ({0}, '{1}', '{2}', '{3}','{4}', '{5}', '{6}', '{7}')",
                                    dttemp.Rows[0].ItemArray.ElementAt(4).ToString(), dttemp.Rows[0].ItemArray.ElementAt(5).ToString()
                                    , dttemp.Rows[0].ItemArray.ElementAt(6).ToString(), dttemp.Rows[0].ItemArray.ElementAt(7).ToString(), dttemp.Rows[0].ItemArray.ElementAt(8).ToString()
                                    , dttemp.Rows[0].ItemArray.ElementAt(9).ToString(), dttemp.Rows[0].ItemArray.ElementAt(10).ToString(), dttemp.Rows[0].ItemArray.ElementAt(11).ToString());
                                NpgsqlCommand cmd = new NpgsqlCommand(sql, npgSqlConnection);
                            }
                            if (dttemp.Rows[0].ItemArray.ElementAt(2).ToString() == "service_rendered_hotel")
                            {
                                sql = String.Format("Insert into service_rendered_hotel (hotel, service_hotel) VALUES  ({0} ,{1})", dttemp.Rows[0].ItemArray.ElementAt(12).ToString(), dttemp.Rows[0].ItemArray.ElementAt(4).ToString());
                                NpgsqlCommand cmd = new NpgsqlCommand(sql, npgSqlConnection);
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("ошибка операции", "Ошибка", MessageBoxButtons.OK);
                    }
                    back--;
                }
                button_Enter_Click(sender, e);
            }
        }

        private void buttonreturn1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Администратор сайта")
            {
                if (textBoxGr.Text == "шагов назад")
                {
                    labelMessage.Visible = true;
                    labelMessage.Text = "Введите количество действий для восстановления, учитывая услуги";
                }
                else
                {
                    labelMessage.Visible = false;
                }
                int back = 0, forward = 0;
                //   ToLongTimeString();
                DateTime date1 = DateTime.Now;

                sql = String.Format("SELECT Id, tim, tablename, operation, idtb, name,  adress, phone,  email, headname, category, type, refer, services " +
                    " FROM temper WHERE tim<'{0}' ORDER BY tim DESC LIMIT 1 ", date1);
                try
                {
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, npgSqlConnection);
                    dstemp.Reset();
                    da.Fill(dstemp);
                    dttemp = dstemp.Tables[0];
                    back = Convert.ToInt32(dttemp.Rows[0].ItemArray.ElementAt(0));
                    MessageBox.Show(back.ToString(), "Ошибка", MessageBoxButtons.OK);
                    //  MessageBox.Show(dttemp.Rows[0].ItemArray.ElementAt(0).ToString(), "Message", MessageBoxButtons.OK);
                    forward = Convert.ToInt32(textBoxGr.Text);
                    MessageBox.Show(forward.ToString(), "Ошибка", MessageBoxButtons.OK);
                }
                catch
                {
                    MessageBox.Show("Нет действий", "Ошибка", MessageBoxButtons.OK);
                }
                for (int i = 0; i < forward; i++)
                {
                    try
                    {
                        sql = String.Format("SELECT Id, tim, tablename, operation, idtb, name,  adress, phone,  email, headname, category, type, refer, services " +
                    " FROM temper WHERE Id='{0}'", back);
                        // MessageBox.Show(sql, "Message", MessageBoxButtons.OK);
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, npgSqlConnection);
                        dstemp.Reset();
                        da.Fill(dstemp);
                        dttemp = dstemp.Tables[0];

                        if (dttemp.Rows[0].ItemArray.ElementAt(3).ToString() == "INSERT")
                        {
                            if (dttemp.Rows[0].ItemArray.ElementAt(2).ToString() == "service_hotel")
                            {
                                sql = String.Format("DELETE FROM service_hotel Where id_service_hotel='{0}'", dttemp.Rows[0].ItemArray.ElementAt(4).ToString());
                                NpgsqlCommand cmd = new NpgsqlCommand(sql, npgSqlConnection);
                            }
                            if (dttemp.Rows[0].ItemArray.ElementAt(2).ToString() == "hotel")
                            {
                                sql = String.Format("DELETE FROM hotel Where id_hotel='{0}'", dttemp.Rows[0].ItemArray.ElementAt(4).ToString());
                                NpgsqlCommand cmd = new NpgsqlCommand(sql, npgSqlConnection);
                            }
                            if (dttemp.Rows[0].ItemArray.ElementAt(2).ToString() == "service_rendered_hotel")
                            {
                                sql = String.Format("DELETE FROM service_rendered_hotel Where hotel='{0}' and service_hotel='{1}'", dttemp.Rows[0].ItemArray.ElementAt(12).ToString(), dttemp.Rows[0].ItemArray.ElementAt(4).ToString());
                                NpgsqlCommand cmd = new NpgsqlCommand(sql, npgSqlConnection);
                            }
                        }
                        if (dttemp.Rows[0].ItemArray.ElementAt(3).ToString() == "UPDATE")
                        {
                            if (dttemp.Rows[0].ItemArray.ElementAt(2).ToString() == "service_hotel")
                            {
                                sql = String.Format("Update service_hotel set id_service_hotel='{0}',services_hotel='{1}'", dttemp.Rows[0].ItemArray.ElementAt(4).ToString(), dttemp.Rows[0].ItemArray.ElementAt(13).ToString());
                                NpgsqlCommand cmd = new NpgsqlCommand(sql, npgSqlConnection);
                            }
                            if (dttemp.Rows[0].ItemArray.ElementAt(2).ToString() == "hotel")
                            {
                                 
                                sql = String.Format("Update hotel set id_hotel ='{0}', name_hotel='{1}', adress='{2}', phone_hotel='{3}', email='{4}', " +
                                    "headname='{5}',  category='{6}', type='{7}'", dttemp.Rows[0].ItemArray.ElementAt(4).ToString(), dttemp.Rows[0].ItemArray.ElementAt(5).ToString()
                                    , dttemp.Rows[0].ItemArray.ElementAt(6).ToString(), dttemp.Rows[0].ItemArray.ElementAt(7).ToString(), dttemp.Rows[0].ItemArray.ElementAt(8).ToString()
                                    , dttemp.Rows[0].ItemArray.ElementAt(9).ToString(), dttemp.Rows[0].ItemArray.ElementAt(10).ToString(), dttemp.Rows[0].ItemArray.ElementAt(11).ToString());
                                NpgsqlCommand cmd = new NpgsqlCommand(sql, npgSqlConnection);
                            }
                            if (dttemp.Rows[0].ItemArray.ElementAt(2).ToString() == "service_rendered_hotel")
                            {
                                sql = String.Format("Update service_rendered_hotel set service_hotel='{0}' and hotel='{1}'", dttemp.Rows[0].ItemArray.ElementAt(12).ToString(), dttemp.Rows[0].ItemArray.ElementAt(4).ToString());
                                NpgsqlCommand cmd = new NpgsqlCommand(sql, npgSqlConnection);
                            }
                        }
                        if (dttemp.Rows[0].ItemArray.ElementAt(3).ToString() == "DELETE")
                        {
                            if (dttemp.Rows[0].ItemArray.ElementAt(2).ToString() == "service_hotel")
                            {
                                sql = String.Format("Insert into service_hotel Values ('{0}','{1}')", dttemp.Rows[0].ItemArray.ElementAt(4).ToString(), dttemp.Rows[0].ItemArray.ElementAt(13).ToString());
                                NpgsqlCommand cmd = new NpgsqlCommand(sql, npgSqlConnection);
                            }
                            if (dttemp.Rows[0].ItemArray.ElementAt(2).ToString() == "hotel")
                            {
                                MessageBox.Show(dttemp.Rows[0].ItemArray.ElementAt(2).ToString(), "Ошибка", MessageBoxButtons.OK);
                                sql = String.Format("INSERT into hotel (id_hotel, name_hotel, adress, phone_hotel , email_hotel,headname, category,type_hotel) Values ('{0}', '{1}', '{2}', '{3}','{4}', '{5}', '{6}', '{7}')",
                                    dttemp.Rows[0].ItemArray.ElementAt(4).ToString(), dttemp.Rows[0].ItemArray.ElementAt(5).ToString()
                                    , dttemp.Rows[0].ItemArray.ElementAt(6).ToString(), dttemp.Rows[0].ItemArray.ElementAt(7).ToString(), dttemp.Rows[0].ItemArray.ElementAt(8).ToString()
                                    , dttemp.Rows[0].ItemArray.ElementAt(9).ToString(), dttemp.Rows[0].ItemArray.ElementAt(10).ToString(), dttemp.Rows[0].ItemArray.ElementAt(11).ToString());
                                MessageBox.Show(sql, "Ошибка", MessageBoxButtons.OK);
                                NpgsqlCommand cmd = new NpgsqlCommand(sql, npgSqlConnection);
                            }
                            if (dttemp.Rows[0].ItemArray.ElementAt(2).ToString() == "service_rendered_hotel")
                            {
                                sql = String.Format("Insert into service_rendered_hotel (hotel, service_hotel) VALUES  ({0} ,{1})", dttemp.Rows[0].ItemArray.ElementAt(12).ToString(), dttemp.Rows[0].ItemArray.ElementAt(4).ToString());
                                NpgsqlCommand cmd = new NpgsqlCommand(sql, npgSqlConnection);
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("ошибка операции", "Ошибка", MessageBoxButtons.OK);
                    }
                    back--;
                }
                button_Enter_Click(sender, e);
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Interf();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((checkedListBox1.GetItemChecked(0)) && (!checkedListBox1.GetItemChecked(1)))
            {
                sql = String.Format("SELECT id_room, Цена, Бронь, Расположение, Тип, Услуги from(SELECT room.hotel, room.id_room, room.price as Цена, " +
                        "room.status as Бронь, room.location as Расположение, room.level as Тип from room where room.hotel = {0} and room.status=false) as rm LEFT JOIN " +
                        "(select room.id_room as idtbserv, STRING_AGG(service_room.services_room, ', ') as Услуги  from service_rendered_room, " +
                        "service_room, room  where service_rendered_room.service_room = service_room.id_service_room  and service_rendered_room.room = room.id_room " +
                        "GROUP BY room.id_room) as tbserv ON  rm.id_room = idtbserv", hotelid);
                Select(sql, DataGridMain, dshotel, dthotel);
            }
            if ((!checkedListBox1.GetItemChecked(0)) && (checkedListBox1.GetItemChecked(1)))
            {
                sql = String.Format("SELECT id_room, Цена, Бронь, Расположение, Тип, Услуги from(SELECT room.hotel, room.id_room, room.price as Цена, " +
                        "room.status as Бронь, room.location as Расположение, room.level as Тип from room where room.hotel = {0} and room.status=true) as rm LEFT JOIN " +
                        "(select room.id_room as idtbserv, STRING_AGG(service_room.services_room, ', ') as Услуги  from service_rendered_room, " +
                        "service_room, room  where service_rendered_room.service_room = service_room.id_service_room  and service_rendered_room.room = room.id_room " +
                        "GROUP BY room.id_room) as tbserv ON  rm.id_room = idtbserv", hotelid);
                Select(sql, DataGridMain, dshotel, dthotel);
            }
            if ((checkedListBox1.GetItemChecked(0)) && (checkedListBox1.GetItemChecked(1)))
            {
                sql = String.Format("SELECT id_room, Цена, Бронь, Расположение, Тип, Услуги from(SELECT room.hotel, room.id_room, room.price as Цена, " +
                        "room.status as Бронь, room.location as Расположение, room.level as Тип from room where room.hotel = {0}) as rm LEFT JOIN " +
                        "(select room.id_room as idtbserv, STRING_AGG(service_room.services_room, ', ') as Услуги  from service_rendered_room, " +
                        "service_room, room  where service_rendered_room.service_room = service_room.id_service_room  and service_rendered_room.room = room.id_room " +
                        "GROUP BY room.id_room) as tbserv ON  rm.id_room = idtbserv", hotelid);
                Select(sql, DataGridMain, dshotel, dthotel);
            }
        }

        private void button_Find_Click(object sender, EventArgs e)
        {
            sql = String.Format("select hotel.id_hotel, hotel.name_hotel AS Имя, hotel.adress AS Адрес, hotel.category AS Категория, hotel.type_hotel AS Тип, " +
                 "STRING_AGG(DISTINCT service_hotel.services_hotel, ', ') AS Услуги from hotel, service_hotel, service_rendered_hotel, room " +
                 "where service_rendered_hotel.hotel = hotel.id_hotel and room.hotel = hotel.id_hotel and service_rendered_hotel.service_hotel =" +
                 " service_hotel.id_service_hotel and case when 'по названию' <> '{0}' and '' <> '{0}' then  hotel.name_hotel = '{0}'  else '1' end and case" +
                 " when  '' <> '{1}' then room.price > '{1}' else '1' end and case when '' <> '{2}' then room.price < '{2}' " +
                 " else '1' end GROUP BY hotel.id_hotel, hotel.name_hotel, hotel.adress, hotel.category, hotel.type_hotel", textBoxNamehotel.Text, textBoxMinPrice.Text,
                 textBoxMaxPrice.Text);

            Select(sql, DataGridMain, dshotel, dthotel);
        }
    }
}
