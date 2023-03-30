using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ExcelCoverter;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Drawing;
using ClientManagement.Interface.Helpers;

namespace ACRApplication.Admin.Interface.Helpers
{
    public class rptReport : GenericExcelReport
    {
        DataSet ds;

        public rptReport(string xmlData)
            : base("Report", true, true)
        {
            ds = new DataSet();
            byte[] data = Encoding.Unicode.GetBytes(xmlData);
            using (MemoryStream ms = new MemoryStream(data))
            {
                ms.Position = 0;
                ds.ReadXml(ms);
            }
        }
        
        public byte[] GetFile(DateTime EndDate)
        {
            ExcelDocument doc = GetExcelDocument();
            List<SheetNames> sheets = new List<SheetNames> { SheetNames.Material, SheetNames.PurchaseOrder};

            if (ds.Tables.Count > 0)
            {
                int headingRow = 1;
                Worksheet ws = GetNewWorksheet(sheets[1], ref headingRow);
                
                {
                    int i = 0;
                    if (ds.Tables.Count >= 1)
                    {                        
                        headingRow = 1;
                        ws = GetNewWorksheet(sheets[i], ref headingRow);
                        BuildHeader(ref ws, ref headingRow, ds.Tables[i].Columns.Cast<DataColumn>().Select(f => f.Caption).ToList(), new List<CrossTabHeading>(), true);
                        BuildDataSets(ref ws, ref headingRow, ds.Tables[i].Rows.Cast<DataRow>().Select(f => f.ItemArray).ToList(), ds.Tables[i].Columns.Cast<DataColumn>().ToList());

                        SetSheetColums(ref ws);
                        doc.WorkBook.Worksheets.Add(ws);
                        i++;
                    }
                    if (ds.Tables.Count >= 2)
                    {
                        headingRow = 1;
                        ws = GetNewWorksheet(sheets[i], ref headingRow);
                        BuildHeader(ref ws, ref headingRow, ds.Tables[i].Columns.Cast<DataColumn>().Select(f => f.Caption).ToList(), new List<CrossTabHeading>(), true);
                        BuildDataSets(ref ws, ref headingRow, ds.Tables[i].Rows.Cast<DataRow>().Select(f => f.ItemArray).ToList(), ds.Tables[i].Columns.Cast<DataColumn>().ToList());

                        SetSheetColums(ref ws);
                        doc.WorkBook.Worksheets.Add(ws);
                        i++;
                    }

                    if (ds.Tables.Count >= 3)
                    {
                        headingRow = 1;
                        ws = GetNewWorksheet(sheets[i], ref headingRow);
                        BuildHeader(ref ws, ref headingRow, ds.Tables[i].Columns.Cast<DataColumn>().Select(f => f.Caption).ToList(), new List<CrossTabHeading>(), true);
                        BuildDataSets(ref ws, ref headingRow, ds.Tables[i].Rows.Cast<DataRow>().Select(f => f.ItemArray).ToList(), ds.Tables[i].Columns.Cast<DataColumn>().ToList());

                        SetSheetColums(ref ws);
                        doc.WorkBook.Worksheets.Add(ws);
                        i++;
                    }

                    if (ds.Tables.Count >= 4)
                    {
                        headingRow = 1;
                        ws = GetNewWorksheet(sheets[i], ref headingRow);
                        BuildHeader(ref ws, ref headingRow, ds.Tables[i].Columns.Cast<DataColumn>().Select(f => f.Caption).ToList(), new List<CrossTabHeading>(), true);
                        BuildDataSets(ref ws, ref headingRow, ds.Tables[i].Rows.Cast<DataRow>().Select(f => f.ItemArray).ToList(), ds.Tables[i].Columns.Cast<DataColumn>().ToList());

                        SetSheetColums(ref ws);
                        doc.WorkBook.Worksheets.Add(ws);
                    }                    
                }
            }

            MemoryStream outStream = new MemoryStream();
            FinishExcelDocument(ref doc).Save(outStream);
            outStream.Position = 0;

            return outStream.GetBuffer();
        }        

