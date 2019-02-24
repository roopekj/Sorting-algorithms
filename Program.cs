using System;
using System.Windows.Forms;

namespace Sorting_algorithms
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Sorter());
        }
    }
}