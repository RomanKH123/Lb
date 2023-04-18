using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demka_0._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();  
        }
        //Обновление базы данных
        private void Update()
        {
            //обновить содержимое таблиц базы данных
            oleDbDataAdapter1.Update(dataSet11);
            //очистить DataSet
            dataSet11.Clear();
            //заполнить таблицы в объекте DataSet
            oleDbDataAdapter1.Fill(dataSet11.Avto);
        }
        //Соединение
        private void Form1_Load(object sender, EventArgs e)
        {
            oleDbConnection1.Open(); //открыть соединение
                                     //заполнить таблицы в объекте DataSet
            oleDbDataAdapter1.Fill(dataSet11.Avto);
            dataGridView1.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            Update();            
        }
       
        //Добавляем
        private void button1_Click(object sender, EventArgs e)
        {
            //если текстовые поля не пусты,
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                //то создать новую запись в таблице Contacts,
                DataRow row = dataSet11.Avto.NewRow();
                //заполнить ее столбцы
                //Вставляем номер
                row["Номер"] = textBox1.Text;
                // Вставляем этаж
                row["Вид работы"] = textBox2.Text;
                //Вставляем работника
                row["Описание работы"] = textBox3.Text;
                //и добавить запись в таблицу
                dataSet11.Avto.Rows.Add(row);
                //обновить содержимое главного окна
                Update();
            }
        }
        private int clik;
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            clik = e.RowIndex;
            //отобразить фамилию и имя выбранного человека 
            //в текстовых полях
            textBox1.Text = dataSet11.Avto.Rows[clik]["Номер"].ToString();
            textBox2.Text = dataSet11.Avto.Rows[clik]["Вид работы"].ToString();
            textBox3.Text = dataSet11.Avto.Rows[clik]["Описание работы"].ToString();
        }
        //Удаление строк
        private void button2_Click(object sender, EventArgs e)
        {
            //если выбрана запись для удаления
            if (dataGridView1.SelectedRows.Count != 0)
            {
                if (MessageBox.Show("Вы действительно хотите удалить запись?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        //удалить выбранную строку
                        dataSet11.Avto.Rows[clik].Delete();
                    }
                    catch (Exception)
                    {
                    }
                    //обновить БД и ее содержимое на форме
                    Update();
                }
            }
            else
                MessageBox.Show("Выберите строку для удаления", "Ошибка");
        }
        //Фильтр
        private void button3_Click(object sender, EventArgs e)
        {
            int n = 0;
           
            //Алгоритм поиска 
            for (int i = 0; ; )
            {
                string name = textBox4.Text;
                if (name != dataSet11.Avto.Rows[i]["Номер"].ToString())
                {
                    i++;
                }
                else
                {
                    n = i;
                    break;
                }
                
            }            
            //изменить фамилию и имя на введенные значения
            textBox1.Text = dataSet11.Avto.Rows[n]["Номер"].ToString();
            textBox2.Text = dataSet11.Avto.Rows[n]["Вид работы"].ToString();
            textBox3.Text = dataSet11.Avto.Rows[n]["Описание работы"].ToString();
            //сохранить изменения и обновить содержимое формы
            Update();
        }
        //Изменение
        private void button4_Click(object sender, EventArgs e)
        {
            int n = 0;
            for (int i = 0; ;)
            {
                string name = textBox1.Text;
                if (name != dataSet11.Avto.Rows[i]["Номер"].ToString())
                {
                    i++;
                }
                else
                {
                    n = i;
                    break;
                }
            }
            //получить содержимое выбранной строки
            DataRow row = dataSet11.Avto.Rows[n];
            //изменить фамилию и имя на введенные значения
            row["Номер"] = textBox1.Text;
            row["Вид работы"] = textBox2.Text;
            row["Описание работы"] = textBox3.Text;
            //сохранить изменения и обновить содержимое формы
            Update();
        }
    }    
}