        public void BuildDataSets(ref Worksheet ws, ref int row, List<object[]> rows, List<DataColumn> columns, int columnOffset = 0)
        {
            column.CellType colType = column.CellType.Text;
            object value = "";
            bool Flag = false;
            int rountCounter = 4;
            bool isFormular = false;
            string col = string.Empty;
            string col1 = string.Empty;

            foreach (object[] dr in rows)
            {                
                for (int i = 0; i < columns.Count; i++)
                {
                    colType = column.CellType.Text;
                    value = dr[i].ToString().Replace(',', ' ');
                    if (columns[i].DataType == typeof(string) && !string.IsNullOrEmpty(value.ToString()))
                    {
                        if (col.ToString() != value.ToString() && i == 0)
                        {
                            value = dr[i];
                            col = dr[i].ToString();
                        }   
                        if (col1.ToString() != value.ToString() && i == 1)
                        {
                            value = dr[i];
                            col1 = dr[i].ToString();
                        }                      
                    }
                    if (columns[i].DataType == typeof(Int32) && !string.IsNullOrEmpty(value.ToString()))
                    {
                        colType = column.CellType.NumericNoDecimal;
                        value = (int)dr[i];
                        
                    }
                    else if (columns[i].DataType == typeof(Decimal) && !string.IsNullOrEmpty(value.ToString()))
                    {
                        colType = column.CellType.Numeric;
                        value = (decimal)dr[i];                        
                    }

                    NewSetContentsCell(ws.GetCell(row, i + 1 + columnOffset), String.IsNullOrEmpty(dr[i].ToString().Trim()) ? null : value, colType, Flag, isFormular);
                    isFormular = false;
                }
                Flag = true;
                row++;
                rountCounter++;
            }
        }

        public void BuildDataSetsHeaders(ref Worksheet ws, ref int row, List<object[]> rows, List<DataColumn> columns, int columnOffset = 0)
        {
            column.CellType colType = column.CellType.Text;
            object value = "";
            bool Flag = false;            
            bool isFormular = false;
            string col = string.Empty;
            string col1 = string.Empty;

            foreach (object[] dr in rows)
            {
                for (int i = 0; i < columns.Count; i++)
                {
                    colType = column.CellType.Text;
                    value = dr[i].ToString().Replace(',', ' ');
                    if (columns[i].DataType == typeof(string) && !string.IsNullOrEmpty(value.ToString()))
                    {
                        if (col.ToString() != value.ToString() && i == 0)
                        {
                            value = dr[i];
                            col = dr[i].ToString();
                        }
                        else if (i == 0)
                        {
                            value = string.Empty;
                            dr[i] = string.Empty;
                        }

                        if (col1.ToString() != value.ToString() && i == 1)
                        {
                            value = dr[i];
                            col1 = dr[i].ToString();
                        }
                        else if (i == 1)
                        {
                            value = string.Empty;
                            dr[i] = string.Empty;
                        }
                    }

                    if (columns[i].DataType == typeof(Int32) && !string.IsNullOrEmpty(value.ToString()))
                    {
                        colType = column.CellType.NumericNoDecimal;
                        value = (int)dr[i];

                    }
                    else if (columns[i].DataType == typeof(Decimal) && !string.IsNullOrEmpty(value.ToString()))
                    {
                        colType = column.CellType.Numeric;
                        value = (decimal)dr[i];
                    }

                    NewSetContentsCell(ws.GetCell(row - 6, 2), String.IsNullOrEmpty(dr[i].ToString().Trim()) ? null : value, colType, Flag, isFormular);
                    isFormular = false;
                    row++;                    
                }
                Flag = true;
               
            }
        }        

