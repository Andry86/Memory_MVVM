using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.ComponentModel; // per INotifyPropertyChanged
using System.Windows.Input; // per ICommand
namespace MemoNoteMvvm
{                

   public delegate void VediMemoEventHandler(object source);
   public delegate void viewEventHandler(object source);
   public delegate void mostraDel(object source);

    class ModelMemo : IModel
    {
        string note;
        string question;
        public event VediMemoEventHandler vediMemoEvent;
        public string Note { get { return note; } set { note = value; } }
        public string Question { get { return question; } set { question = value; } }




        public void addMemo(string memo, string question)
        {

            //string stringConnSql = "Server=Andrea-PC\\SQLEXPRESS; Database=Memory; integrated security=true";
            string stringConnSql = System.Configuration.ConfigurationSettings.AppSettings["DBCON"];    
            using (SqlConnection cn = new SqlConnection(stringConnSql))
            {

            //    cn.ConnectionString= System.Configuration.ConfigurationSettings.AppSettings["DBCON"];    
                try
                {
                    cn.Open();

                    using (SqlCommand commandSql = new SqlCommand("Addmemo", cn))
                    {
                        commandSql.CommandType = CommandType.StoredProcedure;
                        // commandSql.Parameters.AddWithValue("@DateView", DateTime.Now.ToString());
                        DateTime ora = DateTime.Now;
                        commandSql.Parameters.Add("@DateView", SqlDbType.DateTime).Value = ora;
                        commandSql.Parameters.Add("@Question", SqlDbType.VarChar).Value = question;
                        commandSql.Parameters.Add("@Note", SqlDbType.VarChar).Value = memo;
                        commandSql.Parameters.Add("@NumView", SqlDbType.Int).Value = 1;

                        try
                        {
                            commandSql.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                          //  MessageBox.Show(ex.Message);
                            Helpers.fileLog.Log(ex.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                  //  MessageBox.Show(ex.Message);
                    Helpers.fileLog.Log(ex.ToString());

                }
            }
        }

        //metodo che visualizza la nota salvata e cambia la data di prossima visualizzazione
        public void vediMemo()
        {
            int id = 0;
            string question = null;
            string memo = null;
            string stringConnSql = "Server=Andrea-PC\\SQLEXPRESS; Database=Memory; integrated security=true";
            DateTime data = DateTime.Today;

            using (SqlConnection cn = new SqlConnection(stringConnSql))
            {
                cn.Open();
                using (SqlCommand commandSql = new SqlCommand("NoteView", cn))
                {
                    commandSql.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        SqlDataReader reader = commandSql.ExecuteReader();
                        if (reader.Read())
                        {
                            id = (int)reader.GetInt32(0);
                            question = reader.GetString(1);
                            memo = reader.GetString(2);
                        }
                        reader.Close();
                    }
                    catch (System.InvalidOperationException ex)
                    {
                      //  MessageBox.Show("Ops, che succede?" + ex.Message);
                        Helpers.fileLog.Log(ex.ToString());
                    }

                    catch (System.Data.SqlClient.SqlException ex)
                    {
                      //  MessageBox.Show("Ops, che succede?" + ex.Message);
                        Helpers.fileLog.Log(ex.ToString());
                    }

                }

                using (SqlCommand csql = new SqlCommand("UPDATE Notes SET NextDateView = DATEADD(SECOND, NumView*2, GETDATE()), NumView=NumView+1 WHERE (Id=" + id + " AND Note='" + memo + "');", cn))
                {
                    csql.CommandType = CommandType.Text;
                    csql.ExecuteNonQuery();
                }


                if ((memo != null) && question != null)
                {
                    Note = memo;
                    Question = question;
                    vediMemoEvent(this);
                }
            }
        }
    }
}
    
