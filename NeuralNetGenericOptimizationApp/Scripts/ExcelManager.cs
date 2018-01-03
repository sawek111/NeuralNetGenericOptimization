using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NeuralNetGenericOptimizationApp.Scripts
{
    class ExcelManager
    {
        string filePath;


        public ExcelManager(string path)
        {
            filePath = path;
        }

        private ExcelManager(){}

        public void WriteRow(params string[] columns)
        {
            Microsoft.Office.Interop.Excel.Application xlApp = null;
            Workbook xlWorkBook = null;
            Worksheet xlWorkSheet = null;
            object misValue;
            try
            {
                bool create = false;
                xlApp = new Microsoft.Office.Interop.Excel.Application();
                misValue = System.Reflection.Missing.Value;
                xlApp.DisplayAlerts = false;
                if (System.IO.File.Exists(filePath))
                {
                    xlWorkBook = xlApp.Workbooks.Open(filePath, 0, false, 5, "", "", false, XlPlatform.xlWindows, "", true, false, 0, true, false, true);
                }
                else
                {
                    create = true;
                    xlWorkBook = xlApp.Workbooks.Add(misValue);
                }

                xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
                Range last = xlWorkSheet.Cells.SpecialCells(XlCellType.xlCellTypeLastCell, Type.Missing);
                int lastUsedRow = last.Row;
                if (create)
                {
                    lastUsedRow = 0;
                }

                for (int i = 0; i < columns.Length; i++)
                {
                    xlWorkSheet.Cells[lastUsedRow + 1, i + 1] = columns[i];
                }

                xlWorkBook.Close(true, filePath, misValue);
                xlApp.Quit();

            }
            finally
            {
                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);
            }

            return;
        }
    }
}