        protected void NewSetContentsCell(Cell c, object value, column.CellType cellType, bool flag, bool isFormula = false)
        {
            c.Border.Right.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
            c.Border.Bottom.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
            c.Border.Top.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
            c.Border.Left.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;

            switch (cellType)
            {                
                default: c.StyleID = ""; break;
            }

            c.Font = new Font("Calibri", 9, FontStyle.Regular);

            if (isFormula)
            {
                c.Formula = string.Format("{0}", value);
            }
            else
            {
                c.Value = value;
            }            
        }

        public void BuildNormalHeader(ref Worksheet ws, ref int row, List<string> columnNames, List<CrossTabHeading> crossTabList, bool useTableStyleHeadings, int columnOffset = 0)
        {
            for (int x = 0; x < columnNames.Count; x++)
            {
                string columnName = columnNames[x];
                crossTabList.ForEach(f => columnName = columnName.Replace(f.String1, ""));
                if (useTableStyleHeadings)
                    SetTableHeadingCell(ws.GetCell(row,1), columnName, BorderType.All);
                else
                    ChangeHeadingCell(ws.GetCell(row, 1), columnName, true);
                row++;
            }
        }

        public void BuildHeader(ref Worksheet ws, ref int row, List<string> columnNames, List<CrossTabHeading> crossTabList, bool useTableStyleHeadings, int columnOffset = 0)
        {
            for (int x = 0; x < columnNames.Count; x++)
            {
                string columnName = columnNames[x];
                crossTabList.ForEach(f => columnName = columnName.Replace(f.String1, ""));
                if (useTableStyleHeadings)
                    SetTableHeadingCell(ws.GetCell(row, x + 1), columnName.Replace("_"," "), BorderType.All);
                else
                    ChangeHeadingCell(ws.GetCell(row, x + 1), columnName.Replace("_", " "), true);                
            }
            row++;
        }        

        protected void ChangeHeadingCell1(Cell c, string heading, bool isUnderlined, BorderType bt)
        {
            c.Border.Right.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
            c.Border.Bottom.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
            c.Border.Top.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
            c.Border.Left.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        
            c.StyleID = isUnderlined ? "UnderlinedHeading" : "Heading";           
            c.Value = heading;
            if (isUnderlined)
               c.MergeAcross = 1;            
            c.Font = new Font("Calibri", 9, FontStyle.Bold);
        }

        protected void ChangeHeadingCell(Cell c, string heading, bool isUnderlined)
        {
            c.StyleID = isUnderlined ? "UnderlinedHeading" : "Heading";
            c.Value = heading;
            c.Font = new Font("Calibri", 9, FontStyle.Bold);
        }

        private void SetHeadingCells(Cell c, string heading, bool isUnderlined)
        {
            c.StyleID = isUnderlined ? "UnderlinedHeading" : "Heading";
            c.Value = heading;
            c.Font = new Font("Calibri", 16, FontStyle.Regular);            
        }

        private void SetTableHeadingCell(Cell c, string heading, BorderType bt)
        {
            c.Border.Right.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
            c.Border.Bottom.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
            c.Border.Top.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
            c.Border.Left.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;

            switch (bt)
            {
                case BorderType.All: c.StyleID = ""; break;
                default: c.StyleID = ""; break;
            }
            c.Value = heading; 
            
            c.Font = new Font("Calibri", 9, FontStyle.Bold);
        }

        private void SetSheetColums(Worksheet ws)
        {
            for (int i = 1; i < 20; i++)
                ws.SetColumnWidth(i, true, 100);
        }

        private Worksheet GetNewWorksheet(SheetNames sheetName, ref int row)
        {
            var ws = new Worksheet(sheetName.ToString());
            switch (sheetName)
            {
                case SheetNames.Material:
                    SetHeadingCells(ws.GetCell(row, 3), "Payment Report", false);
                    row++;row++; break;             
                case SheetNames.PurchaseOrder:
                    SetHeadingCells(ws.GetCell(row, 3), "Client Payment Report", false);
                    row++;;row++;break;
            }
            ws.SetColumnWidth(row, true);
            return ws;
        }

        private enum SheetNames { Material, PurchaseOrder}

    }
}








