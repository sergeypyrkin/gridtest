using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using MahApps.Metro.Controls;

namespace GridTesting
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public List<Item> items = new List<Item>();
        public List<string> resolver = new List<string> { "method1", "method2" };
        public DispatcherTimer timer;
        string res = "";

        public int Number = 0;  //количество элементов
        public int sleep;       //задержка
        public int MaxCount = 0; //количество итераций
        public int Count;


        public double last1;
        public List<double> t1 = new List<double>();


        public double last2;
        public List<double> t2 = new List<double>();



        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            clearData();
            
            sleep = Convert.ToInt32(tSleep.Text.ToString());
            Count = Convert.ToInt32(tN.Text.ToString());
            MaxCount = Convert.ToInt32(tMaxCount.Text.ToString());
            MaxCount = MaxCount * resolver.Count();
            setInitialData();
            timer = new DispatcherTimer();  // если надо, то в скобках указываем приоритет, например DispatcherPriority.Render
            timer.Tick += new EventHandler(work);
            timer.Interval = new TimeSpan(0, 0, 0, 0, sleep);
            timer.Start();
        }

        private void setInitialData()
        {
            for (int i = 0; i < Count; i++)
            {
                Item it = new Item();
                items.Add(it);
            }
            //начальная сортировка
            items = items.OrderBy(o => o.A1).ThenBy(o => o.A2).ThenBy(o => o.A3).ThenBy(o => o.A4).ToList();
            dataGridView1.ItemsSource = items;
            dataGridView1.Items.Refresh();
            //applysortdirection();


        }

        private void clearData()
        {
            tNUmber.Content = "";
            cur1.Text = "";
            cur2.Text = "";
            av1.Text = "";
            av2.Text = "";
     
            last1 = 0;
            last2 = 0;
            Number = 0;
            items.Clear();
            t1.Clear();
            t2.Clear();
        }

        private void work(object sender, EventArgs e)
        {
            if (Number == MaxCount)
            {
                exitAction();
                return;
            }
            shufflData();

            string method = "";
            //res = getNextResolve();
            //if (Number < MaxCount / 2)
            //{
            //    method = "method1";
            //}
            //else
            //{
            //    method = "method1";
            //}

            if (Number < 4)
            {
                updateConsole();
                Number++;
                return;
            }
            if (Number >= MaxCount / 2 && Number < MaxCount / 2 + 4)
            {
                updateConsole();
                Number++;
                return;
            }
            System.Diagnostics.Stopwatch sw = new Stopwatch();
            sw.Start();
            method1();

            //if (method == "method1")
            //{
            //    method1();
            //}
            //if (method == "method2")
            //{
            //    method2();
            //}
            sw.Stop();
            double milliseconds = sw.ElapsedMilliseconds;
            t1.Add(milliseconds);
            //if (method == "method1")
            //{
            //    t1.Add(milliseconds);
            //}
            //if (method == "method2")
            //{
            //    t2.Add(milliseconds);
            //}
            updateConsole();
            Number++;
        }

        private void method1()
        {

            //System.Diagnostics.Stopwatch sw = new Stopwatch();
            //sw.Start();

            dataGridView1.ItemsSource = items.OrderBy(o => o.A1).ThenBy(o => o.A2).ThenBy(o => o.A3).ThenBy(o => o.A4);
            dataGridView1.Items.Refresh();

            //            sw.Stop();
            //double milliseconds = sw.ElapsedMilliseconds;
            //cur1.Text = milliseconds.ToString();

        }

        private void method2()
        {
            //items = items.OrderBy(o => o.A1).ThenBy(o => o.A2).ThenBy(o => o.A3).ThenBy(o => o.A4).ToList();
            //dataGridView1.ItemsSource = items.OrderBy(o => o.A1).ThenBy(o => o.A2).ThenBy(o => o.A3).ThenBy(o => o.A4);
            applysortdirection();
        }

        private void shufflData()
        {
            //удаляем1
            Random random = new Random(unchecked((int)(DateTime.Now.Ticks)));
            int randomNumber = random.Next(0, Count-1);
            Item delitem = items[randomNumber];
            items.Remove(delitem);

            //вставляем 1
            Item item = new Item();
            items.Add(item);

            //вставляем 2
            Item item2 = new Item();
            items.Add(item2);

            int randomNumber2 = random.Next(0, Count - 1);
            Item editettem = items[randomNumber2];
            editettem.A1 = random.Next(0, 10000);
            editettem.A2 = random.Next(0, 10000);
            editettem.A3 = random.Next(0, 10000);
            editettem.A4 = random.Next(0, 10000);
            editettem.A5 = random.Next(0, 10000);
            editettem.A6 = random.Next(0, 10000);
            editettem.A7 = random.Next(0, 10000);
            editettem.A8 = random.Next(0, 10000);
            items[0].A1 = -random.Next(0, 10000);



        }

        public string getNextResolve()
        {
            string result = "";
            if (res == "")
            {
                return resolver[0];
            }
            int i = resolver.IndexOf(res);
            if (i == resolver.Count - 1)
            {
                return resolver[0];
            }
            return resolver[i+1];
        }


        //обновляем консоль
        private void updateConsole()
        {
            tNUmber.Content = Number.ToString();

            if (t1.Count != 0)
            {
                last1 = t1.Last();
                double N1 = t1.Count();
                double c_av1 = 0;
                if (N1 > 0)
                {
                    c_av1 = t1.Average();
                }
                cur1.Text = last1.ToString();
                av1.Text = c_av1.ToString("0.0000");

            }

            if (t2.Count != 0)
            {
                last2 = t2.Last();
                double N2 = t2.Count();
                double c_av2 = 0;
                if (N2 > 0)
                {
                    c_av2 = t2.Average();
                }
                cur2.Text = last2.ToString();
                av2.Text = c_av2.ToString("0.0000");

            }



        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            exitAction();
        }


        public void exitAction()
        {
            timer.Stop();
            timer = null;
            try
            {

                //Pass the filepath and filename to the StreamWriter Constructor
                string folder = @"C:\Users\sergey\Desktop\data";
                string filename = "clear" +Count + ".txt";
                StreamWriter sw = new StreamWriter(folder + "\\" + filename);

                //Write a line of text
                sw.WriteLine("1: "+ av1.Text);

                //Write a second line of text
                sw.WriteLine("2: " + av2.Text);


                //Close the file
                sw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            method1();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            method2();

        }

        public void applysortdirection()
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(dataGridView1.ItemsSource);
            if (view.SortDescriptions.Count == 0)
            {
                view.SortDescriptions.Add(new SortDescription("A1", ListSortDirection.Ascending));
                view.SortDescriptions.Add(new SortDescription("A2", ListSortDirection.Ascending));
                view.SortDescriptions.Add(new SortDescription("A3", ListSortDirection.Ascending));
                view.SortDescriptions.Add(new SortDescription("A4", ListSortDirection.Ascending));
            }
            view.Refresh();
        }

        public void clearsortdirection()
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(dataGridView1.ItemsSource);
            view.SortDescriptions.Clear();
        }
    }
}
