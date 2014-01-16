using System;
using System.Xml.Serialization;

namespace FilmsTranslator.Main.Code.Spellers.Transport
{
    [Serializable]
    [XmlType(Namespace = "http://speller.yandex.net/services/spellservice")]
    public class YandexSpellerResponse
    {
        private int codeField;
        private int colField;
        private int lenField;
        private int posField;
        private int rowField;
        private string[] sField;
        private string wordField;


        [XmlElement(Order = 0)]
        public string word
        {
            get { return wordField; }
            set { wordField = value; }
        }


        [XmlElement("s", Order = 1)]
        public string[] s
        {
            get { return sField; }
            set { sField = value; }
        }

        [XmlAttribute]
        public int code
        {
            get { return codeField; }
            set { codeField = value; }
        }


        [XmlAttribute]
        public int pos
        {
            get { return posField; }
            set { posField = value; }
        }


        [XmlAttribute]
        public int row
        {
            get { return rowField; }
            set { rowField = value; }
        }


        [XmlAttribute]
        public int col
        {
            get { return colField; }
            set { colField = value; }
        }

        [XmlAttribute]
        public int len
        {
            get { return lenField; }
            set { lenField = value; }
        }
    }
}