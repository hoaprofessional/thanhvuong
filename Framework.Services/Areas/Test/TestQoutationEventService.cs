using Framework.DTOs.Areas.Test;
using Framework.Models.QoutationManagement;
using Framework.Repositories.Configuration;
using Framework.Repositories.QoutationManagement;
using Framework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Services.Areas.Test
{
    public interface ITestQoutationEventService
    {
        List<QoutationEventListDto> GetQoutationEventLists();
        List<QoutationEventDto> GetQoutationEventDetail(int QoutationId);
    }
    public class TestQoutationEventService : ITestQoutationEventService
    {
        IQoutationEventRepository QoutationEventRepository;
        IQoutationRepository QoutationRepository;
        IStaffRepository staffRepository;

        public TestQoutationEventService(IQoutationEventRepository QoutationEventRepository,
            IQoutationRepository QoutationRepository,
            IStaffRepository staffRepository)
        {
            this.QoutationEventRepository = QoutationEventRepository;
            this.QoutationRepository = QoutationRepository;
            this.staffRepository = staffRepository;
        }

        public List<QoutationEventDto> GetQoutationEventDetail(int QoutationId)
        {
            var result = QoutationEventRepository
                .GetMulti(x => x.Active == true 
                            && x.IsTest == true 
                            && x.QoutationId == QoutationId)
                .Join(QoutationRepository
                .GetMulti(x => x.Active == true && x.IsTest == true),
                                                (QoutationEvent => QoutationEvent.QoutationId),
                                                (Qoutation => Qoutation.Id),
                                                ExpressionHelper.JoinSelectResulExpression<QoutationEvent, Qoutation, QoutationEventDto>())
                                .Join(staffRepository.GetMulti(x => x.Active == true && x.IsTest == true),
                                                (QoutationEvent => QoutationEvent.QoutationEventStaffId),
                                                (staff => staff.Id),
                                                ExpressionHelper.JoinSelectResulExpression<QoutationEventDto, Staff>())
                                                .OrderBy(x => x.Time).ToList();

            for (int i = 0; i < result.Count; i++)
            {
                if (i > 0)
                {
                    result[i].ProcessTime = (result[i].Time.Value - result[i - 1].Time.Value).TotalMinutes;
                }
            }

            return result;
        }

        public List<QoutationEventListDto> GetQoutationEventLists()
        {
            var result = QoutationRepository.GetMultiBySelectClassType<QoutationEventListDto>(x => x.Active == true).ToList();

            foreach (var Qoutation in result)
            {
                DateTime? beginTime = QoutationEventRepository.GetMulti(x => x.Active == true && x.IsTest == true && x.QoutationId == Qoutation.QoutationId)
                                                        .OrderBy(x => x.CreationTime)
                                                        .Select(x => x.CreationTime).FirstOrDefault();
                DateTime? endTime = QoutationEventRepository.GetMulti(x => x.Active == true && x.IsTest == true && x.QoutationId == Qoutation.QoutationId)
                                                        .OrderByDescending(x => x.CreationTime)
                                                        .Select(x => x.CreationTime).FirstOrDefault();

                if (beginTime != null)
                {
                    Qoutation.TotalMinutes = (endTime.Value - beginTime.Value).TotalMinutes;
                    Qoutation.TotalDays = (endTime.Value - beginTime.Value).TotalDays;
                }

            }

            return result;
        }
    }
}
