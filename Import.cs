using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using WindowsFormsApp1.Tables;

namespace WindowsFormsApp1
{
    internal class Import
    {
        public void ImportXml(int tab)
        {
            string Path = SelectFile();
            if (Path == null)
            {
                MessageBox.Show("No files for import were selected.", "Import canceled.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            switch (tab) 
            {
                case 1:
                    ReadXml<Customers>(Path);
                    break;
                case 2:
                    ReadXml<Developers>(Path);
                    break;
                case 3:
                    ReadXml<Orders>(Path);
                    break;
                case 4:
                    ReadXml<Products>(Path);
                    break;
                case 5:
                    ReadXml<OrderDetails>(Path);
                    break;
            }
        }

        private void ReadXml<T>(string path)
        {
            string rootElementName = null;

            using (XmlReader reader = XmlReader.Create(path))
            {
                reader.MoveToContent();
                rootElementName = reader.Name;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<T>), new XmlRootAttribute(rootElementName));
            List<T> list = new List<T>();

            using (Stream reader = new FileStream(path, FileMode.Open))
            {
                list = (List<T>)serializer.Deserialize(reader);
            }

            object instance = Activator.CreateInstance(Type.GetType("WindowsFormsApp1.CRUD." + typeof(T).Name + "CRUD"));
            MethodInfo method = instance.GetType().GetMethod("Insert");

            foreach (T item in list) 
            {
                method.Invoke(instance, new object[] {item});
            }
        }

        private string SelectFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.ShowHelp = true;
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "XML files (*.xml)|*.xml";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return openFileDialog.FileName;
                }
            }
            return null;
        }

    }
}