using Framework.DTOs.PrintPdfDto;
using Framework.Repositories.Configuration;
using Framework.Repositories.QoutationManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Services.PrintPdfPage
{
    public interface IPrintPdfQoutationPrintService
    {
        QoutationDto GetQoutationPrintTest();
        QoutationDto GetQoutationFromId(int id);
    }
    public class PrintPdfQoutationPrintService : IPrintPdfQoutationPrintService
    {
        IQoutationRepository QoutationRepository;
        IQoutationDetailRepository QoutationDetailRepository;
        ICommonConfigurationRepository commonConfigurationRepository;

        public PrintPdfQoutationPrintService(IQoutationRepository QoutationRepository,
            ICommonConfigurationRepository commonConfigurationRepository,
            IQoutationDetailRepository QoutationDetailRepository)
        {
            this.QoutationRepository = QoutationRepository;
            this.QoutationDetailRepository = QoutationDetailRepository;
            this.commonConfigurationRepository = commonConfigurationRepository;
        }

        public QoutationDto GetQoutationFromId(int id)
        {
            var configuration = commonConfigurationRepository.GetAll().SingleOrDefault();
            var query = QoutationRepository.GetMulti(x => x.Id == id && x.Active == true).
                Include(x => x.Client).
                Include(x => x.ConfirmStaff).
                Include(x => x.Manager).
                Include(x => x.QouteStaff).
                Include(x => x.SalesAdmin);
            var Qoutation = query.SingleOrDefault();
            Qoutation.QoutationDetails = QoutationDetailRepository.GetMulti(x => x.Active == true && x.QoutationId == id).Include(x => x.Product).ToList();

            QoutationDto QoutationDto = new QoutationDto()
            {
                ClientAddress = Qoutation.Client.Address,
                ClientName = Qoutation.Client.Name,
                ClientPhoneNumber = Qoutation.Client.PhoneNumber,
                ClientEmail = Qoutation.Client.Email,
                CompanyName = configuration.CompanyName,
                ConfirmStaff = Qoutation.ConfirmStaff.Name,
                Contact = configuration.Contact,
                Headquarters = configuration.HeadQuarter,
                HotLine = configuration.Hotline,
                LetterOfQuotationNumber = Qoutation.Id,
                ManagerName = Qoutation.Manager.Name,
                QouteStaffName = Qoutation.QouteStaff.Name,
                SalesAdminName = Qoutation.SalesAdmin.Name,
                WebsiteName = configuration.Website,
                CreationTime = Qoutation.CreationTime,
                EstimatedInstallationTime = Qoutation.EstimatedInstallationTime,
                DeliveryPlace = Qoutation.DeliveryPlace,
                PaymentMethod = Qoutation.PaymentMethod,
                QoutationDetails = new List<QoutationDetailDto>()
            };

            foreach(var QoutationDetail in Qoutation.QoutationDetails)
            {
                if(QoutationDetail.Product!=null)
                {
                    QoutationDto.QoutationDetails.Add(new QoutationDetailDto()
                    {
                        Decription = QoutationDetail.Product.Decription,
                        QoutationId = QoutationDetail.QoutationId,
                        ProductImage = QoutationDetail.Product.Images,
                        ProductName = QoutationDetail.Product.Name,
                        ProductQuantity = QoutationDetail.ProductQuantity,
                        Size = QoutationDetail.Product.Size,
                        TotalPrice = (decimal)QoutationDetail.TotalPriceSell,
                        Unit = QoutationDetail.Product.Unit,
                        Discount = QoutationDetail.Discount,
                        ManageId = QoutationDetail.Product.Unit,
                        UnitPrice = (QoutationDetail.UnitPriceSell+ (decimal)((double)QoutationDetail.UnitPriceSell*QoutationDetail.VATBuy))
                    });
                }
                else
                {
                    QoutationDto.QoutationDetails.Add(new QoutationDetailDto()
                    {
                        Decription = "",
                        QoutationId = QoutationDetail.QoutationId,
                        ProductImage = "",
                        ProductName = QoutationDetail.ProductName,
                        ProductQuantity = QoutationDetail.ProductQuantity,
                        Size = "",
                        TotalPrice = (decimal)QoutationDetail.TotalPriceSell,
                        Discount = QoutationDetail.Discount,
                        Unit = "",
                        UnitPrice = (QoutationDetail.UnitPriceSell + (decimal)((double)QoutationDetail.UnitPriceSell * QoutationDetail.VATBuy))
                    });
                }
                
            }

            QoutationDto.TotalPrice = QoutationDto.QoutationDetails.Sum(x => x.TotalPrice);
            return QoutationDto;

        }

        public QoutationDto GetQoutationPrintTest()
        {
            QoutationDto QoutationDto = new QoutationDto()
            {
                ClientAddress = "Mai Xuân Thưởng",
                ClientName = "SỈ QUAN THÔNG TIN",
                ClientPhoneNumber = "",
                ClientEmail = "",
                CompanyName = "TNHH THƯƠNG MẠI DỊCH VỤ THANH VƯƠNG",
                ConfirmStaff = "Võ Thành Tín",
                Contact = "0258.3551322 hoặc 0258.3550768",
                Headquarters = "Số 22 Điện Biên Phủ, P. Vĩnh Hòa, TP. Nha Trang.",
                HotLine = "0905.154.468 - 0905.059.969",
                LetterOfQuotationNumber = 1,
                ManagerName = "Võ Thành Tín",
                QouteStaffName = "Trần Thị Duyên",
                SalesAdminName = "Trần Thị Duyên",
                WebsiteName = "thanhvuongco.com",
                CreationTime = new DateTime(2018, 06, 27),
                EstimatedInstallationTime = 10,
                DeliveryPlace = "Tầng trệt, tại Sĩ Quan Thông Tin, Địa chỉ: Mai Xuân Thưởng",
                PaymentMethod = "<br>- Tiền mặt hoặc chuyển khoản<br>- Thanh toán 100% báo giá trong vòng 7 ngày sau khi bàn giao nghiệm thu",
                QoutationDetails = new List<QoutationDetailDto>()
            };

            var QoutationDetails = QoutationDto.QoutationDetails;
            QoutationDetails.Add(new QoutationDetailDto()
            {
                ProductImage = "/images/products/product1.png",
                ProductName = "Bàn trưởng phòng",
                ProductQuantity = 2,
                UnitPrice = 1000000,
                Decription = @"- Bàn trưởng phòng, mặt bàn chữ nhật, trên mặt có tấm PVC đen.<br>
- Chân bàn ốp PVC đen mặt trước.<br>
- Yếm bàn ốp tấm gỗ nổi hình chữ nhật.<br>
- Phụ kiện bản lề, cam, liên kết của hafele(không gồm bàn phụ)",
                Size = "1800 x 900 x 760",
                Unit = "Cái",
                TotalPrice = 2000000
            });

            QoutationDetails.Add(new QoutationDetailDto()
            {
                ProductImage = "/images/products/product2.png",
                ProductName = "Bàn văn phòng",
                ProductQuantity = 3,
                UnitPrice = 2000000,
                Decription = @"- Bàn trưởng phòng, mặt bàn hình chữ nhật dày 60mm, bên phải có hôc liền 1 ngăn kéo. <br>
- Bên trái là khoang để CPU, ở giữa có bàn phím.",
                Size = "1600 x 800 x 760",
                Unit = "Cái",
                TotalPrice = 6000000
            });

            QoutationDetails.Add(new QoutationDetailDto()
            {
                ProductImage = "/images/products/product3.png",
                ProductName = "Bàn lấy máu",
                ProductQuantity = 1,
                UnitPrice = 2000000,
                Decription = @"- Bàn văn phòng làm bằng gỗ MFC màu vân gỗ sáng H2102 cao cấp.<br>
-  Bàn sử dụng ke  liên kết gữa chân bàn và mặt bàn tạo thành khe hở.<br>
-  Hộc liền bàn 1 ngăn kéo",
                Size = "1600 x 800 x 760",
                Unit = "Cái",
                TotalPrice = 2000000
            });
            QoutationDetails.Add(new QoutationDetailDto()
            {
                ProductImage = "/images/products/product1.png",
                ProductName = "Bàn trưởng phòng",
                ProductQuantity = 2,
                UnitPrice = 1000000,
                Decription = @"- Bàn trưởng phòng, mặt bàn chữ nhật, trên mặt có tấm PVC đen.<br>
- Chân bàn ốp PVC đen mặt trước.<br>
- Yếm bàn ốp tấm gỗ nổi hình chữ nhật.<br>
- Phụ kiện bản lề, cam, liên kết của hafele(không gồm bàn phụ)",
                Size = "1800 x 900 x 760",
                Unit = "Cái",
                TotalPrice = 2000000
            });

            QoutationDetails.Add(new QoutationDetailDto()
            {
                ProductImage = "/images/products/product2.png",
                ProductName = "Bàn văn phòng",
                ProductQuantity = 3,
                UnitPrice = 2000000,
                Decription = @"- Bàn trưởng phòng, mặt bàn hình chữ nhật dày 60mm, bên phải có hôc liền 1 ngăn kéo, 1 cánh mở. <br>
- Bên trái là khoang để CPU, ở giữa có bàn phím.",
                Size = "1600 x 800 x 760",
                Unit = "Cái",
                TotalPrice = 6000000
            });

            QoutationDetails.Add(new QoutationDetailDto()
            {
                ProductImage = "/images/products/product3.png",
                ProductName = "Bàn lấy máu",
                ProductQuantity = 1,
                UnitPrice = 2000000,
                Decription = @"- Bàn văn phòng làm bằng gỗ MFC màu vân gỗ sáng H2102 cao cấp.<br>
-  Bàn sử dụng ke  liên kết gữa chân bàn và mặt bàn tạo thành khe hở.<br>
-  Hộc liền bàn 1 ngăn kéo, 1 cánh mở.",
                Size = "1600 x 800 x 760",
                Unit = "Cái",
                TotalPrice = 2000000
            });

            QoutationDto.TotalPrice = QoutationDto.QoutationDetails.Sum(x => x.TotalPrice);

            return QoutationDto;
        }
    }
}
