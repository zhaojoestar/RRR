using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CoreConfig
{
    public int ID;
    public string Resource;
    public int[] Attribute;
    private static Dictionary<int, CoreConfig> _dic = null;
    public static Dictionary<int, CoreConfig> dic
    {
        get
        {
            if (_dic == null)
            {
                _dic = new Dictionary<int, CoreConfig>();
                SQLiteHelper sql = new SQLiteHelper();
                SqliteDataReader reader = sql.ReadFullTable("CoreConfig");
                while (reader.Read())
                {
                    var e = new CoreConfig();
                    e.ID = reader.GetInt16(reader.GetOrdinal("ID"));
                    e.Resource = reader.GetString(reader.GetOrdinal("Resource"));
                    e.Attribute = Array.ConvertAll<string, int>(reader.GetString(reader.GetOrdinal("Attribute")).Split(';'), (string s) => { return int.Parse(s); });
                    _dic.Add(e.ID, e);
                }
                sql.CloseConnection();
            }
            return _dic;
        }
    }

    public static CoreConfig Get(int id)
    {
        if (dic.ContainsKey(id))
        {
            return dic[id];
        }
        else
        {
            Debug.LogError("CoreConfig cannot find " + id);
            return null;
        }
    }
}
