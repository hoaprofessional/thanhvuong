﻿using Framework.Models.QoutationManagement;
using Framework.Models.UserManagement;
using Framework.Utils.Anotations.DtoAnotation;
using System;

namespace Framework.DTOs.QoutationManagementDto.BaseOrderDto
{
    public class CreateByFilterDto : IRef<Staff>
    {
        public String Name { get; set; }
    }
}
