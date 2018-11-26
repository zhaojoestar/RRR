using Mono.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapData
{
    public int ID;
    public int Row;
    public int Column;
    public int EventType;
    public string EventValue;

    private static Dictionary<int, MapData> _dic = null;
    public static Dictionary<int, MapData> dic
    {
        get
        {
            if (_dic == null)
            {
                _dic = new Dictionary<int, MapData>();
                SQLiteHelper sql = new SQLiteHelper();
                SqliteDataReader reader = sql.ReadFullTable("MapData");
                while (reader.Read())
                {
                    var e = new MapData();
                    e.ID = reader.GetInt16(reader.GetOrdinal("ID"));
                    e.Row = Mathf.CeilToInt(e.ID * 1f / CONSTANT.CONST.MAP_LENGTH);
                    e.Column = (e.ID - 1) % CONSTANT.CONST.MAP_LENGTH;
                    e.EventType = reader.GetInt16(reader.GetOrdinal("EventType"));
                    e.EventValue = reader.GetString(reader.GetOrdinal("EventValue"));
                    _dic.Add(e.ID, e);
                }
                sql.CloseConnection();
            }
            return _dic;
        }
    }

    public static MapData Get(int id)
    {
        if (dic.ContainsKey(id))
        {
            return dic[id];
        }
        else
        {
            Debug.LogError("MapData cannot find " + id);
            return null;
        }
    }

    public static MapData[] GetRowData(int row)
    {
        return dic.Values.Where(e => e.Row == row).OrderBy(e => e.Column).ToArray();
    }

    public static int RowCount
    {
        get
        {
            return dic.Count / CONSTANT.CONST.MAP_LENGTH;
        }
    }
}
