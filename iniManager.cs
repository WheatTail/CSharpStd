using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace testLoginForm
{
    class iniManager
    {
        string pathToIni; //Путь до ини-файла
        [DllImport("kernel32")] //Импорт виндовой библиотеки и функции из неё (строки ниже)
        static extern long WritePrivateProfileString(string section, string key, string value, 
            string pathToFile);
        [DllImport("kernel32")] //Ещё один импорт из той же библиотеки
        static extern long GetPrivateProfileString(string section, string key, string def, 
            StringBuilder returnValue, int size, string pathToFile);
        public iniManager(string pathToFile) //Конструктор класса, заполняет переменную пути до файла
        {
            pathToIni = new FileInfo(pathToFile).FullName.ToString(); 
        }
        public string readFromIni(string section, string key) /*Читаем ини-файл и возвращаем значение 
            ключа в нужной секции*/
        {
            var returnValue = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", returnValue, 255, pathToIni);
            return returnValue.ToString();
        }
        public void writeToIni(string section, string key, string value) //Пишем значение ключа в секцию
        {
            WritePrivateProfileString(section, key, value, pathToIni);
        }/*
        public void deleteKey(string key, string section = null) //Удаляем ключ. Пока unclaimed
        {
            writeToIni(section, key, null);
        }
        public void deleteSection(string section = null) //Удаление целой секции. Unclaimed
        {
            writeToIni(section, null, null);
        }*/
        public bool keyExist(string key, string section = null) //Проверка на существование ключа в секции
        {
            return readFromIni(section, key).Length > 0;
        }
    }
}
