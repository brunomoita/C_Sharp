using System;
using System.Collections.Generic;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Linq;


namespace assignment4.Models
{
    class Excel
    {
        private static object workSheet;

        public static void CreateSpreadsheetWorkbook(string filepath)
        {
            // Create a spreadsheet document by supplying the filepath.
            // By default, AutoSave = true, Editable = true, and Type = xlsx.
            SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.
                Create(filepath, SpreadsheetDocumentType.Workbook);

            // Add a WorkbookPart to the document.
            WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            // Add a WorksheetPart to the WorkbookPart.
            WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            // Add Sheets to the Workbook.
            Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.
                AppendChild<Sheets>(new Sheets());

            // Append a new worksheet and associate it with the workbook.
            Sheet sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.
                GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = "mySheet"
            };
            sheets.Append(sheet);
            Cell cell = InsertCellInWorksheet("A", 2, worksheetPart);

            // Set the value of cell A1.
            cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
            cell.CellValue = new CellValue("My Name is Saba Sultana");

            workbookpart.Workbook.Save();

            // Close the document.
            spreadsheetDocument.Close();
        }
        


        // Given a document name and text, 
        // inserts a new work sheet and writes the text to cell "A1" of the new worksheet.

        public static void InsertText(string docName, List<Student> studentList)
        {
            uint index = 1;
            // Open the document for editing.
            using (SpreadsheetDocument spreadSheet = SpreadsheetDocument.Open(docName, true))
            {
                // Get the SharedStringTablePart. If it does not exist, create a new one.
                SharedStringTablePart shareStringPart;
                if (spreadSheet.WorkbookPart.GetPartsOfType<SharedStringTablePart>().Count() > 0)
                {
                    shareStringPart = spreadSheet.WorkbookPart.GetPartsOfType<SharedStringTablePart>().First();
                }
                else
                {
                    shareStringPart = spreadSheet.WorkbookPart.AddNewPart<SharedStringTablePart>();
                }

                // Insert a new worksheet.
                WorksheetPart worksheetPart = InsertWorksheet(spreadSheet.WorkbookPart);

                //Set Header Columns

                Cell cell = InsertCellInWorksheet("A", index, worksheetPart);
                cell.CellValue = new CellValue(nameof(Student.StudentGUID));
                cell.DataType = new EnumValue<CellValues>(CellValues.String);

                cell = InsertCellInWorksheet("B", index, worksheetPart);
                cell.CellValue = new CellValue(nameof(Student.StudentUID)); 
                cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
                
                cell = InsertCellInWorksheet("C", index, worksheetPart);
                cell.CellValue = new CellValue(nameof(Student.StudentId)); 
                cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
                
                cell = InsertCellInWorksheet("D", index, worksheetPart);
                cell.CellValue = new CellValue(nameof(Student.FirstName)); 
                cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
                
                cell = InsertCellInWorksheet("E", index, worksheetPart);
                cell.CellValue = new CellValue(nameof(Student.LastName)); 
                cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
                
                cell = InsertCellInWorksheet("F", index, worksheetPart);
                cell.CellValue = new CellValue(nameof(Student.StudentCode)); 
                cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
                
                cell = InsertCellInWorksheet("G", index, worksheetPart);
                cell.CellValue = new CellValue(nameof(Student.DateOfBirth)); 
                cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);

             
                cell = InsertCellInWorksheet("H", index, worksheetPart);
                cell.CellValue = new CellValue(nameof(Student.CreateDate)); 
                cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
                
                cell = InsertCellInWorksheet("I", index, worksheetPart);
                cell.CellValue = new CellValue(nameof(Student.EditDate)); 
                cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);

                cell = InsertCellInWorksheet("J", index, worksheetPart);
                cell.CellValue = new CellValue(nameof(Student.age));
                cell.DataType = new EnumValue<CellValues>(CellValues.Number);

                cell = InsertCellInWorksheet("k", index, worksheetPart);
                cell.CellValue = new CellValue(nameof(Student.Isme));
                cell.DataType = new EnumValue<CellValues>(CellValues.Number);


                // Set the value of cell A1.
                foreach (Student student in studentList)
                {
                    index++;
                    // Insert cell A1 into the new worksheet.
                    cell = InsertCellInWorksheet("A", index, worksheetPart);
                    cell.CellValue = new CellValue(Guid.NewGuid().ToString());
                    cell.DataType = new EnumValue<CellValues>(CellValues.String);

                    cell = InsertCellInWorksheet("B", index, worksheetPart);
                    cell.CellValue = new CellValue(student.StudentUID);
                    cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);

                    cell = InsertCellInWorksheet("C", index, worksheetPart);
                    cell.CellValue = new CellValue(student.StudentId.ToString());
                    cell.DataType = new EnumValue<CellValues>(CellValues.Number);

