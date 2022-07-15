namespace FileMover_2_0
{
    public partial class FileMover : Form
    {
        public FileMover()
        {
            InitializeComponent();
        }

        static string filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\FileMover";
        static string pathPaterns = filepath + @"\Paterns.txt";
        static string pathOptions;

        private void Form1_Load(object sender, EventArgs e)
        {
            BeolvasPaterns();
        }
        string ForrasMappa;
        string CelMappa;
        static List<string> Mappak = new List<string>();
        static List<string> Fileok = new List<string>();
        static List<string> kiterjesztesek = new List<string>();
        static List<patern> Paterns = new List<patern>();
        private void BeolvasPaterns()
        {
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            if (!File.Exists(pathPaterns))
            {
                StreamWriter sw = new StreamWriter(pathPaterns);
                sw.Close();
            }

            StreamReader f = new StreamReader(pathPaterns);
            string[] sor;
            while (!f.EndOfStream)
            {
                sor = f.ReadLine().Split(";");
                patern p = new patern(sor[0], sor[1]);
                Paterns.Add(p);
            }
            f.Close();
            listBox4.Items.Clear();

            for (int i = 0; i < Paterns.Count; i++)
            {
                listBox4.Items.Add(Paterns[i].kiterj + "\t-\t" + Paterns[i].mapp);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Fileok.Clear();
            KisTurelmetkezd1();
            folderBrowserDialog1.ShowDialog();
            KisTurelmetvege1();
            Application.UseWaitCursor = false;
            ForrasMappa = folderBrowserDialog1.SelectedPath + @"\";
            string[] temp = Directory.GetFiles(ForrasMappa);
            for (int i = 0; i < temp.Length; i++) { Fileok.Add(temp[i]); }
            textBox2.Text = ForrasMappa;
            ListBoxFeltoltoFile(listBox1, Fileok);
            KiterjesztesRendezo();
            ComboBoxFeltoltes(comboBox1, kiterjesztesek);
        }
        private void ListBoxFeltoltoFile(ListBox listbox, List<string> list)
        {
            listbox.Items.Clear();
            for (int i = 0; i<list.Count; i++)
            {
                listbox.Items.Add(Path.GetFileName(list[i]));
            }
        }
        private void ListBoxFeltolto(ListBox listbox, List<string> list)
        {
            listbox.Items.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                listbox.Items.Add(list[i]);
            }
        }
        private void KisTurelmetkezd1()
        {
            textBox2.Text = "Kis Türelmet";
            textBox3.Text = "Kis Türelmet";
            comboBox1.Text = "Kis Türelmet";
            listBox1.Items.Add("Kis Türelmet");
            listBox2.Items.Add("Kis Türelmet");
        }
        private void KisTurelmetkezd2()
        {
            textBox1.Text = "Kis Türelmet";
            textBox4.Text = "Kis Türelmet";
            comboBox2.Text = "Kis Türelmet";
            listBox3.Items.Add("Kis Türelmet");
        }
        private void KisTurelmetvege1()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
            listBox1.Items.Clear();
            listBox2.Items.Clear();
        }
        private void KisTurelmetvege2()
        {
            textBox1.Text = "";
            textBox4.Text = "";
            comboBox2.Text = "";
            listBox3.Items.Clear();
        }
        private void KiterjesztesRendezo()
        {
            List<string> temp = new List<string>();
            List<string> list = new List<string>();
            for (int i = 0; i < Fileok.Count; i++) { list.Add(Fileok[i].Split(".")[Fileok[i].Split(".").Length-1]); }

            //kiválogatás
            temp.Add(list[0]);
            bool a = true;
            for (int i = 0; i<list.Count; i++)
            {
                a = true;
                for (int z = 0; z<temp.Count; z++)
                {
                    if (list[i] == temp[z])
                    {
                        a = false;
                    }
                }
                if (a)
                {
                    temp.Add(list[i]);
                }
            }

            //végleges kivitel
            kiterjesztesek.Clear();
            for (int i = 0; i<temp.Count; i++)
            {
                kiterjesztesek.Add(temp[i]);
            }
            ListBoxFeltolto(listBox2, kiterjesztesek);
        }
        private void ComboBoxFeltoltes(ComboBox combobox, List<string> list)
        {
            combobox.Items.Clear();
            comboBox1.Text = "";
            for (int i = 0; i < list.Count; i++)
            {
                combobox.Items.Add(list[i]);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Mappak.Clear();
            KisTurelmetkezd2();
            folderBrowserDialog1.ShowDialog();
            KisTurelmetvege2();
            CelMappa = folderBrowserDialog1.SelectedPath + @"\";
            textBox1.Text = CelMappa;
            string[] temp = Directory.GetDirectories(CelMappa);
            for (int i = 0; i < temp.Length; i++) { Mappak.Add(temp[i]); }
            ListBoxFeltoltoFile(listBox3, Mappak);
            List<string> temp1 = new List<string>();
            for (int i = 0; i < Mappak.Count; i++) { temp1.Add(Path.GetFileName(Mappak[i])); }
            ComboBoxFeltoltes(comboBox2, temp1);
        }
        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            comboBox1.Text = listBox2.SelectedItem.ToString();
        }

        private void listBox3_DoubleClick(object sender, EventArgs e)
        {
            comboBox2.Text = listBox3.SelectedItem.ToString();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "" && comboBox2.Text != "")
            {
                patern j = new patern(comboBox1.Text, comboBox2.Text);
                bool a = true;
                for (int i = 0; i<Paterns.Count; i++)
                {
                    if (Paterns[i].kiterj == j.kiterj && Paterns[i].mapp == j.mapp)
                    {
                        a = false;
                    }
                }
                if (a)
                {
                    Paterns.Add(j);
                }
            }
            
            listBox4.Items.Clear();

            for(int i = 0; i<Paterns.Count; i++)
            {
                listBox4.Items.Add(Paterns[i].kiterj + "\t-\t" + Paterns[i].mapp);
            }
            PaternSave();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i<Paterns.Count; i++)
            {
                for (int z = 0; z<Fileok.Count; z++)
                {
                    string kiterjeszt = Fileok[z].Split(".")[Fileok[z].Split(".").Length - 1];
                    if (!Directory.Exists(Path.Combine(CelMappa, Paterns[i].mapp)))
                    {
                        Directory.CreateDirectory(Path.Combine(CelMappa, Paterns[i].mapp));
                    }
                    if (Paterns[i].kiterj == kiterjeszt)
                    {
                        if (checkBox1.Checked)
                        {
                            File.Move(Path.Combine(ForrasMappa, Path.GetFileName(Fileok[z])), Path.Combine(CelMappa, Paterns[i].mapp) + @"\" + Path.GetFileName(Fileok[z]), true);
                        }
                        else
                        {
                            File.Copy(Path.Combine(ForrasMappa, Path.GetFileName(Fileok[z])), Path.Combine(CelMappa, Paterns[i].mapp) + @"\" + Path.GetFileName(Fileok[z]), true);
                        }
                    }
                }
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            kiterjesztesek.Add(textBox3.Text);
            ComboBoxFeltoltes(comboBox1, kiterjesztesek);
            ListBoxFeltolto(listBox2, kiterjesztesek);
            textBox3.Text = "";
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Mappak.Add(CelMappa+@"\"+textBox4.Text);
            List<string> temp = new List<string>();
            for (int i = 0; i < Mappak.Count; i++) { temp.Add(Path.GetFileName(Mappak[i])); }
            ComboBoxFeltoltes(comboBox2, temp);
            ListBoxFeltoltoFile(listBox3, Mappak);
            textBox4.Text = "";
        }
        private void DeletFromListbox(ListBox listbox)
        {
            if (listbox.SelectedIndex != -1)
            {
                listbox.Items.RemoveAt(listbox.SelectedIndex);
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
                kiterjesztesek.RemoveAt(listBox2.SelectedIndex);
            DeletFromListbox(listBox2);
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndex != -1)
                Mappak.RemoveAt(listBox3.SelectedIndex);
            DeletFromListbox(listBox3);
        }
        private void button9_Click(object sender, EventArgs e)
        {
            if (listBox4.SelectedIndex != -1)
                Paterns.RemoveAt(listBox4.SelectedIndex);
            DeletFromListbox(listBox4);
            PaternSave();
        }
        private void PaternSave()
        {
            StreamWriter z = new StreamWriter(pathPaterns);
            for (int i = 0; i<Paterns.Count;i++)
            {
                z.WriteLine(Paterns[i].kiterj+";"+Paterns[i].mapp);
            }
            z.Close();
        }
    }
    public class patern
    {
        public string kiterj { get; set; }
        public string mapp { get; set; }
        public patern(string a, string b)
        { 
            kiterj = a;
            mapp = b;
        }
    }
}