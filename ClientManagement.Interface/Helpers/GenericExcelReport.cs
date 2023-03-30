using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Drawing;
using System.Data;
using ExcelCoverter;

namespace ClientManagement.Interface.Helpers
{
    public class GenericExcelReport 
    {
        DataTable dtParam = new DataTable("Parameters");
        ExcelCoverter.GenericExcelReportHelper excel;
        public GenericExcelReport(string reportName, bool isZipBatch, bool makeTotalsBold = false)
        {
            dtParam.Columns.Add("ReportName");

            dtParam.Rows.Add(new object[1]);

            this.Param_ReportName = reportName;
            _isZipBatch = isZipBatch;
            _makeTotalsBold = makeTotalsBold;
            excel = new ExcelCoverter.GenericExcelReportHelper();
        }

        #region custom parameters
            public System.String Param_ReportName
            {
                get { return Convert.ToString(dtParam.Rows[0]["ReportName"]); }
                set { dtParam.Rows[0]["ReportName"] = value; }
            }
        #endregion

        private bool _makeTotalsBold = false;

        private bool _isZipBatch = false;
        public bool IsZipBatch
        {
            get { return _isZipBatch; }
        }

        protected SqlCommand cmrptReportCommand;

        protected DataSet GetDataSet(SqlConnection connection)
        {          
            cmrptReportCommand.Connection = connection;
            cmrptReportCommand.CommandTimeout = Convert.ToInt32(99992000);

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmrptReportCommand);
            da.Fill(ds);
            return ds;
        }

        protected ExcelDocument GetExcelDocument()
        {
            return excel.GetExcelDocument();
        }

        protected XmlDocument FinishExcelDocument(ref ExcelDocument doc)
        {
            return excel.FinishExcelDocument(ref doc);
        }

        public Worksheet BuildGenericSheet(string sheetName, DataTable table, bool includeTotals, List<CrossTabHeading> crossTabList, bool useTableStyleHeadings)
        {
            return excel.BuildGenericSheet(sheetName, table, includeTotals, GetCrossTabList(crossTabList), useTableStyleHeadings);
        }

        private static List<ExcelCoverter.CrossTabHeading> GetCrossTabList(List<CrossTabHeading> crossTabList)
        {
            if (crossTabList == null)
                return null;
            List<ExcelCoverter.CrossTabHeading> ctTabList = new List<CrossTabHeading>();
            crossTabList.ForEach(f => ctTabList.Add(new CrossTabHeading(string1: f.String1, string2: f.String2)));
            return ctTabList;
        }

        protected void BuildCrossTabHeadings(ref Worksheet ws, ref int row, DataTable table, List<CrossTabHeading> crossTabList)
        {
            excel.BuildCrossTabHeadings(ref ws, ref row, table, GetCrossTabList(crossTabList));
        }

        protected void BuildNormalHeadings(ref Worksheet ws, ref int row, List<string> columnNames, List<CrossTabHeading> crossTabList, bool useTableStyleHeadings, int columnOffset = 0)
        {
            excel.BuildNormalHeadings(ref ws, ref row, columnNames, GetCrossTabList(crossTabList), useTableStyleHeadings, columnOffset);
        }

        protected void BuildDataSet(ref Worksheet ws, ref int row, List<object[]> rows, List<DataColumn> columns, int columnOffset = 0)
        {
            excel.BuildDataSet(ref ws, ref row, rows, columns, columnOffset);
        }

        protected void BuildTotals(ref Worksheet ws, ref int row, DataTable table)
        {
            excel.BuildTotals(ref ws, ref row, table);
        }

        #region Methods
        public enum BorderType { Top, Bottom, Left, Right, LeftTop, RightTop, LeftBottom, RightBottom, All };
        protected void SetTableHeadingCells(Cell c, string heading, BorderType bt)
        {           
            switch (bt)
            {
                case BorderType.Top:
                    {
                        c.Border.Top.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        break;
                    }
                case BorderType.Bottom:
                    {
                        c.Border.Bottom.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        break;
                    }
                case BorderType.Left:
                    {
                        c.Border.Left.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        break;
                    }
                case BorderType.Right:
                    {
                        c.Border.Right.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        break;
                    }
                case BorderType.LeftTop:
                    {
                        c.Border.Top.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        c.Border.Left.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        break;
                    }
                case BorderType.LeftBottom:
                    {
                        c.Border.Bottom.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        c.Border.Left.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        break;
                    }
                case BorderType.RightTop:
                    {
                        c.Border.Right.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        c.Border.Top.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        break;
                    }
                case BorderType.RightBottom:
                    {
                        c.Border.Right.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        c.Border.Bottom.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        break;
                    }
                default:
                    {
                        c.Border.Right.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        c.Border.Bottom.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        c.Border.Top.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        c.Border.Left.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        break;
                    }
            }
            c.Value = heading;
            c.BackColor = Color.DarkGray;
            c.Font = new Font("Calibri", 8, FontStyle.Bold);
        }