                    cell = InsertCellInWorksheet("D", index, worksheetPart);
                    cell.CellValue = new CellValue(student.FirstName);
                    cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);

                    cell = InsertCellInWorksheet("E", index, worksheetPart);
                    cell.CellValue = new CellValue(student.LastName);
                    cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);

                    cell = InsertCellInWorksheet("F", index, worksheetPart);
                    cell.CellValue = new CellValue(student.StudentCode);
                    cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);

                    cell = InsertCellInWorksheet("G", index, worksheetPart);
                    cell.CellValue = new CellValue(Convert.ToDateTime(student.DateOfBirth).ToString());
                    cell.DataType = new EnumValue<CellValues>(CellValues.Date);

                    cell = InsertCellInWorksheet("H", index, worksheetPart);
                    cell.CellValue = new CellValue(Convert.ToDateTime(student.CreateDate).ToString());
                    cell.DataType = new EnumValue<CellValues>(CellValues.Date);

                    cell = InsertCellInWorksheet("I", index, worksheetPart);
                    cell.CellValue = new CellValue(Convert.ToDateTime(student.EditDate).ToString());
                    cell.DataType = new EnumValue<CellValues>(CellValues.Date);

                    cell = InsertCellInWorksheet("J", index, worksheetPart);
                    cell.DataType = new EnumValue<CellValues>(CellValues.Number);
                    cell.CellFormula = new CellFormula($"=INT((TODAY()-G{index})/365)");


                }


                // Save the new worksheet.
                worksheetPart.Worksheet.Save();
            }
        }

        // Given text and a SharedStringTablePart, creates a SharedStringItem with the specified text 
        // and inserts it into the SharedStringTablePart. If the item already exists, returns its index.
        private static int InsertSharedStringItem(string text, SharedStringTablePart shareStringPart)
        {
            // If the part does not contain a SharedStringTable, create one.
            if (shareStringPart.SharedStringTable == null)
            {
                shareStringPart.SharedStringTable = new SharedStringTable();
            }

            int i = 0;

            // Iterate through all the items in the SharedStringTable. If the text already exists, return its index.
            foreach (SharedStringItem item in shareStringPart.SharedStringTable.Elements<SharedStringItem>())
            {
                if (item.InnerText == text)
                {
                    return i;
                }

                i++;
            }

            // The text does not exist in the part. Create the SharedStringItem and return its index.
            shareStringPart.SharedStringTable.AppendChild(new SharedStringItem(new DocumentFormat.OpenXml.Spreadsheet.Text(text)));
            shareStringPart.SharedStringTable.Save();

            return i;
        }

        // Given a WorkbookPart, inserts a new worksheet.
        private static WorksheetPart InsertWorksheet(WorkbookPart workbookPart)
        {
            // Add a new worksheet part to the workbook.
            WorksheetPart newWorksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            newWorksheetPart.Worksheet = new Worksheet(new SheetData());
            newWorksheetPart.Worksheet.Save();

            Sheets sheets = workbookPart.Workbook.GetFirstChild<Sheets>();
            string relationshipId = workbookPart.GetIdOfPart(newWorksheetPart);

            // Get a unique ID for the new sheet.
            uint sheetId = 1;
            if (sheets.Elements<Sheet>().Count() > 0)
            {
                sheetId = sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;
            }

            string sheetName = "Sheet" + sheetId;

            // Append the new worksheet and associate it with the workbook.
            Sheet sheet = new Sheet() { Id = relationshipId, SheetId = sheetId, Name = sheetName };
            sheets.Append(sheet);
            workbookPart.Workbook.Save();

            return newWorksheetPart;
        }

        // Given a column name, a row index, and a WorksheetPart, inserts a cell into the worksheet. 
        // If the cell already exists, returns it. 
        private static Cell InsertCellInWorksheet(string columnName, uint rowIndex, WorksheetPart worksheetPart)
        {
            Worksheet worksheet = worksheetPart.Worksheet;
            SheetData sheetData = worksheet.GetFirstChild<SheetData>();
            string cellReference = columnName + rowIndex;

            // If the worksheet does not contain a row with the specified row index, insert one.
            Row row;
            if (sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).Count() != 0)
            {
                row = sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
            }
            else
            {
                row = new Row() { RowIndex = rowIndex };
                sheetData.Append(row);
            }

            // If there is not a cell with the specified column name, insert one.  
            if (row.Elements<Cell>().Where(c => c.CellReference.Value == columnName + rowIndex).Count() > 0)
            {
                return row.Elements<Cell>().Where(c => c.CellReference.Value == cellReference).First();
            }
            else
            {
                // Cells must be in sequential order according to CellReference. Determine where to insert the new cell.
                Cell refCell = null;
                foreach (Cell cell in row.Elements<Cell>())
                {
                    if (string.Compare(cell.CellReference.Value, cellReference, true) > 0)
                    {
                        refCell = cell;
                        break;
                    }
                }

                Cell newCell = new Cell() { CellReference = cellReference };
                row.InsertBefore(newCell, refCell);

                worksheet.Save();
                return newCell;
            }
        }
    }
}
