using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Framework.Context.Migrations
{
    public partial class AddProductData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT [dbo].[Product] ([Id], [Active], [CreationTime], [CreationUserName], [Decription], [Images], [IsTest], [ModifiedTime], [ModifiedUserName], [Name], [Note], [Price], [Protected], [Size], [Unit]) VALUES (N'product1', 1, NULL, NULL, N'- Bàn trưởng phòng, mặt bàn chữ nhật, trên mặt có tấm PVC đen. 
- Chân bàn ốp PVC đen mặt trước.
- Yếm bàn ốp tấm gỗ nổi hình chữ nhật.
- Phụ kiện bản lề, cam, liên kết của hafele (không gồm bàn phụ)', N'/images/products/product1.png', 1, NULL, NULL, N'Bàn trưởng phòng', NULL, CAST(3653000.00 AS Decimal(18, 2)), NULL, N'1800x900x760', N'cái')
INSERT [dbo].[Product] ([Id], [Active], [CreationTime], [CreationUserName], [Decription], [Images], [IsTest], [ModifiedTime], [ModifiedUserName], [Name], [Note], [Price], [Protected], [Size], [Unit]) VALUES (N'product10', 1, NULL, NULL, N'- Tủ làm bằng gỗ MFC màu vân gỗ H2102.
- Phần trên có 2 cánh kính khung gỗ mở, bên trong có 2 đợt cố định;
- Phần dưới 2 cánh gỗ mở', N'/images/products/product10.png', 1, NULL, NULL, N'Tủ gỗ', NULL, CAST(2000000.00 AS Decimal(18, 2)), NULL, N'800x400x1830', N'cái')
INSERT [dbo].[Product] ([Id], [Active], [CreationTime], [CreationUserName], [Decription], [Images], [IsTest], [ModifiedTime], [ModifiedUserName], [Name], [Note], [Price], [Protected], [Size], [Unit]) VALUES (N'product11', 1, NULL, NULL, N'- Tủ 2 cánh mở, 2 khoang, 
vách giữa, 2 khóa, 
- Mỗi khoang có 3 đợt di động và 1 thanh treo quần áo', N'/images/products/product11.png', 1, NULL, NULL, N'Tủ sắt hồ sơ', NULL, CAST(3050000.00 AS Decimal(18, 2)), NULL, N'1000x450x1830', N'cái')
INSERT [dbo].[Product] ([Id], [Active], [CreationTime], [CreationUserName], [Decription], [Images], [IsTest], [ModifiedTime], [ModifiedUserName], [Name], [Note], [Price], [Protected], [Size], [Unit]) VALUES (N'product12', 1, NULL, NULL, N'Tủ hồ sơ 2 khoang:
- Ngăn trên có 2 cánh kính mở, khóa đơn, có 2 đợt di động
- Ngăn dưới có 2 khoang, 2 khóa riêng biệt, cánh thép', N'/images/products/product12.png', 1, NULL, NULL, N'Tủ sắt hồ sơ', NULL, CAST(2800000.00 AS Decimal(18, 2)), NULL, N'1000x450x1830', N'cái')
INSERT [dbo].[Product] ([Id], [Active], [CreationTime], [CreationUserName], [Decription], [Images], [IsTest], [ModifiedTime], [ModifiedUserName], [Name], [Note], [Price], [Protected], [Size], [Unit]) VALUES (N'product13', 1, NULL, NULL, N'Tủ hồ sơ 2 khoang:
- 4 ngăn đều, cánh mở, 4 khóa 
- Mỗi ngăn có 1 đợt di động', N'/images/products/product13.png', 1, NULL, NULL, N'Tủ sắt hồ sơ', NULL, CAST(2850000.00 AS Decimal(18, 2)), NULL, N'1000x450x1830', N'cái')
INSERT [dbo].[Product] ([Id], [Active], [CreationTime], [CreationUserName], [Decription], [Images], [IsTest], [ModifiedTime], [ModifiedUserName], [Name], [Note], [Price], [Protected], [Size], [Unit]) VALUES (N'product14', 1, NULL, NULL, N'- Tủ hồ sơ 3 khoang, 2 cánh kính mở khoang trên, 2 cánh sắt mở khoang dưới.
- Bên phải 01 cánh dài mở có khóa, có 1 xà treo đồ, khóa cam dài + tay nắm', N'/images/products/product14.png', 1, NULL, NULL, N'Tủ sắt hồ sơ', NULL, CAST(3980000.00 AS Decimal(18, 2)), NULL, N'1340x450x1830', N'cái')
INSERT [dbo].[Product] ([Id], [Active], [CreationTime], [CreationUserName], [Decription], [Images], [IsTest], [ModifiedTime], [ModifiedUserName], [Name], [Note], [Price], [Protected], [Size], [Unit]) VALUES (N'product15', 1, NULL, NULL, N'Bàn sinh viên:
- Khung ống thép vuông 25x25 và 25x50 sơn tĩnh điện màu ghi
- Mặt bàn gỗ ghép cao su 18mm sơn PU bóng, bụng bàn gỗ ghép cao su 12mm sơn PU bóng
Ghế sinh viên:
- Khung ống thép vuông 20x20 sơn tĩnh điện màu ghi
- Tựa lưng và mặt ngồi gỗ ghép cao su 18mm sơn PU bóng', N'/images/products/product15.png', 1, NULL, NULL, N'Bàn ghế sinh viên', NULL, CAST(1620000.00 AS Decimal(18, 2)), NULL, N'Bàn:
Kích thước: 1200x400x750
Kích thước: 415x455x900', N'bộ')
INSERT [dbo].[Product] ([Id], [Active], [CreationTime], [CreationUserName], [Decription], [Images], [IsTest], [ModifiedTime], [ModifiedUserName], [Name], [Note], [Price], [Protected], [Size], [Unit]) VALUES (N'product16', 1, NULL, NULL, N'Bàn sinh viên liền ghế, có tựa lưng, gỗ cao su ghép 18mm
Khung ống thép 25x50 sơn tĩnh điện màu ghi', N'/images/products/product16.png', 1, NULL, NULL, N'Bàn ghế sinh viên liền ghế', NULL, CAST(1500000.00 AS Decimal(18, 2)), NULL, N'1200x926x750', N'bộ')
INSERT [dbo].[Product] ([Id], [Active], [CreationTime], [CreationUserName], [Decription], [Images], [IsTest], [ModifiedTime], [ModifiedUserName], [Name], [Note], [Price], [Protected], [Size], [Unit]) VALUES (N'product17', 1, NULL, NULL, N'- Bàn làm việc chân sắt chữ I gấp hộp;
- Mặt bàn gỗ MFC, Có ngăn để tài liệu
- Mật bàn màu vân gỗ', N'/images/products/product17.png', 1, NULL, NULL, N'Bàn hội trường chân sắt', NULL, CAST(1255000.00 AS Decimal(18, 2)), NULL, N'1200x600x750', N'cái')
INSERT [dbo].[Product] ([Id], [Active], [CreationTime], [CreationUserName], [Decription], [Images], [IsTest], [ModifiedTime], [ModifiedUserName], [Name], [Note], [Price], [Protected], [Size], [Unit]) VALUES (N'product18', 1, NULL, NULL, N'- Bàn làm việc chân sắt chữ I gấp hộp;
- Mặt bàn gỗ MFC, Có ngăn để tài liệu
- Mật bàn màu vân gỗ', N'/images/products/product18.png', 1, NULL, NULL, N'Bàn hội trường chân sắt', NULL, CAST(1540000.00 AS Decimal(18, 2)), NULL, N'1600x600x750', N'cái')
INSERT [dbo].[Product] ([Id], [Active], [CreationTime], [CreationUserName], [Decription], [Images], [IsTest], [ModifiedTime], [ModifiedUserName], [Name], [Note], [Price], [Protected], [Size], [Unit]) VALUES (N'product19', 1, NULL, NULL, N'Bàn làm việc chân sắt chữ I gấp hộp; Mặt bàn gỗ MFC, Có ngăn để tài liệu
Mặt bàn màu ghi', N'/images/products/product19.png', 1, NULL, NULL, N'Bàn hội trường chân sắt', NULL, CAST(1175000.00 AS Decimal(18, 2)), NULL, N'1200x600x750', N'cái')
INSERT [dbo].[Product] ([Id], [Active], [CreationTime], [CreationUserName], [Decription], [Images], [IsTest], [ModifiedTime], [ModifiedUserName], [Name], [Note], [Price], [Protected], [Size], [Unit]) VALUES (N'product2', 1, NULL, NULL, N'- Bàn trưởng phòng, mặt bàn hình chữ nhật dày 60mm, bên phải có hôc liền 1 ngăn kéo, 1 cánh mở. 
- Bên trái là khoang để CPU, ở giữa có bàn phím.', N'/images/products/product2.png', 1, NULL, NULL, N'Bàn trưởng phòng', NULL, CAST(3653000.00 AS Decimal(18, 2)), NULL, N'1600x800x760', N'cái')
INSERT [dbo].[Product] ([Id], [Active], [CreationTime], [CreationUserName], [Decription], [Images], [IsTest], [ModifiedTime], [ModifiedUserName], [Name], [Note], [Price], [Protected], [Size], [Unit]) VALUES (N'product20', 1, NULL, NULL, N'Bàn làm việc chân sắt chữ I gấp hộp; Mặt bàn gỗ MFC, Có ngăn để tài liệu
Mặt bàn màu ghi', N'/images/products/product20.png', 1, NULL, NULL, N'Bàn hội trường chân sắt', NULL, CAST(1450000.00 AS Decimal(18, 2)), NULL, N'1600x600x750', N'cái')
INSERT [dbo].[Product] ([Id], [Active], [CreationTime], [CreationUserName], [Decription], [Images], [IsTest], [ModifiedTime], [ModifiedUserName], [Name], [Note], [Price], [Protected], [Size], [Unit]) VALUES (N'product21', 1, NULL, NULL, N'- Loại ghế tĩnh.Ghế đầu bò, có 1 nan giữa bản rộng.
- Chất liệu gỗ tự nhiên', N'/images/products/product21.png', 1, NULL, NULL, N'Ghế đầu bò', NULL, CAST(619000.00 AS Decimal(18, 2)), NULL, N'430x520x1050', N'cái')
INSERT [dbo].[Product] ([Id], [Active], [CreationTime], [CreationUserName], [Decription], [Images], [IsTest], [ModifiedTime], [ModifiedUserName], [Name], [Note], [Price], [Protected], [Size], [Unit]) VALUES (N'product3', 1, NULL, NULL, N'- Bàn văn phòng làm bằng gỗ MFC màu vân gỗ sáng H2102 cao cấp.
-  Bàn sử dụng ke  liên kết gữa chân bàn và mặt bàn tạo thành khe hở.
-  Hộc liền bàn 1 ngăn kéo, 1 cánh mở', N'/images/products/product3.png', 1, NULL, NULL, N'Bàn văn phòng', NULL, CAST(1310000.00 AS Decimal(18, 2)), NULL, N'1200x600x750', N'cái')
INSERT [dbo].[Product] ([Id], [Active], [CreationTime], [CreationUserName], [Decription], [Images], [IsTest], [ModifiedTime], [ModifiedUserName], [Name], [Note], [Price], [Protected], [Size], [Unit]) VALUES (N'product4', 1, NULL, NULL, N'- Bàn văn phòng, khung chân làm bằng thép hộp 40x40 sơn.
-  Mặt bàn làm bằng gỗ MFC màu vân gỗ sáng H2102 cao cấp dày 25mm.', N'/images/products/product4.png', 1, NULL, NULL, N'Bàn văn phòng', NULL, CAST(1440000.00 AS Decimal(18, 2)), NULL, N'1400x700x750', N'cái')
INSERT [dbo].[Product] ([Id], [Active], [CreationTime], [CreationUserName], [Decription], [Images], [IsTest], [ModifiedTime], [ModifiedUserName], [Name], [Note], [Price], [Protected], [Size], [Unit]) VALUES (N'product5', 1, NULL, NULL, N'Ghế xoay nhân viên tựa lưới.  Mặt ngồi đệm mút, bọc nỉ. Chân và tay ghế nhựa. Ghế có trục thủy lựcđiều chỉnh và bánh xe di chuyển. Tựa ghế có thể ngả đàn hồi.', N'/images/products/product5.png', 1, NULL, NULL, N'Ghế nhân viên', NULL, CAST(1160000.00 AS Decimal(18, 2)), NULL, N'590x(445-510)x(875-940)
Rc=310', N'cái')
INSERT [dbo].[Product] ([Id], [Active], [CreationTime], [CreationUserName], [Decription], [Images], [IsTest], [ModifiedTime], [ModifiedUserName], [Name], [Note], [Price], [Protected], [Size], [Unit]) VALUES (N'product6', 1, NULL, NULL, N'- Ghế xoay nhân viên tựa lưới. 
-  Mặt ngồi đệm mút, bọc nỉ.
- Chân và tay ghế nhựa. 
- Ghế có trục thủy lựcđiều chỉnh và bánh xe di chuyển. 
- Tựa ghế có thể ngả đàn hồi.', N'/images/products/product6.png', 1, NULL, NULL, N'Ghế nhân viên', NULL, CAST(1630000.00 AS Decimal(18, 2)), NULL, N'565x(412-495)x(865-945)
Rc=325', N'cái')
INSERT [dbo].[Product] ([Id], [Active], [CreationTime], [CreationUserName], [Decription], [Images], [IsTest], [ModifiedTime], [ModifiedUserName], [Name], [Note], [Price], [Protected], [Size], [Unit]) VALUES (N'product7', 1, NULL, NULL, N'Ghế xoay có tay vịn, không có gật gù, điều chỉnh độ cao bằng cần hơi, bọc mút', N'/images/products/product7.png', 1, NULL, NULL, N'Ghế nhân viên', NULL, CAST(620000.00 AS Decimal(18, 2)), NULL, N'550x560x(900-1020)
Rc=280', N'cái')
INSERT [dbo].[Product] ([Id], [Active], [CreationTime], [CreationUserName], [Decription], [Images], [IsTest], [ModifiedTime], [ModifiedUserName], [Name], [Note], [Price], [Protected], [Size], [Unit]) VALUES (N'product8', 1, NULL, NULL, N'- Tủ 2 cánh kính trượt
- Bên trong có 4 đợt di động', N'/images/products/product8.png', 1, NULL, NULL, N'Tủ sắt hồ sơ', NULL, CAST(4200000.00 AS Decimal(18, 2)), NULL, N'1200x450x1950', N'cái')
INSERT [dbo].[Product] ([Id], [Active], [CreationTime], [CreationUserName], [Decription], [Images], [IsTest], [ModifiedTime], [ModifiedUserName], [Name], [Note], [Price], [Protected], [Size], [Unit]) VALUES (N'product9', 1, NULL, NULL, N'- Tủ 2 khoang 8 ngăn đều, cánh mở, 8 khóa', N'/images/products/product9.png', 1, NULL, NULL, NULL, NULL, CAST(3440000.00 AS Decimal(18, 2)), NULL, N'1000x450x1830', N'cái')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
