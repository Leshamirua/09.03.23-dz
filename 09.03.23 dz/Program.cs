using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace _09._03._23_dz
{
    internal class Program
    {
        class Phone
        {
            public string Name { get; set; }
            public string Company { get; set; }
            public int Value { get; set; }
            public int Year { get; set; }
            public Phone() { }
            public Phone(string name, string company, int value, int year)
            {
                Name = name;
                Company = company;
                Value = value;
                Year = year;
            }
        }
        static void XMLadd(XDocument xdoc, Phone phone, string filename)
        {
            XElement root = xdoc.Element("Name");
            if (root != null)
            {
                root.Add(new XElement("phone",
                            new XAttribute("name", phone.Name),
                            new XElement("company", phone.Company),
                            new XElement("value", phone.Value),
                            new XElement("year", phone.Year)));

                xdoc.Save(filename);
            }
        }
        static void XMLSearch(string searched, XDocument xdoc)
        {
            
            var tom = xdoc.Element("phone")?   // получаем корневой узел people
                .Elements("name")             // получаем все элементы person
                .FirstOrDefault(p => p.Attribute("name")?.Value == searched);

            var name = tom?.Attribute("name")?.Value;
            var company = tom?.Element("company")?.Value;
            var value = tom?.Element("value")?.Value;
            var year = tom?.Element("year")?.Value;

            Console.WriteLine($"Name: {name}  Company: {company}  Value: {value}  Year: {year}");

        }
        static void XMLDelete(string searched, XDocument xdoc)
        {
            XElement root = xdoc.Element("phone");
            if (root != null)
            {
                // получим элемент person с name = "Bob"
                var bob = root.Elements("person")
                    .FirstOrDefault(p => p.Attribute("name")?.Value == searched);
                // и удалим его
                if (bob != null)
                {
                    bob.Remove();
                    xdoc.Save("people.xml");
                }
            }
        }
        static void XMLEdit(Phone edit, Phone existing, XDocument xdoc)
        {

        }
        static void Main(string[] args)
        {

            string filename = "phones.xml";

            XDocument xdoc = XDocument.Load(filename);
            Phone phone1 = new Phone("S10", "Samsung", 12000, 2019);
            Phone phone2 = new Phone("S10+", "Samsung", 15000, 2019);
            Phone phone3 = new Phone("S10e", "Samsung", 10000, 2019);

            XMLadd(xdoc, phone1, filename);
            // выводим xml-документ на консоль
            Console.WriteLine(xdoc);



        }
    }
}
