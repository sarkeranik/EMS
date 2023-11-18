using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Media;
using EMS.Models;
using System.Reflection;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;

namespace EMS.Services
{
    public partial interface IExportToExcelService
    {
        public void ExportToExcel(DataGrid dg);
    }
    public partial class ExportToExcelService : IExportToExcelService
    {
        public void ExportToExcel(DataGrid dg)
        {
            dg.SelectAllCells();
            dg.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dg);
            dg.UnselectAllCells();
            String Clipboardresult = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            StreamWriter swObj = new StreamWriter(Environment.GetEnvironmentVariable("USERPROFILE") + @"\" + @"Downloads\Employees.csv");
            swObj.WriteLine(Clipboardresult);
            swObj.Close();
            MessageBox.Show("Please Check your Download folder.(FileName: Employees.csv)");
        }
    }
}
