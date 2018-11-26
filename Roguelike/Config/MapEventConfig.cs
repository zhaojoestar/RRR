using Mono.Data.Sqlite;
using System.Collections.Generic;
using UnityEngine;

public class MapEventConfig
{
    public int ID;
    public string Texture;
    private static Dictionary<int, MapEventConfig> _dic = null;
    public static Dictionary<int, MapEventConfig> dic
    {
        get
        {
            if (_dic == null)
            {
                _dic = new Dictionary<int, MapEventConfig>();
                SQLiteHelper sql = new SQLiteHelper();
                SqliteDataReader reader = sql.ReadFullTable("MapEventConfig");
                while (reader.Read())
                {
                    var e = new MapEventConfig();
                    e.ID = reader.GetInt16(reader.GetOrdinal("ID"));
                    e.Texture = reader.GetString(reader.GetOrdinal("Texture"));
                    _dic.Add(e.ID, e);
                }
                sql.CloseConnection();
            }
            return _dic;
        }
    }

    public static MapEventConfig Get(int id)
    {
        if (dic.ContainsKey(id))
        {
            return dic[id];
        }
        else
        {
            Debug.LogError("MapEventConfig cannot find " + id);
            return null;
        }
    }
}
