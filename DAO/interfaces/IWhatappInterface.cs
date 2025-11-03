using Simpolo_Endpoint.DTO;
using Simpolo_Endpoint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Simpolo_Endpoint.DAO.interfaces
{
    public interface IWhatappInterface
    {
        public Task<string> SendWAMBasedOnActivity(WhatAppNotes App);
    }
}
