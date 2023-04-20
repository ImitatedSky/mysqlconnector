using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisHealth_Upload_UVC.Class
{
    class DBCommunicate
    {
        static string DBPath = @".\Data.db";
        static string ConnStr = "data source=" + DBPath;
        public SQLiteConnection Connection;

        public DBCommunicate()
        {
            Connection = new SQLiteConnection(ConnStr);
        }

        //寫入資料庫
        public int SQLWrite(SQLiteCommand SQLCommand)
        {
            Connection.Open();
            int QuantityOfChangeData = SQLCommand.ExecuteNonQuery();
            Connection.Close();
            return QuantityOfChangeData;
        }

        public List<string[]> SQLRead(SQLiteCommand SQLCommand)
        {
            Connection.Open();
            SQLiteDataReader myReader = SQLCommand.ExecuteReader();

            List<string[]> Raws = new List<string[]>();
            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    string[] RawDataList = new string[myReader.FieldCount];
                    for (int i = 0; i < RawDataList.Length; i++)
                        RawDataList[i] = myReader[i].ToString();
                    Raws.Add(RawDataList);
                }
            }
            else
                Raws = null;

            Connection.Close();
            return Raws;
        }

        public bool IsDataExist(string TableName, string ID)
        {
            bool HasRows = false;

            SQLiteCommand SQLCommand = new SQLiteCommand("SELECT * FROM " + TableName + " WHERE [ID]=@ID", Connection);
            SQLCommand.Parameters.AddWithValue("@ID", ID);
            Connection.Open();
            SQLiteDataReader myReader = SQLCommand.ExecuteReader();
            if (myReader.HasRows)
                HasRows = true;
            else
                HasRows = false;

            Connection.Close();
            return HasRows;
        }
    }
}
