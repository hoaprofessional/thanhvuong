using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Drawing.Imaging;

namespace Framework.Utils.Excel
{
    public class ProductModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Size { get; set; }
    }
    public class ExcelProduct
    {
        public ExcelProduct()
        {
            //string path = Path.Combine(
            //            Directory.GetCurrentDirectory(), "wwwroot/upload/");
            //Workbook workbook = new Workbook();
            //workbook.LoadFromFile(path + "ex.xlsx");
            //Worksheet sheet = workbook.Worksheets[1];
            //var s = sheet.Rows[3].Cells[2];
            //ExcelPicture picture = sheet.Pictures[0];
            //picture.Picture.Save(path + @"\image.png", ImageFormat.Png);
        }
        
    }
}
