namespace pexeso2
{
    public partial class Form1 : Form
    {
        Random generator = new();
        static List<string> reference = generuj(); //Pro vyhodnoceni spravnosti
        static List<string> dvojice = new List<string>(reference);
        static List<string> generuj()
        {
            Random generator = new();
            List<string> dvojice = new List<string>();
            List<int> seznamvysledku = new List<int>();
            int i = 0;
            while (i < 18)
            {
                int znak = generator.Next(4);
                int cisloa = generator.Next(-256, 256);
                int cislob = generator.Next(256);
                int vysledek = 0;
                string vysledekstring;
                switch (znak)
                {
                    case 0:
                        vysledek = cisloa + cislob;
                        if (!seznamvysledku.Contains(vysledek))
                        {
                            vysledekstring = Convert.ToString(cisloa) + " " + "+" + " " + Convert.ToString(cislob);
                            dvojice.Add(vysledekstring);
                            dvojice.Add(Convert.ToString(vysledek));
                            seznamvysledku.Add(vysledek);
                            i++;
                        }
                        break;
                    case 1:
                        vysledek = cisloa - cislob;
                        if (!seznamvysledku.Contains(vysledek))
                        {
                            vysledekstring = Convert.ToString(cisloa) + " " + "-" + " " + Convert.ToString(cislob);
                            dvojice.Add(vysledekstring);
                            dvojice.Add(Convert.ToString(vysledek));
                            seznamvysledku.Add(vysledek);
                            i++;
                        }
                        break;
                    case 2:
                        vysledek = cisloa * cislob;
                        if (!seznamvysledku.Contains(vysledek))
                        {
                            vysledekstring = Convert.ToString(cisloa) + " " + "*" + " " + Convert.ToString(cislob);
                            dvojice.Add(vysledekstring);
                            dvojice.Add(Convert.ToString(vysledek));
                            seznamvysledku.Add(vysledek);
                            i++;
                        }
                        break;
                    case 3:
                        vysledek = cisloa / cislob;
                        if (!seznamvysledku.Contains(vysledek))
                        {
                            vysledekstring = Convert.ToString(cisloa) + " " + "/" + " " + Convert.ToString(cislob);
                            dvojice.Add(vysledekstring);
                            dvojice.Add(Convert.ToString(vysledek));
                            seznamvysledku.Add(vysledek);
                            i++;
                        }
                        break;
                }
            }
            return dvojice;
        } // Generuje nahodne zadani
        static int pocitadlo = 18;
        // Prvni a druha zmacknuta karticka
        Label prvni = null;
        Label druha = null;

        private void Vitezstvi()
        {
            if (pocitadlo == 0)
            {
                MessageBox.Show("Máte mùj obdiv, já to po chvilce vzdal.", "Hurá, vyhrál jste!");
                Close();
            }
        }

        private void RozmistiCisla()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = generator.Next(dvojice.Count);
                    iconLabel.Text = dvojice[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    dvojice.RemoveAt(randomNumber);
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
            RozmistiCisla();
            MessageBox.Show("Toto je pexeso, ve kterém je za úkol spojit správné výsledky vzorcù.\nDìlení je celoèíselné.\n\n(Protihráè není)", "Pexeso");
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            Label zvolenaKarta = sender as Label;

            if (zvolenaKarta != null)
            {
                if (timer1.Enabled == true)
                    return;

                if (zvolenaKarta.ForeColor == Color.Black)
                    return;

                if (prvni == null)
                {
                    prvni = zvolenaKarta;
                    prvni.ForeColor = Color.Black;
                    return;
                }
                druha = zvolenaKarta;
                druha.ForeColor = Color.Black;

                int index = reference.IndexOf(prvni.Text);
                if (index % 2 == 0)
                {
                    if (reference[index] == prvni.Text && druha.Text == reference[index + 1])
                    {
                        prvni = null;
                        druha = null;
                        pocitadlo--;
                        Vitezstvi();
                        return;
                    }
                }
                else
                {
                    if (reference[index-1] == druha.Text && prvni.Text == reference[index])
                    {
                        prvni = null;
                        druha = null;
                        pocitadlo--;
                        Vitezstvi();
                        return;
                    }
                }
                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            prvni.ForeColor = prvni.BackColor;
            druha.ForeColor = druha.BackColor;
            prvni = null;
            druha = null;
        }
    }
}