using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LanguageConfig
{
    public string Key;
    public string Value0;
    private static Dictionary<string, LanguageConfig> _dic = null;
    public static Dictionary<string, LanguageConfig> dic
    {
        get
        {
            if (_dic == null)
            {
                _dic = new Dictionary<string, LanguageConfig>();
                SQLiteHelper sql = new SQLiteHelper();
                SqliteDataReader reader = sql.ReadFullTable("LanguageConfig");
                while (reader.Read())
                {
                    var e = new LanguageConfig();
                    e.Key = reader.GetString(reader.GetOrdinal("Key"));
                    e.Value0 = reader.GetString(reader.GetOrdinal("Value0"));
                    _dic.Add(e.Key, e);
                }
                sql.CloseConnection();
            }
            return _dic;
        }
    }

    public static LanguageConfig Get(string key)
    {
        if (dic.ContainsKey(key))
        {
            return dic[key];
        }
        else
        {
            Debug.LogError("LanguageConfig cannot find " + key);
            return null;
        }
    }
}