        protected void SetTableHeadingCell(ref Worksheet ws,int x, int y , string heading, BorderType bt, bool IsSameColor)
        {
            Cell c = ws.GetCell(x, y);
            switch (bt)
            {
                case BorderType.Top:
                    {
                        c.Border.Top.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single; 
                        break;
                    }
                case BorderType.Bottom:
                    {
                        c.Border.Bottom.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        break;
                    }
                case BorderType.Left:
                    {
                        c.Border.Left.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        break;
                    }
                case BorderType.Right:
                    {
                        c.Border.Right.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        break;
                    }
                case BorderType.LeftTop:
                    {
                        c.Border.Top.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        c.Border.Left.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        break;
                    }
                case BorderType.LeftBottom:
                    {
                        c.Border.Bottom.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        c.Border.Left.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        break;
                    }
                case BorderType.RightTop:
                    {
                        c.Border.Right.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        c.Border.Top.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        break;
                    }
                case BorderType.RightBottom:
                    {
                        c.Border.Right.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        c.Border.Bottom.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        break;
                    }
                default:
                    {
                        c.Border.Right.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        c.Border.Bottom.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        c.Border.Top.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        c.Border.Left.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                        break;
                    }
            }
            c.Value = heading;
            c.BackColor = IsSameColor ? Color.DodgerBlue : Color.Lavender;
            c.Font = new Font("Calibri", 8, FontStyle.Bold);
        }

        protected void SetTableHeadingCell(ref Cell c, string heading, BorderType bt, bool IsSameColor)
        {
            switch (bt)
            {
                case BorderType.Top: c.StyleID = "TableHeadingTop"; c.Font = new Font("Calibri", 8, FontStyle.Bold); c.BackColor = IsSameColor ? Color.DodgerBlue : Color.Lavender; c.Border.Left.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single; c.Border.Right.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single; break;
                case BorderType.Bottom: c.StyleID = "TableHeadingBottom"; c.Font = new Font("Calibri", 8, FontStyle.Bold); c.BackColor = IsSameColor ? Color.DodgerBlue : Color.Lavender; c.Border.Left.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single; c.Border.Right.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single; break;
                case BorderType.Left: c.StyleID = "TableHeadingLeft"; c.Font = new Font("Calibri", 8, FontStyle.Bold); c.BackColor = IsSameColor ? Color.DodgerBlue : Color.Lavender; c.Border.Left.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single; c.Border.Right.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single; break;
                case BorderType.Right: c.StyleID = "TableHeadingRight"; c.Font = new Font("Calibri", 8, FontStyle.Bold); c.BackColor = IsSameColor ? Color.DodgerBlue : Color.Lavender; c.Border.Left.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single; c.Border.Right.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single; break;
                case BorderType.LeftTop: c.StyleID = "TableHeadingLeftTop"; c.Font = new Font("Calibri", 8, FontStyle.Bold); c.BackColor = IsSameColor ? Color.DodgerBlue : Color.Lavender; c.Border.Left.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single; c.Border.Right.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single; break;
                case BorderType.LeftBottom: c.StyleID = "TableHeadingLeftBottom"; c.Font = new Font("Calibri", 8, FontStyle.Bold); c.BackColor = IsSameColor ? Color.DodgerBlue : Color.Lavender; c.Border.Left.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single; c.Border.Right.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single; break;
                case BorderType.RightTop: c.StyleID = "TableHeadingRightTop"; c.Font = new Font("Calibri", 8, FontStyle.Bold); c.BackColor = IsSameColor ? Color.DodgerBlue : Color.Lavender; c.Border.Left.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single; c.Border.Right.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single; break;
                case BorderType.RightBottom: c.StyleID = "TableHeadingRightBottom"; c.Font = new Font("Calibri", 8, FontStyle.Bold); c.BackColor = IsSameColor ? Color.DodgerBlue : Color.Lavender; c.Border.Left.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single; c.Border.Right.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single; break;
                default: c.StyleID = "TableHeadingAll"; c.Font = new Font("Calibri", 8, FontStyle.Bold); c.BackColor = IsSameColor ? Color.DodgerBlue : Color.Lavender; c.Border.Left.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single; c.Border.Right.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single; break;
            }
            c.Value = heading;
        }

