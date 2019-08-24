﻿using DSAProject.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.JSON
{
    [DataContract]
    public abstract class AbstractJSONSerializable<T> : IJSONSerializable where T : class, IJSONSerializable, new()
    {
        string _jsonContent = null;

        public string JSONContent
        {
            get
            {
                this._jsonContent = this.ToJSON(error: out string error);
                return this._jsonContent;
            }
            set { this._jsonContent = value; }
        }
        public static T DeSerializeJson(Stream jsonStream, out string error)
        {
            error = null;
            try
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                T t = (T)serializer.ReadObject(jsonStream);
                return t;
            }
            catch (Exception exc)
            {
                error = typeof(T).Name + ".DeSerializeJson:" + Environment.NewLine + exc.Message;
                return null;
            }
        }
        public static T DeSerializeJson(String jsonContent, Encoding encoding, out string error)
        {
            var bytes = encoding.GetBytes(jsonContent);
            using (var stream = new MemoryStream(bytes))
            {
                T t = DeSerializeJson(stream, out error);
                if (t != null)
                {
                    t.JSONContent = jsonContent;
                }
                return t;
            }
        }
        public static T DeSerializeJson(String jsonContent, out string error)
        {
            return DeSerializeJson(jsonContent, Encoding.UTF8, out error);
        }
        public string ToJSON(Encoding encoding, out string error)
        {
            error = null;
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.WriteObject(stream, this);
                    return encoding.GetString(stream.ToArray());
                }
            }
            catch (Exception exc)
            {
                error = typeof(T).Name + ".ToJSON:" + Environment.NewLine + exc.Message;
                return null;
            }
        }
        public string ToJSON(out string error)
        {
            return ToJSON(encoding: Encoding.UTF8, error: out error);
        }
    }
}