        protected void SetTableHeadingTopRightRightAligned(Cell c, string heading)
        {
            c.StyleID = "TableHeadingRightTopRightAligned";
            c.Value = heading;
        }

        protected void SetHeadingCell(Cell c, string heading, bool isUnderlined)
        {
            c.StyleID = isUnderlined ? "UnderlinedHeading" : "Heading";
            c.Value = heading;
        }

        protected void SetContentsCellFormat(Cell c, object value, column.CellType cellType)
        {      
            switch (cellType)
            {
                case column.CellType.Numeric: 
                    c.StyleID = "NumberValue";
                    c.Border.Right.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                    c.Border.Bottom.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                    c.Border.Top.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                    c.Border.Left.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                    break;
                case column.CellType.NumericNoDecimal: 
                    c.StyleID = "NumberValueNoDecimal";
                    c.Border.Right.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                    c.Border.Bottom.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                    c.Border.Top.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                    c.Border.Left.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                    break;
                case column.CellType.Percent: 
                    c.StyleID = "NormalPercent"; 
                    c.Border.Right.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                    c.Border.Bottom.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                    c.Border.Top.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                    c.Border.Left.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                    break;
                default: 
                    c.StyleID = "NormalText"; 
                    c.Border.Right.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                    c.Border.Bottom.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                    c.Border.Top.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                    c.Border.Left.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
                    break;                    
            }
            c.Value = value;
        }

        protected void SetContentsCell(Cell c, object value, column.CellType cellType)
        {
            c.Border.Right.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
            c.Border.Bottom.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
            c.Border.Top.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;
            c.Border.Left.Style = ExcelCoverter.Border.BorderStyle.LineStyle.Single;

            switch (cellType)
            {
                case column.CellType.Numeric: c.StyleID = "NumberValue"; break;
                case column.CellType.NumericNoDecimal: c.StyleID = "NumberValueNoDecimal"; break;
                case column.CellType.Percent: c.StyleID = "NormalPercent"; break;
                default: c.StyleID = "NormalText"; break;

            }
            c.Value = value;
        }

        protected void SetHeadingCellRight(Cell c, string heading)
        {
            c.StyleID = "HeadingRight";
            c.Value = heading;
        }

        protected void SetTableCellLeft(Cell c, object value, bool isNumeric)
        {
            if (isNumeric)
                c.StyleID = "TableCellLeftNumber";
            else
                c.StyleID = "TableCellLeft";

            c.Value = value;
        }

        protected void SetTableCellRight(Cell c, object value, bool isNumeric)
        {
            if (isNumeric)
                c.StyleID = "TableCellRightNumber";
            else
                c.StyleID = "TableCellRight";

            c.Value = value;
        }

        protected void SetTotalCell(Cell c, object value, column.CellType cellType, bool isDoubleUnderlined, decimal Balancevalue)
        {
            if (!isDoubleUnderlined)
            {
                c.Border.Top.Style = Border.BorderStyle.LineStyle.Single;
                c.Border.Bottom.Style = Border.BorderStyle.LineStyle.Single;
                c.Border.Left.Style = Border.BorderStyle.LineStyle.Single;
                c.Border.Right.Style = Border.BorderStyle.LineStyle.Single;
                c.Font = new Font("Arial", 9, FontStyle.Regular);
            }
            if (Convert.ToDecimal(Balancevalue) == decimal.Zero)            
                c.BackColor = Color.Red;            

            c.Value = value;
        }

        private void BuildStyles(ref ExcelDocument currentDocument)
        {
            excel.BuildStyles(ref currentDocument);
        }
        #endregion

        protected struct column
        {
            public enum CellType { Text, NumericNoDecimal, Numeric, Percent };
            public column(string columnName, string dataColumnName, CellType cellType)
            {
                _columnName = columnName;
                _dataColumnName = dataColumnName;
                _cellType = cellType;
            }
            public string _columnName;
            public string _dataColumnName;
            public CellType _cellType;
        }

        protected void SetTotalNewCell(Cell c, object value, column.CellType cellType, bool isDoubleUnderlined)
        {
            if (!isDoubleUnderlined)
            {
               
            }
            else
            {
                switch (cellType)
                {
                    case column.CellType.Numeric: c.StyleID = "NumberTotalDU"; break;
                    case column.CellType.NumericNoDecimal: c.StyleID = "NumberTotalNoDecimalDU"; break;
                    default: c.StyleID = "NormalTotalDU"; break;
                }
            }
            c.Value = value;
        }

        protected void SetSheetColums(ref Worksheet ws)
        {
            excel.SetSheetColums(ref ws);
        }       
    }
}

